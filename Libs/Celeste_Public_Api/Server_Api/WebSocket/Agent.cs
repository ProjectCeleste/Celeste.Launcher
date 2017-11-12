using System;
using Newtonsoft.Json;
using WebSocket4Net;

namespace Celeste_Public_Api.Server_Api.WebSocket
{
    public class Agent : JsonWebSocket
    {
        public Agent(string uri)
            : base(uri)
        {
        }

        protected override string SerializeObject(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        protected override object DeserializeObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}