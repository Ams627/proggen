using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace proggen
{
    internal static class Settings
    {
        static Dictionary<string, ExtensionCommand> generatorCommandMapping;

        static Settings()
        {
            generatorCommandMapping = new Dictionary<string, ExtensionCommand>();
            ReadGeneratorSettings();
        }

        public static void Go()
        {

        }

        private static void ReadGeneratorSettings()
        {
            var assembly = typeof(Settings).Assembly;
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
                var generatorExtensionDictionary =
                    requiredExtensions.Elements("Generator").Select(x =>
                    {
                        Guid.TryParse(x.Element("Extension").Attribute("Guid").Value, out var guid);
                        return new
                        {
                            ExtGuid = guid,
                            Generator = x.Attribute("Name"),
                            Command = x.Element("Extension").Attribute("Command").Value,
                            Param1 = x.Element("Extension").Attribute("Param1")?.Value,
                            Param2 = x.Element("Extension").Attribute("Param2")?.Value,
                            Param3 = x.Element("Extension").Attribute("Param3")?.Value,
                            Param4 = x.Element("Extension").Attribute("Param4")?.Value
                        };
                    }).ToDictionary(g => g.Generator, g => new ExtensionCommand {
                        ExtensionGuid = g.ExtGuid,
                        Command = g.Command,
                        Param1 = g.Param1,
                        Param2 = g.Param2,
                        Param3 = g.Param3,
                        Param4 = g.Param4 });
                
                var lg = new List<Guid>();
                
                var stuff0 = (from element in doc.Element("Settings").Elements("RequiredExtensions")
                             .Elements("Generator").Elements("Extension")
                              select element
                );


                var stuff = (from element in doc.Element("Settings").Elements("RequiredExtensions")
                             let gen = element.Elements("Generator").Elements("Extension")
                             select gen
                            );

                var stuff2 = doc.Elements("Settings").Elements("RequiredExtensions")
                    .Elements("Generator").Elements("Extension").Select(e => (string)e.Attribute("Guid"));

                //Console.WriteLine($"stuff.Count is {stuff.Count()}");

                var r1 = doc.Element("Settings").Elements("RequiredExtensions");
                var r2 = r1.Elements("Generator").Elements("Extension");
                var r3 = r2.Attributes("Guid");

                //foreach (var p in generatorExtensionDictionary)
                //{
                //    var generatorName = p.Key;
                //    var guid = p.Value.Guid;
                //    Console.WriteLine($"key:{generatorName} guid:{guid}");
                //}

            }
            //var docPath = 
            //XDocument xd = XDocument
        }
        static List<string> GetCommandAndParams(string generatorName)
        {
            return new List<string>();
        }

    }
}

