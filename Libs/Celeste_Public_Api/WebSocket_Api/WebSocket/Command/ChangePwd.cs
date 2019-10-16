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
                if (request.Old == request.New)
                    throw new Exception("Old password = New password!");

                if (request.New.Length < 8)
                    throw new Exception("Password minimum length is 8 char!");

                if (request.New.Length > 32)
                    throw new Exception("Password maximum length is 32 char!");

                if (!Misc.IsValidPassword(request.New))
                    throw new Exception("Invalid password, character ' and \" are not allowed!");

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