using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    /// Interaction logic for SideBarMenuItem.xaml
    /// </summary>
    public partial class SideBarMenuItem : UserControl
    {
        public string LabelContents
        {
            get => (string)GetValue(LabelContentsProperty);
            set => SetValue(LabelContentsProperty, value);
        }

        public static readonly DependencyProperty LabelContentsProperty =
            DependencyProperty.Register("LabelContents", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(default(string)));

        public string DefaultIcon
        {
            get => (string)GetValue(DefaultIconProperty);
            set => SetValue(DefaultIconProperty, value);
        }

        public static readonly DependencyProperty DefaultIconProperty =
            DependencyProperty.Register("DefaultIcon", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(default(string)));

        public string HoverIcon
        {
            get => (string)GetValue(HoverIconProperty);
            set => SetValue(HoverIconProperty, value);
        }

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.Register("HoverIcon", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(default(string)));

        public int LabelSize
        {
            get => (int)GetValue(LabelSizeProperty);
            set => SetValue(LabelSizeProperty, value);
        }

        public static readonly DependencyProperty LabelSizeProperty =
            DependencyProperty.Register("LabelSize", typeof(int), typeof(SideBarMenuItem), new PropertyMetadata(18));

        public RoutedEventHandler Click
        {
            get => (RoutedEventHandler)GetValue(ClickProperty);
            set => SetValue(ClickProperty, value);
        }

        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.Register("Click", typeof(RoutedEventHandler), typeof(SideBarMenuItem), new PropertyMetadata(default(RoutedEventHandler)));

        public SideBarMenuItem()
        {
            InitializeComponent();
            SideBarGrid.DataContext = this;
        }

        private void SideBarGrid_MouseDown(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(sender, e);
        }
    }
}
