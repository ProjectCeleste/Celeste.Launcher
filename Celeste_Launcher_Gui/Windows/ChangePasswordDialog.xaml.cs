using Celeste_Launcher_Gui.Extensions;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logging;
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
            var currentPassword = CurrentPasswordField.PasswordInputBox.Password;
            var newPassword = NewPasswordField.PasswordInputBox.Password;
            var confirmedNewPassword = ConfirmedPasswordField.PasswordInputBox.Password;

            if (newPassword != confirmedNewPassword)
            {
                GenericMessageDialog.Show(Properties.Resources.ChangePasswordMismatch,
                    DialogIcon.Error,
                    DialogOptions.Ok);

                return;
            }

            if (currentPassword == newPassword)
            {
                GenericMessageDialog.Show(Properties.Resources.ChangePasswordSamePassword,
                    DialogIcon.Error,
                    DialogOptions.Ok);

                return;
            }

            if (newPassword.Length < 8 || newPassword.Length > 32)
            {
                GenericMessageDialog.Show(Properties.Resources.ChangePasswordInvalidLength,
                    DialogIcon.Error,
                    DialogOptions.Ok);

                return;
            }

            if (!Misc.IsValidPassword(newPassword))
            {
                GenericMessageDialog.Show(Properties.Resources.ChangePasswordInvalidPassword,
                    DialogIcon.Error,
                    DialogOptions.Ok);

                return;
            }

            IsEnabled = false;

            try
            {
                var changePasswordResponse = await LegacyBootstrapper.WebSocketApi.DoChangePassword(currentPassword, newPassword);

                if (changePasswordResponse.Result)
                {
                    GenericMessageDialog.Show(Properties.Resources.ChangePasswordSuccess,
                        DialogIcon.None,
                        DialogOptions.Ok);

                    Close();
                    return;
                }

                GenericMessageDialog.Show(changePasswordResponse.GetLocalizedMessage(),
                        DialogIcon.Error,
                        DialogOptions.Ok);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage,
                        DialogIcon.Error,
                        DialogOptions.Ok);
            }
            finally
            {
                IsEnabled = true;
            }
        }
    }
}
