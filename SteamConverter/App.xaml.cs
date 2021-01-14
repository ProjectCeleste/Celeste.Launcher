using Celeste_Launcher_Gui;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Windows;
using System.Windows;

namespace SteamConverter
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
            Steam.ConvertToSteam(LegacyBootstrapper.UserConfig.GameFilesPath);

            var dialog = new GenericMessageDialog(Celeste_Launcher_Gui.Properties.Resources.SteamConverterSuccess, DialogIcon.None, DialogOptions.Ok);
            dialog.ShowDialog();

            Current.Shutdown();
        }
    }
}
