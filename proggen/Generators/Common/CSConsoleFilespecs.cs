using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    static class CSConsoleFileSpecs
    {
        static CSConsoleFileSpecs()
        {
            var binFolder= Environment.GetEnvironmentVariable("PROGGENBINFOLDER");
            if (!string.IsNullOrEmpty(binFolder) && Directory.Exists(binFolder))
            {
                postBuildStep = $@"  <PropertyGroup>
    <PostBuildEvent>copy $(TargetPath) {binFolder}</PostBuildEvent>
   </PropertyGroup>";
            }
        }
        private static string postBuildStep = "";
        private static string sixteen = new string(' ', 16);
        private static string errorStuff =
            sixteen + "var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;\r\n" +
            sixteen + "var progname = Path.GetFileNameWithoutExtension(fullname);\r\n" +
            sixteen + "Console.Error.WriteLine($\"{progname} Error: {ex.Message}\");";

        public static FileSpec[] CSOldConsoleSpecs => new []
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
                Pathname = "$$(PROJECTNAMECAMEL)/App.config",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<?xml version=\"1.0\" encoding=\"utf-8\" ?>",
                    "<configuration>",
                    "    <startup> ",
                    "        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.7.2\" />",
                    "    </startup>",
                    "</configuration>"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
                Contents = new[] {@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net472</TargetFramework>
  </PropertyGroup>
</Project>" }
            },
            new FileSpec (CommonFileSpecs.SlnFileSpec),
            new FileSpec (CommonFileSpecs.GitIgnore)
        };
    }
}