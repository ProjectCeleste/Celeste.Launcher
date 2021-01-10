using Celeste_Launcher_Gui;
using Celeste_Public_Api.Logging;
using System;
using System.Windows;

namespace CelesteWindowsFirewallHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += (o, exArgs) =>
            {
                var logger = LoggerFactory.GetLogger();
                var ex = (Exception)exArgs.ExceptionObject;
                logger.Fatal(ex, ex.Message);
            };

            LegacyBootstrapper.LoadUserConfig();
            LegacyBootstrapper.SetUILanguage();
        }
    }
}
