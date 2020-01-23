using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    /// Interaction logic for WideRegisterInputField.xaml
    /// </summary>
    public partial class WideRegisterPasswordInputField : UserControl
    {
        public static readonly DependencyProperty LabelContentProperty =
            DependencyProperty.Register("LabelContent", typeof(string), typeof(WideRegisterPasswordInputField), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(int), typeof(WideRegisterPasswordInputField), new PropertyMetadata(20));

        public string LabelContent
        {
            get => (string)GetValue(LabelContentProperty);
            set => SetValue(LabelContentProperty, value);
        }

        public int LabelFontSize
        {
            get => (int)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public WideRegisterPasswordInputField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
