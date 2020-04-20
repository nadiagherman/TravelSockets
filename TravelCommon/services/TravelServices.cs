using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.model;

namespace TravelCommon.services
{
    public interface TravelServices
    {


        Account Login(String user, String pass);

        void AddTicket(Account account, Flight flight, String clientName, String tourists, String clientAdress, int nrSeats);

        List<Flight> GetAllFlights();

        List<Flight> GetFlightsByDestAndDate(String dest, DateTime date);

        void Logout();

        void AddTravelObserver(ITravelObserver travelObserver);

        void RemoveTravelObserver(ITravelObserver travelObserver);





    }
}
