using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelClient.ctrl;
using TravelClient.service;
using TravelCommon.services;

namespace TravelClient
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main()
        {
            try
            {
                TravelServices travelServices = new TravelServicesProxy(ConfigurationManager.AppSettings["server-host"],
                    Int32.Parse(ConfigurationManager.AppSettings["server-port"]));
                LoginController loginController = new LoginController(travelServices);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                LogInWindow logInWindow = new LogInWindow
                {
                    LoginController = loginController
                };
                Application.Run(logInWindow);
            }
            catch (Exception e)
            {
                // LOGGER.Warn(e.Message);
                Console.WriteLine(e.Message);
            }
        }
    }
}
