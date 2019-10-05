#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.NotLogged;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class ForgotPwd
    {
        public const string CmdName = "FORGOTPWD";

        private DateTime _lastTime = DateTime.UtcNow.AddMinutes(-5);

        public ForgotPwd(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<ForgotPwdResult> DoForgotPwd(ForgotPwdInfo request)
        {
            try
            {
                if (!Misc.IsValidEmailAdress(request.EMail))
                    throw new Exception("Invalid eMail!");

                var lastSendTime = (DateTime.UtcNow - _lastTime).TotalSeconds;
                if (lastSendTime <= 90)
                    throw new Exception(
                        $"You need to wait at least 90 seconds before asking to resend an confirmation key! Last request was {lastSendTime} seconds ago.");

                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                ForgotPwdResult retVal = result.ToObject<ForgotPwdResult>();

                if (retVal.Result)
                    _lastTime = DateTime.UtcNow;

                return retVal;
            }
            catch (Exception e)
            {
                return new ForgotPwdResult(false, e.Message);
            }
        }
    }
}