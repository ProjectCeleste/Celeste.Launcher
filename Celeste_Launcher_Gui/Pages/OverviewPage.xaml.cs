using Celeste_Launcher_Gui.Account;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.Windows;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewPage : Page
    {
        public User CurrentUser => LegacyBootstrapper.CurrentUser;
        private NewsPicture _currentNews = NewsPicture.Default();
        private readonly IFriendService _friendService;

        public OverviewPage()
        {
            InitializeComponent();
            SetNewsPictureSource(_currentNews.ImageSource);
            DataContext = this;
            _friendService = FriendService.GetInstance();
            _friendService.FriendListUpdated += SetFriendListIcon;
        }

        private void SetFriendListIcon(Model.Friends.FriendList e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.IncomingRequests.Count > 0)
                {
                    FriendsIcon.DefaultIcon = "pack://application:,,,/Celeste Launcher;component/Resources/Icons/Friends-Alert-Normal.png";
                    FriendsIcon.HoverIcon = "pack://application:,,,/Celeste Launcher;component/Resources/Icons/Friends-Alert-Hover.png";
                }
                else
                {
                    FriendsIcon.DefaultIcon = "pack://application:,,,/Celeste Launcher;component/Resources/Icons/Friends-Normal.png";
                    FriendsIcon.HoverIcon = "pack://application:,,,/Celeste Launcher;component/Resources/Icons/Friends-Hover.png";
                }
            });
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
            AccountButton.ContextMenu.PlacementTarget = sender as UIElement;
            AccountButton.ContextMenu.IsOpen = true;
        }

        private void OnFriendsClick(object sender, RoutedEventArgs e)
        {
            FriendList.Display();
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
            SettingsButton.ContextMenu.PlacementTarget = sender as UIElement;
            SettingsButton.ContextMenu.IsOpen = true;
        }

        private void OnChangePasswordClick(object sender, RoutedEventArgs e)
        {
            var changePasswordDialog = new ChangePasswordDialog();
            changePasswordDialog.Owner = Window.GetWindow(this);
            changePasswordDialog.ShowDialog();
        }

        private void OnTwitchClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.twitch.tv/projectceleste/");
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

        #region Settings context menu callbacks
        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            LoginBtn.IsEnabled = false;
            await GameService.StartGame();
            LoginBtn.IsEnabled = true;
        }

        private async void OnPlayOffline(object sender, RoutedEventArgs e)
        {
            PlayOfflineBtn.IsEnabled = false;
            await GameService.StartGame(true);
            PlayOfflineBtn.IsEnabled = true;
        }

        private void OpenGameLanguage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LanguageSelectionPage.xaml", UriKind.Relative));
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
                        var dialog = new GenericMessageDialog(Properties.Resources.EnableDiagnosticsModeInstallProcdumpPrompt, DialogIcon.Warning, DialogOptions.YesNo);
                        dialog.Owner = Window.GetWindow(this);

                        var dr = dialog.ShowDialog();
                        if (dr.Value == true)
                        {
                            var procDumpInstallerDialog = new ProcDumpInstaller();
                            procDumpInstallerDialog.Owner = Window.GetWindow(this);
                            procDumpInstallerDialog.ShowDialog();
                        }
                        else
                            LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                    }
                }
                catch (Exception exception)
                {
                    LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                    GenericMessageDialog.Show($"{Properties.Resources.EnableDiagnosticsModeProcdumpInstallError} {exception.Message}", DialogIcon.Warning, DialogOptions.Ok);
                }
            }
        }

        private void OpenSteam(object sender, RoutedEventArgs e)
        {
            var steamConverterWindow = new SteamConverterWindow();
            steamConverterWindow.Owner = Window.GetWindow(this);
            steamConverterWindow.ShowDialog();
        }

        private void OpenWindowsFeatures(object sender, RoutedEventArgs e)
        {
            var featureHelper = new WindowsFeatureHelper();
            featureHelper.Owner = Window.GetWindow(this);
            featureHelper.ShowDialog();
        }

        private void OpenWindowsFirewall(object sender, RoutedEventArgs e)
        {
            var firewallHelper = new Windows.WindowsFirewallHelper();
            firewallHelper.Owner = Window.GetWindow(this);
            firewallHelper.ShowDialog();
        }

        private void OpenGameScanner(object sender, RoutedEventArgs e)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                GenericMessageDialog.Show(Properties.Resources.GameAlreadyRunningError, DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            var scanner = new GamePathSelectionWindow();
            scanner.Owner = Window.GetWindow(this);
            scanner.ShowDialog();
        }

        private void OpenUpdater(object sender, RoutedEventArgs e)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                GenericMessageDialog.Show(Properties.Resources.GameAlreadyRunningError, DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            var updater = new UpdateWindow();
            updater.Owner = Window.GetWindow(this);
            updater.ShowDialog();
        }
        #endregion

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await _friendService.FetchFriendList();

                var newsLoader = new NewsPictureLoader();
                _currentNews = await newsLoader.GetNewsDescription();

                SetNewsPictureSource(_currentNews.ImageSource);
            }
            catch
            {
                _currentNews = NewsPicture.Default();
            }
        }

        private void SetNewsPictureSource(string imageUri)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imageUri);
            bitmapImage.EndInit();

            NewsImage.Source = bitmapImage;
        }

        private void OpenNews(object sender, RoutedEventArgs e)
        {
            Process.Start(_currentNews.Href);
        }

        private void OpenScenarionManager(object sender, RoutedEventArgs e)
        {
            var scenarioManagerDialog = new ScenarioManager();
            scenarioManagerDialog.Owner = Window.GetWindow(this);
            scenarioManagerDialog.Show();
        }

        private void OpenMultiplayerSettings(object sender, RoutedEventArgs e)
        {
            var multiplayerSettingsDialog = new MultiplayerSettings();
            multiplayerSettingsDialog.Owner = Window.GetWindow(this);
            multiplayerSettingsDialog.ShowDialog();
        }

        private void OpenToolsButtonToolTip(object sender, RoutedEventArgs e)
        {
            ToolsButton.ContextMenu.PlacementTarget = sender as UIElement;
            ToolsButton.ContextMenu.IsOpen = true;
        }
    }
}
