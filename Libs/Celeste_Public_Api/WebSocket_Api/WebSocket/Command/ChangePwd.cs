#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class ChangePwd
    {
        public const string CmdName = "CHANGEPWD";

        private DateTime _lastTime = DateTime.UtcNow.AddMinutes(-5);
        private readonly DataExchange _dataExchange;

        public ChangePwd(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<ChangePwdResult> DoChangePwd(ChangePwdInfo request)
        {
            try
            {
                var lastSendTime = (DateTime.UtcNow - _lastTime).TotalSeconds;
                if (lastSendTime <= 90)
                    throw new Exception(
                        $"You need to wait at least 90 seconds before to try again! Last try was {lastSendTime} seconds ago.");

                dynamic requestInfo = request;

                var result = await _dataExchange.DoDataExchange<ChangePwdResult, ChangePwdInfo>(request, CmdName);

                if (result.Result)
                    _lastTime = DateTime.UtcNow;

                return result;
            }
            catch (Exception e)
            {
                return new ChangePwdResult(false, e.Message);
            }
        }
    }
}