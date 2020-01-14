#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.Helpers
{
    public static class SecureStringExtensions
    {
        public static string GetValue(this SecureString secureString)
        {
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString ToSecureString(this string value)
        {
            var secureString = new SecureString();
            foreach (var t in value) secureString.AppendChar(t);

            return secureString;
        }
    }
}