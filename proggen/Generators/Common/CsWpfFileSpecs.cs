﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggen.Generators.Common
{
    class CsWpfFileSpecs
    {
        public static FileSpec[] CSWpfSpecs =
        {
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/App.xaml",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<Application x:Class=\"$$(PROJECTNAMECAMEL).App\"",
                    "             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"",
                    "             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"",
                    "             xmlns:local=\"clr-namespace:$$(PROJECTNAMECAMEL)\"",
                    "             StartupUri=\"MainWindow.xaml\">",
                    "    <Application.Resources>",
                    "",
                    "    </Application.Resources>",
                    "</Application>",
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/App.xaml.cs",
                Contents = new [] {
                    "using System;",
                    "using System.Collections.Generic;",
                    "using System.Configuration;",
                    "using System.Data;",
                    "using System.Linq;",
                    "using System.Threading.Tasks;",
                    "using System.Windows;",
                    "",
                    "namespace $$(PROJECTNAMECAMEL)",
                    "{",
                    "    /// <summary>",
                    "    /// Interaction logic for App.xaml",
                    "    /// </summary>",
                    "    public partial class App : Application",
                    "    {",
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
                    "        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5.2\" />",
                    "    </startup>",
                    "</configuration>"
                }
            },

            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/MainWindow.xaml",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<Window x:Class=\"$$(PROJECTNAMECAMEL).MainWindow\"",
                    "        xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"",
                    "        xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"",
                    "        xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"",
                    "        xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"",
                    "        xmlns:local=\"clr-namespace:$$(PROJECTNAMECAMEL)\"",
                    "        mc:Ignorable=\"d\"",
                    "        Title=\"MainWindow\" Height=\"350\" Width=\"525\">",
                    "    <Grid>",
                    "        ",
                    "    </Grid>",
                    "</Window>"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/MainWindow.xaml.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                     "using System;",
                     "using System.Collections.Generic;",
                     "using System.Linq;",
                     "using System.Text;",
                     "using System.Threading.Tasks;",
                     "using System.Windows;",
                     "using System.Windows.Controls;",
                     "using System.Windows.Data;",
                     "using System.Windows.Documents;",
                     "using System.Windows.Input;",
                     "using System.Windows.Media;",
                     "using System.Windows.Media.Imaging;",
                     "using System.Windows.Navigation;",
                     "using System.Windows.Shapes;",
                     "",
                     "namespace $$(PROJECTNAMECAMEL)",
                     "{",
                     "    public partial class MainWindow : Window",
                     "    {",
                     "        public MainWindow()",
                     "        {",
                     "            InitializeComponent();",
                     "            //startstarttypingtypingherehere",
                     "        }",
                     "    }",
                     "}"
                }
            },

            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Properties/AssemblyInfo.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "using System.Reflection;",
                    "using System.Resources;",
                    "using System.Runtime.CompilerServices;",
                    "using System.Runtime.InteropServices;",
                    "using System.Windows;",
                    "",
                    "// General Information about an assembly is controlled through the following ",
                    "// set of attributes. Change these attribute values to modify the information",
                    "// associated with an assembly.",
                    "[assembly: AssemblyTitle(\"$$(PROJECTNAMECAMEL)\")]",
                    "[assembly: AssemblyDescription(\"\")]",
                    "[assembly: AssemblyConfiguration(\"\")]",
                    "[assembly: AssemblyCompany(\"\")]",
                    "[assembly: AssemblyProduct(\"$$(PROJECTNAMECAMEL)\")]",
                    "[assembly: AssemblyCopyright(\"Copyright Ã‚Â©  2015\")]",
                    "[assembly: AssemblyTrademark(\"\")]",
                    "[assembly: AssemblyCulture(\"\")]",
                    "",
                    "// Setting ComVisible to false makes the types in this assembly not visible ",
                    "// to COM components.  If you need to access a type in this assembly from ",
                    "// COM, set the ComVisible attribute to true on that type.",
                    "[assembly: ComVisible(false)]",
                    "",
                    "//In order to begin building localizable applications, set ",
                    "//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file",
                    "//inside a <PropertyGroup>.  For example, if you are using US english",
                    "//in your source files, set the <UICulture> to en-US.  Then uncomment",
                    "//the NeutralResourceLanguage attribute below.  Update the \"en-US\" in",
                    "//the line below to match the UICulture setting in the project file.",
                    "",
                    "//[assembly: NeutralResourcesLanguage(\"en-US\", UltimateResourceFallbackLocation.Satellite)]",
                    "",
                    "",
                    "[assembly: ThemeInfo(",
                    "    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located",
                    "                                     //(used if a resource is not found in the page, ",
                    "                                     // or application resource dictionaries)",
                    "    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located",
                    "                                              //(used if a resource is not found in the page, ",
                    "                                              // app, or any theme specific resource dictionaries)",
                    ")]",
                    "",
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
                Pathname = "$$(PROJECTNAMECAMEL)/app.config",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<?xml version=\"1.0\" encoding=\"utf-8\" ?>",
                    "<configuration>",
                    "    <startup>",
                    "        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5.2\" />",
                    "    </startup>",
                    "</configuration>"
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Properties/Settings.settings",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<?xml version='1.0' encoding='utf-8'?>",
                    "  <Profiles>",
                    "<SettingsFile xmlns=\"uri:settings\" CurrentProfile=\"(Default)\">",
                    "    <Profile Name=\"(Default)\" />",
                    "  </Profiles>",
                    "  <Settings />",
                    "</SettingsFile>",
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Properties/Resources.Designer.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "//------------------------------------------------------------------------------",
                    "// <auto-generated>",
                    "//     This code was generated by a tool.",
                    "//     Runtime Version:4.0.30319.0",
                    "//",
                    "//     Changes to this file may cause incorrect behavior and will be lost if",
                    "//     the code is regenerated.",
                    "// </auto-generated>",
                    "//------------------------------------------------------------------------------",
                    "",
                    "namespace $$(PROJECTNAMECAMEL).Properties",
                    "{",
                    "",
                    "",
                    "    /// <summary>",
                    "    ///   A strongly-typed resource class, for looking up localized strings, etc.",
                    "    /// </summary>",
                    "    // This class was auto-generated by the StronglyTypedResourceBuilder",
                    "    // class via a tool like ResGen or Visual Studio.",
                    "    // To add or remove a member, edit your .ResX file then rerun ResGen",
                    "    // with the /str option, or rebuild your VS project.",
                    "    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"System.Resources.Tools.StronglyTypedResourceBuilder\", \"4.0.0.0\")]",
                    "    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]",
                    "    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]",
                    "    internal class Resources",
                    "    {",
                    "",
                    "        private static global::System.Resources.ResourceManager resourceMan;",
                    "",
                    "        private static global::System.Globalization.CultureInfo resourceCulture;",
                    "",
                    "        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"Microsoft.Performance\", \"CA1811:AvoidUncalledPrivateCode\")]",
                    "        internal Resources()",
                    "        {",
                    "        }",
                    "",
                    "        /// <summary>",
                    "        ///   Returns the cached ResourceManager instance used by this class.",
                    "        /// </summary>",
                    "        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]",
                    "        internal static global::System.Resources.ResourceManager ResourceManager",
                    "        {",
                    "            get",
                    "            {",
                    "                if ((resourceMan == null))",
                    "                {",
                    "                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(\"$$(PROJECTNAMECAMEL).Properties.Resources\", typeof(Resources).Assembly);",
                    "                    resourceMan = temp;",
                    "                }",
                    "                return resourceMan;",
                    "            }",
                    "        }",
                    "",
                    "        /// <summary>",
                    "        ///   Overrides the current thread's CurrentUICulture property for all",
                    "        ///   resource lookups using this strongly typed resource class.",
                    "        /// </summary>",
                    "        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]",
                    "        internal static global::System.Globalization.CultureInfo Culture",
                    "        {",
                    "            get",
                    "            {",
                    "                return resourceCulture;",
                    "            }",
                    "            set",
                    "            {",
                    "                resourceCulture = value;",
                    "            }",
                    "        }",
                    "    }",
                    "}",
                }
            },
            new FileSpec {
                Pathname = "$$(PROJECTNAMECAMEL)/Properties/Resources.resx",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                    "<root>",
                    "  <!-- ",
                    "    Microsoft ResX Schema ",
                    "    ",
                    "    Version 2.0",
                    "    ",
                    "    The primary goals of this format is to allow a simple XML format ",
                    "    that is mostly human readable. The generation and parsing of the ",
                    "    various data types are done through the TypeConverter classes ",
                    "    associated with the data types.",
                    "    ",
                    "    Example:",
                    "    ",
                    "    ... ado.net/XML headers & schema ...",
                    "    <resheader name=\"resmimetype\">text/microsoft-resx</resheader>",
                    "    <resheader name=\"version\">2.0</resheader>",
                    "    <resheader name=\"reader\">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>",
                    "    <resheader name=\"writer\">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>",
                    "    <data name=\"Name1\"><value>this is my long string</value><comment>this is a comment</comment></data>",
                    "    <data name=\"Color1\" type=\"System.Drawing.Color, System.Drawing\">Blue</data>",
                    "    <data name=\"Bitmap1\" mimetype=\"application/x-microsoft.net.object.binary.base64\">",
                    "        <value>[base64 mime encoded serialized .NET Framework object]</value>",
                    "    </data>",
                    "    <data name=\"Icon1\" type=\"System.Drawing.Icon, System.Drawing\" mimetype=\"application/x-microsoft.net.object.bytearray.base64\">",
                    "        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>",
                    "        <comment>This is a comment</comment>",
                    "    </data>",
                    "                ",
                    "    There are any number of \"resheader\" rows that contain simple ",
                    "    name/value pairs.",
                    "    ",
                    "    Each data row contains a name, and value. The row also contains a ",
                    "    type or mimetype. Type corresponds to a .NET class that support ",
                    "    text/value conversion through the TypeConverter architecture. ",
                    "    Classes that don't support this are serialized and stored with the ",
                    "    mimetype set.",
                    "    ",
                    "    The mimetype is used for serialized objects, and tells the ",
                    "    ResXResourceReader how to depersist the object. This is currently not ",
                    "    extensible. For a given mimetype the value must be set accordingly:",
                    "    ",
                    "    Note - application/x-microsoft.net.object.binary.base64 is the format ",
                    "    that the ResXResourceWriter will generate, however the reader can ",
                    "    read any of the formats listed below.",
                    "    ",
                    "    mimetype: application/x-microsoft.net.object.binary.base64",
                    "    value   : The object must be serialized with ",
                    "            : System.Serialization.Formatters.Binary.BinaryFormatter",
                    "            : and then encoded with base64 encoding.",
                    "    ",
                    "    mimetype: application/x-microsoft.net.object.soap.base64",
                    "    value   : The object must be serialized with ",
                    "            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter",
                    "            : and then encoded with base64 encoding.",
                    "",
                    "    mimetype: application/x-microsoft.net.object.bytearray.base64",
                    "    value   : The object must be serialized into a byte array ",
                    "            : using a System.ComponentModel.TypeConverter",
                    "            : and then encoded with base64 encoding.",
                    "    -->",
                    "  <xsd:schema id=\"root\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">",
                    "    <xsd:element name=\"root\" msdata:IsDataSet=\"true\">",
                    "      <xsd:complexType>",
                    "        <xsd:choice maxOccurs=\"unbounded\">",
                    "          <xsd:element name=\"metadata\">",
                    "            <xsd:complexType>",
                    "              <xsd:sequence>",
                    "                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" />",
                    "              </xsd:sequence>",
                    "              <xsd:attribute name=\"name\" type=\"xsd:string\" />",
                    "              <xsd:attribute name=\"type\" type=\"xsd:string\" />",
                    "              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" />",
                    "            </xsd:complexType>",
                    "          </xsd:element>",
                    "          <xsd:element name=\"assembly\">",
                    "            <xsd:complexType>",
                    "              <xsd:attribute name=\"alias\" type=\"xsd:string\" />",
                    "              <xsd:attribute name=\"name\" type=\"xsd:string\" />",
                    "            </xsd:complexType>",
                    "          </xsd:element>",
                    "          <xsd:element name=\"data\">",
                    "            <xsd:complexType>",
                    "              <xsd:sequence>",
                    "                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />",
                    "                <xsd:element name=\"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\" />",
                    "              </xsd:sequence>",
                    "              <xsd:attribute name=\"name\" type=\"xsd:string\" msdata:Ordinal=\"1\" />",
                    "              <xsd:attribute name=\"type\" type=\"xsd:string\" msdata:Ordinal=\"3\" />",
                    "              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\" />",
                    "            </xsd:complexType>",
                    "          </xsd:element>",
                    "          <xsd:element name=\"resheader\">",
                    "            <xsd:complexType>",
                    "              <xsd:sequence>",
                    "                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />",
                    "              </xsd:sequence>",
                    "              <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" />",
                    "            </xsd:complexType>",
                    "          </xsd:element>",
                    "        </xsd:choice>",
                    "      </xsd:complexType>",
                    "    </xsd:element>",
                    "  </xsd:schema>",
                    "  <resheader name=\"resmimetype\">",
                    "    <value>text/microsoft-resx</value>",
                    "  </resheader>",
                    "  <resheader name=\"version\">",
                    "    <value>2.0</value>",
                    "  </resheader>",
                    "  <resheader name=\"reader\">",
                    "    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>",
                    "  </resheader>",
                    "  <resheader name=\"writer\">",
                    "    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>",
                    "  </resheader>",
                    "</root>"
                }
            },
            new FileSpec
            {
                Pathname = "$$(PROJECTNAMECAMEL)/Properties/Settings.Designer.cs",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "//------------------------------------------------------------------------------",
                    "// <auto-generated>",
                    "//     This code was generated by a tool.",
                    "//     Runtime Version:4.0.30319.0",
                    "//",
                    "//     Changes to this file may cause incorrect behavior and will be lost if",
                    "//     the code is regenerated.",
                    "// </auto-generated>",
                    "//------------------------------------------------------------------------------",
                    "",
                    "namespace $$(PROJECTNAMECAMEL).Properties",
                    "{",
                    "",
                    "",
                    "    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]",
                    "    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator\", \"11.0.0.0\")]",
                    "    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase",
                    "    {",
                    "",
                    "        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));",
                    "",
                    "        public static Settings Default",
                    "        {",
                    "            get",
                    "            {",
                    "                return defaultInstance;",
                    "            }",
                    "        }",
                    "    }",
                    "}"
                }
            },
            new FileSpec
            {
                Pathname = "$$(PROJECTNAMECAMEL)/$$(PROJECTNAMECAMEL).$$(SUFFIX)",
                Contents = new [] {
                    "\uFEFF" + // prepended BOM
                    "<Project ToolsVersion=\"14.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">",
                    "  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />",
                    "  <PropertyGroup>",
                    "    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>",
                    "    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>",
                    "    <ProjectGuid>$$(PROJECTGUID)</ProjectGuid>",
                    "    <OutputType>WinExe</OutputType>",
                    "    <AppDesignerFolder>Properties</AppDesignerFolder>",
                    "    <RootNamespace>$$(PROJECTNAMECAMEL)</RootNamespace>",
                    "    <AssemblyName>$$(PROJECTNAMECAMEL)</AssemblyName>",
                    "    <TargetFrameworkVersion>v4.5.2</TargetFrameworkVersion>",
                    "    <FileAlignment>512</FileAlignment>",
                    "    <ProjectTypeGuids>{60dc8134-eba5-43b8-bcc9-bb4bc16c2548};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</ProjectTypeGuids>",
                    "    <WarningLevel>4</WarningLevel>",
                    "    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>",
                    "  </PropertyGroup>",
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">",
                    "    <PlatformTarget>AnyCPU</PlatformTarget>",
                    "    <DebugSymbols>true</DebugSymbols>",
                    "    <DebugType>full</DebugType>",
                    "    <Optimize>false</Optimize>",
                    "    <OutputPath>bin\\Debug\\</OutputPath>",
                    "    <DefineConstants>DEBUG;TRACE</DefineConstants>",
                    "    <ErrorReport>prompt</ErrorReport>",
                    "    <WarningLevel>4</WarningLevel>",
                    "  </PropertyGroup>",
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">",
                    "    <PlatformTarget>AnyCPU</PlatformTarget>",
                    "    <DebugType>pdbonly</DebugType>",
                    "    <Optimize>true</Optimize>",
                    "    <OutputPath>bin\\Release\\</OutputPath>",
                    "    <DefineConstants>TRACE</DefineConstants>",
                    "    <ErrorReport>prompt</ErrorReport>",
                    "    <WarningLevel>4</WarningLevel>",
                    "  </PropertyGroup>",
                    "  <ItemGroup>",
                    "    <Reference Include=\"System\" />",
                    "    <Reference Include=\"System.Data\" />",
                    "    <Reference Include=\"System.Xml\" />",
                    "    <Reference Include=\"Microsoft.CSharp\" />",
                    "    <Reference Include=\"System.Core\" />",
                    "    <Reference Include=\"System.Xml.Linq\" />",
                    "    <Reference Include=\"System.Data.DataSetExtensions\" />",
                    "    <Reference Include=\"System.Net.Http\" />",
                    "    <Reference Include=\"System.Xaml\">",
                    "      <RequiredTargetFramework>4.0</RequiredTargetFramework>",
                    "    </Reference>",
                    "    <Reference Include=\"WindowsBase\" />",
                    "    <Reference Include=\"PresentationCore\" />",
                    "    <Reference Include=\"PresentationFramework\" />",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <ApplicationDefinition Include=\"App.xaml\">",
                    "      <Generator>MSBuild:Compile</Generator>",
                    "      <SubType>Designer</SubType>",
                    "    </ApplicationDefinition>",
                    "    <Page Include=\"MainWindow.xaml\">",
                    "      <Generator>MSBuild:Compile</Generator>",
                    "      <SubType>Designer</SubType>",
                    "    </Page>",
                    "    <Compile Include=\"App.xaml.cs\">",
                    "      <DependentUpon>App.xaml</DependentUpon>",
                    "      <SubType>Code</SubType>",
                    "    </Compile>",
                    "    <Compile Include=\"MainWindow.xaml.cs\">",
                    "      <DependentUpon>MainWindow.xaml</DependentUpon>",
                    "      <SubType>Code</SubType>",
                    "    </Compile>",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <Compile Include=\"Properties\\AssemblyInfo.cs\">",
                    "      <SubType>Code</SubType>",
                    "    </Compile>",
                    "    <Compile Include=\"Properties\\Resources.Designer.cs\">",
                    "      <AutoGen>True</AutoGen>",
                    "      <DesignTime>True</DesignTime>",
                    "      <DependentUpon>Resources.resx</DependentUpon>",
                    "    </Compile>",
                    "    <Compile Include=\"Properties\\Settings.Designer.cs\">",
                    "      <AutoGen>True</AutoGen>",
                    "      <DependentUpon>Settings.settings</DependentUpon>",
                    "      <DesignTimeSharedInput>True</DesignTimeSharedInput>",
                    "    </Compile>",
                    "    <EmbeddedResource Include=\"Properties\\Resources.resx\">",
                    "      <Generator>ResXFileCodeGenerator</Generator>",
                    "      <LastGenOutput>Resources.Designer.cs</LastGenOutput>",
                    "    </EmbeddedResource>",
                    "    <None Include=\"Properties\\Settings.settings\">",
                    "      <Generator>SettingsSingleFileGenerator</Generator>",
                    "      <LastGenOutput>Settings.Designer.cs</LastGenOutput>",
                    "    </None>",
                    "    <AppDesigner Include=\"Properties\\\" />",
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

