using Celeste_Launcher_Gui.Helpers;
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
                var exePath = Assembly.GetEntryAssembly().Location;
                if (exePath.EndsWith("AOEOnline.exe", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Celeste Fan Project Launcher is already compatible with \"Steam\".");

                var exeFolder = Path.GetDirectoryName(exePath);
                if (!string.Equals(LegacyBootstrapper.UserConfig.GameFilesPath, exeFolder, StringComparison.OrdinalIgnoreCase))
                    throw new Exception(
                        "Celeste Fan Project Launcher need to be installed in the same folder has the game.");

                Steam.ConvertToSteam(LegacyBootstrapper.UserConfig.GameFilesPath);

                GenericMessageDialog.Show(@"Celeste Fan Project Launcher is now compatible with Steam. It will now re-start.", DialogIcon.None, DialogOptions.Ok);

                Process.Start(Assembly.GetEntryAssembly().Location
                    .Replace("Celeste_Launcher_Gui.exe", "AOEOnline.exe"));

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
            }
        }
    }
}
