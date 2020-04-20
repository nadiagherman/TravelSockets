using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.dto;
using TravelCommon.model;
using TravelCommon.network;
using TravelCommon.services;

namespace TravelServer.net
{
    public class TravelClientRpcWorker : ITravelObserver
    {
        private TravelServices travelServices;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public TravelClientRpcWorker(TravelServices travelServices, TcpClient connection)
        {

            this.travelServices = travelServices;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);
            }
        }

        public void TicketInserted(Ticket ticket)
        {

            TicketDTO ticketDto = new TicketDTO(new AccountDTO(ticket.AccountId), new FlightDTO(ticket.FlightId), ticket.ClientName, ticket.Tourists, ticket.ClientAdress, ticket.NrSeats);
            Response response = new Response()
            {
                Type = ResponseType.TICKET_ADDED,
                Data = ticketDto
            };
            try
            {
                SendResponse(response);
            }
            catch (Exception ex)
            {
                throw new TravelException(ex.Message);
            }

        }

        public void Run()
        {

            while (connected)
            {
                try
                {
                    Request request = (Request)formatter.Deserialize(stream);

                    Response response = HandleRequest(request);

                    if (response != null)
                    {

                        SendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    if (connected)
                    {
                        throw new TravelException("running worker stopped with exception {0}", e);
                    }
                    break;
                }
            }

            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new TravelException(e.Message);
            }
        }
        private Response HandleRequest(Request request)
        {

            if (request.Type == RequestType.LOGIN)
            {

                AccountDTO accountDto = (AccountDTO)request.Data;
                Account account = null;
                try
                {
                    lock (travelServices)
                    {
                        account = travelServices.Login(accountDto.Username, accountDto.Password);
                    }
                    if (account == null)
                    {
                        return new Response()
                        {
                            Type = ResponseType.OK
                        };
                    }
                    travelServices.AddTravelObserver(this);
                    return new Response()
                    {
                        Type = ResponseType.OK,
                        Data = new AccountDTO(account.Id, account.Username, account.Password)
                    };
                }
                catch (Exception e)
                {

                    return new Response()
                    {
                        Type = ResponseType.ERROR,
                        Data = e.Message
                    };
                }
            }
            if (request.Type == RequestType.LOGOUT)
            {
                //LOGGER.Info("Logout request");
                lock (travelServices)
                {
                    travelServices.Logout();
                }
                travelServices.RemoveTravelObserver(this);
                connected = false;
                return new Response()
                {
                    Type = ResponseType.OK
                };
            }
            if (request.Type == RequestType.GET_ALL_FLIGHTS)
            {
                // LOGGER.Info("get all trips request");
                List<Flight> flights = null;
                try
                {
                    lock (travelServices)
                    {
                        flights = travelServices.GetAllFlights();
                    }
                    return new Response()
                    {
                        Type = ResponseType.OK,
                        Data = flights
                                    .Select(f => new FlightDTO(f.Id, f.Destination, f.Data, f.AirportName, f.AvailableSeats))
                                    .ToList()
                    };
                }
                catch (Exception e)
                {
                    //LOGGER.Warn("getting all flights failed ");
                    return new Response()
                    {
                        Type = ResponseType.ERROR,
                        Data = e.Message
                    };
                }
            }
            if (request.Type == RequestType.SEARCH_FLIGHTS)
            {
                //LOGGER.Info("search flights request");
                List<Flight> flights = null;
                try
                {
                    FlightDTO flightDto = (FlightDTO)request.Data;
                    lock (travelServices)
                    {
                        flights = travelServices.GetFlightsByDestAndDate(flightDto.Destination, flightDto.Date);
                    }
                    //LOGGER.Info("returning response with searched flights");
                    return new Response()
                    {
                        Type = ResponseType.OK,
                        Data = flights
                                    .Select(f => new FlightDTO(f.Id, f.Destination, f.Data, f.AirportName, f.AvailableSeats))
                                    .ToList()
                    };
                }
                catch (Exception e)
                {
                    //LOGGER.Warn("searching flights failed ");
                    return new Response()
                    {
                        Type = ResponseType.ERROR,
                        Data = e.Message
                    };
                }
            }
            if (request.Type == RequestType.ADD_TICKET)
            {
                //LOGGER.Info("add ticket request");
                TicketDTO ticketDto = (TicketDTO)request.Data;
                FlightDTO flightDto = ticketDto.Flight;
                AccountDTO accountDto = ticketDto.Account;
                Flight t = new Flight(flightDto.Id, flightDto.Destination, flightDto.Date, flightDto.Airport, flightDto.Available);
                Account a = new Account(accountDto.Id, accountDto.Username, accountDto.Password);
                try
                {
                    lock (travelServices)
                    {
                        travelServices.AddTicket(a, t, ticketDto.ClientName, ticketDto.Tourists, ticketDto.ClientAdress, ticketDto.NrSeats);
                    }
                    //LOGGER.Info("saved ticket returning ok response");
                    return new Response()
                    {
                        Type = ResponseType.OK
                    };
                }
                catch (Exception e)
                {
                    //LOGGER.Warn("add ticket failed ");
                    return new Response()
                    {
                        Type = ResponseType.ERROR,
                        Data = e.Message
                    };
                }
            }
            return null;
        }
        private void SendResponse(Response response)
        {
            //LOGGER.InfoFormat("sending response {0}", response);
            formatter.Serialize(stream, response);
            stream.Flush();
        }
    }
}
