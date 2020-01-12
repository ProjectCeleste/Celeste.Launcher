#region Using directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public static class InternetUtils
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsConnectedToInternet()
        {
            try
            {
                return InternetGetConnectedState(out int _, 0);
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}