namespace Proggen.Generators.Common;

class MacroAttribute : System.Attribute
{
    public string Macroname { get; set; }
    public MacroAttribute(string macroName)
    {
        Macroname = macroName;
    }
}
