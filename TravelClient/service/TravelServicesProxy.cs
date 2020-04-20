using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.dto;
using TravelCommon.model;
using TravelCommon.network;
using TravelCommon.services;

namespace TravelClient.service
{
    class TravelServicesProxy : BaseServicesProxy, TravelServices
    {
        protected ITravelObserver travelObserver;
        public TravelServicesProxy(string host, int port) : base(host, port)
        {
        }

        public void AddTicket(Account account, Flight flight, String clientName, String tourists, String clientAddress, int nrSeats)
        {

            EnsureConnected();
            FlightDTO f = new FlightDTO(flight.Id, flight.Destination, flight.Data, flight.AirportName, flight.AvailableSeats);
            AccountDTO a = new AccountDTO(account.Id, account.Username, account.Password);
            Request r = new Request()
            {
                Type = RequestType.ADD_TICKET,
                Data = new TicketDTO(a, f, clientName, tourists, clientAddress, nrSeats)
            };
            SendRequest(r);
            Response response = ReadResponse();
            //LOGGER.InfoFormat("response for add ticket {0}", response);
            if (response.Type == ResponseType.OK)
            {
                Console.WriteLine("ticket added");
                return;
            }
            if (response.Type == ResponseType.ERROR)
            {
                String err = response.Data.ToString();
                Console.WriteLine("received ERROR response " + err);
                throw new TravelException(err);
            }
        }



        public Account Login(string name, string password)
        {
            //LOGGER.InfoFormat("finding account with name {0} password {1}", name, password);
            EnsureConnected();
            Request r = new Request()
            {
                Type = RequestType.LOGIN,
                Data = new AccountDTO(name, password)
            };
            SendRequest(r);
            Response response = ReadResponse();
            //LOGGER.InfoFormat("response for find account received is {0}", response);
            if (response.Type == ResponseType.OK)
            {
                AccountDTO accountDto = (AccountDTO)response.Data;
                if (accountDto == null)
                {
                    Console.WriteLine("No account found");
                    return null;
                }
                Account account = new Account(accountDto.Id, accountDto.Username, accountDto.Password);
                Console.WriteLine("found account {0}", account);
                return account;
            }
            if (response.Type == ResponseType.ERROR)
            {
                String err = response.Data.ToString();
                //LOGGER.Info("finding account failed received ERROR response " + err);
                throw new TravelException(err);
            }
            return null;
        }


        public List<Flight> GetAllFlights()
        {
            //LOGGER.InfoFormat("getting all trips");
            EnsureConnected();
            Request r = new Request()
            {
                Type = RequestType.GET_ALL_FLIGHTS
            };
            SendRequest(r);
            Response response = ReadResponse();
            // LOGGER.InfoFormat("response for get all flights received is {0}", response);
            if (response.Type == ResponseType.OK)
            {
                List<FlightDTO> flightDtos = (List<FlightDTO>)response.Data;
                List<Flight> flights = flightDtos
                    .Select(f => new Flight(f.Id, f.Destination, f.Date, f.Airport, f.Available))
                    .ToList();
                Console.WriteLine("found all flights");
                return flights;
            }
            if (response.Type == ResponseType.ERROR)
            {
                String err = response.Data.ToString();
                // LOGGER.Info("getting all flights failed ERROR response " + err);
                throw new TravelException(err);
            }
            return null;
        }

        public List<Flight> GetFlightsByDestAndDate(String dest, DateTime date)
        {

            EnsureConnected();
            Request r = new Request()
            {
                Type = RequestType.SEARCH_FLIGHTS,
                Data = new FlightDTO(dest, date)
            };
            SendRequest(r);
            Response response = ReadResponse();
            //LOGGER.InfoFormat("response for search flights received is {0}", response);
            if (response.Type == ResponseType.OK)
            {
                List<FlightDTO> flightDtos = (List<FlightDTO>)response.Data;
                List<Flight> flights = flightDtos
                    .Select(f => new Flight(f.Id, f.Destination, f.Date, f.Airport, f.Available))
                    .ToList();
                Console.WriteLine("found all trips");
                return flights;
            }
            if (response.Type == ResponseType.ERROR)
            {
                String err = response.Data.ToString();
                // LOGGER.Info("searching flights failed ERROR response " + err);
                throw new TravelException(err);
            }
            return null;
        }

        public void Logout()
        {
            Console.WriteLine("logging out");
            if (connection == null)
            {
                Console.WriteLine("connection already closed");
                return;
            }
            Request r = new Request()
            {
                Type = RequestType.LOGOUT
            };
            SendRequest(r);
            Response response = ReadResponse();
            CloseConnection();
            if (response.Type == ResponseType.ERROR)
            {
                String err = response.Data.ToString();
                //LOGGER.Warn("logging out failed " + err);
                throw new TravelException(err);
            }
        }
        public void AddTravelObserver(ITravelObserver observer)
        {
            travelObserver = observer;
        }
        public void RemoveTravelObserver(ITravelObserver observer)
        {
            travelObserver = null;
        }

        public override void HandleTicketAdded(Response response)
        {
            Console.WriteLine("handling ticket added response {0}", response);
            TicketDTO ticketDto = (TicketDTO)response.Data;
            Ticket ticket = new Ticket(ticketDto.Id, ticketDto.Account.Id, ticketDto.Flight.Id, ticketDto.ClientName, ticketDto.Tourists, ticketDto.ClientAdress, ticketDto.NrSeats);
            travelObserver.TicketInserted(ticket);
        }
    }
}
