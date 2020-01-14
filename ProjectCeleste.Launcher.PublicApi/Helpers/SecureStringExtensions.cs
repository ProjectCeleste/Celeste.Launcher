using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ProjectCeleste.Launcher.PublicApi.Helpers
{
    public static class SecureStringExtensions
    {
        public static string GetValue(this SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
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
            SecureString secureString = new SecureString();
            for (int i = 0; i < value.Length; i++)
            {
                secureString.AppendChar(value[i]);
            }

            return secureString;
        }
    }
}