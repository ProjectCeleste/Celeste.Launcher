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

        public GetFriends(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<GetFriendsResult> DoGetFriends(GetFriendsInfo request)
        {
            try
            {
                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                GetFriendsResult retVal = result.ToObject<GetFriendsResult>();

                return retVal;
            }
            catch (Exception e)
            {
                return new GetFriendsResult(false, e.Message);
            }
        }
    }
}