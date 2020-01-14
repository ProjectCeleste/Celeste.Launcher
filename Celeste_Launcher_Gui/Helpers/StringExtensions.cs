namespace Celeste_Launcher_Gui.Helpers
{
    public static class StringExtensions
    {
        public static string WrapIfLengthIsLongerThan(this string theString, int maxLength, string prefix = "")
        {
            if (theString.Length > maxLength)
            {
                var cutIndex = theString.Length - maxLength;
                return prefix + theString.Substring(cutIndex);
            }

            return theString;
        }
    }
}