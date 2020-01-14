#region Using directives

using Celeste_Launcher_Gui.Services;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Enum;
using System;
using System.Windows;
using System.Windows.Input;

#endregion Using directives

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    ///     Interaction logic for AddFriendDialog.xaml
    /// </summary>
    public partial class AddFriendDialog : Window
    {
        private readonly IFriendService _friendService;

        public AddFriendDialog(IFriendService friendService)
        {
            _friendService = friendService;
            InitializeComponent();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void AddFriendClick(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            try
            {
                var result = await _friendService.SendFriendRequest(UsernameInputField.InputContent);

                if (result)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    GenericMessageDialog.Show(
                        string.Format(Properties.Resources.SendFriendRequestFailed, UsernameInputField.InputContent),
                        DialogIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("ErrorCode") && ex.Data["ErrorCode"] is CommandErrorCode errorCode)
                    switch (errorCode)
                    {
                        case CommandErrorCode.InvalidUsername:
                        case CommandErrorCode.InvalidUsernameLength:
                            GenericMessageDialog.Show(Properties.Resources.RegisterInvalidUsername, DialogIcon.Error);
                            break;

                        default:
                            GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage,
                                DialogIcon.Error);
                            break;
                    }
                else
                    GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
            }
            finally
            {
                IsEnabled = true;
            }
        }

        private void CancelFriendClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}