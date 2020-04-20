using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.model;
using TravelCommon.services;
using TravelServer.repository;

namespace TravelServer.service
{
    public class TravelServiceImpl : TravelServices
    {

        private AccountDbRepository accountRepository;
        private FlightDbRepository flightRepository;
        private TicketDbRepository ticketRepository;

        List<ITravelObserver> travelObservers = new List<ITravelObserver>();

        public TravelServiceImpl(AccountDbRepository accountRepository, FlightDbRepository flightRepository, TicketDbRepository ticketRepository)
        {
            this.accountRepository = accountRepository;
            this.flightRepository = flightRepository;
            this.ticketRepository = ticketRepository;
        }

        public void AddTicket(Account acc, Flight fli, String clientName, String tourists, String clientAddress, int nrSeats)
        {

            if (fli.AvailableSeats < nrSeats)
            {
                throw new TravelException("not enough seats for this flight");
            }
            Ticket toSave = new Ticket(-1, acc.Id, fli.Id, clientName, tourists, clientAddress, nrSeats);
            Ticket t = ticketRepository.save(toSave);
            flightRepository.update(fli, nrSeats);
            //LOGGER.Info("Notifying observers");
            Task.Run(() =>
            {
                travelObservers.ForEach(o =>
                {
                    //LOGGER.InfoFormat("notifying {0}", o);
                    o.TicketInserted(t);
                });
            });
        }



        public Account Login(string name, string password)
        {
            // LOGGER.InfoFormat("finding account by name {0} and password {1}", name, password);
            return accountRepository.findAfterUserAndPass(name, password);
        }

        public List<Flight> GetAllFlights()
        {
            return flightRepository.findAll().ToList();
        }

        public List<Flight> GetFlightsByDestAndDate(String dest, DateTime date)
        {
            String dateS = date.ToString();
            String dateStr = dateS.Split(' ')[0];
            return flightRepository.findAfterDestAndDate(dest, dateStr).ToList();
        }

        public void Logout()
        {

        }
        public void AddTravelObserver(ITravelObserver observer)
        {
            travelObservers.Add(observer);
        }
        public void RemoveTravelObserver(ITravelObserver observer)
        {
            travelObservers.Remove(observer);
        }
    }
}
