using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.network
{
    public enum ResponseType
    {
        OK, ERROR,
        TICKET_ADDED
    }
    [Serializable]
    public class Response
    {
        private ResponseType type;
        private object data;


        public Response()
        {

        }

        public ResponseType Type { get => type; set => type = value; }
        public object Data { get => data; set => data = value; }

        public override string ToString()
        {
            return "Response{ type= " + type + ", data=" + data + "}";
        }

    }
}
