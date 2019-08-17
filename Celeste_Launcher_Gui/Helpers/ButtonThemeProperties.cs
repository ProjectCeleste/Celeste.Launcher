using System.Windows;
using System.Windows.Media;

namespace Celeste_Launcher_Gui.Helpers
{
    public static class ButtonThemeProperties
    {
        public static ImageSource GetDefaultIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(DefaultIconProperty);
        }

        public static void SetDefaultIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(DefaultIconProperty, value);
        }

        public static readonly DependencyProperty DefaultIconProperty =
            DependencyProperty.RegisterAttached(
                "DefaultIcon",
                typeof(ImageSource),
                typeof(ButtonThemeProperties),
                new FrameworkPropertyMetadata(default(ImageSource)));

        public static ImageSource GetHoverIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(HoverIconProperty);
        }

        public static void SetHoverIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(HoverIconProperty, value);
        }

        public static readonly DependencyProperty HoverIconProperty =
            DependencyProperty.RegisterAttached(
                "HoverIcon",
                typeof(ImageSource),
                typeof(ButtonThemeProperties),
                new FrameworkPropertyMetadata(default(ImageSource)));
    }
}
