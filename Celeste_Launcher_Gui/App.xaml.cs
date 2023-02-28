using Celeste_Public_Api.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Celeste_Public_Api.Helpers;

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

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LoggerFactory.GetLogger().Error(exception, exception.Message);
        }
    }
}
