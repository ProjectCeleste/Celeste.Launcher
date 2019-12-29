#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class ConfirmFriend
    {
        public const string CmdName = "CONFFRIEND";

        private DataExchange _dataExchange;

        public ConfirmFriend(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<ConfirmFriendResult> DoConfirmFriend(ConfirmFriendInfo request)
        {
            try
            {
                return await _dataExchange.DoDataExchange<ConfirmFriendResult, ConfirmFriendInfo>(request, CmdName);
            }
            catch (Exception e)
            {
                return new ConfirmFriendResult(false, e.Message);
            }
        }
    }
}