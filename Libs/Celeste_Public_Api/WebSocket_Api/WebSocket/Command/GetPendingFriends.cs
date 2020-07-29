#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class GetPendingFriends
    {
        public const string CmdName = "GETPFRIENDS";

        private readonly DataExchange _dataExchange;

        public GetPendingFriends(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<GetPendingFriendsResult> DoGetPendingFriends(GetPendingFriendsInfo request)
        {
            try
            {
                return await _dataExchange.DoDataExchange<GetPendingFriendsResult, GetPendingFriendsInfo>(request, CmdName);
            }
            catch (Exception e)
            {
                return new GetPendingFriendsResult(false, e.Message);
            }
        }
    }
}