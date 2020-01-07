using Celeste_Launcher_Gui.Model.Friends;
using Celeste_Public_Api.WebSocket_Api;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.Services
{
    public interface IFriendService
    {
        Task<IList<Friend>> GetFriendList();

        Task<(IList<Friend> incomingRequests, IList<Friend> outgoingRequests)> GetFriendRequests();

        Task<bool> RemoveFriend(long xuid);

        Task<bool> SendFriendRequest(string username);

        Task<bool> ConfirmFriendRequest(long xuid);
    }

    public class FriendService : IFriendService
    {
        private WebSocketApi _webSocket;

        public const int MaxAllowedFriends = 99;

        public FriendService(WebSocketApi webSocket)
        {
            _webSocket = webSocket;
        }

        public async Task<IList<Friend>> GetFriendList()
        {
            var response = await _webSocket.DoGetFriends();

            if (!response.Result || response.Friends == null)
            {
                throw new Exception($"Unable to get friend list: {response.Message}");
            }

            var friends = new List<Friend>();

            foreach (var friend in response.Friends.Friends)
            {
                friends.Add(MapFriend(friend));
            }

            return friends;
        }

        public async Task<(IList<Friend> incomingRequests, IList<Friend> outgoingRequests)> GetFriendRequests()
        {
            var response = await _webSocket.DoGetPendingFriends();

            if (!response.Result)
            {
                throw new Exception("Unable to get friend list");
            }

            var incomingRequests = new List<Friend>();
            var outgoingRequests = new List<Friend>();

            foreach (var friend in response.PendingFriendsRequest.Friends)
            {
                outgoingRequests.Add(MapFriend(friend));
            }

            foreach (var friend in response.PendingFriendsInvite.Friends)
            {
                incomingRequests.Add(MapFriend(friend));
            }

            return (incomingRequests, outgoingRequests);
        }

        public async Task<bool> RemoveFriend(long xuid)
        {
            var response = await _webSocket.DoRemoveFriend(xuid);
            return response.Result;
        }

        public async Task<bool> SendFriendRequest(string username)
        {
            var response = await _webSocket.DoAddFriend(username);
            return response.Result;
        }

        public async Task<bool> ConfirmFriendRequest(long xuid)
        {
            var response = await _webSocket.DoConfirmFriend(xuid);
            return response.Result;
        }

        private Friend MapFriend(FriendJson friend)
        {
            return new Friend
            {
                Xuid = friend.Xuid,
                Username = friend.ProfileName,
                IsOnline = friend.IsConnected,
                RichPresence = friend.RichPresence
            };
        }
    }
}
