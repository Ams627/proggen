using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggen.Generators.Common;

namespace Proggen.Generators.Common
{
    static class CPPFileSpecs
    {
        public static FileSpec[] CPPSpecs { get; } =
        {
            new FileSpec {
                Pathname = "$$(PROJECTNAME)/$$(PROJECTNAME).cpp",
                Contents = new [] {
                    "#include \"stdafx.h\"\n",
                    "// DELETE THIS COMMENT BLOCK OR USE WHAT IS IN IT!",
                    "// Use the following line to make a windows program with main startup (rather than a console program)",
                    "// Remove the /entry part if you want to use WinMain instead...",
                    "//",
                    "//#pragma comment(linker, \"/subsystem:windows /entry:mainCRTStartup\")",
                    "//",
                    "//",
                    "// This is the syntax for including libraries:",
                    "//",
                    "//#pragma comment(lib,\"comctl32.lib\")",
                    "",
                    "int main()",
                    "{\n",
                    "}"
                }
            },
            new FileSpec
            {
                Pathname = "$$(PROJECTNAME)/$$(PROJECTNAME).$$(SUFFIX).filters",

                Contents = new[]
                {
                    "\uFEFF" + // prepended BOM
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                    "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">",
                    "  <ItemGroup>",
                    "    <Filter Include=\"Source Files\">",
                    "      <UniqueIdentifier>{4FC737F1-C7A5-4376-A066-2A32D752A2FF}</UniqueIdentifier>",
                    "      <Extensions>cpp;c;cc;cxx;def;odl;idl;hpj;bat;asm;asmx</Extensions>",
                    "    </Filter>",
                    "    <Filter Include=\"Header Files\">",
                    "      <UniqueIdentifier>{93995380-89BD-4b04-88EB-625FBE52EBFB}</UniqueIdentifier>",
                    "      <Extensions>h;hh;hpp;hxx;hm;inl;inc;xsd</Extensions>",
                    "    </Filter>",
                    "    <Filter Include=\"Resource Files\">",
                    "      <UniqueIdentifier>{67DA6AB6-F800-4c08-8B7A-83BB121AAD01}</UniqueIdentifier>",
                    "      <Extensions>rc;ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe;resx;tiff;tif;png;wav;mfcribbon-ms</Extensions>",
                    "    </Filter>",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <Text Include=\"ReadMe.txt\" />",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <ClInclude Include=\"stdafx.h\">",
                    "      <Filter>Header Files</Filter>",
                    "    </ClInclude>",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <ClCompile Include=\"stdafx.cpp\">",
                    "      <Filter>Source Files</Filter>",
                    "    </ClCompile>",
                    "    <ClCompile Include=\"$$(PROJECTNAME).cpp\">",
                    "      <Filter>Source Files</Filter>",
                    "    </ClCompile>",
                    "  </ItemGroup>",
                    "</Project>"
                }
            },
            new FileSpec
            {
                Pathname = "$$(PROJECTNAME)/$$(PROJECTNAME).$$(SUFFIX)",
                Contents = new[]
                {
                    "\uFEFF" +  // prepended BOM
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                    "<Project DefaultTargets=\"Build\" ToolsVersion=\"14.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">",
                    "  <ItemGroup Label=\"ProjectConfigurations\">",
                    "    <ProjectConfiguration Include=\"Debug|Win32\">",
                    "      <Configuration>Debug</Configuration>",
                    "      <Platform>Win32</Platform>",
                    "    </ProjectConfiguration>",
                    "    <ProjectConfiguration Include=\"Release|Win32\">",
                    "      <Configuration>Release</Configuration>",
                    "      <Platform>Win32</Platform>",
                    "    </ProjectConfiguration>",
                    "  </ItemGroup>",
                    "  <PropertyGroup Label=\"Globals\">",
                    "    <ProjectGuid>$$(PROJECTGUID)</ProjectGuid>",
                    "    <Keyword>Win32Proj</Keyword>",
                    "    <RootNamespace>$$(PROJECTNAME)</RootNamespace>",
                    "  </PropertyGroup>",
                    "  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.Default.props\" />",
                    "  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)'=='Debug|Win32'\" Label=\"Configuration\">",
                    "    <ConfigurationType>Application</ConfigurationType>",
                    "    <UseDebugLibraries>true</UseDebugLibraries>",
                    "    <PlatformToolset>$$(PLATFORMTOOLSET)</PlatformToolset>",
                    "    <CharacterSet>MultiByte</CharacterSet>",
                    "  </PropertyGroup>",
                    "  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)'=='Release|Win32'\" Label=\"Configuration\">",
                    "    <ConfigurationType>Application</ConfigurationType>",
                    "    <UseDebugLibraries>false</UseDebugLibraries>",
                    "    <PlatformToolset>$$(PLATFORMTOOLSET)</PlatformToolset>",
                    "    <WholeProgramOptimization>true</WholeProgramOptimization>",
                    "    <CharacterSet>MultiByte</CharacterSet>",
                    "  </PropertyGroup>",
                    "  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.props\" />",
                    "  <ImportGroup Label=\"ExtensionSettings\">",
                    "  </ImportGroup>",
                    "  <ImportGroup Label=\"Shared\">",
                    "  </ImportGroup>",
                    "  <ImportGroup Label=\"PropertySheets\" Condition=\"'$(Configuration)|$(Platform)'=='Debug|Win32'\">",
                    "    <Import Project=\"$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props\" Condition=\"exists('$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props')\" Label=\"LocalAppDataPlatform\" />",
                    "  </ImportGroup>",
                    "  <ImportGroup Label=\"PropertySheets\" Condition=\"'$(Configuration)|$(Platform)'=='Release|Win32'\">",
                    "    <Import Project=\"$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props\" Condition=\"exists('$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props')\" Label=\"LocalAppDataPlatform\" />",
                    "  </ImportGroup>",
                    "  <PropertyGroup Label=\"UserMacros\" />",
                    "  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)'=='Debug|Win32'\">",
                    "    <LinkIncremental>true</LinkIncremental>",
                    "  </PropertyGroup>",
                    "  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)'=='Release|Win32'\">",
                    "    <LinkIncremental>false</LinkIncremental>",
                    "  </PropertyGroup>",
                    "  <ItemDefinitionGroup Condition=\"'$(Configuration)|$(Platform)'=='Debug|Win32'\">",
                    "    <ClCompile>",
                    "      <PrecompiledHeader>Use</PrecompiledHeader>",
                    "      <WarningLevel>Level3</WarningLevel>",
                    "      <Optimization>Disabled</Optimization>",
                    "      <PreprocessorDefinitions>WIN32;_DEBUG;_CONSOLE;%(PreprocessorDefinitions)</PreprocessorDefinitions>",
                    "    </ClCompile>",
                    "    <Link>",
                    "      <SubSystem>Console</SubSystem>",
                    "      <GenerateDebugInformation>true</GenerateDebugInformation>",
                    "    </Link>",
                    "  </ItemDefinitionGroup>",
                    "  <ItemDefinitionGroup Condition=\"'$(Configuration)|$(Platform)'=='Release|Win32'\">",
                    "    <ClCompile>",
                    "      <WarningLevel>Level3</WarningLevel>",
                    "      <PrecompiledHeader>Use</PrecompiledHeader>",
                    "      <Optimization>MaxSpeed</Optimization>",
                    "      <FunctionLevelLinking>true</FunctionLevelLinking>",
                    "      <IntrinsicFunctions>true</IntrinsicFunctions>",
                    "      <PreprocessorDefinitions>WIN32;NDEBUG;_CONSOLE;%(PreprocessorDefinitions)</PreprocessorDefinitions>",
                    "    </ClCompile>",
                    "    <Link>",
                    "      <SubSystem>Console</SubSystem>",
                    "      <GenerateDebugInformation>true</GenerateDebugInformation>",
                    "      <EnableCOMDATFolding>true</EnableCOMDATFolding>",
                    "      <OptimizeReferences>true</OptimizeReferences>",
                    "    </Link>",
                    "  </ItemDefinitionGroup>",
                    "  <ItemGroup>",
                    "    <Text Include=\"ReadMe.txt\" />",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <ClInclude Include=\"stdafx.h\" />",
                    "  </ItemGroup>",
                    "  <ItemGroup>",
                    "    <ClCompile Include=\"$$(PROJECTNAME).cpp\" />",
                    "    <ClCompile Include=\"stdafx.cpp\">",
                    "      <PrecompiledHeader Condition=\"'$(Configuration)|$(Platform)'=='Debug|Win32'\">Create</PrecompiledHeader>",
                    "      <PrecompiledHeader Condition=\"'$(Configuration)|$(Platform)'=='Release|Win32'\">Create</PrecompiledHeader>",
                    "    </ClCompile>",
                    "  </ItemGroup>",
                    "  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.targets\" />",
                    "  <ImportGroup Label=\"ExtensionTargets\">",
                    "  </ImportGroup>",
                    "</Project>"
                }
            },
            new FileSpec
            {
                Pathname = "$$(PROJECTNAME)/stdafx.h",
                Contents = new []
                {
                    "// AMS - This program was generated by $$(GENERATOR).exe\n",
                    "// don't display warning at dbghelp.h lines 1544, and 3190 (c:\\program files (x86)\\windows kits\\8.1\\include\\um\\dbghelp.h):",
                    "#pragma warning(disable : 4091)\n",
                    "#include <windows.h>",
                    "#include <cstdint>",
                    "#include <cassert>",
                    "#include <array>",
                    "#include <vector>",
                    "#include <deque>",
                    "#include <iostream>",
                    "#include <fstream>",
                    "#include <sstream>",
                    "#include <iomanip>",
                    "#include <sstream>",
                    "#include <algorithm>",
                    "#include <iterator>",
                    "#include <map>",
                    "#include <set>",
                    "#include <unordered_map>",
                    "#include <unordered_set>",
                    "#include <functional>",
                    "// basic exception handler class:",
                    "struct QException : public std::exception",
                    "{",
                    "    QException(std::string msg) : std::exception(msg.c_str()) {}",
                    "    QException(char *msg) : std::exception(msg) {}",
                    "    virtual ~QException() {}",
                    "};",
                }
            },
            new FileSpec
            {
                Pathname = "$$(PROJECTNAME)/stdafx.cpp",
                Contents = new[]
                {
                    "// #define _USING_V110_SDK71_",
                    "#include \"stdafx.h\""
                }
            },
            new FileSpec (CommonFileSpecs.SlnFileSpec),
            new FileSpec (CommonFileSpecs.GitIgnore)

        };
    }
}
