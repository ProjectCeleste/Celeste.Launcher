using Celeste_Launcher_Gui;
using System.Windows;

namespace CelesteGameScannerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LegacyBootstrapper.LoadUserConfig();
            LegacyBootstrapper.SetUILanguage();
        }
    }
}
