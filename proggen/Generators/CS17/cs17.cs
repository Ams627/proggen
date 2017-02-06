using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen
{
    class CS17 : ProgramGenerator
    {
        public override string Name => "cs17";
        public override string Description => "Generate a C++ Win32 console application for Visual Studio 2017 - windows.h included.";
        public override string VSVersion => "2017";
        public override string PlatformToolset => "v141";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenCSharpConApp";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.CSConsoleFileSpecs.CSConsoleSpecs;
    }
}
