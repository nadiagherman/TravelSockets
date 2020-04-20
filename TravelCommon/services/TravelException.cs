using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.services
{
    public class TravelException : Exception
    {

        public TravelException() : base() { }

        public TravelException(String msg) : base(msg) { }

        public TravelException(String msg, Exception ex) : base(msg, ex) { }


    }
}
