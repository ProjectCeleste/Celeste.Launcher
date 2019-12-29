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
    /// Interaction logic for SideBarMenuItem.xaml
    /// </summary>
    public partial class SideBarMenuItem : UserControl
    {
        public string LabelContents
        {
            get { return (string)GetValue(LabelContentsProperty); }
            set { SetValue(LabelContentsProperty, value); }
        }

        public static readonly DependencyProperty LabelContentsProperty =
            DependencyProperty.Register("LabelContents", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(default(string)));

        public string DefaultIcon
        {
            get { return (string)GetValue(DefaultIconProperty); }
            set { SetValue(DefaultIconProperty, value); }
        }

        public static readonly DependencyProperty DefaultIconProperty =
            DependencyProperty.Register("DefaultIcon", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(default(string)));

        public string HoverIcon
        {
            get { return (string)GetValue(HoverIconProperty); }
            set { SetValue(HoverIconProperty, value); }
        }

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.Register("HoverIcon", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(default(string)));

        public int LabelSize
        {
            get { return (int)GetValue(LabelSizeProperty); }
            set { SetValue(LabelSizeProperty, value); }
        }

        public static readonly DependencyProperty LabelSizeProperty =
            DependencyProperty.Register("LabelSize", typeof(int), typeof(SideBarMenuItem), new PropertyMetadata(18));

        public RoutedEventHandler Click
        {
            get { return (RoutedEventHandler)GetValue(ClickProperty); }
            set { SetValue(ClickProperty, value); }
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
