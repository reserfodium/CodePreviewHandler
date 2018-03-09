using CodePreview.Controls;
using FastColoredTextBoxNS;
using PreviewHandler;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CodePreview
{
    class CodePreviewHandler : FileBasedPreviewHandlerControl
    {
        protected override void DoPreview()
        {
            CodePreviewHandlerControl content = new CodePreviewHandlerControl(hFile)
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(content);
        }
    }
}
