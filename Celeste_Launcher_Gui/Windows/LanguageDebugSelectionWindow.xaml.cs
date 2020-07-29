using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for LanguageDebugSelectionWindow.xaml
    /// </summary>
    public partial class LanguageDebugSelectionWindow : Window
    {
        public LanguageDebugSelectionWindow()
        {
            InitializeComponent();
        }

        private void SetGerman(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.deDE);
        }

        private void SetEnglish(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.enUS);
        }

        private void SetSpanish(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.esES);
        }

        private void SetFrench(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.frFR);
        }

        private void SetItalian(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.itIT);
        }

        private void SetChinese(object sender, RoutedEventArgs e)
        {
            SetLanguage(GameLanguage.zhCHT);
        }

        private void SetBrazilian(object sender, RoutedEventArgs e)
        {

            SetLanguage(GameLanguage.ptBR);
        }

        private void SetLanguage(GameLanguage language)
        {
            LegacyBootstrapper.UserConfig.GameLanguage = language;
            LegacyBootstrapper.SetUILanguage();

            foreach (var window  in App.Current.Windows)
            {
                if (window is MainWindow mainWindow)
                {
                    mainWindow.NavigationFrame.Navigate(new Uri("Pages/EmptyPage.xaml", UriKind.Relative));
                }
            }
        }
    }
}
