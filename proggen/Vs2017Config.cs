using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.Setup.Configuration;
using System.Runtime.InteropServices;

namespace Proggen
{
    /// <summary>
    /// This class gets information about installed instances of Visual Studio 2017 of which there may be more than one
    /// </summary>
    class Vs2017Config
    {
        private const int REGDB_E_CLASSNOTREG = unchecked((int)0x80040154);

        private ISetupConfiguration _setupConfig;
        private List<string> _paths = new List<string>();
        public Vs2017Config()
        {
            try
            {
                // Try to CoCreate the class object.
                _setupConfig = new SetupConfiguration();
            }
            catch (COMException ex) when (ex.HResult == REGDB_E_CLASSNOTREG)
            {
                // Try to get the class object using app-local call.
                var result = GetSetupConfiguration(out _setupConfig, IntPtr.Zero);

                if (result < 0)
                {
                    throw new COMException("Failed to get query", result);
                }
            }

            var setupConfig2 = (ISetupConfiguration2)_setupConfig;
            var e = setupConfig2.EnumAllInstances();
            var helper = (ISetupHelper)_setupConfig;

            int fetched;
            var instances = new ISetupInstance[1];
            do
            {
                e.Next(1, instances, out fetched);
                if (fetched > 0)
                {
                    var instance2 = (ISetupInstance2)instances[0];
                    var ipath = instance2.GetInstallationPath();
                    var ppath = instance2.GetProductPath();
                    var combinedPath = Path.Combine(ipath, ppath);
                    _paths.Add(combinedPath);
                }
            }
            while (fetched > 0);
        }

        /// <summary>
        /// returns a list of full installation paths for the VS2017 product
        /// </summary>
        public List<string> InstallationPaths
        {
            get
            {
                return _paths;
            }
        }
    }
}
