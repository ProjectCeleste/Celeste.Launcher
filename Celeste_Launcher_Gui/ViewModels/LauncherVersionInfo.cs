using System;
using System.ComponentModel;

namespace Celeste_Launcher_Gui.ViewModels
{
    public class LauncherVersionInfo : INotifyPropertyChanged
    {
        private string _changeLog;
        private string _newVersion;
        private string _currentVersion;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ChangeLog {
            get => _changeLog;
            set
            {
                _changeLog = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChangeLog)));
            }
        }

        public string NewVersion
        {
            get => _newVersion;
            set
            {
                _newVersion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewVersion)));
            }
        }

        public string CurrentVersion
        {
            get => _currentVersion;
            set
            {
                _currentVersion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentVersion)));
            }
        }
    }
}
