#region Using directives

using Celeste_Launcher_Gui.Account;
using Celeste_Launcher_Gui.Services;
using ProjectCeleste.Launcher.PublicApi.Logging;
using Serilog;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#endregion Using directives

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ContentMoved(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
        }

        private void OnMinimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OpenLegalTermsWebsite(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.xbox.com/en-us/developers/rules");
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var savedCredentials = UserCredentialService.GetStoredUserCredentials();

            if (LegacyBootstrapper.UserConfig?.LoginInfo.AutoLogin == true && savedCredentials != null)
            {
                NavigationFrame.IsEnabled = false;
                try
                {
                    var response =
                        await LegacyBootstrapper.WebSocketApi.DoLogin(savedCredentials.Email,
                            savedCredentials.Password);

                    if (response.Result)
                    {
                        GameService.SetCredentials(savedCredentials.Email, savedCredentials.Password);
                        LegacyBootstrapper.CurrentUser = response.User;
                        NavigationFrame.Navigate(new Uri("Pages/OverviewPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        GenericMessageDialog.Show(Properties.Resources.AutoLoginFailed, DialogIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, ex.Message);
                    GenericMessageDialog.Show(Properties.Resources.AutoLoginError, DialogIcon.Error);
                }

                NavigationFrame.IsEnabled = true;
            }
        }
    }
}