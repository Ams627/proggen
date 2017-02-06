using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    //$$(VSVERSION)
    //$$(PROJECTNAME)
    //$$(GENERATORNAME)
    //$$(PTYPEGUID)
    //$$(PROJGUID)
    //$$(GENDATE)


namespace proggen
{
    class MakeMacroAttribute : System.Attribute
    {
        public MakeMacroAttribute(string macroname)
        {

        }
    }
    class MacroManager
    {
        
        [MakeMacro]
        public string VsVersion { get; set; } = "2017";
        [MakeMacro]
        public string ProjectName { get; set; }
        [MakeMacro]
        public string GeneratorName { get; set; } // e.g. pg15
        [MakeMacro]
        public string ProjectTypeGUID { get; set; } // a GUID defined by microsoft
        [MakeMacro("PROJECTGUID")]
        public string ProjectGUID { get; set; }     // a unique GUID for this project
        [MakeMacro]
        public string GenerationDate { get; set; } = DateTime.Now.ToString();
    }
}
