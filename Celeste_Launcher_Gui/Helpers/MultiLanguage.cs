using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.Helpers
{
    public class MultiLanguage
    {
        //@Vitalivs, see "Walkthrough - Localizing Windows Forms": https://msdn.microsoft.com/en-us/library/y99d1cd3(v=vs.71).aspx


        private static ResourceManager _locRM;
        
        public static string GetString(string stringName)
        {
            if (_locRM == null)
                _locRM = new ResourceManager("Celeste_Launcher_Gui.StringResources.MLStrings", typeof(Program).Assembly);

            return _locRM.GetString(stringName);
        }


    }
}
