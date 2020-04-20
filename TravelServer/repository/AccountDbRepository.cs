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
	public class AccountDbRepository : ICrudRepository<int, Account>

	{
		private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public AccountDbRepository()
		{
			log.Info("Account Repository created......");
		}
		public void delete(int id)
		{
			throw new NotImplementedException();
		}


		public IEnumerable<Account> findAll()
		{
			IDbConnection con = DbUtils.GetConnection();
			IList<Account> accounts = new List<Account>();
			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id,username,password from Account";

				using (var dataR = comm.ExecuteReader())
				{
					while (dataR.Read())
					{
						int id = dataR.GetInt32(0);
						String user = dataR.GetString(1);
						String pass = dataR.GetString(2);
						Account account = new Account(id, user, pass);
						accounts.Add(account);
					}
				}
			}

			return accounts;


		}

		public Account findOne(int id)
		{
			log.InfoFormat("Entering findOne with value {0}", id);
			IDbConnection con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id,username,password from Account where id=@id";
				IDbDataParameter paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = id;
				comm.Parameters.Add(paramId);

				using (var dataR = comm.ExecuteReader())
				{
					if (dataR.Read())
					{
						int idacc = dataR.GetInt32(0);
						String user = dataR.GetString(1);
						String pass = dataR.GetString(2);
						Account acc = new Account(id, user, pass);
						log.InfoFormat("Exiting findOne with value {0}", acc);
						return acc;
					}
				}
			}
			log.InfoFormat("Exiting findOne with value {0}", null);
			return null;
		}

		public Account findAfterUserAndPass(String user, String pass)
		{
			log.InfoFormat("Entering findAfterUserAndPass with values {0} {1}", user, pass);
			IDbConnection con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "select id,username,password from Account where username=@user and password=@pass";
				IDbDataParameter paramUser = comm.CreateParameter();
				paramUser.ParameterName = "@user";
				paramUser.Value = user;
				comm.Parameters.Add(paramUser);

				IDbDataParameter paramPass = comm.CreateParameter();
				paramPass.ParameterName = "@pass";
				paramPass.Value = pass;
				comm.Parameters.Add(paramPass);

				using (var dataR = comm.ExecuteReader())
				{
					if (dataR.Read())
					{
						int idacc = dataR.GetInt32(0);
						String username = dataR.GetString(1);
						String password = dataR.GetString(2);
						Account acc = new Account(idacc, username, password);
						log.InfoFormat("Exiting findAfterUserAndPass with value {0}", acc);
						return acc;
					}
				}
			}
			log.InfoFormat("Exiting findOne with value {0}", null);
			return null;
		}


		public Account save(Account entity)
		{
			var con = DbUtils.GetConnection();

			using (var comm = con.CreateCommand())
			{
				comm.CommandText = "insert into Account values (@id, @username, @password)";
				var paramId = comm.CreateParameter();
				paramId.ParameterName = "@id";
				paramId.Value = entity.Id;
				comm.Parameters.Add(paramId);

				var paramUser = comm.CreateParameter();
				paramUser.ParameterName = "@username";
				paramUser.Value = entity.Username;
				comm.Parameters.Add(paramUser);

				var paramPass = comm.CreateParameter();
				paramPass.ParameterName = "@password";
				paramPass.Value = entity.Password;
				comm.Parameters.Add(paramPass);


				var result = comm.ExecuteNonQuery();
				if (result == 0)
					throw new RepositoryException("No account added !");

				return entity;
			}
		}
	}
}
