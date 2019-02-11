﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    static class CSConsoleFileSpecs
    {
        private static string sixteen = new string(' ', 16);
        private static string errorStuff =
            sixteen + "var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;\n" +
            sixteen + "var progname = Path.GetFileNameWithoutExtension(fullname);\n" +
            sixteen + "Console.Error.WriteLine(progname + \": Error: \" + ex.Message);";

        public static FileSpec[] CSConsoleSpecs =
        {
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Program.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "using System;",
                    "using System.Collections.Generic;",
                    "using System.IO;",
                    "using System.Linq;",
                    "using System.Text;",
                    "using System.Threading.Tasks;\n",
                    "namespace $$(PROJECTNAMECAMEL)",
                    "{",
                    "    class Program",
                    "    {",
                    "        private static void Main(string[] args)",
                    "        {",
                    $"            try\n            {{\n    //startstarttypingtypingherehere\n            }}\n            catch (Exception ex)\n            {{\n{errorStuff}\n            }}\n",
                    "        }",
                    "    }",
                    "}"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/App.config",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<?xml version=\"1.0\" encoding=\"utf-8\" ?>",
                    "<configuration>",
                    "    <startup> ",
                    "        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.7.2\" />",
                    "    </startup>",
                    "</configuration>"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Properties/AssemblyInfo.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "using System.Reflection;",
                    "using System.Runtime.CompilerServices;",
                    "using System.Runtime.InteropServices;",
                    "",
                    "// General Information about an assembly is controlled through the following ",
                    "// set of attributes. Change these attribute values to modify the information",
                    "// associated with an assembly.",
                    "[assembly: AssemblyTitle(\"$$(PROJECTNAME)\")]",
                    "[assembly: AssemblyDescription(\"This program was automatically generated by $$(GENERATOR) - AMS 2017\")]",
                    "[assembly: AssemblyConfiguration(\"\")]",
                    "[assembly: AssemblyCompany(\"\")]",
                    "[assembly: AssemblyProduct(\"$$(PROJECTNAME)\")]",
                    "[assembly: AssemblyCopyright(\"Copyright © Whoever put this wonderful program together 2017\")]",
                    "[assembly: AssemblyTrademark(\"\")]",
                    "[assembly: AssemblyCulture(\"\")]",
                    "",
                    "// Setting ComVisible to false makes the types in this assembly not visible ",
                    "// to COM components.  If you need to access a type in this assembly from ",
                    "// COM, set the ComVisible attribute to true on that type.",
                    "[assembly: ComVisible(false)]",
                    "",
                    "// The following GUID is for the ID of the typelib if this project is exposed to COM",
                    $"[assembly: Guid(\"{Guid.NewGuid()}\")]",
                    "",
                    "// Version information for an assembly consists of the following four values:",
                    "//",
                    "//      Major Version",
                    "//      Minor Version ",
                    "//      Build Number",
                    "//      Revision",
                    "//",
                    "// You can specify all the values or you can default the Build and Revision Numbers ",
                    "// by using the '*' as shown below:",
                    "// [assembly: AssemblyVersion(\"1.0.*\")]",
                    "[assembly: AssemblyVersion(\"1.0.0.0\")]",
                    "[assembly: AssemblyFileVersion(\"1.0.0.0\")]"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                                        "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                    "<Project ToolsVersion=\"14.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">",
                    "  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />",
                    "  <PropertyGroup>",
                    "    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>",
                    "    <Platform Condition=\" '$(Platform)' == '' \">$$(SOLUTIONCONFIGNOSPACE)</Platform>",
                    "    <ProjectGuid>{$$(PROJECTGUID)}</ProjectGuid>",
                    "    <OutputType>Exe</OutputType>",
                    "    <AppDesignerFolder>Properties</AppDesignerFolder>",
                    "    <RootNamespace>$$(PROJECTNAMECAMEL)</RootNamespace>",
                    "    <AssemblyName>$$(PROJECTNAMECAMEL)</AssemblyName>",
                    "    <TargetFrameworkVersion>v4.5.2</TargetFrameworkVersion>",
                    "    <FileAlignment>512</FileAlignment>",
                    "    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>",
                    "  </PropertyGroup>",
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|$$(SOLUTIONCONFIGNOSPACE)' \">",
                    "    <PlatformTarget>$$(SOLUTIONCONFIGNOSPACE)</PlatformTarget>",
                    "    <DebugSymbols>true</DebugSymbols>",
                    "    <DebugType>full</DebugType>",
                    "    <Optimize>false</Optimize>",
                    "    <OutputPath>bin\\Debug\\</OutputPath>",
                    "    <DefineConstants>DEBUG;TRACE</DefineConstants>",
                    "    <ErrorReport>prompt</ErrorReport>",
                    "    <WarningLevel>4</WarningLevel>",
                    "  </PropertyGroup>",
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|$$(SOLUTIONCONFIGNOSPACE)' \">",
                    "    <PlatformTarget>$$(SOLUTIONCONFIGNOSPACE)</PlatformTarget>",
                    "    <DebugType>pdbonly</DebugType>",
                    "    <Optimize>true</Optimize>",
                    "    <OutputPath>bin\\Release\\</OutputPath>",
                    "    <DefineConstants>TRACE</DefineConstants>",
                    "    <ErrorReport>prompt</ErrorReport>",
                    "    <WarningLevel>4</WarningLevel>",
                    "  </PropertyGroup>",
                    "  <ItemGroup>",
                    "    <Reference Include=\"System\" />",
                    "    <Reference Include=\"System.Core\" />",
                    "    <Reference Include=\"System.Xml.Linq\" />",
                    "    <Reference Include=\"System.Data.DataSetExtensions\" />",
                    "    <Reference Include=\"Microsoft.CSharp\" />",
                    "    <Reference Include=\"System.Data\" />",
                    "    <Reference Include=\"System.Net.Http\" />",
                    "    <Reference Include=\"System.Xml\" />",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <Compile Include=\"Program.cs\" />",
                    "    <Compile Include=\"Properties\\AssemblyInfo.cs\" />",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <None Include=\"App.config\" />",
                    "  </ItemGroup>",
                    "  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />",
                    "  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. ",
                    "       Other similar extension points exist, see Microsoft.Common.targets.",
                    "  <Target Name=\"BeforeBuild\">",
                    "  </Target>",
                    "  <Target Name=\"AfterBuild\">",
                    "  </Target>",
                    "  -->",
                    "</Project>"
                }
            },
            new FileSpec (CommonFileSpecs.SlnFileSpec),
            new FileSpec (CommonFileSpecs.GitIgnore)
        };
    }
}