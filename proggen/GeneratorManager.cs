﻿using System;
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
        static GeneratorManager()
        {
            var currAssembly = Assembly.GetExecutingAssembly();

            foreach (var type in currAssembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(ProgramGenerator)))
                {
                    Console.WriteLine(type.ToString());
                    var generator = (ProgramGenerator)Activator.CreateInstance(type);
                    Generators.Add(generator.Name, generator);
                }
            }
        }

        private static readonly Dictionary<string, ProgramGenerator> Generators = new Dictionary<string, ProgramGenerator>();

        public void MakeAllGenerators()
        {
            var fullpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var dir = Path.GetDirectoryName(fullpath);
            foreach (var g in Generators)
            {
                var fullGeneratorPath = Path.Combine(dir, g.Value.Name) + ".exe";
                if (!File.Exists(fullGeneratorPath))
                {
                    File.Copy(fullpath, fullGeneratorPath);
                }
            }
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
            else if (vsVersion == "2017")
            {
                var vsConfig = new Vs2017Config();
                var paths = vsConfig.InstallationPaths;
                if (paths.Count > 1)
                {
                    var message = "Cannot start visual studio as I do not know which installed instance to use - it could be: \n";
                    foreach (var path in paths)
                    {
                        message += path + "\n";
                    }
                    throw new Exception(message);
                }
                vsExecutableName = paths[0];
            }

            Process process = new Process();
            process.StartInfo.FileName = vsExecutableName;
            var command = String.IsNullOrWhiteSpace(VSMacros.VSCommand) ? "" : " /command " + VSMacros.VSCommand;
            process.StartInfo.Arguments = Path.Combine(VSMacros.ProjectName, VSMacros.ProjectName + ".sln") + command;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
        }
    }
}