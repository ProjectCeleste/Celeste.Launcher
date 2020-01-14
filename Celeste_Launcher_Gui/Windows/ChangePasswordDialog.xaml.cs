#region Using directives

using ProjectCeleste.Launcher.PublicApi.Logging;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Enum;
using Serilog;
using System;
using System.Windows;
using System.Windows.Input;

#endregion Using directives

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    ///     Interaction logic for ChangePasswordDialog.xaml
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
            var currentPassword = CurrentPasswordField.PasswordInputBox.Password;
            var newPassword = NewPasswordField.PasswordInputBox.Password;
            var confirmedNewPassword = ConfirmedPasswordField.PasswordInputBox.Password;

            if (newPassword != confirmedNewPassword)
            {
                GenericMessageDialog.Show(Properties.Resources.ChangePasswordMismatch,
                    DialogIcon.Error);

                return;
            }

            IsEnabled = false;

            try
            {
                var changePasswordResponse =
                    await LegacyBootstrapper.WebSocketApi.DoChangePassword(currentPassword, newPassword);

                if (changePasswordResponse.Result)
                {
                    GenericMessageDialog.Show(Properties.Resources.ChangePasswordSuccess);

                    Close();
                    return;
                }

                GenericMessageDialog.Show(
                    $"{Properties.Resources.ChangePasswordError} {changePasswordResponse.Message}",
                    DialogIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                if (ex.Data.Contains("ErrorCode") && ex.Data["ErrorCode"] is CommandErrorCode errorCode)
                    switch (errorCode)
                    {
                        case CommandErrorCode.InvalidPassword:
                        case CommandErrorCode.InvalidNewPassword:
                            GenericMessageDialog.Show(Properties.Resources.ChangePasswordInvalidPassword,
                                DialogIcon.Error);
                            break;

                        case CommandErrorCode.InvalidPasswordLength:
                        case CommandErrorCode.InvalidNewPasswordLength:
                            GenericMessageDialog.Show(Properties.Resources.ChangePasswordInvalidLength,
                                DialogIcon.Error);
                            break;

                        case CommandErrorCode.InvalidPasswordMatch:
                            GenericMessageDialog.Show(Properties.Resources.ChangePasswordSamePassword,
                                DialogIcon.Error);
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
    }
}