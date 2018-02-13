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
        
        public RemoveFriend(Client webSocketClient)
        {
            DataExchange = new DataExchange(webSocketClient, CmdName);
        }

        private DataExchange DataExchange { get; }

        public async Task<RemoveFriendResult> DoRemoveFriend(RemoveFriendInfo request)
        {
            try
            {
                dynamic requestInfo = request;

                var result = await DataExchange.DoDataExchange((object) requestInfo);

                RemoveFriendResult retVal = result.ToObject<RemoveFriendResult>();
                
                return retVal;
            }
            catch (Exception e)
            {
                return new RemoveFriendResult(false, e.Message);
            }
        }
    }
}