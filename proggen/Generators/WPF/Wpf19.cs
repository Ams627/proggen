using System;
using System.Collections.Generic;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class Wpf19 : ProgramGenerator
    {
        public override string Name => "wpf19";
        public override string Description => "Generate a WPF application for Visual Studio 2015.";
        public override string VSVersion => "2019";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csSdkGuid;
        public override string PlatformToolset => "v141";
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenWPFApp";
        public override string CommandParam => "";
        public override FileSpec[] FileSpecs => CsWpfFileSpecs.CSWpfSpecs;
        public override List<string> Folders => null;
    }
}
