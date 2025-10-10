using Celeste_Launcher_Gui.Helpers;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.UserControls
{
    public partial class ColorComboBoxSelection : UserControl
    {
        public static readonly DependencyProperty LabelContentProperty =
            DependencyProperty.Register("LabelContent", typeof(string), typeof(ColorComboBoxSelection), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty InputContentProperty =
            DependencyProperty.Register("InputContent", typeof(string), typeof(ColorComboBoxSelection), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(int), typeof(ColorComboBoxSelection), new PropertyMetadata(18));

        // RoutedEvent for ColorChanged
        public static readonly RoutedEvent ColorChangedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ColorComboBoxSelection));

        // .NET event wrapper
        public event RoutedEventHandler ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

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

        public string SelectedColor
        {
            get
            {
                if (ColorElements.SelectedIndex < 0)
                {
                    return "";
                }

                var selectedItem = (ComboBoxItem)ColorElements.SelectedItem;
                return selectedItem.Content.ToString();
            }
            set
            {
                if (ColorElements.Items.Count > 0)
                {
                    var item = ColorElements.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == value);
                    if (item != null)
                    {
                        ColorElements.SelectedItem = item;
                    }
                    else
                    {
                        var newItem = new ComboBoxItem { Content = value };
                        ColorElements.Items.Add(newItem);
                        ColorElements.SelectedItem = newItem;
                    }
                }
            }
        }

        public ColorComboBoxSelection()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            ColorElements.SelectionChanged += ColorElements_SelectionChanged;
        }

        private void ColorElements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorElements.SelectedIndex >= 0)
            {
                var selectedItem = (ComboBoxItem)ColorElements.SelectedItem;

                ColorElements.Foreground = GetColorFromColorName(selectedItem.Content.ToString());
                // Raise the routed event
                RaiseEvent(new RoutedEventArgs(ColorChangedEvent));
            }
        }

        private System.Windows.Media.Brush GetColorFromColorName(string colorName)
        {
            if (colorName == ColorPickerColors.Blue)
            {
                return System.Windows.Media.Brushes.Blue;
            }
            else if (colorName == ColorPickerColors.Red)
            {
                return System.Windows.Media.Brushes.Red;
            }
            else if (colorName == ColorPickerColors.Yellow)
            {
                return System.Windows.Media.Brushes.Yellow;
            }
            else if (colorName == ColorPickerColors.Purple)
            {
                return System.Windows.Media.Brushes.Purple;
            }
            else if (colorName == ColorPickerColors.Green)
            {
                return System.Windows.Media.Brushes.Green;
            }
            else if (colorName == ColorPickerColors.Orange)
            {
                return System.Windows.Media.Brushes.Orange;
            }
            else if (colorName == ColorPickerColors.Cyan)
            {
                return System.Windows.Media.Brushes.Cyan;
            }
            else if (colorName == ColorPickerColors.Pink)
            {
                return System.Windows.Media.Brushes.Pink;
            }
            else if (colorName == ColorPickerColors.BlueSelf)
            {
                return System.Windows.Media.Brushes.DarkBlue;
            }
            else if (colorName == ColorPickerColors.RedFoe)
            {
                return System.Windows.Media.Brushes.Red;
            }
            else if (colorName == ColorPickerColors.YellowAlly)
            {
                return System.Windows.Media.Brushes.Yellow;
            }
            else
            {
                return System.Windows.Media.Brushes.Black;
            }
        }
    }
}
