using System;
using System.Collections.Generic;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class Db19 : ProgramGenerator
    {
        public override string Name => "db19";
        public override string Description => "Generate a C# console application with Database support for Visual Studio 2019.";
        public override string VSVersion => "2019";
        public override string PlatformToolset => "v141";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenCSharpConApp";
        public override string CommandParam => "";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.CSConsoleFileSpecs.DbConsoleSpecsNet19;
        public override List<string> Folders => null;

    }

}
