using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    /// Interaction logic for EmptyPage.xaml
    /// </summary>
    public partial class EmptyPage : Page
    {
        public EmptyPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
