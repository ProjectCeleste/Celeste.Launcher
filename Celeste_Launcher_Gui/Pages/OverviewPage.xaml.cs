using Celeste_Launcher_Gui.Account;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.Windows;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewPage : Page
    {
        public User CurrentUser => LegacyBootstrapper.CurrentUser;

        public OverviewPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnWebisteClick(object sender, RoutedEventArgs e)
        {
            new UpdateWindow().Show();
        }

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

        private void OnAccountClick(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void OnFriendsClick(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void OnDonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/donations/project-celeste-donations.2/campaign");
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            UserCredentialService.ClearVault();

            try
            {
                LegacyBootstrapper.WebSocketApi?.Disconnect();
            }
            catch
            {
                // TODO: Log exception here
            }

            //Save UserConfig
            if (LegacyBootstrapper.UserConfig?.LoginInfo != null)
            {
                LegacyBootstrapper.UserConfig.LoginInfo.AutoLogin = false;
                LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);
            }

            NavigationService.Navigate(new Uri("Pages/MainMenuPage.xaml", UriKind.Relative));
        }

        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void OnAoeodbClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://aoedb.net/");
        }

        private void OnPatchNotesClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/forums/announcements.12/");
        }

        private void OnChampionModeInfoClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://champion.projectceleste.com/greek/military");
        }
        #endregion

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            LoginBtn.IsEnabled = false;
            GameService.StartGame();
            LoginBtn.IsEnabled = true;
        }

        private void OnPlayOffline(object sender, RoutedEventArgs e)
        {
            PlayOfflineBtn.IsEnabled = false;
            GameService.StartGame();
            PlayOfflineBtn.IsEnabled = true;
        }
    }
}
