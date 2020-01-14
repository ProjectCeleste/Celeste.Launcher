using ProjectCeleste.Launcher.PublicApi.Helpers;
using ProjectCeleste.Launcher.PublicApi.Logging;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Enum;
using Serilog;
using System;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public ChangePasswordDialog()
        {
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

        private async void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            string currentPassword = CurrentPasswordField.PasswordInputBox.Password;
            string newPassword = NewPasswordField.PasswordInputBox.Password;
            string confirmedNewPassword = ConfirmedPasswordField.PasswordInputBox.Password;

            if (newPassword != confirmedNewPassword)
            {
                GenericMessageDialog.Show(Properties.Resources.ChangePasswordMismatch,
                    DialogIcon.Error,
                    DialogOptions.Ok);

                return;
            }

            IsEnabled = false;

            try
            {
                ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.CommandInfo.Member.ChangePwdResult changePasswordResponse = await LegacyBootstrapper.WebSocketApi.DoChangePassword(currentPassword, newPassword);

                if (changePasswordResponse.Result)
                {
                    GenericMessageDialog.Show(Properties.Resources.ChangePasswordSuccess,
                        DialogIcon.None,
                        DialogOptions.Ok);

                    Close();
                    return;
                }

                GenericMessageDialog.Show($"{Properties.Resources.ChangePasswordError} {changePasswordResponse.Message}",
                        DialogIcon.Error,
                        DialogOptions.Ok);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                if (ex.Data.Contains("ErrorCode") && ex.Data["ErrorCode"] is CommandErrorCode errorCode)
                {
                    switch (errorCode)
                    {
                        case CommandErrorCode.InvalidPassword:
                        case CommandErrorCode.InvalidNewPassword:
                            GenericMessageDialog.Show(Properties.Resources.ChangePasswordInvalidPassword, DialogIcon.Error);
                            break;
                        case CommandErrorCode.InvalidPasswordLength:
                        case CommandErrorCode.InvalidNewPasswordLength:
                            GenericMessageDialog.Show(Properties.Resources.ChangePasswordInvalidLength, DialogIcon.Error);
                            break;
                        case CommandErrorCode.InvalidPasswordMatch:
                            GenericMessageDialog.Show(Properties.Resources.ChangePasswordSamePassword, DialogIcon.Error);
                            break;
                        default:
                            GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
                            break;
                    }
                }
                else
                {
                    GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
                }
            }
            finally
            {
                IsEnabled = true;
            }
        }
    }
}
