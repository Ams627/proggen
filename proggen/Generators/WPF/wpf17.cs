using System;
using System.Collections.Generic;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class Wpf17 : ProgramGenerator
    {
        public override string Name => "wpf17|pgwpf";
        public override string Description => "Generate a WPF application for Visual Studio 2017";
        public override string VSVersion => "2017";
        public override string PlatformToolset => "v141";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "OpenStartFile";
        public override string CommandParam => "MainWindow.xaml.cs";
        public override FileSpec[] FileSpecs => CsWpfFileSpecs.CSWpfSpecs;
        public override List<string> Folders => null;

    }
}
