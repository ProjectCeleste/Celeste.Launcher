using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for MultiplayerSettings.xaml
    /// </summary>
    public partial class MultiplayerSettings : Window
    {
        public MultiplayerSettings()
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
    }
}
