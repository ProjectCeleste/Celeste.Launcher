﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    /// Interaction logic for VersionNumberLabel.xaml
    /// </summary>
    public partial class VersionNumberLabel : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty LabelContentsProperty =
            DependencyProperty.Register("LabelContents", typeof(string), typeof(VersionNumberLabel), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty VersionNumberProperty =
            DependencyProperty.Register("VersionNumber", typeof(string), typeof(VersionNumberLabel), new PropertyMetadata(default(string)));

        public event PropertyChangedEventHandler PropertyChanged;

        public string LabelContents
        {
            get { return (string)GetValue(LabelContentsProperty); }
            set
            {
                SetValue(LabelContentsProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LabelContents)));
            }
        }

        public string VersionNumber
        {
            get { return (string)GetValue(VersionNumberProperty); }
            set
            {
                SetValue(VersionNumberProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VersionNumber)));
            }
        }

        public VersionNumberLabel()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
