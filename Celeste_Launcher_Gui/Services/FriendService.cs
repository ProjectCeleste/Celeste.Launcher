#region Using directives

using Celeste_Launcher_Gui.Model.Friends;
using ProjectCeleste.Launcher.PublicApi.Logging;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.Member;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion Using directives

namespace Celeste_Launcher_Gui.Services
{
    public delegate void FriendListUpdatedEventHandler(FriendList e);

    public interface IFriendService
    {
        Task<FriendList> FetchFriendList();

        Task<bool> RemoveFriend(long xuid);

        Task<bool> SendFriendRequest(string username);

        Task<bool> ConfirmFriendRequest(long xuid);

        event FriendListUpdatedEventHandler FriendListUpdated;
    }

    public class FriendService : IFriendService
    {
        public const int MaxAllowedFriends = 99;
        private const int UpdateIntervalInMs = 30 * 1000;
        private static FriendService _instance;

        public event FriendListUpdatedEventHandler FriendListUpdated;

        private readonly WebSocketApi _webSocket;

        private readonly Timer _updateTimer;

        private readonly ILogger _logger;

        private FriendService(WebSocketApi webSocket, ILogger logger)
        {
            _webSocket = webSocket;
            _logger = logger;

            _updateTimer = new Timer(_ => UpdateFriendList(), null, UpdateIntervalInMs, UpdateIntervalInMs);
        }

        // TODO: Singleton instance should be handled through DI once .net core 3.1
        public static FriendService GetInstance()
        {
            return _instance ??
                   (_instance = new FriendService(LegacyBootstrapper.WebSocketApi, LoggerFactory.GetLogger()));
        }

        public async Task<FriendList> FetchFriendList()
        {
            var friends = await GetFriendList();
            var (incomingFriends, outgoingFriends) = await GetFriendRequests();

            var friendList = new FriendList
            {
                Friends = friends,
                IncomingRequests = incomingFriends,
                OutgoingRequests = outgoingFriends
            };

            FriendListUpdated?.Invoke(friendList);

            return friendList;
        }

        private async void UpdateFriendList()
        {
            try
            {
                var friends = await GetFriendList();
                var (incomingFriends, outgoingFriends) = await GetFriendRequests();

                FriendListUpdated?.Invoke(new FriendList
                {
                    Friends = friends,
                    IncomingRequests = incomingFriends,
                    OutgoingRequests = outgoingFriends
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        private async Task<IList<Friend>> GetFriendList()
        {
            var response = await _webSocket.DoGetFriends();

            if (!response.Result || response.Friends == null)
                throw new Exception($"Unable to get friend list: {response.Message}");

            var friends = new List<Friend>();

            foreach (var friend in response.Friends.Friends) friends.Add(MapFriend(friend));

            return friends;
        }

        private async Task<(IList<Friend> incomingRequests, IList<Friend> outgoingRequests)> GetFriendRequests()
        {
            var response = await _webSocket.DoGetPendingFriends();

            if (!response.Result) throw new Exception($"Unable to get friend list: {response.Message}");

            var incomingRequests = new List<Friend>();
            var outgoingRequests = new List<Friend>();

            foreach (var friend in response.PendingFriendsRequest.Friends) outgoingRequests.Add(MapFriend(friend));

            foreach (var friend in response.PendingFriendsInvite.Friends) incomingRequests.Add(MapFriend(friend));

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