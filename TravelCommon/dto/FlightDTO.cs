using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.dto
{
	[Serializable]
	public class FlightDTO
	{

		private int id;
		private String destination;
		private DateTime date;
		private String airport;
		private int available;

		public FlightDTO(int id, String destination, DateTime date, String airport, int available)
		{
			this.id = id;
			this.destination = destination;
			this.date = date;
			this.airport = airport;
			this.available = available;
		}

		public FlightDTO(int id)
		{
			this.id = id;
		}

		public FlightDTO(String destination, DateTime date)
		{
			this.destination = destination;
			this.date = date;
		}

		public virtual int Id
		{
			get { return id; }
			set { id = value; }
		}

		public virtual String Destination
		{
			get { return destination; }
			set { destination = value; }

		}

		public virtual DateTime Date
		{
			get { return date; }
			set { date = value; }

		}

		public virtual String Airport
		{
			get { return airport; }
			set { airport = value; }
		}

		public virtual int Available
		{
			get { return available; }
			set { available = value; }
		}

	}
}
