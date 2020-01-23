using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    /// Interaction logic for MainMenuButton.xaml
    /// </summary>
    public partial class WindowControl : UserControl
    {
        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.Register("Click", typeof(RoutedEventHandler), typeof(WindowControl), new PropertyMetadata(default(RoutedEventHandler)));

        public static readonly DependencyProperty DefaultIconProperty =
            DependencyProperty.Register("DefaultIcon", typeof(string), typeof(WindowControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.Register("HoverIcon", typeof(string), typeof(WindowControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(int), typeof(WindowControl), new PropertyMetadata(31));

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(int), typeof(WindowControl), new PropertyMetadata(32));

        public string DefaultIcon
        {
            get => (string)GetValue(DefaultIconProperty);
            set => SetValue(DefaultIconProperty, value);
        }

        public string HoverIcon
        {
            get => (string)GetValue(HoverIconProperty);
            set => SetValue(HoverIconProperty, value);
        }

        public RoutedEventHandler Click
        {
            get => (RoutedEventHandler)GetValue(ClickProperty);
            set => SetValue(ClickProperty, value);
        }

        public int IconWidth
        {
            get => (int)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        public int IconHeight
        {
            get => (int)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        public WindowControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(sender, e);
        }
    }
}
