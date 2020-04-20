using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.dto
{
	[Serializable]
	public class AccountDTO
	{
		private int id;
		private String username;
		private String password;

		public AccountDTO(int id, String user, String pass)
		{
			this.id = id;
			this.username = user;
			this.password = pass;

		}

		public AccountDTO(String user, String pass)
		{
			this.username = user;
			this.password = pass;
		}

		public AccountDTO(int id)
		{
			this.id = id;
		}
		public virtual int Id
		{
			get { return id; }
			set { id = value; }
		}

		public virtual String Username
		{
			get { return username; }
			set { username = value; }
		}

		public virtual String Password
		{
			get { return password; }
			set { password = value; }
		}




	}
}
