# proggen
## Solution generator for Visual Studio
This is a command line solution generator and used as an alternative to the new project wizard. It
sets up a new solution containing one new project. It then opens visual studio and builds the project - that's where its job ends.
A single command can generate many projects (and for each project open an instance of Visual Studio) **as long as they are all of the same type.**



A new generator class (i.e. one that implements the IGenerator interface) is added for each type of project. For example,
there is one for a c++ console application project, one for a c# wpf project, one for a .net core project. The
GeneratorManager class uses reflection to register all such generators. Adding another generator is simply
a matter of defining a class that implements the IGenerator interface - that's all that is required.

Here is an example of a generator class:
```
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
        public override FileSpec[] FileSpecs => CPPFileSpecs.CPPSpecs;
        public override List<string> Folders => null;
    }
'''
