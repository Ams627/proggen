// Visual studio program generator
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;
using System.Xml.Linq;
using System.Reflection;

namespace Proggen
{
    class Program
    {
        private static string progname;
        private static object path;

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
                else if (args[0] == "-makeg")
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
                        throw new Exception("-vsversions option does not take parameters");
                    }
                    var vs2017Info = new VS2017Info.Vs2017SetupConfig();
                    var instances = vs2017Info.VSInstances;
                    foreach (var instance in instances)
                    {
                        Console.WriteLine($"ID: {instance.Id} Path: {Path.Combine(instance.InstalledPath, instance.ProductPath)}");
                    }
                    Environment.Exit(0);
                }

                ReadGeneratorSettings();

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
                        VSGlobals.DoGit = true;
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
                        VSGlobals.DoGit = true;
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
                        VSGlobals.DoGit = true;
                        Utility.Shift(ref arglist);
                    }
                }

                foreach (var project in arglist)
                {
                    if (project[0] ==  '-')
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
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }
        }

        private static void ReadGeneratorSettings()
        {
            var assembly = typeof(Program).Assembly;
            var companyAttribute = AssemblyCompanyAttribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute;
            var productAttribute = AssemblyProductAttribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute)) as AssemblyProductAttribute;
            var companyName = companyAttribute?.Company;
            var productName = productAttribute?.Product;
            var specialPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!string.IsNullOrWhiteSpace(companyName) && !string.IsNullOrWhiteSpace(productName) && !string.IsNullOrWhiteSpace(specialPath))
            {
                var folder = Path.Combine(specialPath, companyName, productName);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var filename = Path.Combine(folder, "settings.xml");
                var doc = XDocument.Load(filename);

                var requiredExtensions = doc.Element("Settings").Elements("RequiredExtensions");
                var generatorExtensionDictionary = requiredExtensions.Elements("Generator").ToDictionary(
                    x => x.Attribute("Name").Value,
                    x => new
                    {
                        Guid = x.Element("Extension").Attribute("Guid").Value,
                        Command = x.Element("Extension").Attribute("Command").Value,
                        CommandParameter = x.Element("Extension").Attribute("CommandParameter").Value,
                        Replace = x.Element("Extension").Attribute("Replace").Value
                    }
            );


                foreach (var p in generatorExtensionDictionary)
                {
                    var generatorName = p.Key;
                    var guid = p.Value.Guid;
                    Console.WriteLine($"key:{generatorName} guid:{guid}");
                }

            }


            //var docPath = 
            //XDocument xd = XDocument
        }
    }
}
