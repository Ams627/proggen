using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VS2017Info
{
    internal static class RegistryNativeMethods
    {
        [Flags]
    public enum RegSAM
    {
        AllAccess = 0x000f003f,
        KEY_CREATE_LINK = 0x0020,
        KEY_CREATE_SUB_KEY = 0x0004,
        KEY_ENUMERATE_SUB_KEYS = 0x0008,
        KEY_EXECUTE = 0x20019,
        KEY_NOTIFY = 0x0010,
        KEY_QUERY_VALUE = 0x0001,
        KEY_READ = 0x20019,
        KEY_SET_VALUE = 0x0002,
        KEY_WOW64_32KEY = 0x0200,
        KEY_WOW64_64KEY = 0x0100,
        KEY_WRITE = 0x20006,
    }

    private const int REG_PROCESS_APPKEY = 0x00000001;

    // approximated from pinvoke.net's RegLoadKey and RegOpenKey
    // NOTE: changed return from long to int so we could do Win32Exception on it
    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern int RegLoadAppKey(String hiveFile, out int hKey, RegSAM samDesired, int options, int reserved);

    public static int RegLoadAppKey(String hiveFile)
    {
        int hKey;
        int rc = RegLoadAppKey(hiveFile, out hKey, RegSAM.KEY_ENUMERATE_SUB_KEYS | RegSAM.KEY_QUERY_VALUE | RegSAM.KEY_READ, 0, 0);

        if (rc != 0)
        {
            throw new Win32Exception(rc, "Failed during RegLoadAppKey of file " + hiveFile);
        }

        return hKey;
    }
}
}
