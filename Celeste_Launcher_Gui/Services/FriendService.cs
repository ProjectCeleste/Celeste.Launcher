using Celeste_Launcher_Gui.Model.Friends;
using ProjectCeleste.Launcher.PublicApi.Logging;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.Member;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        private static FriendService Instance;

        public event FriendListUpdatedEventHandler FriendListUpdated;

        private readonly WebSocketApi _webSocket;

        private readonly Timer _updateTimer;

        private readonly ILogger _logger;

        private FriendService(WebSocketApi webSocket, ILogger logger)
        {
            _webSocket = webSocket;
            _logger = logger;

            _updateTimer = new Timer(new TimerCallback((_) => UpdateFriendList()), null, UpdateIntervalInMs, UpdateIntervalInMs);
        }

        // TODO: Singleton instance should be handled through DI once .net core 3.1
        public static FriendService GetInstance()
        {
            return Instance ?? (Instance = new FriendService(LegacyBootstrapper.WebSocketApi, LoggerFactory.GetLogger()));
        }

        public async Task<FriendList> FetchFriendList()
        {
            IList<Friend> friends = await GetFriendList();
            (IList<Friend> incomingFriends, IList<Friend> outgoingFriends) = await GetFriendRequests();

            FriendList friendList = new FriendList
            {
                Friends = friends,
                IncomingRequests = incomingFriends,
                OutgoingRequests = outgoingFriends
            };

            FriendListUpdated(friendList);

            return friendList;
        }

        private async void UpdateFriendList()
        {
            try
            {
                IList<Friend> friends = await GetFriendList();
                (IList<Friend> incomingFriends, IList<Friend> outgoingFriends) = await GetFriendRequests();

                FriendListUpdated(new FriendList
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
            GetFriendsResult response = await _webSocket.DoGetFriends();

            if (!response.Result || response.Friends == null)
            {
                throw new Exception($"Unable to get friend list: {response.Message}");
            }

            List<Friend> friends = new List<Friend>();

            foreach (FriendJson friend in response.Friends.Friends)
            {
                friends.Add(MapFriend(friend));
            }

            return friends;
        }

        private async Task<(IList<Friend> incomingRequests, IList<Friend> outgoingRequests)> GetFriendRequests()
        {
            GetPendingFriendsResult response = await _webSocket.DoGetPendingFriends();

            if (!response.Result)
            {
                throw new Exception($"Unable to get friend list: {response.Message}");
            }

            List<Friend> incomingRequests = new List<Friend>();
            List<Friend> outgoingRequests = new List<Friend>();

            foreach (FriendJson friend in response.PendingFriendsRequest.Friends)
            {
                outgoingRequests.Add(MapFriend(friend));
            }

            foreach (FriendJson friend in response.PendingFriendsInvite.Friends)
            {
                incomingRequests.Add(MapFriend(friend));
            }

            return (incomingRequests, outgoingRequests);
        }

        public async Task<bool> RemoveFriend(long xuid)
        {
            RemoveFriendResult response = await _webSocket.DoRemoveFriend(xuid);
            return response.Result;
        }

        public async Task<bool> SendFriendRequest(string username)
        {
            AddFriendResult response = await _webSocket.DoAddFriend(username);
            return response.Result;
        }

        public async Task<bool> ConfirmFriendRequest(long xuid)
        {
            ConfirmFriendResult response = await _webSocket.DoConfirmFriend(xuid);
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
