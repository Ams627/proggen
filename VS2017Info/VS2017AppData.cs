using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VS2017Info
{
    public static class VS2017AppData
    {
        private static string _localAppDataPath;
        static VS2017AppData()
        {
            _localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// Get the local appdata path for the given visual studio instance
        /// </summary>
        /// <param name="instance">A VS2017 instance identifier e.g. 4b0ba1c0 </param>
        /// <returns>The localappdata path for the instance</returns>
        public static string GetLocalAppDataPath(string version, string instanceID)
        {
            const string pattern = @"^[0-9][0-9]";
            var match = Regex.Match(version, pattern);
            var keyVersion = match.Success ? version.Substring(match.Index, match.Length) + ".0" : "15.0";

            keyVersion += "_" + instanceID;
            var vsAppDataPath = Path.Combine(_localAppDataPath, @"Microsoft\visualstudio", keyVersion);
            return vsAppDataPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        public static string GetVersionForRegistryKey(string version, string instanceID)
        {
            const string pattern = @"^[0-9][0-9]";
            var match = Regex.Match(version, pattern);
            var keyVersion = match.Success ? version.Substring(match.Index, match.Length) + ".0" : "15.0";

            keyVersion += "_" + instanceID;
            return keyVersion;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetPrivateRegFilename(string version, string instance)
        {
            return Path.Combine(GetLocalAppDataPath(version, instance), "privateregistry.bin");
        }

        public static List<KeyValuePair<string, string>> GetInstalledExtensions(string version, string instance)
        {
            var result = new List<KeyValuePair<string, string>>();

            var keypath = $@"Software\Microsoft\VisualStudio\{GetVersionForRegistryKey(version, instance)}\ExtensionManager\EnabledExtensions";

            // check the private hive exists and open it:
            var hivefile = VS2017Info.VS2017AppData.GetPrivateRegFilename(version, instance);
            if (!File.Exists(hivefile))
            {
                throw new VsInfoException($"{hivefile} does not exist.");
            }
            var hKey = RegistryNativeMethods.RegLoadAppKey(hivefile);

            using (var safeRegistryHandle = new SafeRegistryHandle(new IntPtr(hKey), true))
            using (var appKey = RegistryKey.FromHandle(safeRegistryHandle))
            using (var extensionsKey = appKey.OpenSubKey(keypath, true))
            {
                if (extensionsKey == null)
                {
                    throw new VsInfoException($"the key {keypath} does not exist in {hivefile}");
                }

                // get a list of key-value pairs - use the value names to get the values
                result = extensionsKey == null ? result :
                    extensionsKey.GetValueNames().Select(x => new KeyValuePair<string, string>(x, extensionsKey.GetValue(x).ToString())).ToList();

                var extensions = extensionsKey?.GetValueNames() ?? Enumerable.Empty<string>();
                foreach (var key in extensions)
                {
                    var value = extensionsKey.GetValue(key).ToString();
                    result.Add(new KeyValuePair<string, string>(key, value));
                }
            }

            return result;
        }
    }
}
