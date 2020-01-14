using Celeste_Launcher_Gui.Windows;
using ProjectCeleste.Launcher.PublicApi.Logging;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Enum;
using Serilog;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void OnResendVerificationKey(object sender, RoutedEventArgs args)
        {
            OnVerifyEmail(sender, args);
        }

        private async void OnVerifyEmail(object sender, RoutedEventArgs args)
        {
            VerifyEmailBtn.IsEnabled = false;
            ResentKeyBtn.IsEnabled = false;

            try
            {
                ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.NotLogged.ValidMailResult response = await LegacyBootstrapper.WebSocketApi.DoValidMail(EmailField.InputContent);

                if (response.Result)
                {
                    GenericMessageDialog.Show($"{response.Message}", DialogIcon.Warning);
                    UserInformationInputGroup.IsEnabled = true;
                }
                else
                {
                    GenericMessageDialog.Show($"{Properties.Resources.RegisterError} {response.Message}", DialogIcon.Error);
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
                            GenericMessageDialog.Show(Properties.Resources.RegisterInvalidEmail, DialogIcon.Error);
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
                VerifyEmailBtn.IsEnabled = true;
                ResentKeyBtn.IsEnabled = true;
            }
        }

        private async void OnRegister(object sender, RoutedEventArgs args)
        {
            if (ConfirmPasswordField.PasswordInputBox.Password != PasswordField.PasswordInputBox.Password)
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterPasswordMismatch, DialogIcon.Error);
                return;
            }

            RegisterBtn.IsEnabled = false;

            try
            {
                ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.NotLogged.RegisterUserResult response = await LegacyBootstrapper.WebSocketApi.DoRegister(EmailField.InputContent, VerifyKeyField.InputContent,
                    UsernameField.InputContent, PasswordField.PasswordInputBox.Password);

                if (response.Result)
                {
                    GenericMessageDialog.Show($"{response.Message}", DialogIcon.Error);

                    NavigationService.Navigate(new Uri("Pages/MainMenuPage.xaml", UriKind.Relative));
                }
                else
                {
                    GenericMessageDialog.Show($"{Properties.Resources.RegisterError} {response.Message}", DialogIcon.Error);
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
                            GenericMessageDialog.Show(Properties.Resources.RegisterInvalidEmail, DialogIcon.Error);
                            break;
                        case CommandErrorCode.InvalidUsername:
                        case CommandErrorCode.InvalidUsernameLength:
                            GenericMessageDialog.Show(Properties.Resources.RegisterInvalidUsername, DialogIcon.Error);
                            break;
                        case CommandErrorCode.InvalidPassword:
                        case CommandErrorCode.InvalidPasswordLength:
                            GenericMessageDialog.Show(Properties.Resources.RegisterInvalidPasswordLength, DialogIcon.Error);
                            break;
                        case CommandErrorCode.InvalidVerifyKey:
                            GenericMessageDialog.Show(Properties.Resources.RegisterInvalidKeyLength, DialogIcon.Error);
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
                RegisterBtn.IsEnabled = true;
            }
        }

        private void OnAbort(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
