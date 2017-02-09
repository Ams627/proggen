using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    // Derive from this class to implement macros against public properties using the CRTP
    // Simply add the attribute [Macro("MACRONAME")] attribute to a public property in the derived
    // class
    public class MacroBase<T> where T : MacroBase<T>
    {
        static Dictionary<string, MemberInfo> macroToPropertyMapping = new Dictionary<string, MemberInfo>();
        static MacroBase()
        {
            var mytype = typeof(T);
            var properties = mytype.GetProperties();
            var fields = mytype.GetFields();
            var memberlist = fields.ToList<MemberInfo>().Concat(properties.ToList<MemberInfo>());

            var pcount = properties.Count();
            Console.WriteLine("count is {0}", pcount);
            var attType = typeof(MacroAttribute);

            foreach (var prop in memberlist)
            {
                var macro = prop.GetCustomAttribute(attType, true) as MacroAttribute;
                if (macro != null)
                {
                    var name = macro.Macroname;
                    macroToPropertyMapping[name] = prop;
                }
            }

            //var fields = mytype.GetFields().ToList();
            //var conc = fields.Concat(properties);
        }

        public static string ExpandMacros(string input)
        {
            string pattern = @"\$\$\(([A-Z]+)\)";
            var matches = Regex.Matches(input, pattern);
            var outputString = "";
            var currentIndex = 0;

            foreach (Match match in matches)
            {
                outputString += input.Substring(currentIndex, match.Index - currentIndex);
                currentIndex = match.Index + match.Length;

                bool matchfound = false;
                if (match.Groups.Count > 1)
                {
                    var macroname = match.Groups[1].Value;
                    MemberInfo memberInfo;
                    if (!macroToPropertyMapping.TryGetValue(macroname, out memberInfo))
                    {
                        throw new Exception($"Undefined Macro {macroname} used.");
                    }
                    matchfound = true;
                    string result;
                    if (memberInfo is PropertyInfo)
                    {
                        var pi = memberInfo as PropertyInfo;
                        result = pi.GetValue(null).ToString();
                        if (pi.PropertyType == typeof(Guid))
                        {
                            result = result.ToUpper();
                        }
                    }
                    else if (memberInfo is FieldInfo)
                    {
                        var fi = memberInfo as FieldInfo;
                        result = fi.GetValue(null).ToString();
                        if (fi.FieldType == typeof(Guid))
                        {
                            result = result.ToUpper();
                        }
                    }
                    else
                    {
                        result = "MACRO EXPANSION ERROR";
                    }
                    var t = memberInfo.GetType();
                    if (memberInfo.GetType() == typeof(Guid))
                    {
                        result = result.ToUpper();
                    }
                    outputString += result;
                }

                if (!matchfound)
                {
                    outputString += input.Substring(match.Index, match.Length);
                }
            }
            if (matches.Count > 0)
            {
                var lastIndex = matches[matches.Count - 1].Index + matches[matches.Count - 1].Length;
                outputString += input.Substring(lastIndex);
            }
            else
            {
                outputString = input;
            }
            return outputString;
        }
    }
}
