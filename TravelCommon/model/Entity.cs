using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.model
{
	public interface Entity<T>
	{
		T Id { get; set; }
	}
}
