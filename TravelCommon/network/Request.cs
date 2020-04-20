using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCommon.network
{
    public enum RequestType
    {
        LOGIN, GET_ALL_FLIGHTS,
        LOGOUT,
        SEARCH_FLIGHTS,
        ADD_TICKET
    }
    [Serializable]
    public class Request
    {
        private RequestType type;
        private object data;


        public Request()
        {

        }

        public RequestType Type { get => type; set => type = value; }
        public object Data { get => data; set => data = value; }

        public override string ToString()
        {
            return "Request{ type= " + type + ", data=" + data + "}";
        }

    }
}
