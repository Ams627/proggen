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
        private ISetupConfiguration _setupConfig;
        private readonly List<string> _paths = new List<string>();
        public Vs2017Config()
        {
            try
            {
                _setupConfig = new SetupConfiguration();
            }
            catch (Exception ex)
            {
                throw new COMException("Failed to get query VS2017 instances", ex);
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
        public List<string> InstallationPaths => _paths;
    }
}
