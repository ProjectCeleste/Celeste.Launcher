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
    /// Interaction logic for ResetPasswordDialog.xaml
    /// </summary>
    public partial class ResetPasswordDialog : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

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
            if (!Misc.IsValidEmailAdress(EmailAddressField.InputContent))
            {
                GenericMessageDialog.Show(Properties.Resources.ResetPasswordInvalidEmail, DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            if (ResetKeyField.InputContent.Length != 32)
            {
                GenericMessageDialog.Show(Properties.Resources.ResetPasswordInvalidKey, DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoResetPwd(EmailAddressField.InputContent, ResetKeyField.InputContent);

                if (response.Result)
                {
                    GenericMessageDialog.Show(response.GetLocalizedMessage(), DialogIcon.None, DialogOptions.Ok);

                    DialogResult = true;
                    Close();
                    return;
                }

                GenericMessageDialog.Show(response.GetLocalizedMessage(), DialogIcon.Error, DialogOptions.Ok);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.ResetPasswordError, DialogIcon.Error, DialogOptions.Ok);
            }

            IsEnabled = true;
        }

        private async void OnSendResetKeyClick(object sender, RoutedEventArgs e)
        {
            if (!Misc.IsValidEmailAdress(EmailAddressField.InputContent))
            {
                GenericMessageDialog.Show(Properties.Resources.ResetPasswordInvalidEmail, DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoForgotPwd(EmailAddressField.InputContent);

                if (response.Result)
                {
                    ResetKeyField.IsEnabled = true;
                    ResetPasswordBtn.IsEnabled = true;

                    GenericMessageDialog.Show(response.GetLocalizedMessage(), DialogIcon.None, DialogOptions.Ok);
                }
                else
                {
                    GenericMessageDialog.Show(response.GetLocalizedMessage(), DialogIcon.Error, DialogOptions.Ok);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.ResetPasswordError, DialogIcon.Error, DialogOptions.Ok);
            }

            IsEnabled = true;
        }
    }
}
