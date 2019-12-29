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
    /// Interaction logic for ShortRegisterInputField.xaml
    /// </summary>
    public partial class ShortRegisterInputField : UserControl
    {
        public static readonly DependencyProperty LabelContentProperty =
            DependencyProperty.Register("LabelContent", typeof(string), typeof(ShortRegisterInputField), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty InputContentProperty =
            DependencyProperty.Register("InputContent", typeof(string), typeof(ShortRegisterInputField), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(int), typeof(ShortRegisterInputField), new PropertyMetadata(20));

        public string LabelContent
        {
            get { return (string)GetValue(LabelContentProperty); }
            set { SetValue(LabelContentProperty, value); }
        }
        
        public string InputContent
        {
            get { return (string)GetValue(InputContentProperty); }
            set { SetValue(InputContentProperty, value); }
        }

        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        public ShortRegisterInputField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
