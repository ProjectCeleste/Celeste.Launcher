using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void OnPlayOfflineClick(object sender, RoutedEventArgs e)
        {
            GameService.StartGame(true);
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

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LanguageSelectionPage.xaml", UriKind.Relative));
        }
        #endregion
    }
}
