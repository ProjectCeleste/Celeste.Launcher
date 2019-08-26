using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for GamePathSelectionWindow.xaml
    /// </summary>
    public partial class GamePathSelectionWindow : Window
    {
        public GamePathSelectionWindow()
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

        private void BrowseBtnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                Title = "Select Celeste location",
                IsFolderPicker = true,
                InitialDirectory = LegacyBootstrapper.UserConfig.GameFilesPath,

                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = LegacyBootstrapper.UserConfig.GameFilesPath,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PathLocation.Text = dialog.FileName;
            }

            Focus();
        }

        private void ScanBtnClick(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(PathLocation.Text))
            {
                GenericMessageDialog.Show($@"The selected game path does not exist", DialogIcon.Error, DialogOptions.Ok);
            }
            else
            {
                Close();

                var scanner = new GameScannerWindow(PathLocation.Text, LegacyBootstrapper.UserConfig.IsSteamVersion);
                scanner.ShowDialog();
            }
        }
    }
}
