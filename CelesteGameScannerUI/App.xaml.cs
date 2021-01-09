using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace CelesteGameScannerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string GamePath { get; set; }

        public static bool IsSteamInstallation { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length < 3)
            {
                MessageBox.Show("Process needs to be started with arguments <gamePath> <language> <isSteamInstallation>");
                Environment.Exit(1);
            }

            GamePath = e.Args[0];
            Thread.CurrentThread.CurrentCulture = new CultureInfo(e.Args[1]);
            IsSteamInstallation = bool.Parse(e.Args[2]);
        }
    }
}
