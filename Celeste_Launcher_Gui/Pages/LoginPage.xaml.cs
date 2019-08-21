﻿using Celeste_Launcher_Gui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
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

            // TODO: Rewrite how passwords are stored as this is _highly_ insecure
            if (LegacyBootstrapper.UserConfig?.LoginInfo?.RememberMe == true)
            {
                EmailInputField.InputContent = LegacyBootstrapper.UserConfig.LoginInfo.Email;
                PasswordInputField.PasswordInputBox.Password = LegacyBootstrapper.UserConfig.LoginInfo.Password;
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
                var response = await LegacyBootstrapper.WebSocketApi.DoLogin(EmailInputField.InputContent, PasswordInputField.PasswordInputBox.Password);

                if (response.Result)
                {
                    // Save UserConfig
                    if (LegacyBootstrapper.UserConfig == null)
                    {
                        LegacyBootstrapper.UserConfig = new UserConfig
                        {
                            LoginInfo = new LoginInfo()
                        };
                    }
                 
                    LegacyBootstrapper.UserConfig.LoginInfo.Email = EmailInputField.InputContent;
                    LegacyBootstrapper.UserConfig.LoginInfo.Password = PasswordInputField.PasswordInputBox.Password;
                    LegacyBootstrapper.UserConfig.LoginInfo.RememberMe = RememberPasswordOption.IsChecked ?? false;
                    LegacyBootstrapper.UserConfig.LoginInfo.AutoLogin = AutoLoginOption.IsChecked ?? false;

                    LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);

                    LegacyBootstrapper.CurrentUser = response.User;

                    NavigationService.Navigate(new Uri("Pages/OverviewPage.xaml", UriKind.Relative));
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

            LoginButton.IsEnabled = true;
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