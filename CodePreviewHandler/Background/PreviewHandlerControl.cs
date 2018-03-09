// Name: PreviewHandlerControl.cs
// Author: Stephen Toub

using System;
using System.IO;
using System.Windows.Forms;

namespace PreviewHandler
{
    public abstract class PreviewHandlerControl : Form
    {
        protected PreviewHandlerControl() 
            => FormBorderStyle = FormBorderStyle.None;

        public new abstract void Load(FileInfo file);

        public virtual void Unload()
        {
            foreach (Control c in Controls) c.Dispose();
            Controls.Clear();
        }
    }
}
