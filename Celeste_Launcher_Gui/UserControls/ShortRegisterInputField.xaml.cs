#region Using directives

using System.Windows;
using System.Windows.Controls;

#endregion Using directives

namespace Celeste_Launcher_Gui.UserControls
{
    /// <summary>
    ///     Interaction logic for ShortRegisterInputField.xaml
    /// </summary>
    public partial class ShortRegisterInputField : UserControl
    {
        public static readonly DependencyProperty LabelContentProperty =
            DependencyProperty.Register("LabelContent", typeof(string), typeof(ShortRegisterInputField),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty InputContentProperty =
            DependencyProperty.Register("InputContent", typeof(string), typeof(ShortRegisterInputField),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(int), typeof(ShortRegisterInputField),
                new PropertyMetadata(20));

        public string LabelContent
        {
            get => (string)GetValue(LabelContentProperty);
            set => SetValue(LabelContentProperty, value);
        }

        public string InputContent
        {
            get => (string)GetValue(InputContentProperty);
            set => SetValue(InputContentProperty, value);
        }

        public int LabelFontSize
        {
            get => (int)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public ShortRegisterInputField()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}