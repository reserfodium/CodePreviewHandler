using CodePreview.Controls;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CodePreview
{
    public partial class CodePreviewHandlerControl : UserControl
    {
        private FileInfo hFile;
        ContextMenuStrip menu;

        public CodePreviewHandlerControl(FileInfo fileName)
        {
            InitializeComponent();

            hFile = fileName;

            OpenFile();
            SyntaxHighlight();
            CreateContextMenu();
        }

        #region FCTB

        private void OpenFile()
        {
            fctb.Text = File.ReadAllText(hFile.FullName, Encoding.Default);
        }

        private void SyntaxHighlight()
        {
            string ext = hFile.Extension;

            foreach (var i in Manager.Languages.Keys)
            {
                if (i.Contains(ext))
                {
                    fctb.Language = Manager.Languages[i];
                    fctb.SyntaxHighlighter.HighlightSyntax(fctb.Language, fctb.Range);

                    break;
                }
            }
        }

        private void CreateContextMenu()
        {
            // Create context menu
            menu = new ContextMenuStrip();

            #region Items

            // Open file
            ToolStripMenuItem openFile = new ToolStripMenuItem("Open file");
            openFile.Image = Icon.ExtractAssociatedIcon(hFile.FullName).ToBitmap();
            openFile.Click += (e, s) =>
            {
                Cursor.Current = Cursors.WaitCursor;
                Process.Start(hFile.FullName);
                Cursor.Current = Cursors.Default;
            };

            // Show ruler
            TogglerItem rulerToggler = new TogglerItem("Show ruler", CheckState.Checked);
            rulerToggler.Click += (e, s) => ruler.Visible = !ruler.Visible;

            // Show line numbers
            TogglerItem numLineToggler = new TogglerItem("Show line numbers", CheckState.Checked);
            numLineToggler.Click += (e, s) =>
            {
                fctb.ShowLineNumbers = !fctb.ShowLineNumbers;

                ruler.Visible = false;
                ruler.Visible = true;
            };

            // Show folding lines
            TogglerItem foldingLineToggler = new TogglerItem("Show folding lines", CheckState.Checked);
            foldingLineToggler.Click += (e, s) => fctb.ShowFoldingLines = !fctb.ShowFoldingLines;

            #endregion

            // Add items
            menu.Items.AddRange(new ToolStripItem[]
            {
                openFile,
                new ToolStripSeparator(),
                rulerToggler,
                numLineToggler,
                new ToolStripSeparator(),
                foldingLineToggler
            });

            fctb.ContextMenuStrip = menu;
        }

        #endregion
    }
}
