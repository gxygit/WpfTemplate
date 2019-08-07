using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WpfTemplate
{
    public static class SecureStringHelpers
    {
        public static string Unsecure(this SecureString securePassword)
        {
            if (securePassword == null) return string.Empty;
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
