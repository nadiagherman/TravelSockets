using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.model
{
	public class Account : Entity<int>
	{

		private int id;
		private String username;
		private String password;

		public Account(String user, String pass)
		{
			this.username = user;
			this.password = pass;

		}
		public Account(int id, String user, String pass)
		{
			this.id = id;
			this.username = user;
			this.password = pass;
		}
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public String Username
		{
			get { return username; }
			set { username = value; }
		}

		public String Password
		{
			get { return password; }
			set { password = value; }

		}

		public override String ToString()
		{
			return String.Format("[Account: Id={0}, Username={1} , Password={2}]", Id, Username, Password);
		}


		public override bool Equals(object obj)
		{
			if (obj is Account)
			{
				Account st = obj as Account;
				return Id == st.Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		
	}
}
