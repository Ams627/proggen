using System;
using System.Collections.Generic;
using Proggen.Generators.Common;

namespace Proggen.Generators.WebAPI17
{
    [AutoRegister]
    class WebAPI17 : ProgramGenerator
    {
        public override string Name => "WebAPI17";
        public override string Description => "Generate a .Net Core 1.1 Web API project for Visual Studio 2017";
        public override string VSVersion => "2017";
        public override string PlatformToolset => "v141";
        public override Guid ProjectTypeGUID => ProjectTypeGUIDs.csGuid;
        public override string SolutionConfig => "Any CPU";
        public override string ProjectSuffix => "csproj";
        public override string Command => "";
        public override FileSpec[] FileSpecs => Proggen.Generators.Common.WebAPIFileSpecs.WebAPISpecs;
        public override List<string> Folders => new List<string> { "$$(PROJECTNAMECAMEL)/wwwroot" };

    }
}

