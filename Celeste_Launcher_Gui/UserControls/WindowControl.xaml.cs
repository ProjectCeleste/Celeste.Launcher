using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            get { return (string)GetValue(DefaultIconProperty); }
            set { SetValue(DefaultIconProperty, value); }
        }

        public string HoverIcon
        {
            get { return (string)GetValue(HoverIconProperty); }
            set { SetValue(HoverIconProperty, value); }
        }

        public RoutedEventHandler Click
        {
            get { return (RoutedEventHandler)GetValue(ClickProperty); }
            set { SetValue(ClickProperty, value); }
        }

        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public WindowControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(sender, e);
        }
    }
}
