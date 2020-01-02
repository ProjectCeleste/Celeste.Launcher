using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Logging;
using Serilog;
using System;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for ProcDumpInstallerxaml.xaml
    /// </summary>
    public partial class ProcDumpInstaller : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public ProcDumpInstaller()
        {
            InitializeComponent();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var progress = new Progress<int>();
                progress.ProgressChanged += (s, o) => { ProgressIndicator.ProgressBar.Value = o; };

                await ProcDump.DoDownloadAndInstallProcDump(progress);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, exception.Message);
                GenericMessageDialog.Show($@"Error: {exception.Message}", DialogIcon.Error, DialogOptions.Ok);
            }
            finally
            {
                Close();
            }
        }
    }
}
