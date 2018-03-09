using CodePreview.Controls;
using FastColoredTextBoxNS;
using PreviewHandler;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CodePreview
{
    [PreviewHandler(Manager.Name, Manager.Exts, Manager.AppId)]
    [ProgId(Manager.ProgId)]
    [Guid(Manager.Guid)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CodePreviewHandlerProvider : FileBasedPreviewHandler
    {
        protected override PreviewHandlerControl CreatePreviewHandlerControl()
            => new CodePreviewHandler();
    }
}