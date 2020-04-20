using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelClient
{
	public enum TravelClientEvent
	{
		TicketAdded
	};
	public class TravelClientEventArgs : EventArgs
	{
		private readonly TravelClientEvent clientEvent;
		private readonly Object data;

		public TravelClientEventArgs(TravelClientEvent clientEvent, object data)
		{
			this.clientEvent = clientEvent;
			this.data = data;
		}

		public TravelClientEvent ClientEventType
		{
			get { return clientEvent; }
		}

		public object Data
		{
			get { return data; }
		}
	}
}
