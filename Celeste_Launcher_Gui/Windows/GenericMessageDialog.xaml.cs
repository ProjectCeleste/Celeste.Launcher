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
        public string LogFilePath { get; set; }

        public GenericMessageDialog(string message, DialogIcon icon = DialogIcon.None, DialogOptions option = DialogOptions.Ok, string logFilePath = null)
        {
            InitializeComponent();
            SystemSounds.Beep.Play();
            DataContext = this;

            Message = message;

            if (icon == DialogIcon.Error)
                IconSource = "pack://application:,,,/Celeste Launcher;component/Resources/Error.png";
            else if (icon == DialogIcon.Warning)
                IconSource = "pack://application:,,,/Celeste Launcher;component/Resources/Warning.png";
            else
                DialogIconContents.Visibility = Visibility.Collapsed;

            if (option == DialogOptions.Ok)
            {
                YesNoOptions.Visibility = Visibility.Collapsed;
                OkOptions.Visibility = Visibility.Visible;
                LogOptions.Visibility = Visibility.Collapsed;
            }
            else if (option == DialogOptions.YesNo)
            {
                YesNoOptions.Visibility = Visibility.Visible;
                OkOptions.Visibility = Visibility.Collapsed;
                LogOptions.Visibility = Visibility.Collapsed;
            }
            else if (option == DialogOptions.ViewLog)
            {
                YesNoOptions.Visibility = Visibility.Collapsed;
                OkOptions.Visibility = Visibility.Collapsed;
                LogOptions.Visibility = Visibility.Visible;
                LogFilePath = logFilePath;
            }
        }

        public static bool? Show(string message, DialogIcon icon = DialogIcon.None, DialogOptions option = DialogOptions.Ok, string logFilePath = null)
        {
            var dialog = new GenericMessageDialog(message, icon, option, logFilePath);
            dialog.Owner = Application.Current.MainWindow;

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

        private void OpenLogClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(LogFilePath) && System.IO.File.Exists(LogFilePath))
                {
                    System.Diagnostics.Process.Start(LogFilePath);
                }
                else
                {
                    // Fallback: try to find most recent launcher log
                    var defaultLog = Celeste_Public_Api.Logging.LogHelper.FindMostRecentLogFile(
                        System.IO.Path.Combine("Logs", "launcherlog.log"));
                    if (System.IO.File.Exists(defaultLog))
                    {
                        System.Diagnostics.Process.Start(defaultLog);
                    }
                    else
                    {
                        SystemSounds.Beep.Play();
                    }
                }
            }
            catch (System.Exception ex)
            {
                // Avoid recursive error dialogs - just log and beep
                Celeste_Public_Api.Logging.LoggerFactory.GetLogger().Error(ex, "Failed to open log file");
                SystemSounds.Beep.Play();
            }
            // Don't close dialog - let user read error and review log
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
        Ok,
        ViewLog
    }
}
