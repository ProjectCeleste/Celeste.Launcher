using Celeste_Launcher_Gui;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Windows;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
            if (e.Args.Length < 1)
            {
                MessageBox.Show("Missing argument to convert application to Steam game");
                Current.Shutdown(1);
                return;
            }

            var originProcess = e.Args[0];

            LegacyBootstrapper.LoadUserConfig();
            LegacyBootstrapper.SetUILanguage();
            Steam.ConvertToSteam(LegacyBootstrapper.UserConfig.GameFilesPath);

            GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.SteamConverterSuccess, DialogIcon.None, DialogOptions.Ok);

            Process.Start(Assembly.GetEntryAssembly().Location
                .Replace(Path.GetFileName(originProcess), "AOEOnline.exe"));

            Current.Shutdown();
        }
    }
}
