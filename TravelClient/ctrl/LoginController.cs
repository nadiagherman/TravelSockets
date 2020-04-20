using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.model;
using TravelCommon.services;

namespace TravelClient.ctrl
{
    class LoginController
    {
        //private static readonly ILog LOGGER = LogManager.GetLogger("LoginController");

        TravelServices travelServices;

        public LoginController(TravelServices travelServices)
        {
            this.travelServices = travelServices;
        }

        public TravelServices TravelServices { get => travelServices; }

        public Account LogIn(string name, string password)
        {
            //LOGGER.InfoFormat("handle login with name {0} and password {1}", name, password);
            return travelServices.Login(name, password);
        }

        internal void Logout()
        {
            //LOGGER.Info("assuring connection to server is closed");
            travelServices.Logout();
        }
    }
}
