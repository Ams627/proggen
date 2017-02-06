using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen.Generators.CPP15.FileSpecs
{
    internal class MainCpp : FileSpec 
    {
        public override string Pathname { get; set; } = "$$(PROJECTNAME)/main.cpp";
        public override string[] Contents { get; set; } = new[]
        {
            "#include \"stdafx.h\"\n",
            "// DELETE THIS COMMENT BLOCK OR USE WHAT IS IN IT!",
            "// Use the following line to make a windows program with main startup (rather than a console program)",
            "// Remove the /entry part if you want to use WinMain instead...",
            "//",
            "//#pragma comment(linker, \"/subsystem:windows /entry:mainCRTStartup\")",
            "//",
            "//",
            "// This is the syntax for including libraries:",
            "//",
            "//#pragma comment(lib,\"comctl32.lib\")",
            "",
            "int main()",
            "{\n",
            "}"
        };
    }
}
