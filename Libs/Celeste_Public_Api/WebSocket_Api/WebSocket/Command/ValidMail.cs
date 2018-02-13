#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.NotLogged;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class ValidMail
    {
        public const string CmdName = "VALIDMAIL";

        private DateTime _lastTime = DateTime.UtcNow.AddMinutes(-5);

        public ValidMail(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<ValidMailResult> DoValidMail(ValidMailInfo request)
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

                ValidMailResult retVal = result.ToObject<ValidMailResult>();

                if (retVal.Result)
                    _lastTime = DateTime.UtcNow;

                return retVal;
            }
            catch (Exception e)
            {
                return new ValidMailResult (false, e.Message);
            }
        }
    }
}