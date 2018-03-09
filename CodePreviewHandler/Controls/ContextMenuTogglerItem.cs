using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodePreview.Controls
{
    public class TogglerItem : ToolStripMenuItem
    {
        public TogglerItem(string text, CheckState checkState = CheckState.Indeterminate)
        {
            this.Text = text;
            this.Checked = checkState == CheckState.Checked ? true : false;

            this.Click += (e, s) =>
            {
                this.Checked = !this.Checked;
            };
        }
    }
}
