using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.services;
using TravelServer.net;
using TravelServer.repository;
using TravelServer.service;

namespace TravelServer
{
	class Program
	{
        static void Main(string[] args)
        {

            try
            {
                AccountDbRepository accountRepository = new AccountDbRepository();
                FlightDbRepository flightRepository = new FlightDbRepository();
                TicketDbRepository ticketRepository = new TicketDbRepository();

                TravelServices travelServices = new TravelServiceImpl(accountRepository, flightRepository, ticketRepository);
                AbstractServer server = new RpcConcurrentServer(ConfigurationManager.AppSettings["host"],
                    Int32.Parse(ConfigurationManager.AppSettings["port"]), travelServices);
                server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
