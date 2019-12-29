#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class GetFriends
    {
        public const string CmdName = "GETFRIENDS";

        private readonly DataExchange _dataExchange;

        public GetFriends(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<GetFriendsResult> DoGetFriends(GetFriendsInfo request)
        {
            try
            {
                return await _dataExchange.DoDataExchange<GetFriendsResult, GetFriendsInfo>(request, CmdName);
            }
            catch (Exception e)
            {
                return new GetFriendsResult(false, e.Message);
            }
        }
    }
}