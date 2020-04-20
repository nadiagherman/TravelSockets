using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.model
{
	public class Ticket : Entity<int>

	{

		private int id;
		private int accountId;
		private int flightId;
		private String clientName;
		private String tourists;
		private String clientAdress;
		private int nrSeats;

		public Ticket(int id, int accountId, int flightId, String clientName, String tourists, String clientAdress, int nrSeats)
		{
			this.id = id;
			this.flightId = flightId;
			this.accountId = accountId;
			this.clientName = clientName;
			this.tourists = tourists;
			this.clientAdress = clientAdress;
			this.nrSeats = nrSeats;
		}


		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public int AccountId
		{
			get { return accountId; }
			set { accountId = value; }
		}

		public int FlightId
		{
			get { return flightId; }
			set { flightId = value; }
		}

		public String ClientName
		{
			get { return clientName; }
			set { clientName = value; }
		}

		public String Tourists
		{
			get { return tourists; }
			set { tourists = value; }
		}

		public String ClientAdress
		{
			get { return clientAdress; }
			set { clientAdress = value; }
		}

		public int NrSeats
		{
			get { return nrSeats; }
			set { nrSeats = value; }
		}


		public override string ToString()
		{
			return string.Format("[Ticket: Id={0}, Account Id={1} , Flight id={2}, client ={3}, Tourists={4}, Client Adress={5}, Nr of seats={6}]", Id, AccountId, FlightId, ClientName, Tourists, ClientAdress, NrSeats);
		}


		public override bool Equals(object obj)
		{
			if (obj is Ticket)
			{
				Ticket st = obj as Ticket;
				return Id == st.Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator ==(Ticket t1, Ticket t2)
		{
			return t1.Id == t2.Id;
		}

		public static bool operator !=(Ticket t1, Ticket t2)
		{
			return t1.Id != t2.Id;
		}
	}
}
