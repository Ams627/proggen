using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;
using System.IO;
using System.Diagnostics;

namespace Proggen
{
    internal abstract class ProgramGenerator
    {
        public abstract string Name { get; }                // name of generator - use vertical bar ("pipe") character for aliases
        public abstract string Description { get; }         // description for command-line help
        public abstract string VSVersion { get; }           // version of Visual Studio to start
        public abstract string PlatformToolset { get; }     // version of Visual Studio toolset to use (v140 or v141)
        public abstract Guid ProjectTypeGUID { get; }       // predefined by VS
        public abstract string SolutionConfig { get; }      // "Win32" or "Any CPU"
        public abstract string ProjectSuffix { get; }       // project suffix - e.g. "csproj"
        public abstract string Command { get; }             // command to run on opening VS
        public abstract string CommandParam { get; }        // command to run on opening VS
        public abstract FileSpec[] FileSpecs { get; }       // array of file specifiers
        public abstract List<string> Folders { get; }       // any empty folders required - can be null
        public virtual void Generate()
        {
            VSGlobals.GeneratorName = Name;
            VSGlobals.GeneratedDate = DateTime.Now;
            VSGlobals.ProjectGUID = Guid.NewGuid();
            VSGlobals.ProjectTypeGuid = ProjectTypeGUID;
            VSGlobals.VisualStudioVersion = VSVersion;
            VSGlobals.PlatformToolset = PlatformToolset;
            VSGlobals.VSCommand = Command;
            VSGlobals.VSCommandParam = CommandParam;
            VSGlobals.ProjectSuffix = ProjectSuffix;
            VSGlobals.SolutionConfig = SolutionConfig;

            // Make empty folders if any are specified:
            if (Folders != null)
            {
                foreach (var folder in Folders)
                {
                    var pathname = Path.Combine(VSGlobals.ProjectName, VSGlobals.ExpandMacros(folder));
                    if (!Directory.Exists(pathname))
                    {
                        Directory.CreateDirectory(pathname);
                    }
                }
            }

            foreach (var filespec in FileSpecs)
            {
                var pathname = Path.Combine(VSGlobals.ProjectName, VSGlobals.ExpandMacros(filespec.Pathname));

                // get relative dir from filename (i.e. relative to current working directory):
                var fullRelativeDir = Path.GetDirectoryName(pathname);
                if (!string.IsNullOrWhiteSpace(fullRelativeDir))
                {
                    Directory.CreateDirectory(fullRelativeDir);
                }
            }

            foreach (var filespec in FileSpecs)
            {
                var relativePathname = VSGlobals.ExpandMacros(filespec.Pathname);
                var fullPathname = Path.Combine(VSGlobals.ProjectName, relativePathname);
                using (var file = new System.IO.StreamWriter(fullPathname))
                {
                    foreach (var line in filespec.Contents)
                    {
                        var outputline = VSGlobals.ExpandMacros(line);
                        file.WriteLine(outputline.Replace("\n", "\r\n"));
                    }
                }
            }
        }
    }
}
