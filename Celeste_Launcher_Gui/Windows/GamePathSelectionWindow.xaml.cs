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
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = Properties.Resources.GamePathSelectorTitle,
                InitialDirectory = PathLocation.Text,
                Multiselect = false,
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Spartan.exe"
            };

            if (openFileDialog.ShowDialog(Window.GetWindow(this)) == true)
            {
                PathLocation.Text = openFileDialog.FileName;
            }

            Focus();
        }

        private void ScanBtnClick(object sender, RoutedEventArgs e)
        {
            string spartanDirectory = Path.GetDirectoryName(PathLocation.Text);

            if (!Directory.Exists(spartanDirectory))
            {
                GenericMessageDialog.Show(Properties.Resources.GamePathInvalidPath, DialogIcon.Error, DialogOptions.Ok);
            }
            else
            {
                Close();

                LegacyBootstrapper.UserConfig.GameFilesPath = spartanDirectory;
                LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);
                GameScannerWindow scanner = new GameScannerWindow(spartanDirectory, LegacyBootstrapper.UserConfig.IsSteamVersion)
                {
                    Owner = Owner
                };
                scanner.ShowDialog();
            }
        }
    }
}
