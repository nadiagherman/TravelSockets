using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCommon.model;

namespace TravelCommon.services
{
	public interface ITravelObserver
	{
		void TicketInserted(Ticket ticket);
	}
}

