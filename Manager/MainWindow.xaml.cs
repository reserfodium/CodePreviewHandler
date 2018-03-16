using Manager.Dialogs;
using Manager.Dialogs.InstallationDialog;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;

namespace Manager
{
    public partial class MainWindow : Window
    {
        #region Data

        static string RegAsmBinary = "regasm.exe";
        static string GacUtilBinary = "gacutil.exe";

        static string LibraryName = "CodePreviewHandler.dll";
        static string DependencyName = "FastColoredTextBox.dll";

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Functions

        private bool RunCommand(string cmd)
        {
            try
            {
                ProcessStartInfo procStartInfo =
                    new ProcessStartInfo("cmd", "/c " + cmd)
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void InstallCodePreviewHandler(Installation installationOption)
        {
            #region GAC

            string cmd = GacUtilBinary + " /if ";

            RunCommand(cmd + LibraryName);
            RunCommand(cmd + DependencyName);

            #endregion

            string regasm = 
                RegAsmBinary +
                (installationOption == Installation.ForCurrentUser ? " /U " : " ") +
                LibraryName;

            RunCommand(regasm);
        }

        #endregion

        #region Events

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            new OkDialog("Test", Properties.Resources.Access, this).ShowDialog();
        }

        private void UninstallButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
