// Visual studio program generator
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen
{
    class Program
    {
        private static string progname;
        private static void DoGenerate(string projectname, string generator="")
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
            VSGlobals.ProjectGUID = Guid.NewGuid();
            var s = VSGlobals.ExpandMacros("$$(PROJECTGUID)");

            var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            progname = Path.GetFileNameWithoutExtension(codeBase);
            try
            {
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
                if (args.Count() == 1 && args[0] == "-makeg")
                {
                    GeneratorManager.MakeAllGenerators();
                    Environment.Exit(0);
                }

                // is the program name a generator name?
                if (GeneratorManager.IsAGenarator(progname))
                {
                    Console.WriteLine("Found generator");
                    int count = 0;
                    foreach (var arg in args)
                    {
                        if (arg == "-g" && count++ == 0)
                        {
                            VSGlobals.DoGit = true;
                        }
                        else
                        {
                            DoGenerate(arg);
                        }
                    }
                }
                else if (args.Count() > 1)
                {
                    // the program name was not the name of a generator so the first argument is the generator
                    // and the subsequent arguments are the programs to be generated:
                    var generatorName = args[0];
                    for (var i = 0; i < args.Count() - 1; i++)
                    {
                        var arg = args[i + 1];
                        if (arg == "-g")
                        {
                            if (i == 0)
                            {
                                VSGlobals.DoGit = true;
                            }
                            else
                            {
                                throw new Exception("-g must be the first argument")
                            }
                        }
                        else
                        {
                            DoGenerate(arg, generatorName);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Syntax error");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }
        }
    }
}
