using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    /// Interaction logic for ShortRegisterInputField.xaml
    /// </summary>
    public partial class ShortRegisterPasswordInputField : UserControl
    {
        public static readonly DependencyProperty LabelContentProperty =
            DependencyProperty.Register("LabelContent", typeof(string), typeof(ShortRegisterPasswordInputField), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(int), typeof(ShortRegisterPasswordInputField), new PropertyMetadata(20));

        public int LabelFontSize
        {
            get => (int)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public string LabelContent
        {
            get => (string)GetValue(LabelContentProperty);
            set => SetValue(LabelContentProperty, value);
        }

        public ShortRegisterPasswordInputField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
