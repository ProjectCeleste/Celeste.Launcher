#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class RegisterRequest
    {
        public Version Version { get; set; }

        public string Mail { get; set; }

        public string VerifyKey { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }
    }

    public class Register
    {
        public const string CmdName = "REGISTER";

        private DateTime _lastTime = DateTime.UtcNow.AddMinutes(-5);

        public Register(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<RegisterResponse> DoRegister(RegisterRequest request)
        {
            try
            {
                if (!Misc.IsValideEmailAdress(request.Mail))
                    throw new Exception("Invalid eMail!");

                if (!Misc.IsValideUserName(request.UserName))
                    throw new Exception(
                        "Invalid User Name, only letters and digits allowed, minimum length is 3 char and maximum length is 15 char!");

                if (request.Password.Length < 8 || request.Password.Length > 32)
                    throw new Exception("Password minimum length is 8 char,  maximum length is 32 char!");

                if (!Misc.IsValidePassword(request.Password))
                    throw new Exception("Invalid password, character ' and \" are not allowed!");

                if (request.VerifyKey.Length != 32)
                    throw new Exception("Invalid Verify Key!");

                var lastSendTime = (DateTime.UtcNow - _lastTime).TotalSeconds;
                if (lastSendTime <= 90)
                    throw new Exception(
                        $"You need to wait at least 90 seconds before to try again! Last try was {lastSendTime} seconds ago.");

                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                RegisterResponse retVal = result.ToObject<RegisterResponse>();

                if (retVal.Result)
                    _lastTime = DateTime.UtcNow;

                return retVal;
            }
            catch (Exception e)
            {
                return new RegisterResponse {Result = false, Message = e.Message};
            }
        }
    }
}