using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using System.Collections.Generic;
using System.IO;

namespace AMS.AMSExtensions
{
    public abstract class WindowKinds
    {
        public const string vsWindowKindKindStartPage = "{387CB18D-6153-4156-9257-9AC3F9207BBE}";
        public const string vsWindowKindCommunityWindow = "{96DB1F3B-0E7A-4406-B73E-C6F0A2C67B97}";
        public const string vsWindowKindDeviceExplorer = "{B65E9355-A4C7-4855-96BB-1D3EC8514E8F}";
        public const string vsWindowKindBookmarks = "{A0C5197D-0AC7-4B63-97CD-8872A789D233}";
        public const string vsWindowKindApplicationBrowser = "{399832EA-70A8-4AE7-9B99-3C0850DAD152}";
        public const string vsWindowKindFavorites = "{57DC5D59-11C2-4955-A7B4-D7699D677E93}";
        public const string vsWindowKindErrorList = "{D78612C7-9962-4B83-95D9-268046DAD23A}";
        public const string vsWindowKindHelpSearch = "{46C87F81-5A06-43A8-9E25-85D33BAC49F8}";
        public const string vsWindowKindHelpIndex = "{73F6DD58-437E-11D3-B88E-00C04F79F802}";
        public const string vsWindowKindHelpContents = "{4A791147-19E4-11D3-B86B-00C04F79F802}";
        public const string vsWindowKindCallBrowser = "{5415EA3A-D813-4948-B51E-562082CE0887}";
        public const string vsWindowKindCodeDefinition = "{588470CC-84F8-4A57-9AC4-86BCA0625FF4}";
        public const string vsWindowKindImmediate = "{28836128-FC2C-11D2-A433-00C04F72D18A}";
        public const string vsWindowKindTeamExplorer = "{131369F2-062D-44A2-8671-91FF31EFB4F4}";
        public const string vsWindowKindTestExplorer = "{e1b7d1f8-9b3c-49b1-8f4f-bfc63a88835d}";
    }

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidAMSExPkgString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class AMSExtensionsPackage : Package
    {
        public AMSExtensionsPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers (commands must exist in the .vsct file)

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidAMSExCmdSet, (int)PkgCmdIDList.cmdidCloseCrap);
                CommandID menuCommandID2 = new CommandID(GuidList.guidAMSExCmdSet, (int)PkgCmdIDList.cmdidOpenConApp);
                CommandID menuCommandID3 = new CommandID(GuidList.guidAMSExCmdSet, (int)PkgCmdIDList.cmdidOpenCSharpConApp);
                CommandID menuCommandID4 = new CommandID(GuidList.guidAMSExCmdSet, (int)PkgCmdIDList.cmdidOpenWPFApp);
                CommandID menuCommandID5 = new CommandID(GuidList.guidAMSExCmdSet, (int)PkgCmdIDList.cmdidOpenStartFile);
                OleMenuCommand mc1 = new OleMenuCommand(ProcessCloseCrap, menuCommandID);
                OleMenuCommand mc2 = new OleMenuCommand(ProcessOpenConApp, menuCommandID2);
                OleMenuCommand mc3 = new OleMenuCommand(ProcessOpenCSharpConApp, menuCommandID3);
                OleMenuCommand mc4 = new OleMenuCommand(ProcessOpenWPFApp, menuCommandID4);
                OleMenuCommand mc5 = new OleMenuCommand(ProcessOpenStartFile, menuCommandID5);
                mc5.ParametersDescription = "<Filename>";
                mcs.AddCommand(mc1);
                mcs.AddCommand(mc2);
                mcs.AddCommand(mc3);
                mcs.AddCommand(mc4);
                mcs.AddCommand(mc5);
            }
        }

        void ProcessCloseCrap(object sender, EventArgs e)
        {
            DTE dte = (DTE)GetService(typeof(EnvDTE.DTE));

            var l1 = new List<string>();
            var windows = dte.Windows;
            foreach (Window win in windows)
            {
                l1.Add($"{win.Kind} {win.ObjectKind}");
            }
            File.WriteAllLines(@"c:\temp\w1.txt", l1);
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindClassView).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Close();

            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindResults1).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindResults2).IsFloating = false;

            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindResults2).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindResults2).IsFloating = false;

            dte.Windows.Item(EnvDTE.Constants.vsWindowKindAutoLocals).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindCallStack).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindCommandWindow).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindDocumentOutline).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindDynamicHelp).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindReplace).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindSymbolResults).Close();

            // output
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).IsFloating = false;

            dte.Windows.Item(EnvDTE.Constants.vsWindowKindProperties).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindResourceView).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindServerExplorer).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindTaskList).Close();
            File.WriteAllLines(@"c:\temp\ww1.txt", new [] {"w1"});
            dte.Windows.Item(WindowKinds.vsWindowKindTeamExplorer).Close();
            File.WriteAllLines(@"c:\temp\ww1.txt", new[] { "w2" });
            dte.Windows.Item(WindowKinds.vsWindowKindErrorList).Close();
            File.WriteAllLines(@"c:\temp\ww1.txt", new[] { "w3" });

            dte.Windows.Item(WindowKinds.vsWindowKindTestExplorer).Close();
            dte.Windows.Item(WindowKinds.vsWindowKindTestExplorer).IsFloating = false;

            File.WriteAllLines(@"c:\temp\ww1.txt", new[] { "w4" });
        }

        bool ShowProjectItems(ProjectItems pritems, int i, string searchname)
        {
            bool found = false;
            foreach (ProjectItem item in pritems)
            {
                for (short u = 0; u < item.FileCount; u++)
                {
                    System.Diagnostics.Debug.WriteLine($"filename:{item.FileNames[u]}");
                    if (item.FileNames[u].IndexOf(searchname, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var window = item.Open();
                        window.Visible = true;
                        window.Activate();
                        found = true;
                        break;
                    }
                }
                ProjectItems subpritems = item.ProjectItems;
                if (subpritems != null)
                {
                    found = ShowProjectItems(subpritems, i + 1, searchname);
                    if (found)
                    {
                        break;
                    }
                }
            }
            return found;
        }

        void ProcessOpenStartFile(object sender, EventArgs e)
        {
            Debug.WriteLine("OpenStartFile");

            var inValue = ((OleMenuCmdEventArgs)e).InValue as string;

            Debug.WriteLine($"param: {inValue}");

            DTE dte = (DTE)GetService(typeof(EnvDTE.DTE));

            var prjs = dte.Solution.Projects;

            System.Diagnostics.Debug.WriteLine($"Number of projects is {prjs.Count}");

            if (prjs.Count > 0)
            {
                var prj = prjs.Item(1);
                var pritems = prj.ProjectItems;
                ShowProjectItems(pritems, 0, inValue);

                dte.ActiveDocument.Activate();
                dte.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxRegExpr;
                dte.Find.FindWhat = "//startstarttypingtypingherehere";
                dte.Find.Target = vsFindTarget.vsFindTargetCurrentDocument;
                dte.Find.MatchCase = false;
                dte.Find.MatchWholeWord = false;
                dte.Find.Backwards = false;
                dte.Find.MatchInHiddenText = false;
                dte.Find.Action = vsFindAction.vsFindActionFind;
                if (dte.Find.Execute() == vsFindResult.vsFindResultNotFound)
                {
                    throw new System.Exception("vsFindResultNotFound");
                }
                dte.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Close();
                TextSelection ts = (TextSelection)dte.ActiveDocument.Selection;
                ts.Delete();

                dte.ExecuteCommand("File.SaveAll", string.Empty);
                dte.ExecuteCommand("Build.BuildSolution");
                dte.ActiveDocument.Activate();
            }

        }

        void ProcessOpenConApp(object sender, EventArgs e)
        {
            Debug.WriteLine("Open Console App");
            DTE dte = (DTE)GetService(typeof(EnvDTE.DTE));

            var prjs = dte.Solution.Projects;
            if (prjs.Count > 0)
            {
                string s = prjs.Item(1).FullName;
                s = s.Replace(".vcxproj", ".cpp");
                dte.ItemOperations.OpenFile(s);
                dte.ActiveDocument.Activate();
                dte.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxRegExpr;
                dte.Find.FindWhat = "int main\\(.*\\n\\{.*\\n";
                dte.Find.Target = vsFindTarget.vsFindTargetCurrentDocument;
                dte.Find.MatchCase = false;
                dte.Find.MatchWholeWord = false;
                dte.Find.Backwards = false;
                dte.Find.MatchInHiddenText = false;
                dte.Find.Action = vsFindAction.vsFindActionFind;
                if (dte.Find.Execute() == vsFindResult.vsFindResultNotFound)
                {
                    throw new System.Exception("vsFindResultNotFound");
                }
                dte.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Close();
                TextSelection ts = (TextSelection)dte.ActiveDocument.Selection;
                ts.CharRight();
                ts.Indent();

                dte.ExecuteCommand("File.SaveAll", string.Empty);
                dte.ExecuteCommand("Build.BuildSolution");
                dte.ActiveDocument.Activate();

            }
        }

        void ProcessOpenCSharpConApp(object sender, EventArgs e)
        {
            Debug.WriteLine("Open CSharp Console App");
            IVsOutputWindow outWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            Guid generalPaneGuid = VSConstants.GUID_OutWindowGeneralPane;
            Guid debugPane = VSConstants.GUID_OutWindowDebugPane;
            outWindow.GetPane(ref debugPane, out var paneToWriteTo);
            paneToWriteTo.OutputString("Opening start file\r\n");


            DTE dte = (DTE)GetService(typeof(EnvDTE.DTE));

            var prjs = dte.Solution.Projects;
            if (prjs.Count > 0)
            {
                // get full path of solution (including name of solution file):
                string solutionFullPathname = dte.Solution.FullName;

                // extract the directory of the solution:
                string dir = System.IO.Path.GetDirectoryName(solutionFullPathname);

                // get the name of the first project
                string projectName = dte.Solution.Projects.Item(1).Name;

                // combine the solution dir with the project dir and "program.cs":
                string programfname = System.IO.Path.Combine(dir, projectName, "program.cs");
                //System.Windows.Forms.MessageBox.Show("hello: " + programfname);

                dte.ItemOperations.OpenFile(programfname, EnvDTE.Constants.vsViewKindCode);

                var findString = "//startstarttypingtypingherehere";
                paneToWriteTo.OutputString($"opened file {programfname}\r\n");
                paneToWriteTo.OutputString($"searching for {findString}\r\n");

                dte.ActiveDocument.Activate();
                dte.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxRegExpr;
                dte.Find.FindWhat = findString;
                dte.Find.Target = vsFindTarget.vsFindTargetCurrentDocument;
                dte.Find.MatchCase = false;
                dte.Find.MatchWholeWord = false;
                dte.Find.Backwards = false;
                dte.Find.MatchInHiddenText = false;
                dte.Find.Action = vsFindAction.vsFindActionFind;
                if (dte.Find.Execute() == vsFindResult.vsFindResultNotFound)
                {
                    paneToWriteTo.OutputString($"{findString} not found.\r\n");
                }
                else
                {
                    paneToWriteTo.OutputString($"{findString} found\r\n");
                }
                dte.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Close();
                TextSelection ts = (TextSelection)dte.ActiveDocument.Selection;
                ts.Delete();

                dte.ExecuteCommand("File.SaveAll", string.Empty);
                dte.ExecuteCommand("Build.BuildSolution");
                dte.ActiveDocument.Activate();
            }

            paneToWriteTo.Activate(); // Brings this pane into view
        }

        void ProcessOpenWPFApp(object sender, EventArgs e)
        {
            Debug.WriteLine("Open WPF App");
            DTE dte = (DTE)GetService(typeof(EnvDTE.DTE));

            var prjs = dte.Solution.Projects;
            if (prjs.Count > 0)
            {
                // get full path of solution (including name of solution file):
                string solutionFullPathname = dte.Solution.FullName;

                // extract the directory of the solution:
                string dir = System.IO.Path.GetDirectoryName(solutionFullPathname);

                // get the name of the first project
                string projectName = dte.Solution.Projects.Item(1).Name;

                // combine the solution dir with the project dir and "program.cs":
                string programfname = System.IO.Path.Combine(dir, projectName, "mainwindow.xaml");
                //System.Windows.Forms.MessageBox.Show("hello: " + programfname);

                dte.ItemOperations.OpenFile(programfname, EnvDTE.Constants.vsViewKindCode);
                //var findString = @"static void Main\(string\[\] args\)\r?\n +{";

                //dte.ActiveDocument.Activate();
                //dte.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxRegExpr;
                //dte.Find.FindWhat = findString;
                //dte.Find.Target = vsFindTarget.vsFindTargetCurrentDocument;
                //dte.Find.MatchCase = false;
                //dte.Find.MatchWholeWord = false;
                //dte.Find.Backwards = false;
                //dte.Find.MatchInHiddenText = false;
                //dte.Find.Action = vsFindAction.vsFindActionFind;
                //if (dte.Find.Execute() == vsFindResult.vsFindResultNotFound)
                //{
                //    throw new System.Exception("vsFindResultNotFound");
                //}
                //dte.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Close();
                //TextSelection ts = (TextSelection)dte.ActiveDocument.Selection;
                //ts.CharRight();
                //ts.Indent();

                dte.ExecuteCommand("File.SaveAll", string.Empty);
                dte.ExecuteCommand("Build.BuildSolution");
                dte.ActiveDocument.Activate();
            }

        }

    }
}
