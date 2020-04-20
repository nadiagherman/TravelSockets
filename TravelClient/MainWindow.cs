using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelClient.ctrl;
using TravelCommon.model;
using TravelCommon.services;

namespace TravelClient
{
	public partial class MainWindow : Form
	{


		internal MainController MainController { get; set; }
		public Form Login { get; internal set; }

		private IList<Flight> flights;
		private IList<Flight> flightsSearched;
		public MainWindow()

		{
			InitializeComponent();

		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			MainController.UpdateEvent += TravelUpdate;
			this.FormClosed += MainForm2_FormClosed;
			flights = new List<Flight>(MainController.GetFlights());
			flightsSearched = new List<Flight>();
			flightsListBox.DataSource = flights;
			flightsSearchedListBox.DataSource = flightsSearched;

		}


		private void TravelUpdate(object sender, TravelClientEventArgs e)
		{
			if (e.ClientEventType == TravelClientEvent.TicketAdded)
			{
				Ticket t = (Ticket)e.Data;
				//LOGGER.InfoFormat("flight update ticket added {0}", b);
				flightsListBox.BeginInvoke(new UpdateFlightsCallback(this.UpdateFlights), new object[] { flightsListBox, flights, t });
				flightsSearchedListBox.BeginInvoke(new UpdateFlightsCallback(this.UpdateFlights), new Object[] { flightsSearchedListBox, flightsSearched, t });
			}
		}

		private void UpdateFlights(ListBox listBox, List<Flight> flights, Ticket ticket)
		{
			int id = ticket.FlightId;
			int i = flights.FindIndex(f => f.Id == id);
			if (i >= 0)
			{
				flights[i].AvailableSeats = flights[i].AvailableSeats - ticket.NrSeats;
				PopulateListBox(listBox, flights);
			}
		}

		public delegate void UpdateFlightsCallback(ListBox listBox, List<Flight> flights, Ticket ticket);

		private void button1_Click(object sender, EventArgs e)
		{
			//LOGGER.Info("handling search trips");
			try
			{
				String dest = textBox1.Text;
				DateTime date = dateTimePicker1.Value;
				if (dest == "")
				{
					MessageBox.Show("All fields must be entered");
					return;
				}

				List<Flight> flights = MainController.SearchFlights(dest, date);
				flightsSearched = flights;
				PopulateListBox(flightsSearchedListBox, flightsSearched);
			}

			catch (Exception ex)
			{
				Console.WriteLine("handling search trips failed " + ex.Message);
				MessageBox.Show(ex.Message);
				return;
			}

		}

		private void updateListBox(ListBox listBox, IList<String> newData)
		{
			listBox.DataSource = null;
			listBox.DataSource = newData;
		}

		private void PopulateListBox(ListBox listBox, IList<Flight> flights)
		{
			listBox.DataSource = null;
			listBox.DataSource = flights;
			//MarkNoPlacesTrips(listBox);


		}




		void MainForm2_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				MainController.Logout();
				MainController.UpdateEvent -= TravelUpdate;
				Login.Show();
			}
			catch (Exception ex)
			{
				//LOGGER.Warn("logging out failed " + ex);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{

			try
			{
				String client = textBox2.Text;
				String tourists = textBox3.Text;
				String address = textBox4.Text;
				String nrSeats = textBox5.Text;


				if (flightsSearchedListBox.SelectedItem is null)
				{
					MessageBox.Show("A flight must be selected");
					return;
				}
				int indexFlight = flightsSearchedListBox.SelectedIndex;
				Flight flight = (Flight)flightsSearched[indexFlight];
				if (client.Equals(""))
				{
					MessageBox.Show("Client name must be entered");
					return;
				}
				if (tourists.Equals(""))
				{
					MessageBox.Show("All tourists name must be entered");
					return;
				}

				if (address.Equals(""))
				{
					MessageBox.Show("Client address must be entered");
					return;
				}

				if (nrSeats.Equals(""))
				{
					MessageBox.Show("Number of tickets must be entered");
				}

				int noTickets = Int32.Parse(nrSeats);
				MainController.AddTicket(flight, client, tourists, address, noTickets);
				clearTextFields();
				MessageBox.Show("Ticket saved");
			}
			catch (FormatException)
			{
				//LOGGER.Info("handle add ticket failed invalid data");
				MessageBox.Show("Invalid data");
			}
			catch (TravelException se)
			{
				MessageBox.Show(se.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
		}

		private void clearTextFields()
		{
			textBox2.Clear();
			textBox3.Clear();
			textBox4.Clear();
			textBox5.Clear();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
