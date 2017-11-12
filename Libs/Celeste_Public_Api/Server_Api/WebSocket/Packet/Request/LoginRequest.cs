using System;

namespace Celeste_Public_Api.Server_Api.WebSocket.Packet.Request
{
    public class LoginRequest
    {
        public string Mail { get; set; }

        public string Password { get; set; }

        public Version Version { get; set; }

        public string FingerPrint { get; set; }
    }
}