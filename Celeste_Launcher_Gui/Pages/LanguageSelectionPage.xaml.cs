#region Using directives

using System.Windows;
using System.Windows.Controls;

#endregion Using directives

namespace Celeste_Launcher_Gui.Pages
{
    /// <summary>
    ///     Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LanguageSelectionPage : Page
    {
        public LanguageSelectionPage()
        {
            InitializeComponent();
        }

        private void OnAbortLoginClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void SelectGerman(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.deDE);
        }

        private void SelectEnglish(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.enUS);
        }

        private void SelectSpanish(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.esES);
        }

        private void SelectFrench(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.frFR);
        }

        private void SelectItalian(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.itIT);
        }

        private void SelectChinese(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.zhCHT);
        }

        private void SelectPortugese(object sender, RoutedEventArgs e)
        {
            //SetLanguage(GameLanguage.ptBR);
        }

        private void SetLanguage(GameLanguage language)
        {
            LegacyBootstrapper.UserConfig.GameLanguage = language;
            LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);
            NavigationService?.GoBack();
        }
    }
}