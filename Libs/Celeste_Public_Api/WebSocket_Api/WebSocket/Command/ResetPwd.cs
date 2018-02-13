#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.NotLogged;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class ResetPwd
    {
        public const string CmdName = "RESETPWD";

        private DateTime _lastTime = DateTime.UtcNow.AddMinutes(-5);

        public ResetPwd(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<ResetPwdResult> DoResetPwd(ResetPwdInfo request)
        {
            try
            {
                if (!Misc.IsValideEmailAdress(request.EMail))
                    throw new Exception("Invalid eMail!");

                if (request.VerifyKey.Length != 32)
                    throw new Exception("Invalid Verify Key!");

                var lastSendTime = (DateTime.UtcNow - _lastTime).TotalSeconds;
                if (lastSendTime <= 90)
                    throw new Exception(
                        $"You need to wait at least 90 seconds before asking to resend an new password! Last request was {lastSendTime} seconds ago.");

                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                ResetPwdResult retVal = result.ToObject<ResetPwdResult>();

                if (retVal.Result)
                    _lastTime = DateTime.UtcNow;

                return retVal;
            }
            catch (Exception e)
            {
                return new ResetPwdResult (false, e.Message);
            }
        }
    }
}