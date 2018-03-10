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

        public CodePreviewHandlerControl(FileInfo fileName)
        {
            InitializeComponent();

            hFile = fileName;

            OpenFile();
            SyntaxHighlight();
        }

        #region FCTB

        private void OpenFile() => fctb.Text = File.ReadAllText(hFile.FullName, Encoding.Default);

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

        #endregion
    }
}
