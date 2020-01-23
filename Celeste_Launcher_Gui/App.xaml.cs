using Celeste_Public_Api.Logging;
using System;
using System.Windows;

namespace Celeste_Launcher_Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // TODO: Invoke legacy main function from here
        public App()
        {
            LegacyBootstrapper.InitializeLegacyComponents();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LoggerFactory.GetLogger().Error(exception, exception.Message);
        }
    }
}
