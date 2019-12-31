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
                GenericMessageDialog.Show("New password value and confirm new password value don't match",
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
                    GenericMessageDialog.Show("Password successfully changed.",
                        DialogIcon.None,
                        DialogOptions.Ok);

                    Close();
                    return;
                }

                GenericMessageDialog.Show($@"Error: {changePasswordResponse.Message}",
                        DialogIcon.Error,
                        DialogOptions.Ok);
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show($@"Error: {ex.Message}",
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
