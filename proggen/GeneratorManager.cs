using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace proggen
{
    class GeneratorManager
    {
        static GeneratorManager()
        {
            Dictionary<string, Type> typeMap = new Dictionary<string, Type>();

            Assembly currAssembly = Assembly.GetExecutingAssembly();

            Type baseType = typeof(DataType);

            foreach (Type type in currAssembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract ||
                    !type.IsSubclassOf(baseType))
                {
                    continue;
                }
            }
        }

        static List<IGenerator> _generators = new List<IGenerator>();
        public void RegisterGenerator(IGenerator g)
        {
            _generators.Add(g);
        }

        public void MakeAllGenerators()
        {
            string fullpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dir = Path.GetDirectoryName(fullpath);
            foreach (var g in _generators)
            {

            }
        }
    }
}
