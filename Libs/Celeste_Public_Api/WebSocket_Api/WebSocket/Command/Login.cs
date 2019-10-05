#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class Login
    {
        public const string CmdName = "LOGIN";

        public Login(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<LoginResult> DoLogin(LoginInfo request)
        {
            try
            {
                if (request.Password.Length < 8)
                    throw new Exception("Password minimum length is 8 char!");

                if (request.Password.Length > 32)
                    throw new Exception("Password maximum length is 32 char!");

                if (!Misc.IsValidEmailAdress(request.Mail))
                    throw new Exception("Invalid eMail!");

                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                return result.ToObject<LoginResult>();
            }
            catch (Exception e)
            {
                return new LoginResult(false, e.Message);
            }
        }
    }
}