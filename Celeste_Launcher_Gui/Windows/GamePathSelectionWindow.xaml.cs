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
            PathLocation.Text = LegacyBootstrapper.UserConfig.GameFilesPath;
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
                Title = Properties.Resources.GamePathSelectorTitle,
                IsFolderPicker = true,
                InitialDirectory = PathLocation.Text,

                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = PathLocation.Text,
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
                GenericMessageDialog.Show(Properties.Resources.GamePathInvalidPath, DialogIcon.Error, DialogOptions.Ok);
            }
            else
            {
                Close();

                LegacyBootstrapper.UserConfig.GameFilesPath = PathLocation.Text;
                LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);
                var scanner = new GameScannerWindow(PathLocation.Text, LegacyBootstrapper.UserConfig.IsSteamVersion);
                scanner.Owner = Owner;
                scanner.ShowDialog();
            }
        }
    }
}
