using Microsoft.Win32;
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
            var openFileDialog = new OpenFileDialog
            {
                Title = Properties.Resources.GamePathSelectorTitle,
                InitialDirectory = PathLocation.Text,
                Multiselect = false,
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Spartan.exe"
            };

            if (openFileDialog.ShowDialog(GetWindow(this)) == true)
            {
                PathLocation.Text = openFileDialog.FileName;
            }

            Focus();
        }

        private void ScanBtnClick(object sender, RoutedEventArgs e)
        {
            var spartanDirectory = Directory.Exists(PathLocation.Text) ? PathLocation.Text : Path.GetDirectoryName(PathLocation.Text);

            if (!Directory.Exists(spartanDirectory))
            {
                GenericMessageDialog.Show(Properties.Resources.GamePathInvalidPath, DialogIcon.Error);
            }
            else
            {
                Close();

                LegacyBootstrapper.UserConfig.GameFilesPath = spartanDirectory;
                LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);
                var scanner =
                    new GameScannerWindow(spartanDirectory, LegacyBootstrapper.UserConfig.IsSteamVersion)
                    {
                        Owner = Owner
                    };
                scanner.ShowDialog();
            }
        }
    }
}
