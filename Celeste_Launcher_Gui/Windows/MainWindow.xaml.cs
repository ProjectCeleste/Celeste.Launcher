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

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            if (LegacyBootstrapper.UserConfig?.LoginInfo.AutoLogin == true)
            {
                NavigationFrame.IsEnabled = false;
                try
                {
                    var response = await LegacyBootstrapper.WebSocketApi.DoLogin(LegacyBootstrapper.UserConfig.LoginInfo.Email,
                        LegacyBootstrapper.UserConfig.LoginInfo.Password);

                    if (response.Result)
                    {
                        LegacyBootstrapper.CurrentUser = response.User;
                        NavigationFrame.Navigate(new Uri("Pages/OverviewPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        GenericMessageDialog.Show($@"Could not perform auto-signin, please sign in again manually", DialogIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    GenericMessageDialog.Show($@"Could not perform auto-signin: {ex.Message}", DialogIcon.Error);
                }

                NavigationFrame.IsEnabled = true;
            }
        }
    }
}
