using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelServer.db
{
	class SqliteConnectionFactory : ConnectionFactory
	{
		public override IDbConnection CreateConnection()
		{

			//Windows Sqlite Connection, fisierul .db ar trebuie sa fie in directorul debug/bin
			Console.WriteLine("creating connection to database");
			String connectionString = ConfigurationManager.AppSettings["connectionString"];
			Console.WriteLine("created connection to database)");
			return new SQLiteConnection(connectionString, true); 
			
		}
	}
}
