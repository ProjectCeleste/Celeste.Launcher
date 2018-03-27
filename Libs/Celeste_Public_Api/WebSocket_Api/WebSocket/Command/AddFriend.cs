#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
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
                if (!Misc.IsValideUserName(request.FriendName))
                    throw new Exception(
                        "Invalid User Name, only letters and digits allowed, minimum length is 3 char and maximum length is 15 char!");

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