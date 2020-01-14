using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using Celeste_Launcher_Gui.Windows;
using Serilog;
using System;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Commands.FriendList
{
    internal class RemoveFriendCommand : ICommand
    {
        private readonly FriendListViewModel _friendListViewModel;
        private readonly Action _updateFriendAction;
        private readonly IFriendService _friendService;
        private readonly ILogger _logger;

        public RemoveFriendCommand(FriendListViewModel friendListViewModel, Action updateFriendAction, IFriendService friendService, ILogger logger)
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
                FriendListItem friend = (FriendListItem)parameter;
                _friendListViewModel.FriendListItems.Remove(friend);
                await _friendService.RemoveFriend(friend.XUid);
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

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }
    }
}
