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

        public ConfirmFriend(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<ConfirmFriendResult> DoConfirmFriend(ConfirmFriendInfo request)
        {
            try
            {
                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                ConfirmFriendResult retVal = result.ToObject<ConfirmFriendResult>();

                return retVal;
            }
            catch (Exception e)
            {
                return new ConfirmFriendResult(false, e.Message);
            }
        }
    }
}