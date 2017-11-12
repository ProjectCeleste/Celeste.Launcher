using Celeste_Public_Api.Server_Api.Models.User;

namespace Celeste_Public_Api.Server_Api.WebSocket.Packet.Response
{
    public class LoginResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public RemoteUser RemoteUser { get; set; }
    }
}