using System;
using System.Collections.Generic;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class CSNet6 : ProgramGenerator
    {
        public override string Name => "net6";
        public override string Description => "Generate a .Net 6.0 C# console application for Visual Studio 2022.";
        public override string VSVersion => "2022";
        public override string PlatformToolset => "v142";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenCSharpConApp";
        public override string CommandParam => "";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.CSConsoleFileSpecs.CSConsoleSpecsNet6;
        public override List<string> Folders => null;

    }

    [AutoRegister]
    class CSNet7 : ProgramGenerator
    {
        public override string Name => "net7";
        public override string Description => "Generate a .Net 7.0 C# console application for Visual Studio 2022.";
        public override string VSVersion => "2022";
        public override string PlatformToolset => "v142";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenCSharpConApp";
        public override string CommandParam => "";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.CSConsoleFileSpecs.CSConsoleSpecsNet7;
        public override List<string> Folders => null;

    }
}
