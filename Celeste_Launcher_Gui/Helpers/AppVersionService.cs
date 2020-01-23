using System.Reflection;

namespace Celeste_Launcher_Gui.Helpers
{
    public static class AppVersionService
    {
        public static string CurrentAppVersion => $"V {Assembly.GetExecutingAssembly().GetName().Version}";
    }
}
