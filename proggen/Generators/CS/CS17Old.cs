using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class CS17Old : ProgramGenerator
    {
        public override string Name => "cs17old";
        public override string Description => "Generate an old-format C# console application for Visual Studio 2017.";
        public override string VSVersion => "2017";
        public override string PlatformToolset => "v141";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenCSharpConApp";
        public override string CommandParam => "";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.CSOldConsoleFileSpecs.CSOldConsoleSpecs;
        public override List<string> Folders => null;
    }
}
