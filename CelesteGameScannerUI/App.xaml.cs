using Celeste_Launcher_Gui;
using Celeste_Public_Api.Logging;
using Serilog;
using System;
using System.Windows;

namespace CelesteGameScannerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += (o, exArgs) =>
            {
                var logger = LoggerFactory.GetLogger();
                var ex = (Exception)exArgs.ExceptionObject;
                logger.Fatal(ex, ex.Message);
            };

            LegacyBootstrapper.LoadUserConfig();

            if (e.Args.Length >= 3)
            {
                try
                {
                    LegacyBootstrapper.UserConfig.GameFilesPath = e.Args[0];
                    if (Enum.TryParse(e.Args[1], true, out GameLanguage gamelang))
                    {
                        LegacyBootstrapper.UserConfig.GameLanguage = gamelang;
                    }
                    LegacyBootstrapper.UserConfig.IsSteamVersion = bool.Parse(e.Args[2]);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, ex.Message);
                }
            }

            LegacyBootstrapper.SetUILanguage();
        }
    }
}
