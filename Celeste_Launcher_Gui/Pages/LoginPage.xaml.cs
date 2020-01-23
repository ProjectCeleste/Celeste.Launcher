using Celeste_Launcher_Gui.Account;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.Windows;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logging;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
using Serilog;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly ILogger _logger = LoggerFactory.GetLogger();

        public LoginPage()
        {
            InitializeComponent();

            var storedCredentials = UserCredentialService.GetStoredUserCredentials();

            if (!LegacyBootstrapper.UserConfig.LoginInfo.RememberMe || storedCredentials == null)
                return;

            _logger.Information("User has already stored credentials before");
            EmailInputField.InputContent = storedCredentials.Email;
            PasswordInputField.PasswordInputBox.Password = "**********";
            RememberPasswordOption.IsChecked = true;
            AutoLoginOption.IsEnabled = true;
        }

        private void OnAbortLoginClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void PerformLogin(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            try
            {
                var storedCredentials = UserCredentialService.GetStoredUserCredentials();
                LoginResult loginResult;

                _logger.Information("Stored credentials is null: {@IsNull}", storedCredentials == null);

                if (storedCredentials != null && (RememberPasswordOption.IsChecked ?? false))
                {
                    _logger.Information("Performing login with stored credentials");
                    loginResult = await PerformLogin(storedCredentials.Email, storedCredentials.Password);
                }
                else
                {
                    _logger.Information("Performing login with entered credentials");

                    var password = PasswordInputField.PasswordInputBox.SecurePassword;
                    var email = EmailInputField.InputContent;

                    if (password.Length < 8)
                    {
                        GenericMessageDialog.Show($"{Properties.Resources.LoginTooShortPassword}", DialogIcon.Error);
                        return;
                    }

                    if (password.Length > 32)
                    {
                        GenericMessageDialog.Show($"{Properties.Resources.LoginTooLongPassword}", DialogIcon.Error);
                        return;
                    }

                    if (!Misc.IsValidEmailAdress(email))
                    {
                        GenericMessageDialog.Show($"{Properties.Resources.LoginBadEmail}", DialogIcon.Error);
                        return;
                    }

                    loginResult = await PerformLogin(email, password);
                }

                if (loginResult.Result)
                {
                    _logger.Information("Login succeeded");
                    LegacyBootstrapper.UserConfig.LoginInfo.RememberMe = RememberPasswordOption.IsChecked ?? false;
                    LegacyBootstrapper.UserConfig.LoginInfo.AutoLogin = AutoLoginOption.IsChecked ?? false;

                    if (LegacyBootstrapper.UserConfig.LoginInfo.RememberMe && storedCredentials == null)
                    {
                        _logger.Information("User has selected to store new credentials");
                        UserCredentialService.StoreCredential(EmailInputField.InputContent, PasswordInputField.PasswordInputBox.SecurePassword);
                    }

                    LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);

                    LegacyBootstrapper.CurrentUser = loginResult.User;

                    NavigationService.Navigate(new Uri("Pages/OverviewPage.xaml", UriKind.Relative));
                }
                else
                {
                    _logger.Information("Failed signing in because {@Message}", loginResult.Message);
                    GenericMessageDialog.Show($"{Properties.Resources.LoginErrorMessage} {loginResult.Message}", DialogIcon.Error);
                    PasswordInputField.PasswordInputBox.Clear();
                    UserCredentialService.ClearVault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
                PasswordInputField.PasswordInputBox.Clear();
                UserCredentialService.ClearVault();
            }
            finally
            {
                LoginButton.IsEnabled = true;
            }
        }

        private static async Task<LoginResult> PerformLogin(string email, SecureString password)
        {
            GameService.SetCredentials(email, password);

            return await LegacyBootstrapper.WebSocketApi.DoLogin(email, password);
        }

        private void ForgottenPasswordClick(object sender, RoutedEventArgs e)
        {
            var resetPasswordDialog = new ResetPasswordDialog
            {
                Owner = Window.GetWindow(this)
            };
            resetPasswordDialog.ShowDialog();
        }

        private void DisableRememberMeOption(object sender, RoutedEventArgs e)
        {
            AutoLoginOption.IsChecked = false;
        }

        private void AutoLoginOption_Checked(object sender, RoutedEventArgs e)
        {
            RememberPasswordOption.IsChecked = true;
        }
    }
}
