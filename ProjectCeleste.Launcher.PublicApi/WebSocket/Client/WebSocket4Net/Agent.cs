#region Using directives

using System;
using Newtonsoft.Json;
using WebSocket4Net;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.Client.WebSocket4Net
{
    internal class Agent : JsonWebSocket
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