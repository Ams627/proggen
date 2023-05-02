namespace Proggen.Generators.Common;

internal class CsWpfFileSpecs
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
                """
                using System;
                using System.Collections.Generic;
                using System.Configuration;
                using System.Data;
                using System.Linq;
                using System.Threading.Tasks;
                using System.Text.RegularExpressions;
                using System.Xml.Linq;
                using System.Windows;

                namespace $$(PROJECTNAMECAMEL);

                public partial class App : Application
                {
                }
                """
            }
        },

        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/ViewModel.cs",
            Contents = new [] {
                """
                using System;
                using System.Windows;
                using System.Collections.Generic;
                using System.Linq;
                
                namespace $$(PROJECTNAMECAMEL);

                public class ViewModel
                {
                    public Thickness MyMargin { get; set; } = new Thickness(50, 50, 50, 50);
                }
                """
            }
        },

        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Converters/ConverterBase.cs",
            Contents = new [] {
                """
                using System;
                using System.Windows.Markup;
                using System.Collections.Generic;
                using System.Linq;
                
                namespace $$(PROJECTNAMECAMEL);

                public class ConverterBase : MarkupExtension
                {
                    public override object ProvideValue(IServiceProvider serviceProvider)
                    {
                        return this;
                    }
                }
                """
            }
        },

        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/Converters/MyValueConverter.cs",
            Contents = new [] {
                """
                using System;
                using System.Windows;
                using System.Windows.Data;
                using System.Globalization;
                using System.Collections.Generic;
                using System.Linq;
                
                namespace $$(PROJECTNAMECAMEL);

                public class MyValueConverter : ConverterBase, IValueConverter
                {
                    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
                    {
                        if (value is Thickness thickness)
                        {
                            System.Diagnostics.Debug.WriteLine($"Thickness : {thickness.Left},{thickness.Top},{thickness.Right},{thickness.Bottom}");
                        }
                        return value;
                    }
                    
                    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
                    {
                        throw new NotImplementedException();
                    }
                }
                """
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
                "<Window.DataContext>",
                "    <local:ViewModel/>",
                "</Window.DataContext>",
                "    <Grid>",
                "        <TextBlock Text=\"Hello, World!\" FontSize=\"20\" Margin=\"{Binding MyMargin, Converter={local:MyValueConverter}}\"/>",
                "    </Grid>",
                "</Window>"
            }
        },
        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/MainWindow.xaml.cs",
            Contents = new [] {
                "\uFEFF" + // prepended BOM
                """
                using System;
                using System.Collections.Generic;
                using System.Linq;
                using System.Text;
                using System.Threading.Tasks;
                using System.Windows;
                using System.Windows.Controls;
                using System.Windows.Data;
                using System.Windows.Documents;
                using System.Windows.Input;
                using System.Windows.Media;
                using System.Windows.Media.Imaging;
                using System.Windows.Navigation;
                using System.Windows.Shapes;
                
                namespace $$(PROJECTNAMECAMEL);
                
                public partial class MainWindow : Window
                {
                    public MainWindow()
                    {
                        InitializeComponent();
                        //startstarttypingtypingherehere
                    }
                }
                
                """
            }
        },

        new FileSpec {
            Pathname = "$$(PROJECTNAMECAMEL)/app.config",
            Contents = new [] {
                "\uFEFF" + // prepended BOM
                "<?xml version=\"1.0\" encoding=\"utf-8\" ?>",
                "<configuration>",
                "    <startup>",
                "        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.7.2\" />",
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
                CommonFileUtils.GetCsProj("net48", useWpf:true),
            }
        },
                //XElement.Parse(@"<Project Sdk=""Microsoft.NET.Sdk"">
                //  <PropertyGroup>
                //    <OutputType>Exe</OutputType>
                //    <TargetFramework>net472</TargetFramework>
                //    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
                //    <UseWpf>true</UseWpf>
                //    <ApplicationIcon />
                //    <StartupObject />
                //  </PropertyGroup>
                //  <ItemGroup>
                //    <Reference Include=""PresentationCore"" />
                //    <Reference Include=""PresentationFramework"" />
                //    <Reference Include=""System.Xaml"" />
                //  </ItemGroup>
                //</Project>").ToString()
                //                }
                //          },
        new FileSpec(CommonFileSpecs.SlnFileSpec),
        new FileSpec(CommonFileSpecs.GitIgnore),
        new FileSpec(CommonFileSpecs.PolyFill)
    };
}

