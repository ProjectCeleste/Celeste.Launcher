using Celeste_Launcher_Gui;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Windows;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Threading;

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
            if (e.Args.Length > 0)
            {
                if (e.Args.Contains("update"))
                {
                    Thread.Sleep(2500); // Wait for the updater to exit and Steam to notice it's closed
                    Steam.ConvertToSteam(LegacyBootstrapper.UserConfig.GameFilesPath);
                    Process.Start("steam://rungameid/105430"); //Run from Steam itself
                    Current.Shutdown();
                }
            }

            Steam.ConvertToSteam(LegacyBootstrapper.UserConfig.GameFilesPath);

            var dialog = new GenericMessageDialog(Celeste_Launcher_Gui.Properties.Resources.SteamConverterSuccess, DialogIcon.None, DialogOptions.Ok);
            dialog.ShowDialog();

            Current.Shutdown();
        }
    }
}
