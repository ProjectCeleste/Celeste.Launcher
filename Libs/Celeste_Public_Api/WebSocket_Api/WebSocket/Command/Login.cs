#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.WebSocket_Api.Models;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class LoginRequest
    {
        public string Mail { get; set; }

        public string Password { get; set; }

        public Version Version { get; set; }

        public string FingerPrint { get; set; }
    }

    public class LoginResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public RemoteUser RemoteUser { get; set; }
    }

    public class Login
    {
        public const string CmdName = "LOGIN";

        public Login(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<LoginResponse> DoLogin(LoginRequest request)
        {
            try
            {
                if (request.Password.Length < 8)
                    throw new Exception("Password minimum length is 8 char!");

                if (request.Password.Length > 32)
                    throw new Exception("Password maximum length is 32 char!");

                if (!Misc.IsValideEmailAdress(request.Mail))
                    throw new Exception("Invalid eMail!");

                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                return result.ToObject<LoginResponse>();
            }
            catch (Exception e)
            {
                return new LoginResponse {Result = false, Message = e.Message, RemoteUser = null};
            }
        }
    }
}