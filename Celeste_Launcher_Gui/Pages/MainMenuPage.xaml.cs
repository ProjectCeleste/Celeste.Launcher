﻿using Celeste_Launcher_Gui.Forms;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        #region Navigation
        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/RegisterPage.xaml", UriKind.Relative));
        }

        private async void OnPlayOfflineClick(object sender, RoutedEventArgs e)
        {
            PlayOfflineBtn.IsEnabled = false;
            await GameService.StartGame(true);
            PlayOfflineBtn.IsEnabled = true;
        }
        #endregion

        #region Links
        private void OnCelesteClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://projectceleste.com");
        }

        private void OnDiscordClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.gg/xXFUvWA");
        }

        private void OnRedditClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.reddit.com/r/projectceleste/");
        }

        private void OnYoutubeClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UC5gS69ffCAeqvJrqWvlnZgg");
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            SettingsButton.ContextMenu.PlacementTarget = sender as UIElement;
            SettingsButton.ContextMenu.IsOpen = true;
        }

        private void OnDonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/donations/project-celeste-donations.2/campaign");
        }
        #endregion

        #region Settings
        private void OpenWindowsFeatures(object sender, RoutedEventArgs e)
        {
            var osInfo = OsVersionInfo.GetOsVersionInfo();
            if (osInfo.Major < 6 || osInfo.Major == 6 && osInfo.Minor < 2)
            {
                GenericMessageDialog.Show("Only for Windows 8 and more\r\n" +
                    $"Your current OS is {osInfo.FullName}",
                    DialogIcon.Warning);

                return;
            }

            new WindowsFeatureHelper().ShowDialog();
        }

        private void EnableDiagnosticsMode(object sender, RoutedEventArgs e)
        {
            LegacyBootstrapper.UserConfig.IsDiagnosticMode = !LegacyBootstrapper.UserConfig.IsDiagnosticMode;

            if (LegacyBootstrapper.UserConfig.IsDiagnosticMode)
            {
                try
                {
                    var procdumpFileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "procdump.exe");
                    if (!File.Exists(procdumpFileName))
                    {
                        var dialog = new GenericMessageDialog("ProcDump.exe need to be installed first. Would you like to install it now?", DialogIcon.Warning, DialogOptions.YesNo);

                        var dr = dialog.ShowDialog();
                        if (dr.Value == true)
                            using (var form2 = new InstallProcDump())
                            {
                                form2.ShowDialog();
                            }
                        else
                            LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                    }
                }
                catch (Exception exception)
                {
                    LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                    GenericMessageDialog.Show($"Warning: Failed to enable \"Diagnostic Mode\". Error message: {exception.Message}", DialogIcon.Warning, DialogOptions.Ok);
                }
            }
        }

        private void OpenSteam(object sender, RoutedEventArgs e)
        {
            using (var form = new SteamForm())
            {
                form.ShowDialog();
            }
        }

        private void OpenGameLanguage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LanguageSelectionPage.xaml", UriKind.Relative));
        }

        private void OpenGameScanner(object sender, RoutedEventArgs e)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                GenericMessageDialog.Show($@"Game is still running, please close it and try again", DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            var scanner = new GamePathSelectionWindow();
            scanner.ShowDialog();
        }

        private void OpenFirewallHelper(object sender, RoutedEventArgs e)
        {
            new Windows.WindowsFirewallHelper().ShowDialog();
        }

        private void OpenLauncherUpdater(object sender, RoutedEventArgs e)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                GenericMessageDialog.Show($@"Game is still running, please close it and try again", DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            var updater = new UpdateWindow();
            updater.ShowDialog();
        }
        #endregion
    }
}
