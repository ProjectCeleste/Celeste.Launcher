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

        public GetPendingFriends(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<GetPendingFriendsResult> DoGetPendingFriends(GetPendingFriendsInfo request)
        {
            try
            {
                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                GetPendingFriendsResult retVal = result.ToObject<GetPendingFriendsResult>();

                return retVal;
            }
            catch (Exception e)
            {
                return new GetPendingFriendsResult(false, e.Message);
            }
        }
    }
}