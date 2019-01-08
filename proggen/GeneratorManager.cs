using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;
using Microsoft.Win32;

namespace Proggen
{
    class GeneratorManager
    {
        private const string InstanceEnvVar = "VSDefaultInstanceID";
        private static HashSet<string> _generatorNames;
        public static List<string> HelpTexts { get; private set;}
        static GeneratorManager()
        {
            HelpTexts = new List<string>();
            var currAssembly = Assembly.GetEntryAssembly();
            _generatorNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var type in currAssembly.GetTypes())
            {
                if (type.IsDefined(typeof(AutoRegisterAttribute)))
                {
                    if (type.IsSubclassOf(typeof(ProgramGenerator)))
                    {
                        var generator = (ProgramGenerator)Activator.CreateInstance(type);
                        var names = generator.Name.Split('|');

                        names.ForEachExceptTheLast(
                            x => HelpTexts.Add($"{x} or"),
                            x => HelpTexts.Add($"{names.Last(),-10}{generator.Description}")
                            );

                        foreach (var name in names)
                        {
                            Generators.Add(name, generator);
                            _generatorNames.Add(name);
                        }
                    }
                }
            }
        }

        private static readonly Dictionary<string, ProgramGenerator> Generators = new Dictionary<string, ProgramGenerator>(StringComparer.OrdinalIgnoreCase);

        public static void MakeAllGenerators()
        {
            var fullpath = System.Reflection.Assembly.GetEntryAssembly().Location;
            var dir = Path.GetDirectoryName(fullpath);
            foreach (var g in Generators)
            {
                foreach (var name in g.Value.Name.Split('|'))
                {
                    var fullGeneratorPath = Path.Combine(dir, name.Trim()) + ".exe";
                    if (!File.Exists(fullGeneratorPath))
                    {
                        File.Copy(fullpath, fullGeneratorPath);
                    }
                }
            }
        }

        public static bool IsAGenarator(string s)
        {
            return _generatorNames.Contains(s);
        }

        public static void Generate(string progname)
        {
            ProgramGenerator generator;
            if (Generators.TryGetValue(progname, out generator))
            {
                generator.Generate();
                StartVisualStudio(generator.VSVersion);
            }
            else
            {
                throw new Exception("There is no generator called '" + progname + "'");
            }
        }

        public static void StartVisualStudio(string vsVersion)
        {
            var vsExecutableName="";
            var command = string.IsNullOrWhiteSpace(VSGlobals.VSCommand) ? "" : VSGlobals.VSCommand;
            var commandParam = "";

            if (vsVersion == "2010")
            {
                var regkey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\10.0";
                vsExecutableName = (string)Registry.GetValue(regkey, "InstallDir", "");
                vsExecutableName += "devenv.exe";
            }
            else if (vsVersion == "2012")
            {
                var regkey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\11.0";
                vsExecutableName = (string)Registry.GetValue(regkey, "InstallDir", "");
                vsExecutableName += "devenv.exe";
            }
            else if (vsVersion == "2013")
            {
                var regkey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\12.0";
                vsExecutableName = (string)Registry.GetValue(regkey, "InstallDir", "");
                vsExecutableName += "devenv.exe";
            }
            else if (vsVersion == "2015")
            {
                var regkey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\14.0";
                vsExecutableName = (string)Registry.GetValue(regkey, "InstallDir", "");
                vsExecutableName += "devenv.exe";
            }
            else if (vsVersion == "2017" || vsVersion == "2019")
            {
                // Both VS2017 and VS2019 have the same "private registry" mechanism for storing installed instances:
                var vsConfig = new VS2017Info.Vs2017SetupConfig();
                var instances = vsConfig.VSInstances;
                var instanceToStart = instances[0];

                // first get the numeric version number of the version of Visual Studio we wish to start. For VS2017, this is 15, for 
                // VS2019 this is 16.
                var ver = vsVersion == "2017" ? 15 : 16;

                //Console.WriteLine($"Looking for numeric version ID {ver}");

                // get instances of the specified version of Visual Studio:
                var instancesForVersion = instances.Where(x =>
                {
                    var dotPos = x.Version.IndexOf(".");
                    var numericVersion = Convert.ToInt32(x.Version.Substring(0, dotPos));
                    return numericVersion == ver;
                });


                //instancesForVersion.ToList().ForEach(x => Console.WriteLine($"Found instance {x.Id} {x.InstalledPath}"));

                if (instancesForVersion.Count() > 1)
                {

                    var defaultInstanceId = ver == 15 ? Environment.GetEnvironmentVariable("VSDefaultInstanceID") : Environment.GetEnvironmentVariable("VSDefaultInstanceID19");

                    if (!string.IsNullOrWhiteSpace(defaultInstanceId))
                    {
                        foreach(var instance in instances)
                        {
                            if (instance.Id == defaultInstanceId)
                            {
                                instanceToStart = instance;
                            }
                        }
                    }
                    else
                    {
                        var message = "Cannot start visual studio as I do not know which installed instance to use - it could be: \n";
                        foreach (var instance in instances)
                        {
                            message += Path.Combine(instance.InstalledPath, instance.ProductPath) + "\n";
                        }
                        message += $"Set the environment variable {InstanceEnvVar} to the instance ID you want to use to resolve this.";
                        throw new Exception(message);
                    }
                }
                vsExecutableName = Path.Combine(instanceToStart.InstalledPath, instanceToStart.ProductPath);

                // find out if AMSExtensions present:
                var extensions = VS2017Info.VS2017AppData.GetInstalledExtensions(instances[0].Version, instances[0].Id);
                if (extensions != null)
                {
                    var amsExtPresent = extensions.Any(x => !string.IsNullOrWhiteSpace(x.Key) && x.Key.Split(',')[0] == "3c6063ca-5f33-4889-aaae-387e9d5a0368");
                    if (amsExtPresent)
                    {
                        Console.WriteLine("AMS extensions found");
                        commandParam = VSGlobals.VSCommandParam;
                    }
                }
            }

            Process process = new Process();
            process.StartInfo.FileName = vsExecutableName;

            // if command has a parameter then the whole command-plus-parameter string needs to be in quotes:
            if (!string.IsNullOrWhiteSpace(command))
            {
                if (!string.IsNullOrWhiteSpace(commandParam))
                {
                    command = "\"" + command + " " + commandParam + "\"";
                }
                else
                {
                    command = VSGlobals.VSCommand;
                }
                command = "/command " + command;
            }

            if (!string.IsNullOrWhiteSpace(VSGlobals.VSCommandParam))
            {
                command = command + "";
                // command = "\"" + command + " " + VSGlobals.VSCommandParam + "\"";
            }
            process.StartInfo.Arguments = Path.Combine(VSGlobals.ProjectName, VSGlobals.ProjectName + ".sln") + " " + command;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
            process.WaitForInputIdle();

            Console.WriteLine($"{process.Id} {vsExecutableName} {command}");
        }
    }
}
