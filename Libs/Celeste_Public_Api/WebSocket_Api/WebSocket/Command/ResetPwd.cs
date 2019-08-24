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

        private readonly DataExchange _dataExchange;

        public ResetPwd(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

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

                var result = await _dataExchange.DoDataExchange<ResetPwdResult, ResetPwdInfo>(request, CmdName);

                if (result.Result)
                    _lastTime = DateTime.UtcNow;

                return result;
            }
            catch (Exception e)
            {
                return new ResetPwdResult(false, e.Message);
            }
        }
    }
}