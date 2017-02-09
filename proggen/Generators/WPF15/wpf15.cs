using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class Wpf15 : ProgramGenerator
    {
        public override string Name => "wpf15";
        public override string Description => "Generate a WPF application for Visual Studio 2015.";
        public override string VSVersion => "2015";
        public override string PlatformToolset => "v140";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenWPFApp";
        public override FileSpec[] FileSpecs => null;
        public override List<string> Folders => null;

    }
}
