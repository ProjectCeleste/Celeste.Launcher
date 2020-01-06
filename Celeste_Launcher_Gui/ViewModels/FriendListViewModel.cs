using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.ViewModels
{
    public class FriendListViewModel
    {
        public ObservableCollection<FriendListItem> FriendListItems { get; set; }
        public int OnlineFriendsCount { get; set; }
    }

    public class OnlineFriend : ExistingFriend
    {
        public string Faction { get; set; }
        public string Status { get; set; }
        public string ProfilePictureBackgroundLocation { get; set; }
        public string ListViewItemBackgroundLocation { get; set; }
    }

    public class OfflineFriend : ExistingFriend
    {
    }

    public class ExistingFriend : FriendListItem
    {
        public ICommand RemoveFriendCommand { get; set; }
    }

    public class OutgoingFriendRequest : FriendListItem
    {
        public ICommand CancelFriendRequestCommand { get; set; }
    }

    public class IncomingFriendRequest : FriendListItem
    {
        public ICommand AcceptFriendRequestCommand { get; set; }
        public ICommand DeclineFriendRequestCommand { get; set; }
    }

    public class FriendListSeparator : FriendListItem
    {
    }

    public abstract class FriendListItem
    {
        public long XUid { get; set; }
        public string Username { get; set; }
    }
}
