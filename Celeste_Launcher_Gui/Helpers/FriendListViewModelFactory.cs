using Celeste_Launcher_Gui.Commands.FriendList;
using Celeste_Launcher_Gui.Model.Friends;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Helpers
{
    class FriendListViewModelFactory
    {
        private IFriendService _friendService;
        private ILogger _logger;

        public FriendListViewModelFactory(IFriendService friendService, ILogger logger)
        {
            _friendService = friendService;
            _logger = logger;
        }

        public FriendListViewModel CreateFriendListViewModel(FriendList friendList, Action refreshFriendListAction)
        {
            var friendListViewModel = new FriendListViewModel()
            {
                FriendListItems = new ObservableCollection<FriendListItem>()
            };

            var acceptFriendCommand = new AcceptFriendRequestCommand(friendListViewModel, refreshFriendListAction, _friendService, _logger);
            var removeFriendCommand = new RemoveFriendCommand(friendListViewModel, refreshFriendListAction, _friendService, _logger);

            foreach (var incomingRequest in friendList.IncomingRequests)
                friendListViewModel.FriendListItems.Add(MapFriendRequestToViewModel(incomingRequest, acceptFriendCommand, removeFriendCommand));

            foreach (var friend in friendList.Friends.Where(t => t.IsOnline))
                friendListViewModel.FriendListItems.Add(MapFriendToViewModel(friend, removeFriendCommand));

            foreach (var friend in friendList.Friends.Where(t => !t.IsOnline))
                friendListViewModel.FriendListItems.Add(MapFriendToViewModel(friend, removeFriendCommand));

            friendListViewModel.FriendListItems.Add(new FriendListSeparator());

            foreach (var outgoingRequest in friendList.OutgoingRequests)
                friendListViewModel.FriendListItems.Add(MapOutoingFriendRequest(outgoingRequest, removeFriendCommand));

            friendListViewModel.OnlineFriendsCount = friendListViewModel.FriendListItems.Count(t => t is OnlineFriend);
            friendListViewModel.FriendListCount = friendList.Friends.Count;

            return friendListViewModel;
        }

        private FriendListItem MapFriendToViewModel(Friend friend, ICommand removeFriendCommand)
        {
            var presenceComponents = friend.RichPresence.Replace("\\r", "").Split('\n');
            var friendIsInGame = presenceComponents.Length > 1;
            var friendStatus = friendIsInGame ? presenceComponents[1] : string.Empty;

            if (friend.IsOnline)
            {
                var faction = presenceComponents[0];

                return new OnlineFriend()
                {
                    XUid = friend.Xuid,
                    Username = friend.Username,
                    Faction = faction,
                    Status = friendStatus,
                    RemoveFriendCommand = removeFriendCommand,
                    ProfilePictureBackgroundLocation = DecideProfilePictureForFaction(faction),
                    ListViewItemBackgroundLocation = DecideBackgroundForFaction(faction)
                };
            }
            else
            {
                return new OfflineFriend()
                {
                    XUid = friend.Xuid,
                    Username = friend.Username,
                    RemoveFriendCommand = removeFriendCommand
                };
            }
        }

        private FriendListItem MapFriendRequestToViewModel(Friend incomingFriend, ICommand acceptFriendCommand, ICommand declineFriendCommand)
        {
            return new IncomingFriendRequest()
            {
                AcceptFriendRequestCommand = acceptFriendCommand,
                DeclineFriendRequestCommand = declineFriendCommand,
                Username = incomingFriend.Username,
                XUid = incomingFriend.Xuid
            };
        }

        private FriendListItem MapOutoingFriendRequest(Friend outgoingRequest, ICommand cancelRequestCommand)
        {
            return new OutgoingFriendRequest()
            {
                Username = outgoingRequest.Username,
                XUid = outgoingRequest.Xuid,
                CancelFriendRequestCommand = cancelRequestCommand
            };
        }

        private string DecideProfilePictureForFaction(string status)
        {
            var factionCardName = GetFaction(status) ?? "Default";
            return $"pack://application:,,,/Celeste Launcher;component/Resources/FriendList/ProfilePictures/{factionCardName}.png";
        }

        private string DecideBackgroundForFaction(string status)
        {
            var factionCardName = GetFaction(status) ?? "Empty";
            return $"pack://application:,,,/Celeste Launcher;component/Resources/FriendList/FriendPlate-{factionCardName}.png";
        }

        private string GetFaction(string status)
        {
            var comparer = CultureInfo.InvariantCulture.CompareInfo;

            var factions = new[] { "Babylon", "Celt", "Egypt", "Greek", "Norse", "Roman", "Persia" };

            foreach (var faction in factions)
            {
                if (comparer.IndexOf(status, faction, CompareOptions.IgnoreCase) >= 0)
                    return faction;
            }

            return null;
        }
    }
}
