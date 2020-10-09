using Proggen.Generators.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    static class CsCoreCFileSpecs
    {
        private static string sixteen = new string(' ', 16);
        private static string errorStuff =
            sixteen + "var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;\r\n" +
            sixteen + "var progname = Path.GetFileNameWithoutExtension(fullname);\r\n" +
            sixteen + "Console.Error.WriteLine(progname + \": Error: \" + ex.Message);";

        public static FileSpec[] CsCoreCSpecs =
        {
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "using System;",
                    "using System.Collections.Generic;",
                    "using System.IO;",
                    "using System.Linq;",
                    "using System.Text;",
                    "using System.Text.RegularExpressions;",
                    "using System.Xml.Linq;",
                    "using System.Threading.Tasks;\r\n",
                    "namespace $$(PROJECTNAMECAMEL)",
                    "{",
                    "    class Program",
                    "    {",
                    "        private static void Main(string[] args)",
                    "        {",
                    $"            try\r\n            {{\r\n                //startstarttypingtypingherehere\r\n            }}\r\n            catch (Exception ex)\r\n            {{\r\n{errorStuff}\r\n            }}\r\n",
                    "        }",
                    "    }",
                    "}"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<Project Sdk=\"Microsoft.NET.Sdk\" >",
                    "  <PropertyGroup>",
                    "    <OutputType>Exe</OutputType>",
                    "    <TargetFramework>netcoreapp2.1</TargetFramework>",
                    "  </PropertyGroup>",
                    "</Project>"
                }
            },
            new FileSpec (CommonFileSpecs.SlnFileSpec),
            new FileSpec (CommonFileSpecs.GitIgnore)
        };
    }
}

