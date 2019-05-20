#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class AddFriend
    {
        public const string CmdName = "ADDFRIEND";

        public AddFriend(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<AddFriendResult> DoAddFriend(AddFriendInfo request)
        {
            try
            {
                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                AddFriendResult retVal = result.ToObject<AddFriendResult>();

                return retVal;
            }
            catch (Exception e)
            {
                return new AddFriendResult(false, e.Message);
            }
        }
    }
}