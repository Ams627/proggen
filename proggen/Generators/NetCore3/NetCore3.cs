using System;
using System.Collections.Generic;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class NetCore3 : ProgramGenerator
    {
        public override string Name => "netcore3";

        public override string Description => "Generate a .Net Core 3 console application for Visual Studio 2019.";

        public override string VSVersion => "2019";

        public override string PlatformToolset => "v142";

        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;

        public override string SolutionConfig => "Any CPU";

        public override string ProjectSuffix => "csproj";

        public override string Command => "OpenCSharpConApp";

        public override string CommandParam => "";

        public override FileSpec[] FileSpecs => Proggen.Generators.Common.NetCore3FileSpecs.NetCore3Specs;

        public override List<string> Folders => null;
    }
}
