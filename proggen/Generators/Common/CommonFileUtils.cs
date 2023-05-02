using System.Xml.Linq;

namespace Proggen.Generators.Common;

internal static class CommonFileUtils
{
    public static string GetCsProj(string netVersion,
                                    string langVersion = "Latest",
                                    string rtti = "",
                                    bool pubSingle = false,
                                    bool selfContained = false,
                                    bool implicitUsings = false,
                                    bool useWpf = false
                                    )
    {
        var projdoc = XDocument.Parse(
        $"""
        <Project Sdk="Microsoft.NET.Sdk">
          <PropertyGroup>
            <OutputType>Exe</OutputType>
            <TargetFramework>{netVersion}</TargetFramework>
            <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
            <RuntimeIdentifier>win-x64</RuntimeIdentifier>
            <DebugType>Embedded</DebugType>
          </PropertyGroup>
        </Project>
        """);

        var propGroupElement = projdoc.Root.Element("PropertyGroup");
        if (pubSingle)
        {
            propGroupElement.Add(new XElement("PublishSingleFile", true));
        }

        if (selfContained)
        {
            propGroupElement.Add(new XElement("SelfContained", true));
        }

        if (implicitUsings)
        {
            propGroupElement.Add(new XElement("ImplicitUsings", "Enable"));
        }

        propGroupElement.Add(new XElement("LangVersion", langVersion));

        if (useWpf)
        {
            propGroupElement.Add(new XElement("UseWpf", true));
        }

        return projdoc.ToString();
    }
}

