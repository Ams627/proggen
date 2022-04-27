using System;
using System.Collections.Generic;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]

    class CS22 : ProgramGenerator
    {
        public override string Name => "cs22";
        public override string Description => "Generate a C# console application for Visual Studio 2019.";
        public override string VSVersion => "2022";
        public override string PlatformToolset => "v143";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenCSharpConApp";
        public override string CommandParam => "";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.CSConsoleFileSpecs.CSConsoleSpecs;
        public override List<string> Folders => null;

    }
}
