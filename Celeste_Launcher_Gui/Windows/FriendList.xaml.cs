using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using Celeste_Public_Api.Logging;
using Serilog;
using System;
using System.Globalization;
using System.Threading;
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
        private Timer _updateTimer;
        private IFriendService _friendService;
        private ILogger _logger;
        private FriendListViewModelFactory _friendListViewModelFactory;

        private static FriendList Instance;

        public static void Display()
        {
            if (Instance == null)
                Instance = new FriendList();

            Instance.Show();
        }

        private FriendList()
        {
            InitializeComponent();
            _updateTimer = new Timer(new TimerCallback((o) => UpdateFriendList()), null, 0, 30000);
            _friendService = new FriendService(LegacyBootstrapper.WebSocketApi);
            _logger = LoggerFactory.GetLogger();
            _friendListViewModelFactory = new FriendListViewModelFactory(_friendService, _logger);
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private async void UpdateFriendList()
        {
            try
            {
                var friendListViewModel = await _friendListViewModelFactory.CreateFriendListViewModel(UpdateFriendList);

                Dispatcher.Invoke(() =>
                {
                    DataContext = friendListViewModel;

                    var view = (CollectionView)CollectionViewSource.GetDefaultView(friendListViewModel.FriendListItems);
                    view.Filter = FilterFriendListViewItem;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        private bool FilterFriendListViewItem(object item)
        {
            var friendListViewItem = item as FriendListItem;
            var filterText = FilterInputText.Text;

            if (string.IsNullOrWhiteSpace(filterText) || friendListViewItem == null || friendListViewItem.Username == null)
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
            var friendList = DataContext as FriendListViewModel;
            if (friendList?.FriendListCount >= FriendService.MaxAllowedFriends)
            {
                GenericMessageDialog.Show(Properties.Resources.FriendListMaxFriendsReached,
                                       DialogIcon.Warning,
                                       DialogOptions.Ok);
                return;
            }

            var userSelectedAddFriend = new AddFriendDialog(_friendService).ShowDialog();
            if (userSelectedAddFriend == true)
                UpdateFriendList();
        }

        private void FilterTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            CollectionViewSource.GetDefaultView(((FriendListViewModel)DataContext).FriendListItems)?.Refresh();
        }

        private void FriendListClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _updateTimer.Dispose();
        }
    }
}
