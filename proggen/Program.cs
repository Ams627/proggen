// Copyright (c) Adrian Sims 2018
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;
using System.Xml.Linq;
using System.Reflection;
using proggen;

namespace Proggen
{
    class Program
    {
        private static string progname;

        private static void PrintProgramVersion()
        {
            var asm = System.Reflection.Assembly.GetEntryAssembly();
            var version = asm.GetName().Version;
            var buildDate = BuildStats.GetBuildDate(asm);
            var now = DateTime.Now;

            Console.WriteLine($"{progname} a Visual Studio solution generator for Visual Studio 2015/7/9.");
            Console.WriteLine($"{progname} version {version}");
            Console.WriteLine($"{progname} location: {asm.Location}");
            Console.WriteLine($"{progname} build date: {buildDate:yyyy-MMM-dd} at {buildDate:HH:mm:ss}");
            Console.WriteLine($"Current date/time: {now:yyyy-MMM-dd HH:mm:ss}");
        }


        private static void DoGenerate(string projectname, string generator = "")
        {
            if (File.Exists(projectname) || Directory.Exists(projectname))
            {
                Console.Error.WriteLine($"{progname}: file or directory {projectname} already exists.");
            }
            else
            {
                VSGlobals.ProjectName = projectname;
                if (string.IsNullOrWhiteSpace(generator))
                {
                    generator = progname;
                }
                GeneratorManager.Generate(generator);
            }
        }

        static void Main(string[] args)
        {
            Settings.Go();
            VSGlobals.ProjectGUID = Guid.NewGuid();
            var s = VSGlobals.ExpandMacros("$$(PROJECTGUID)");

            var codeBase = System.Reflection.Assembly.GetEntryAssembly().CodeBase;
            progname = Path.GetFileNameWithoutExtension(codeBase);
            try
            {
                var arglist = args.ToList();

                if (args.Count() == 0)
                {
                    Console.WriteLine($"{progname} - make various types of Visual Studio solution and start Visual Studio.\n");
                    Console.WriteLine("Usage:\n\n    proggen <SOLUTIONTYPE> <solution-name> <solution-name> ...\n");
                    Console.WriteLine("An instance of Visual Studio is started for each solution name specified.");
                    Console.WriteLine("Solutions types are as follows:\n");
                    foreach (var helpText in GeneratorManager.HelpTexts)
                    {
                        Console.WriteLine(helpText);
                    }
                    Console.WriteLine("\nAlternatively if this program has the name of one of the solution types\nabove, the solution type parameter is not needed (the solution type is determined from the program name).");
                    Console.WriteLine($"\nSpecifying {progname} -makeg with no other parameters causes proggen to\ncopy itself to each of the solution-types specified above (with\na .exe extension)");
                    Environment.Exit(0);
                }
                else if (args[0] == "-makeg" || args[0] == "--makeg")
                {
                    if (args.Count() > 1)
                    {
                        throw new Exception("-makeg option does not take parameters");
                    }
                    GeneratorManager.MakeAllGenerators();
                    Environment.Exit(0);
                }
                else if (args[0] == "-instances")
                {
                    if (args.Count() > 1)
                    {
                        throw new Exception("-instances does not take parameters.");
                    }
                    var vs2017Info = new VS2017Info.Vs2017SetupConfig();
                    var instances = vs2017Info.VSInstances;
                    foreach (var instance in instances)
                    {
                        Console.WriteLine($"ID: {instance.Id} Path: {Path.Combine(instance.InstalledPath, instance.ProductPath)}");
                    }
                    Environment.Exit(0);
                }
                else if (args[0] == "-version" || args[0] == "--version")
                {
                    PrintProgramVersion();
                    Environment.Exit(0);
                }

                var generatorName = "";
                // is the program name a generator name?
                if (GeneratorManager.IsAGenarator(progname))
                {
                    generatorName = progname;
                    if (arglist[0] == "-g")
                    {
                        if (arglist.Count < 2)
                        {
                            throw new Exception("You must specify the name of a project to create.");
                        }
                        Utility.Shift(ref arglist);
                    }
                }
                else
                {
                    bool haveOption = arglist[0][0] == '-';
                    var minargs = haveOption ? 3 : 2;
                    if (arglist.Count < minargs)
                    {
                        throw new Exception("You must specify at least one project name.");
                    }

                    if (haveOption)
                    {
                        var option = arglist[0];
                        if (option != "-g")
                        {
                            throw new Exception($"'{option}' is an invalid option");
                        }
                        Utility.Shift(ref arglist);
                    }

                    generatorName = arglist[0];
                    if (!GeneratorManager.IsAGenarator(generatorName))
                    {
                        throw new Exception($"'{generatorName}' is not a valid project type.");
                    }
                    Utility.Shift(ref arglist);

                    // alternatively an option can go after the project type:
                    var optionAfterGenerator = arglist[0][0] == '-';
                    if (optionAfterGenerator)
                    {
                        var option = arglist[0];
                        if (option != "-g")
                        {
                            throw new Exception($"'{option}' is an invalid option");
                        }
                        Utility.Shift(ref arglist);
                    }
                }

                foreach (var project in arglist)
                {
                    if (project[0] == '-')
                    {
                        Console.Error.WriteLine($"{progname}: option {project} ignored.");
                    }
                    else
                    {
                        DoGenerate(project, generatorName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{progname}: error: {ex}");
            }
        }

    }
}
