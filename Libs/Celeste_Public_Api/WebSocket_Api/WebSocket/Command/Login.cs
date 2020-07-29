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

        private readonly DataExchange _dataExchange;

        public Login(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<LoginResult> DoLogin(LoginInfo request)
        {
            try
            {
                return await _dataExchange.DoDataExchange<LoginResult, LoginInfo>(request, CmdName);               
            }
            catch (Exception e)
            {
                return new LoginResult(false, e.Message);
            }
        }
    }
}