#region Using directives

using System.Windows;

#endregion Using directives

namespace Celeste_Launcher_Gui
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // TODO: Invoke legacy main function from here
        public App()
        {
            LegacyBootstrapper.InitializeLegacyComponents();
        }
    }
}