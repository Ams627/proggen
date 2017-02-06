using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    class MacroAttribute : System.Attribute
    {
        public string Macroname { get; set; }
        public MacroAttribute(string macroName)
        {
            Macroname = macroName;
        }
    }
}
