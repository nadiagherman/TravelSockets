using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelCommon.network;
using TravelCommon.services;

namespace TravelClient.service
{
    abstract class BaseServicesProxy
    {
        //protected static readonly ILog LOGGER = LogManager.GetLogger("BaseServicesProxy");
        private string host;
        private int port;
        private NetworkStream stream;

        private IFormatter formatter;
        protected TcpClient connection = null;

        private Queue<Response> responses = new Queue<Response>();
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public BaseServicesProxy(string host, int port)
        {
            Console.WriteLine("initializing server port and host");
            this.host = host;
            this.port = port;
        }
        protected void EnsureConnected()
        {
            Console.WriteLine("ensuring connection to server");
            if (connection != null)
            {
                return;
            }
            try
            {
                Console.WriteLine("creating connection");
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                //LOGGER.Warn(e.StackTrace);
                throw new TravelException("creating connection failed", e);
            }
        }
        protected void CloseConnection()
        {
            finished = true;
            if (connection == null)
            {
                return;
            }
            TcpClient tempConnection = connection;
            connection = null;
            Console.WriteLine("closing connection");
            try
            {
                stream.Close();
                tempConnection.Close();
                _waitHandle.Close();
                Console.WriteLine("connection closed");
            }
            catch (Exception e)
            {
                Console.WriteLine("closing connection failed");
            }

        }
        protected void SendRequest(Request request)
        {
            Console.WriteLine("sending request to server {0}", request);
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("sending request to server failed");
                throw new TravelException("Error sending object " + e);
            }
        }
        protected Response ReadResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    response = responses.Dequeue();
                    Console.WriteLine("read response from server {0}", response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("reading response from server failed {0}", e);
                //LOGGER.Warn(e.StackTrace);
            }
            return response;
        }

        private void StartReader()
        {
            Console.WriteLine("starting reader thread");
            Thread tw = new Thread(Run);
            tw.Start();
        }

        private void Run()
        {
            Console.WriteLine("running reader thread");
            while (!finished)
            {
                try
                {
                    Response response = (Response)formatter.Deserialize(stream);
                    Console.WriteLine("response received from server {0}", response);
                    if (response.Type == ResponseType.TICKET_ADDED)
                    {
                        HandleTicketAdded(response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue(response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (SerializationException)
                {
                    CloseConnection();
                    break;
                }
                catch (Exception e)
                {
                    //LOGGER.Warn("Reading error " + e);
                    //LOGGER.Warn(e.StackTrace);
                    CloseConnection();
                    break;
                }

            }
        }

        public abstract void HandleTicketAdded(Response response);
    }
}
