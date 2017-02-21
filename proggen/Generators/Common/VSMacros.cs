using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    public class VSGlobals : MacroBase<VSGlobals>
    {
        [Macro("VSVERSION")] public static string VisualStudioVersion { get; set; }
        [Macro("PLATFORMTOOLSET")] public static string PlatformToolset { get; set; }
        [Macro("GENERATOR")] public static string GeneratorName { get; set; }
        [Macro("PROJECTNAME")] public static string ProjectName { get; set; }
        [Macro("PROJECTNAMECAMEL")] public static string ProjectNameCamel => ProjectName[0].ToString().ToUpper() + ProjectName.Substring(1);
        [Macro("VSCOMMAND")] public static string VSCommand { get; set; }
        [Macro("VSCOMMANDPARAM")] public static string VSCommandParam { get; set; }
        [Macro("PROJECTGUID")] public static Guid ProjectGUID { get; set; }
        [Macro("PROJECTTYPEGUID")] public static Guid ProjectTypeGuid { get; set; }
        [Macro("SOLUTIONCONFIG")] public static string SolutionConfig { get; set; }
        [Macro("SOLUTIONCONFIGNOSPACE")] public static string SolutionConfigNoSpace => SolutionConfig.Replace(" ", "");
        [Macro("GENERATEDDATE")] public static DateTime GeneratedDate { get; set; }
        [Macro("SUFFIX")] public static string ProjectSuffix { get; set; }
        public static bool DoGit { get; set; } = false;
    }
}
