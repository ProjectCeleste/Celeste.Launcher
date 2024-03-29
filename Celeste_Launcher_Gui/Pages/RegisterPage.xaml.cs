﻿using Celeste_Launcher_Gui.Extensions;
using Celeste_Launcher_Gui.Windows;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logging;
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
            if (!Misc.IsValidEmailAddress(EmailField.InputContent))
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterInvalidEmail, DialogIcon.Error);
                return;
            }

            EmailInputGroup.IsEnabled = false;
            ResentKeyBtn.IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoValidMail(EmailField.InputContent);

                if (response.Result)
                {
                    GenericMessageDialog.Show(response.GetLocalizedMessage(), DialogIcon.Warning);
                    UserInformationInputGroup.IsEnabled = true;
                    EmailInputGroup.IsEnabled = false;
                }
                else
                {
                    GenericMessageDialog.Show(response.GetLocalizedMessage(), DialogIcon.Error);
                    EmailInputGroup.IsEnabled = true;
                    ResentKeyBtn.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
            }
        }

        private async void OnRegister(object sender, RoutedEventArgs args)
        {
            if (ConfirmPasswordField.PasswordInputBox.Password != PasswordField.PasswordInputBox.Password)
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterPasswordMismatch, DialogIcon.Error);
                return;
            }

            if (!Misc.IsValidEmailAddress(EmailField.InputContent))
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterInvalidEmail, DialogIcon.Error);
                return;
            }

            if (!Misc.IsValidUserName(UsernameField.InputContent))
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterInvalidUsername, DialogIcon.Error);
                return;
            }

            if (PasswordField.PasswordInputBox.Password.Length < 8 || PasswordField.PasswordInputBox.Password.Length > 32)
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterInvalidPasswordLength, DialogIcon.Error);
                return;
            }

            if (!Misc.IsValidPassword(PasswordField.PasswordInputBox.Password))
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterInvalidPasswordLength, DialogIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(VerifyKeyField.InputContent) || VerifyKeyField.InputContent.Length != 32)
            {
                GenericMessageDialog.Show(Properties.Resources.RegisterInvalidKeyLength, DialogIcon.Error);
                return;
            }

            RegisterBtn.IsEnabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoRegister(EmailField.InputContent, VerifyKeyField.InputContent, 
                    UsernameField.InputContent, PasswordField.PasswordInputBox.Password);

                if (response.Result)
                {
                    GenericMessageDialog.Show(response.GetLocalizedMessage());

                    NavigationService.Navigate(new Uri("Pages/MainMenuPage.xaml", UriKind.Relative));
                }
                else
                {
                    GenericMessageDialog.Show(response.GetLocalizedMessage(), DialogIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
            }

            RegisterBtn.IsEnabled = true;
        }

        private void OnAbort(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
