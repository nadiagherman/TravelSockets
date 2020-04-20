using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.services;

namespace TravelServer.net
{
    public abstract class AbstractServer
    {

        private TcpListener server;
        private String host;
        private int port;
        public AbstractServer(String host, int port)
        {
            this.host = host;
            this.port = port;
        }
        public void Start()
        {
            try
            {
                IPAddress adr = IPAddress.Parse(host);
                IPEndPoint ep = new IPEndPoint(adr, port);
                server = new TcpListener(ep);
                server.Start();
                while (true)
                {
                    Console.WriteLine("Waiting for clients ...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected ...");
                    ProcessRequest(client);
                }
            }
            catch (Exception e)
            {
                throw new TravelException(e.Message);
            }

            finally
            {
                Stop();
            }
        }

        public void Stop()
        {
            try
            {

                server.Stop();
            }
            catch (Exception e)
            {

                throw new TravelException("Closing server error " + e.Message);
            }
        }

        public abstract void ProcessRequest(TcpClient client);

    }


}
