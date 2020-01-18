using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Logging;
using Serilog;
using System;
using System.Diagnostics;
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

                var currentApplicationDirectory = Path.GetDirectoryName(currentApplicationFullPath);
                if (!string.Equals(LegacyBootstrapper.UserConfig.GameFilesPath, currentApplicationDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    GenericMessageDialog.Show(Properties.Resources.SteamConverterIncorrectInstallationDirectory, DialogIcon.None, DialogOptions.Ok);
                    Close();
                    return;
                }

                Steam.ConvertToSteam(LegacyBootstrapper.UserConfig.GameFilesPath);

                GenericMessageDialog.Show(Properties.Resources.SteamConverterSuccess, DialogIcon.None, DialogOptions.Ok);

                Process.Start(Assembly.GetEntryAssembly().Location
                    .Replace(Path.GetFileName(currentApplicationFullPath), "AOEOnline.exe"));

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error, DialogOptions.Ok);
            }
        }
    }
}
