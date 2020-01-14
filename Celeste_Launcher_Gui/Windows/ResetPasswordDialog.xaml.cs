using ProjectCeleste.Launcher.PublicApi.Logging;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Enum;
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
            IsEnabled = false;

            try
            {
                ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.NotLogged.ResetPwdResult response = await LegacyBootstrapper.WebSocketApi.DoResetPwd(EmailAddressField.InputContent, ResetKeyField.InputContent);

                if (response.Result)
                {
                    GenericMessageDialog.Show(response.Message, DialogIcon.None, DialogOptions.Ok);

                    DialogResult = true;
                    Close();
                    return;
                }

                GenericMessageDialog.Show($"{Properties.Resources.ResetPasswordFailed} {response.Message}", DialogIcon.Error, DialogOptions.Ok);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                if (ex.Data.Contains("ErrorCode") && ex.Data["ErrorCode"] is CommandErrorCode errorCode)
                {
                    switch (errorCode)
                    {
                        case CommandErrorCode.InvalidEmail:
                            GenericMessageDialog.Show(Properties.Resources.ResetPasswordInvalidEmail, DialogIcon.Error);
                            break;
                        case CommandErrorCode.InvalidVerifyKey:
                            GenericMessageDialog.Show(Properties.Resources.ResetPasswordInvalidKey, DialogIcon.Error);
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

        private async void OnSendResetKeyClick(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            try
            {
                ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.NotLogged.ForgotPwdResult response = await LegacyBootstrapper.WebSocketApi.DoForgotPwd(EmailAddressField.InputContent);

                if (response.Result)
                {
                    ResetKeyField.IsEnabled = true;
                    ResetPasswordBtn.IsEnabled = true;

                    GenericMessageDialog.Show(response.Message, DialogIcon.None, DialogOptions.Ok);
                }
                else
                {
                    GenericMessageDialog.Show($"{Properties.Resources.ResetPasswordFailed} {response.Message}", DialogIcon.Error, DialogOptions.Ok);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                if (ex.Data.Contains("ErrorCode") && ex.Data["ErrorCode"] is CommandErrorCode errorCode)
                {
                    switch (errorCode)
                    {
                        case CommandErrorCode.InvalidEmail:
                            GenericMessageDialog.Show(Properties.Resources.ResetPasswordInvalidEmail, DialogIcon.Error);
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
