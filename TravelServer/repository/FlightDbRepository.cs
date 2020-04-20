using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.model;
using TravelServer.db;

namespace TravelServer.repository
{
	public class FlightDbRepository : ICrudRepository<int, Flight>
	{
		private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public FlightDbRepository()
		{
			log.Info("Flight Repository created......");
		}

		public void delete(int id)
		{
			throw new NotImplementedException();
		}


		public IEnumerable<Flight> findAfterDestAndDate(String dest, String date)
		{
			IList<Flight> flights = new List<Flight>();
			IDbConnection con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id,destination,date,time,airport,available from Flight where destination=@destination and date=@date";
				IDbDataParameter paramDest = comm.CreateParameter();
				paramDest.ParameterName = "@destination";
				paramDest.Value = dest;
				comm.Parameters.Add(paramDest);


				IDbDataParameter paramDate = comm.CreateParameter();
				paramDate.ParameterName = "@date";
				paramDate.Value = date;
				comm.Parameters.Add(paramDate);


				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int idf = dataR.GetInt32(0);
						String destf = dataR.GetString(1);
						String datef = dataR.GetString(2);
						String time = dataR.GetString(3);
						String datetimes = datef + " " + time;
						CultureInfo culture = new CultureInfo("en-GB");
						DateTime datentime = DateTime.Parse(datetimes, culture);
						String airport = dataR.GetString(4);
						int available = dataR.GetInt32(5);

						Flight flight = new Flight(idf, destf, datentime, airport, available);
						flights.Add(flight);

					}
				}
			}
			return flights;

		}
		public IEnumerable<Flight> findAll()
		{
			/*String datamea = "11-03-2020 18:30:30";
			CultureInfo culture = new CultureInfo("en-GB");
			DateTime nouadata = DateTime.Parse(datamea, culture);
			Console.WriteLine(nouadata);
			Console.ReadLine();
			*/
			IDbConnection con = DbUtils.GetConnection();
			IList<Flight> flights = new List<Flight>();
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select * from Flight";

				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int id = dataR.GetInt32(0);
						String destination = dataR.GetString(1);
						String date = dataR.GetString(2);
						String time = dataR.GetString(3);
						String datetimes = date + " " + time;
						CultureInfo culture = new CultureInfo("en-GB");
						DateTime datetime = DateTime.Parse(datetimes, culture);
						String airport = dataR.GetString(4);
						int available = dataR.GetInt32(5);

						Flight flight = new Flight(id, destination, datetime, airport, available);
						flights.Add(flight);
					}
				}
			}

			return flights;

		}

		public void update(Flight flight, int nrSeats)
		{

			var con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "UPDATE Flight SET available=@a WHERE id=@id";
				var paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = flight.Id;
				comm.Parameters.Add(paramId);

				var paramAv = comm.CreateParameter();
				paramAv.ParameterName = "@a";
				paramAv.Value = flight.AvailableSeats - nrSeats;
				comm.Parameters.Add(paramAv);


				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No flight updated !");
			}
		}

		public Flight findOne(int id)
		{
			log.InfoFormat("Entering findOne with value {0}", id);
			IDbConnection con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id,destination,date,time,airport,available from Flight where id=@id";
				IDbDataParameter paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = id;
				comm.Parameters.Add(paramId);

				using (var dataR = comm.ExecuteReader())
				{
					if (dataR.Read())
					{
						int idf = dataR.GetInt32(0);
						String destf = dataR.GetString(1);
						String date = dataR.GetString(2);
						String time = dataR.GetString(3);
						String datetimes = date + " " + time;
						CultureInfo culture = new CultureInfo("en-GB");
						DateTime datetime = DateTime.Parse(datetimes, culture);
						String airport = dataR.GetString(4);
						int available = dataR.GetInt32(5);

						Flight flight = new Flight(idf, destf, datetime, airport, available);


						log.InfoFormat("Exiting findOne with value {0}", flight);
						return flight;
					}
				}
			}
			log.InfoFormat("Exiting findOne with value {0}", null);
			return null;
		}

		public Flight save(Flight entity)
		{
			var con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "insert into Flight values (@id, @destination, @date, @time,@airport, @available)";
				var paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = entity.Id;
				comm.Parameters.Add(paramId);

				var paramDest = comm.CreateParameter();
				paramDest.ParameterName = "@destination";
				paramDest.Value = entity.Destination;
				comm.Parameters.Add(paramDest);

				String[] dateandtime = entity.Data.ToString().Split(' ');
				String dates = dateandtime[0];
				String times = dateandtime[1];

				var paramDate = comm.CreateParameter();
				paramDate.ParameterName = "@date";
				paramDate.Value = dates;
				comm.Parameters.Add(paramDate);

				var paramTime = comm.CreateParameter();
				paramTime.ParameterName = "@time";
				paramTime.Value = times;
				comm.Parameters.Add(paramTime);

				var paramAirport = comm.CreateParameter();
				paramAirport.ParameterName = "@airport";
				paramAirport.Value = entity.AirportName;
				comm.Parameters.Add(paramAirport);

				var paramAvailable = comm.CreateParameter();
				paramAvailable.ParameterName = "@available";
				paramAvailable.Value = entity.AvailableSeats;
				comm.Parameters.Add(paramAvailable);


				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No flight added !");

				return entity;
			}
		}

	}
}
