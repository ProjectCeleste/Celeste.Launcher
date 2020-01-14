#region Using directives

using Celeste_Launcher_Gui.Properties;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using Celeste_Launcher_Gui.Windows;
using Serilog;
using System;
using System.Windows.Input;

#endregion Using directives

namespace Celeste_Launcher_Gui.Commands.FriendList
{
    public class AcceptFriendRequestCommand : ICommand
    {
        private readonly FriendListViewModel _friendListViewModel;
        private readonly Action _updateFriendAction;
        private readonly IFriendService _friendService;
        private readonly ILogger _logger;

        public AcceptFriendRequestCommand(FriendListViewModel friendListViewModel, Action updateFriendAction,
            IFriendService friendService, ILogger logger)
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
                    GenericMessageDialog.Show(Resources.FriendListMaxFriendsReached,
                        DialogIcon.Warning);
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
                GenericMessageDialog.Show(Resources.GenericUnexpectedErrorMessage,
                    DialogIcon.Error);
            }
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }
    }
}