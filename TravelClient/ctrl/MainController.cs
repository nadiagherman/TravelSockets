using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.model;
using TravelCommon.services;

namespace TravelClient.ctrl
{
    class MainController : ITravelObserver
    {
        //private static readonly ILog LOGGER = LogManager.GetLogger("MainController");
        public event EventHandler<TravelClientEventArgs> UpdateEvent;

        private TravelServices travelServices;

        public Account Account { get; internal set; }

        public MainController(TravelServices travelService)
        {
            this.travelServices = travelService;
            this.travelServices.AddTravelObserver(this);
        }

        public List<Flight> GetFlights()
        {
            Console.WriteLine("getting all trips");
            return travelServices.GetAllFlights();
        }

        internal List<Flight> SearchFlights(String dest, DateTime date)
        {
            Console.WriteLine("searching trips");

            return travelServices.GetFlightsByDestAndDate(dest, date);
        }

        internal void AddTicket(Flight fli, String clientName, String tourists, String clientAddress, int nrSeats)
        {
            Console.WriteLine("adding booking");
            travelServices.AddTicket(Account, fli, clientName, tourists, clientAddress, nrSeats);
        }

        internal void Logout()
        {
            Console.WriteLine("logging out");
            travelServices.Logout();
            travelServices.RemoveTravelObserver(this);
        }

        public void TicketInserted(Ticket ticket)
        {
            Console.WriteLine("ticket inserted {0}", ticket);
            TravelClientEventArgs travelEventArgs = new TravelClientEventArgs(TravelClientEvent.TicketAdded, ticket);
            OnTripEvent(travelEventArgs);
        }

        private void OnTripEvent(TravelClientEventArgs travelEventArgs)
        {
            if (UpdateEvent == null)
            {
                return;
            }
            UpdateEvent(this, travelEventArgs);
            Console.WriteLine("Update Event called");
        }
    }
}
