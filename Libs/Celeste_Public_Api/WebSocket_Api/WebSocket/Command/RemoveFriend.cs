#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class RemoveFriend
    {
        public const string CmdName = "REMFRIEND";

        private readonly DataExchange _dataExchange;

        public RemoveFriend(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<RemoveFriendResult> DoRemoveFriend(RemoveFriendInfo request)
        {
            try
            {
                return await _dataExchange.DoDataExchange<RemoveFriendResult, RemoveFriendInfo>(request, CmdName);
            }
            catch (Exception e)
            {
                return new RemoveFriendResult(false, e.Message);
            }
        }
    }
}