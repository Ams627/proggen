using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen
{
    class NetCore15 : ProgramGenerator
    {
        public override string Name => "ASPCore15";
        public override string Description => "Generate ASP.Net Core Web API for Visual Studio 2015";
        public override string VSVersion => "2015";
        public override string PlatformToolset => "v141";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenCSharpConApp";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.CSConsoleFileSpecs.CSConsoleSpecs;
        public override List<string> Folders => null;

    }
}
