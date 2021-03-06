﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen
{
    [AutoRegister]
    class CPP15 : ProgramGenerator
    {
        public override string Name => "cpp15|pg15";
        public override string Description => "Generate a C++ Win32 console app for VS 2015 - windows.h included.";
        public override string VSVersion => "2015";
        public override string PlatformToolset => "v140";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.cppGuid;
        public override string SolutionConfig => "Win32";
        public override string ProjectSuffix => "vcxproj";
        public override string Command => "OpenConApp";
        public override string CommandParam => "";
        public override FileSpec[] FileSpecs => CPPFileSpecs.CPPSpecs;
        public override List<string> Folders => null;

    }
}
