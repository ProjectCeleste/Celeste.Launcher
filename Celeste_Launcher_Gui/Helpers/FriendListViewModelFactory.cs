using Celeste_Launcher_Gui.Commands.FriendList;
using Celeste_Launcher_Gui.Model.Friends;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Helpers
{
    internal class FriendListViewModelFactory
    {
        private readonly IFriendService _friendService;
        private readonly ILogger _logger;

        public FriendListViewModelFactory(IFriendService friendService, ILogger logger)
        {
            _friendService = friendService;
            _logger = logger;
        }

        public FriendListViewModel CreateFriendListViewModel(FriendList friendList, Action refreshFriendListAction)
        {
            FriendListViewModel friendListViewModel = new FriendListViewModel()
            {
                FriendListItems = new ObservableCollection<FriendListItem>()
            };

            AcceptFriendRequestCommand acceptFriendCommand = new AcceptFriendRequestCommand(friendListViewModel, refreshFriendListAction, _friendService, _logger);
            RemoveFriendCommand removeFriendCommand = new RemoveFriendCommand(friendListViewModel, refreshFriendListAction, _friendService, _logger);

            foreach (Friend incomingRequest in friendList.IncomingRequests)
                friendListViewModel.FriendListItems.Add(MapFriendRequestToViewModel(incomingRequest, acceptFriendCommand, removeFriendCommand));

            foreach (Friend friend in friendList.Friends.Where(t => t.IsOnline))
                friendListViewModel.FriendListItems.Add(MapFriendToViewModel(friend, removeFriendCommand));

            foreach (Friend friend in friendList.Friends.Where(t => !t.IsOnline))
                friendListViewModel.FriendListItems.Add(MapFriendToViewModel(friend, removeFriendCommand));

            friendListViewModel.FriendListItems.Add(new FriendListSeparator());

            foreach (Friend outgoingRequest in friendList.OutgoingRequests)
                friendListViewModel.FriendListItems.Add(MapOutoingFriendRequest(outgoingRequest, removeFriendCommand));

            friendListViewModel.OnlineFriendsCount = friendListViewModel.FriendListItems.Count(t => t is OnlineFriend);
            friendListViewModel.FriendListCount = friendList.Friends.Count;

            return friendListViewModel;
        }

        private FriendListItem MapFriendToViewModel(Friend friend, ICommand removeFriendCommand)
        {
            string[] presenceComponents = friend.RichPresence.Replace("\\r", "").Split('\n');
            bool friendIsInGame = presenceComponents.Length > 1;
            string friendStatus = friendIsInGame ? presenceComponents[1] : string.Empty;

            if (friend.IsOnline)
            {
                string faction = presenceComponents[0];

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
            string factionCardName = GetFaction(status) ?? "Default";
            return $"pack://application:,,,/Celeste Launcher;component/Resources/FriendList/ProfilePictures/{factionCardName}.png";
        }

        private string DecideBackgroundForFaction(string status)
        {
            string factionCardName = GetFaction(status) ?? "Empty";
            return $"pack://application:,,,/Celeste Launcher;component/Resources/FriendList/FriendPlate-{factionCardName}.png";
        }

        private string GetFaction(string status)
        {
            CompareInfo comparer = CultureInfo.InvariantCulture.CompareInfo;

            foreach (string faction in new[] { "Babylon", "Celt", "Egypt", "Greek", "Norse", "Roman", "Persia" })
            {
                if (comparer.IndexOf(status, faction, CompareOptions.IgnoreCase) >= 0)
                    return faction;
            }

            return null;
        }
    }
}
