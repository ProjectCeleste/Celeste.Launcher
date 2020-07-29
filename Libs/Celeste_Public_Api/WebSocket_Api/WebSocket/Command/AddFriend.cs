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

        private readonly DataExchange _dataExchange;

        public AddFriend(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<AddFriendResult> DoAddFriend(AddFriendInfo request)
        {
            try
            {
                return await _dataExchange.DoDataExchange<AddFriendResult, AddFriendInfo>(request, CmdName);
            }
            catch (Exception e)
            {
                return new AddFriendResult(false, e.Message);
            }
        }
    }
}