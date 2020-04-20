using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.dto
{   [Serializable]
	public class TicketDTO
	{
		private int id;
		private AccountDTO account;
		private FlightDTO flight;
		private String clientName;
		private String tourists;
		private string clientAddress;
		private int nrSeats;

		public TicketDTO(int id, AccountDTO acc, FlightDTO fli, String clientName, String tourists, String clientAddr, int nr)
		{
			this.id = id;
			this.account = acc;
			this.flight = fli;
			this.clientName = clientName;
			this.tourists = tourists;
			this.clientAddress = clientAddr;
			this.nrSeats = nr;
		}

		public TicketDTO(AccountDTO acc, FlightDTO fli, String clientName, String tourists, String clientAddr, int nr)
		{

			this.account = acc;
			this.flight = fli;
			this.clientName = clientName;
			this.tourists = tourists;
			this.clientAddress = clientAddr;
			this.nrSeats = nr;
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public AccountDTO Account
		{
			get { return account; }
			set { account = value; }
		}

		public FlightDTO Flight
		{
			get { return flight; }
			set { flight = value; }
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
			get { return clientAddress; }
			set { clientAddress = value; }
		}

		public int NrSeats
		{
			get { return nrSeats; }
			set { nrSeats = value; }
		}


	}
}
