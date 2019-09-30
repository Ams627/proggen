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

namespace AMS.AMSExtensions
{
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
                MenuCommand mc1 = new MenuCommand(ProcessCloseCrap, menuCommandID);
                MenuCommand mc2 = new MenuCommand(ProcessOpenConApp, menuCommandID2);
                MenuCommand mc3 = new MenuCommand(ProcessOpenCSharpConApp, menuCommandID3);
                MenuCommand mc4 = new MenuCommand(ProcessOpenWPFApp, menuCommandID4);
                mcs.AddCommand(mc1);
                mcs.AddCommand(mc2);
                mcs.AddCommand(mc3);
                mcs.AddCommand(mc4);
            }
        }

        void ProcessCloseCrap(object sender, EventArgs e)
        {
            DTE dte = (DTE)GetService(typeof(EnvDTE.DTE));

            dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindClassView).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindResults1).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindResults2).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindAutoLocals).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindCallStack).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindCommandWindow).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindDocumentOutline).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindDynamicHelp).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindReplace).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindSymbolResults).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindProperties).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindResourceView).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindServerExplorer).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Close();
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox).Close();
            dte.Windows.Item("{131369F2-062D-44A2-8671-91FF31EFB4F4}").Close();
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
                var findString = @"static void Main\(string\[\] args\)\r?\n +{";

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
