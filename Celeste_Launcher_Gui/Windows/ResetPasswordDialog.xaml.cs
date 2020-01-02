using System;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for ResetPasswordDialog.xaml
    /// </summary>
    public partial class ResetPasswordDialog : Window
    {
        public ResetPasswordDialog()
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

        private async void OnResetPasswordClick(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoResetPwd(EmailAddressField.InputContent, ResetKeyField.InputContent);

                if (response.Result)
                {
                    GenericMessageDialog.Show(response.Message, DialogIcon.None, DialogOptions.Ok);

                    DialogResult = true;
                    Close();
                    return;
                }

                GenericMessageDialog.Show($@"Error: {response.Message}", DialogIcon.Error, DialogOptions.Ok);
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
            }

            IsEnabled = true;
        }

        private async void OnSendResetKeyClick(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoForgotPwd(EmailAddressField.InputContent);

                if (response.Result)
                {
                    ResetKeyField.IsEnabled = true;
                    ResetPasswordBtn.IsEnabled = true;

                    GenericMessageDialog.Show(response.Message, DialogIcon.None, DialogOptions.Ok);
                }
                else
                {
                    GenericMessageDialog.Show($@"Error: {response.Message}", DialogIcon.Error, DialogOptions.Ok);
                }
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
            }

            IsEnabled = true;
        }
    }
}
