using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelCommon.services;

namespace TravelServer.net
{
    class RpcConcurrentServer : AbstractConcurrentServer
    {


        private TravelServices travelServices;

        public RpcConcurrentServer(string host, int port, TravelServices travelServices) : base(host, port)
        {
            this.travelServices = travelServices;
        }
        protected override Thread CreateWorker(TcpClient client)
        {

            TravelClientRpcWorker worker = new TravelClientRpcWorker(travelServices, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}
