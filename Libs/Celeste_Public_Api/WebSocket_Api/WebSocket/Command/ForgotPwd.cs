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
        private readonly DataExchange _dataExchange;

        public ForgotPwd(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<ForgotPwdResult> DoForgotPwd(ForgotPwdInfo request)
        {
            try
            {
                var lastSendTime = (DateTime.UtcNow - _lastTime).TotalSeconds;
                if (lastSendTime <= 90)
                    throw new Exception(
                        $"You need to wait at least 90 seconds before asking to resend an confirmation key! Last request was {lastSendTime} seconds ago.");

                var result = await _dataExchange.DoDataExchange<ForgotPwdResult, ForgotPwdInfo>(request, CmdName);

                if (result.Result)
                    _lastTime = DateTime.UtcNow;

                return result;
            }
            catch (Exception e)
            {
                return new ForgotPwdResult(false, e.Message);
            }
        }
    }
}