using Celeste_Launcher_Gui.Account;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.Windows;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
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
        public LoginPage()
        {
            InitializeComponent();

            var storedCredentials = UserCredentialService.GetStoredUserCredentials();

            if (LegacyBootstrapper.UserConfig?.LoginInfo?.RememberMe == true && storedCredentials != null)
            {
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

                if (storedCredentials != null && (RememberPasswordOption.IsChecked ?? false))
                {
                    loginResult = await PerformLogin(storedCredentials.Email, storedCredentials.Password);
                }
                else
                {
                    loginResult = await PerformLogin(EmailInputField.InputContent, PasswordInputField.PasswordInputBox.SecurePassword);
                }

                if (loginResult.Result)
                {
                    LegacyBootstrapper.UserConfig.LoginInfo.RememberMe = RememberPasswordOption.IsChecked ?? false;
                    LegacyBootstrapper.UserConfig.LoginInfo.AutoLogin = AutoLoginOption.IsChecked ?? false;

                    if (LegacyBootstrapper.UserConfig.LoginInfo.RememberMe && storedCredentials == null)
                    {
                        UserCredentialService.StoreCredential(EmailInputField.InputContent, PasswordInputField.PasswordInputBox.SecurePassword);
                    }

                    LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);

                    LegacyBootstrapper.CurrentUser = loginResult.User;

                    NavigationService.Navigate(new Uri("Pages/OverviewPage.xaml", UriKind.Relative));
                }
                else
                {
                    GenericMessageDialog.Show($@"Error: {loginResult.Message}", DialogIcon.Error, DialogOptions.Ok);
                    PasswordInputField.PasswordInputBox.Clear();
                    UserCredentialService.ClearVault();
                }
            }
            catch (Exception ex)
            {
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

        private void EnableRememberMe(object sender, RoutedEventArgs e)
        {
            AutoLoginOption.IsEnabled = true;
        }

        private void DisableRememberMeOption(object sender, RoutedEventArgs e)
        {
            AutoLoginOption.IsEnabled = false;
            AutoLoginOption.IsChecked = false;
        }
    }
}
