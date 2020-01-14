using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using ProjectCeleste.Launcher.PublicApi.Logging;
using Serilog;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for FriendList.xaml
    /// </summary>
    public partial class FriendList : Window
    {
        private readonly IFriendService _friendService;
        private readonly ILogger _logger;
        private readonly FriendListViewModelFactory _friendListViewModelFactory;

        private static FriendList Instance;

        public static void Display()
        {
            (Instance ?? (Instance = new FriendList())).Show();
        }

        private FriendList()
        {
            InitializeComponent();
            _friendService = FriendService.GetInstance();
            _logger = LoggerFactory.GetLogger();
            _friendListViewModelFactory = new FriendListViewModelFactory(_friendService, _logger);

            _friendService.FriendListUpdated += FriendService_FriendListUpdated;
        }

        private void FriendService_FriendListUpdated(Model.Friends.FriendList friendList)
        {
            SetFriendList(friendList);
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void FriendListLoaded(object sender, RoutedEventArgs e)
        {
            UpdateFriendList();
        }

        private async void UpdateFriendList()
        {
            try
            {
                Model.Friends.FriendList friendList = await _friendService.FetchFriendList();
                SetFriendList(friendList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        private void SetFriendList(Model.Friends.FriendList friendList)
        {
            FriendListViewModel friendListViewModel = _friendListViewModelFactory.CreateFriendListViewModel(friendList, UpdateFriendList);

            Dispatcher.Invoke(() =>
            {
                DataContext = friendListViewModel;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(friendListViewModel.FriendListItems);
                view.Filter = FilterFriendListViewItem;
            });
        }

        private bool FilterFriendListViewItem(object item)
        {
            string filterText = FilterInputText.Text;

            if (string.IsNullOrWhiteSpace(filterText) || !(item is FriendListItem friendListViewItem) || friendListViewItem.Username == null)
                return true;

            if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(friendListViewItem.Username, filterText, CompareOptions.IgnoreCase) >= 0)
                return true;

            if (string.Equals("online", filterText, StringComparison.OrdinalIgnoreCase) && friendListViewItem is OnlineFriend)
                return true;

            if (string.Equals("offline", filterText, StringComparison.OrdinalIgnoreCase) && friendListViewItem is OfflineFriend)
                return true;

            if (string.Equals("incoming", filterText, StringComparison.OrdinalIgnoreCase) && friendListViewItem is IncomingFriendRequest)
                return true;

            if (string.Equals("outgoing", filterText, StringComparison.OrdinalIgnoreCase) && friendListViewItem is OutgoingFriendRequest)
                return true;

            return false;
        }

        private void AddFriendClick(object sender, RoutedEventArgs e)
        {
            FriendListViewModel friendList = DataContext as FriendListViewModel;
            if (friendList?.FriendListCount >= FriendService.MaxAllowedFriends)
            {
                GenericMessageDialog.Show(Properties.Resources.FriendListMaxFriendsReached,
                                       DialogIcon.Warning,
                                       DialogOptions.Ok);
                return;
            }

            AddFriendDialog addFriendDialog = new AddFriendDialog(_friendService)
            {
                Owner = this
            };

            bool? userSelectedAddFriend = addFriendDialog.ShowDialog();

            if (userSelectedAddFriend == true)
                UpdateFriendList();
        }

        private void FilterTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(((FriendListViewModel)DataContext).FriendListItems)?.Refresh();
        }
    }
}
