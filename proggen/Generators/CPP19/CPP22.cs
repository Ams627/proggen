using Proggen.Generators.Common;

namespace Proggen;

[AutoRegister]
class CPP22 : ProgramGenerator
{
    public override string Name => "cpp22";
    public override string Description => "Generate a C++ Win32 console app for VS 2022 - windows.h included.";
    public override string VSVersion => "2022";
    public override string PlatformToolset => "v143";
    public override Guid ProjectTypeGUID => ProjectTypeGUIDs.cppGuid;
    public override string SolutionConfig => "Win32";
    public override string ProjectSuffix => "vcxproj";
    public override string Command => "OpenConApp";
    public override string CommandParam => "";
    public override FileSpec[] FileSpecs => CPPFileSpecs.CPPSpecs;
    public override List<string> Folders => null;
}
