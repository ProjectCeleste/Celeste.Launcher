using Celeste_Launcher_Gui.Win32;
using Celeste_Public_Api.Logging;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for SteamConverterWindow.xaml
    /// </summary>
    public partial class SteamConverterWindow : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public SteamConverterWindow()
        {
            InitializeComponent();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentApplicationFullPath = Assembly.GetEntryAssembly().Location;
                if (currentApplicationFullPath.EndsWith("AOEOnline.exe", StringComparison.OrdinalIgnoreCase))
                {
                    GenericMessageDialog.Show(Properties.Resources.SteamConverterAlreadySteamGame, DialogIcon.None, DialogOptions.Ok);
                    Close();
                    return;
                }

                var currentWorkingDirectory = Path.GetDirectoryName(currentApplicationFullPath);

                if (!File.Exists($"{currentWorkingDirectory}\\Spartan.exe"))
                {
                    GenericMessageDialog.Show(Properties.Resources.SteamConverterIncorrectInstallationDirectory, DialogIcon.None, DialogOptions.Ok);
                    Close();
                    return;
                }

                LegacyBootstrapper.UserConfig.GameFilesPath = currentWorkingDirectory;
                LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);

                var aoeoOnlineExePath = Path.Combine(currentWorkingDirectory, "AOEOnline.exe");
                var converterNeedsAdmin = !IsWritableDirectory(aoeoOnlineExePath);

                ProcesInvoker.StartNewProcessAsDialog("SteamConverter.exe", runAsElevated: converterNeedsAdmin);
                ProcesInvoker.StartNewProcess(aoeoOnlineExePath);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error, DialogOptions.OkWithLog, LogHelper.GetLogFilePath());
            }
        }

        private static bool IsWritableDirectory(string path)
        {
            try
            {
                using var _ = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
