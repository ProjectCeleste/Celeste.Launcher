#region Using directives

using System.Resources;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public class MultiLanguage
    {
        //@Vitalivs, see "Walkthrough - Localizing Windows Forms": https://msdn.microsoft.com/en-us/library/y99d1cd3(v=vs.71).aspx


        private static ResourceManager _locRm;

        public static string GetString(string stringName)
        {
            if (_locRm == null)
                _locRm = new ResourceManager("Celeste_Launcher_Gui.StringResources.MLStrings",
                    typeof(Program).Assembly);

            return _locRm.GetString(stringName);
        }
    }
}