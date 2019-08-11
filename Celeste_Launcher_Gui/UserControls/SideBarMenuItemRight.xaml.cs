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
    /// Interaction logic for SideBarMenuItemRight.xaml
    /// </summary>
    public partial class SideBarMenuItemRight : UserControl
    {
        public string LabelContents
        {
            get { return (string)GetValue(LabelContentsProperty); }
            set { SetValue(LabelContentsProperty, value); }
        }

        public static readonly DependencyProperty LabelContentsProperty =
            DependencyProperty.Register("LabelContents", typeof(string), typeof(SideBarMenuItemRight), new PropertyMetadata(default(string)));

        public string DefaultIcon
        {
            get { return (string)GetValue(DefaultIconProperty); }
            set { SetValue(DefaultIconProperty, value); }
        }

        public static readonly DependencyProperty DefaultIconProperty =
            DependencyProperty.Register("DefaultIcon", typeof(string), typeof(SideBarMenuItemRight), new PropertyMetadata(default(string)));

        public string HoverIcon
        {
            get { return (string)GetValue(HoverIconProperty); }
            set { SetValue(HoverIconProperty, value); }
        }

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.Register("HoverIcon", typeof(string), typeof(SideBarMenuItemRight), new PropertyMetadata(default(string)));

        public int LabelSize
        {
            get { return (int)GetValue(LabelSizeProperty); }
            set { SetValue(LabelSizeProperty, value); }
        }

        public static readonly DependencyProperty LabelSizeProperty =
            DependencyProperty.Register("LabelSize", typeof(int), typeof(SideBarMenuItemRight), new PropertyMetadata(18));
        
        public RoutedEventHandler Click
        {
            get { return (RoutedEventHandler)GetValue(ClickProperty); }
            set { SetValue(ClickProperty, value); }
        }

        public static readonly DependencyProperty ClickProperty =
           DependencyProperty.Register("Click", typeof(RoutedEventHandler), typeof(SideBarMenuItemRight), new PropertyMetadata(default(RoutedEventHandler)));


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
