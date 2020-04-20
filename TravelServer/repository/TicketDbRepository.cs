using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.model;
using TravelServer.db;

namespace TravelServer.repository
{
	public class TicketDbRepository : ICrudRepository<int, Ticket>
	{
		private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public TicketDbRepository()
		{
			log.Info("Ticket Repository created......");
		}

		public void delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Ticket> findAll()
		{
			IDbConnection con = DbUtils.GetConnection();
			IList<Ticket> tickets = new List<Ticket>();
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id,accid,fid,client,tourists,adress,nr from Ticket";

				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int id = dataR.GetInt32(0);
						int idacc = dataR.GetInt32(1);
						int fid = dataR.GetInt32(2);
						String client = dataR.GetString(3);
						String tourists = dataR.GetString(4);
						String addr = dataR.GetString(5);
						int nr = dataR.GetInt32(6);
						Ticket ticket = new Ticket(id, idacc, fid, client, tourists, addr, nr);
						tickets.Add(ticket);
					}
				}
			}

			return tickets;
		}

		public Ticket findOne(int id)
		{
			IDbConnection con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select  id,accid,fid,client,tourists,adress,nr from Ticket where id=@id";
				IDbDataParameter paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = id;
				comm.Parameters.Add(paramId);

				using (var dataR = comm.ExecuteReader())
				{
					if (dataR.Read())
					{
						int idt = dataR.GetInt32(0);
						int idacc = dataR.GetInt32(1);
						int fid = dataR.GetInt32(2);
						String client = dataR.GetString(3);
						String tourists = dataR.GetString(4);
						String addr = dataR.GetString(5);
						int nr = dataR.GetInt32(6);
						Ticket ticket = new Ticket(idt, idacc, fid, client, tourists, addr, nr);
						log.InfoFormat("Exiting findOne with value {0}", ticket);
						return ticket;
					}
				}
			}
			log.InfoFormat("Exiting findOne with value {0}", null);
			return null;
		}

		public int size()
		{
			return findAll().ToList().Count;
		}
		public Ticket save(Ticket entity)
		{

			var con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "insert into Ticket values (@id, @accid,@fid,@client,@tourists,@adress,@nr)";
				entity.Id = size() + 1;
				var paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = entity.Id;
				comm.Parameters.Add(paramId);

				var paramAcc = comm.CreateParameter();
				paramAcc.ParameterName = "@accid";
				paramAcc.Value = entity.AccountId;
				comm.Parameters.Add(paramAcc);


				var paramFlight = comm.CreateParameter();
				paramFlight.ParameterName = "@fid";
				paramFlight.Value = entity.FlightId;
				comm.Parameters.Add(paramFlight);

				var paramClient = comm.CreateParameter();
				paramClient.ParameterName = "@client";
				paramClient.Value = entity.ClientName;
				comm.Parameters.Add(paramClient);

				var paramTourists = comm.CreateParameter();
				paramTourists.ParameterName = "@tourists";
				paramTourists.Value = entity.Tourists;
				comm.Parameters.Add(paramTourists);

				var paramAddr = comm.CreateParameter();
				paramAddr.ParameterName = "@adress";
				paramAddr.Value = entity.ClientAdress;
				comm.Parameters.Add(paramAddr);


				var paramNr = comm.CreateParameter();
				paramNr.ParameterName = "@nr";
				paramNr.Value = entity.NrSeats;
				comm.Parameters.Add(paramNr);


				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No flight added !");
				return entity;
			}
		}
	}
}
