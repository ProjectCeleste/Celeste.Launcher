using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.Helpers
{
    public static class AppVersionService
    {
        public static string CurrentAppVersion
        {
            get
            {
                return $"V {Assembly.GetExecutingAssembly().GetName().Version}";
            }
        }
    }
}
