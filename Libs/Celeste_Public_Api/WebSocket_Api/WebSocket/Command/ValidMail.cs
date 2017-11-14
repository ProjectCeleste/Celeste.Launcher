#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class ValidMailRequest
    {
        public Version Version { get; set; }

        public string EMail { get; set; }
    }

    public class ValidMailResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }
    }

    public class ValidMail
    {
        public const string CmdName = "VALIDMAIL";

        private DateTime _lastTime = DateTime.UtcNow.AddMinutes(-5);

        public ValidMail(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<ValidMailResponse> DoValidMail(ValidMailRequest request)
        {
            try
            {
                if (!Misc.IsValideEmailAdress(request.EMail))
                    throw new Exception("Invalid eMail!");

                var lastSendTime = (DateTime.UtcNow - _lastTime).TotalSeconds;
                if (lastSendTime <= 90)
                    throw new Exception(
                        $"You need to wait at least 90 seconds before asking to resend an confirmation key! Last request was {lastSendTime} seconds ago.");

                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                ValidMailResponse retVal = result.ToObject<ValidMailResponse>();

                if (retVal.Result)
                    _lastTime = DateTime.UtcNow;

                return retVal;
            }
            catch (Exception e)
            {
                return new ValidMailResponse {Result = false, Message = e.Message};
            }
        }
    }
}