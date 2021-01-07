using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Setup.Configuration;
using System.Runtime.InteropServices;

namespace VS2017Info
{
    public class Vs2017SetupConfig
    {
        private const int REGDB_E_CLASSNOTREG = unchecked((int)0x80040154);

        private ISetupConfiguration _setupConfig;
        private List<VSInstanceInfo> _instanceInfos = new List<VSInstanceInfo>();
        public Vs2017SetupConfig()
        {
            try
            {
                _setupConfig = new SetupConfiguration();
            }
            catch (Exception ex) 
            {
                throw new Exception($"Cannot create a new SetupConfiguration for Visual Studio", ex);
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
                    var instanceInfo = new VSInstanceInfo
                    {
                        Id = instance2.GetInstanceId(),
                        Name = instance2.GetInstallationName(),
                        Version = instance2.GetInstallationVersion(),
                        DispName = instance2.GetDisplayName(),
                        Description = instance2.GetDescription(),
                        ResPath = instance2.ResolvePath(),
                        EnginePath = instance2.GetEnginePath(),
                        InstalledPath = instance2.GetInstallationPath(),
                        ProductPath = instance2.GetProductPath()
                    };
                    _instanceInfos.Add(instanceInfo);
                }
            }
            while (fetched > 0);
        }

        /// <summary>
        /// returns a list of full installation paths for the VS2017 product
        /// </summary>
        public List<VSInstanceInfo> VSInstances
        {
            get
            {
                return _instanceInfos;
            }
        }
    }
}
