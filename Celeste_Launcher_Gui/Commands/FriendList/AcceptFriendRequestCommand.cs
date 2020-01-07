using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using Celeste_Launcher_Gui.Windows;
using Serilog;
using System;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Commands.FriendList
{
    public class AcceptFriendRequestCommand : ICommand
    {
        private FriendListViewModel _friendListViewModel;
        private Action _updateFriendAction;
        private IFriendService _friendService;
        private ILogger _logger;

        public AcceptFriendRequestCommand(FriendListViewModel friendListViewModel, Action updateFriendAction, IFriendService friendService, ILogger logger)
        {
            _friendListViewModel = friendListViewModel;
            _updateFriendAction = updateFriendAction;
            _friendService = friendService;
            _logger = logger;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (_friendListViewModel?.FriendListCount >= FriendService.MaxAllowedFriends)
                {
                    GenericMessageDialog.Show(Properties.Resources.FriendListMaxFriendsReached,
                                           DialogIcon.Warning,
                                           DialogOptions.Ok);
                    return;
                }

                var friend = (FriendListItem)parameter;
                _friendListViewModel.FriendListItems.Remove(friend);
                await _friendService.ConfirmFriendRequest(friend.XUid);
                _updateFriendAction();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage,
                        DialogIcon.Error,
                        DialogOptions.Ok);
            }
        }
    }
}
