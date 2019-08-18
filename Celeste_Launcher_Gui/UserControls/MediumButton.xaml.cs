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
    /// Interaction logic for MediumButton.xaml
    /// </summary>
    public partial class MediumButton : UserControl
    {
        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.Register("Click", typeof(RoutedEventHandler), typeof(MediumButton), new PropertyMetadata(default(RoutedEventHandler)));

        public static readonly DependencyProperty IsDefaultProperty =
            DependencyProperty.Register("IsDefault", typeof(bool), typeof(MediumButton), new PropertyMetadata(false));

        public RoutedEventHandler Click
        {
            get { return (RoutedEventHandler)GetValue(ClickProperty); }
            set { SetValue(ClickProperty, value); }
        }

        public bool IsDefault
        {
            get { return (bool)GetValue(IsDefaultProperty); }
            set { SetValue(IsDefaultProperty, value); }
        }

        public MediumButton()
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
