using System.Media;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for YesNoDialog.xaml
    /// </summary>
    public partial class GenericMessageDialog : Window
    {
        public string Message { get; set; }
        public string IconSource { get; set; }

        public GenericMessageDialog(string message, DialogIcon icon = DialogIcon.None, DialogOptions option = DialogOptions.Ok)
        {
            InitializeComponent();
            SystemSounds.Beep.Play();
            DataContext = this;

            Message = message;

            if (icon == DialogIcon.Error)
                IconSource = "pack://application:,,,/Celeste_Launcher_Gui;component/Resources/Error.png";
            else if (icon == DialogIcon.Warning)
                IconSource = "pack://application:,,,/Celeste_Launcher_Gui;component/Resources/Warning.png";
            else
                DialogIconContents.Visibility = Visibility.Collapsed;

            if (option == DialogOptions.Ok)
            {
                YesNoOptions.Visibility = Visibility.Collapsed;
                OkOptions.Visibility = Visibility.Visible;
            }
            else if (option == DialogOptions.YesNo)
            {
                YesNoOptions.Visibility = Visibility.Visible;
                OkOptions.Visibility = Visibility.Collapsed;
            }
        }

        public static bool? Show(string message, DialogIcon icon = DialogIcon.None, DialogOptions option = DialogOptions.Ok)
        {
            var dialog = new GenericMessageDialog(message, icon, option);
            return dialog.ShowDialog();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ContentMoved(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void YesClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    public enum DialogIcon
    {
        None,
        Warning,
        Error
    }

    public enum DialogOptions
    {
        YesNo,
        Ok
    }
}
