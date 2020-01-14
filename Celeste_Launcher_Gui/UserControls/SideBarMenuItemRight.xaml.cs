#region Using directives

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#endregion Using directives

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    ///     Interaction logic for SideBarMenuItemRight.xaml
    /// </summary>
    public partial class SideBarMenuItemRight : UserControl
    {
        public string LabelContents
        {
            get => (string)GetValue(LabelContentsProperty);
            set => SetValue(LabelContentsProperty, value);
        }

        public static readonly DependencyProperty LabelContentsProperty =
            DependencyProperty.Register("LabelContents", typeof(string), typeof(SideBarMenuItemRight),
                new PropertyMetadata(default(string)));

        public string DefaultIcon
        {
            get => (string)GetValue(DefaultIconProperty);
            set => SetValue(DefaultIconProperty, value);
        }

        public static readonly DependencyProperty DefaultIconProperty =
            DependencyProperty.Register("DefaultIcon", typeof(string), typeof(SideBarMenuItemRight),
                new PropertyMetadata(default(string)));

        public string HoverIcon
        {
            get => (string)GetValue(HoverIconProperty);
            set => SetValue(HoverIconProperty, value);
        }

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.Register("HoverIcon", typeof(string), typeof(SideBarMenuItemRight),
                new PropertyMetadata(default(string)));

        public int LabelSize
        {
            get => (int)GetValue(LabelSizeProperty);
            set => SetValue(LabelSizeProperty, value);
        }

        public static readonly DependencyProperty LabelSizeProperty =
            DependencyProperty.Register("LabelSize", typeof(int), typeof(SideBarMenuItemRight),
                new PropertyMetadata(18));

        public RoutedEventHandler Click
        {
            get => (RoutedEventHandler)GetValue(ClickProperty);
            set => SetValue(ClickProperty, value);
        }

        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.Register("Click", typeof(RoutedEventHandler), typeof(SideBarMenuItemRight),
                new PropertyMetadata(default(RoutedEventHandler)));

        public SideBarMenuItemRight()
        {
            InitializeComponent();
            SideBarGrid.DataContext = this;
        }

        private void SideBarGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Click?.Invoke(sender, e);
        }
    }
}