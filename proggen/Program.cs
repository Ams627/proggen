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
        static void Main(string[] args)
        {
            try
            {
                var p = new GeneratorManager();
                p.MakeAllGenerators();
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var progname = Path.GetFileNameWithoutExtension(codeBase);
                if (args.Count() < 2)
                {
                    throw new Exception("You must supply at least two arguments");
                }
                var generator = args[0];
                var dirname = args[1];
                if (File.Exists(dirname))
                {
                    throw new Exception($"File {dirname} already exists.");
                }
                VSMacros.ProjectName = dirname;
                GeneratorManager.Generate(generator);
            }
            catch (Exception ex)
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                string progname = Path.GetFileNameWithoutExtension(codeBase);
                Console.Error.WriteLine(progname + ": Error: " + ex.ToString());
            }
        }
    }
}
