using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.model
{
	public class Flight : Entity<int>
	{
		private int id;
		private String destination;
		private DateTime data;
		private String airportName;
		private int availableSeats;


		public Flight(int id, String destination, DateTime data, String airportName, int availableSeats)
		{
			this.id = id;
			this.destination = destination;
			this.data = data;
			this.airportName = airportName;
			this.availableSeats = availableSeats;
		}
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public String Destination
		{
			get { return destination; }
			set { destination = value; }

		}

		public DateTime Data
		{
			get { return data; }
			set { data = value; }

		}

		public String AirportName
		{
			get { return airportName; }
			set { airportName = value; }
		}

		public int AvailableSeats
		{
			get { return availableSeats; }
			set { availableSeats = value; }
		}


		public override string ToString()
		{
			return string.Format("Destination:{0}   Date:{1}   Airport:{2}   Available:{3}", Destination, Data, AirportName, AvailableSeats);
		}


		public override bool Equals(object obj)
		{
			if (obj is Flight)
			{
				Flight st = obj as Flight;
				return Id == st.Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator ==(Flight t1, Flight t2)
		{
			return t1.Id == t2.Id;
		}

		public static bool operator !=(Flight t1, Flight t2)
		{
			return t1.Id != t2.Id;
		}
	}
}
