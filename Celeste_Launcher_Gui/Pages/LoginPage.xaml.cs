using Celeste_Launcher_Gui.Account;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.Windows;
using Celeste_Public_Api.Logging;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private ILogger _logger = LoggerFactory.GetLogger();

        public LoginPage()
        {
            InitializeComponent();

            var storedCredentials = UserCredentialService.GetStoredUserCredentials();

            if (LegacyBootstrapper.UserConfig.LoginInfo.RememberMe == true && storedCredentials != null)
            {
                _logger.Information("User has already stored credentials before");
                EmailInputField.InputContent = storedCredentials.Email;
                PasswordInputField.PasswordInputBox.Password = "**********";
                RememberPasswordOption.IsChecked = true;
                AutoLoginOption.IsEnabled = true;
            }
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

                _logger.Information("Stored credentials is null: {@IsNull}", (storedCredentials == null));

                if (storedCredentials != null && (RememberPasswordOption.IsChecked ?? false))
                {
                    _logger.Information("Performing login with stored credentials");
                    loginResult = await PerformLogin(storedCredentials.Email, storedCredentials.Password);
                }
                else
                {
                    _logger.Information("Performing login with entered credentials");
                    loginResult = await PerformLogin(EmailInputField.InputContent, PasswordInputField.PasswordInputBox.SecurePassword);
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
                    GenericMessageDialog.Show($@"Error: {loginResult.Message}", DialogIcon.Error, DialogOptions.Ok);
                    PasswordInputField.PasswordInputBox.Clear();
                    UserCredentialService.ClearVault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
                PasswordInputField.PasswordInputBox.Clear();
                UserCredentialService.ClearVault();
            }

            LoginButton.IsEnabled = true;
        }

        private async Task<LoginResult> PerformLogin(string email, SecureString password)
        {
            GameService.SetCredentials(email, password);
            return await LegacyBootstrapper.WebSocketApi.DoLogin(email, password);
        }

        private void ForgottenPasswordClick(object sender, RoutedEventArgs e)
        {
            using (var form = new Forms.ResetPwdForm())
            {
                form.ShowDialog();
            }
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
