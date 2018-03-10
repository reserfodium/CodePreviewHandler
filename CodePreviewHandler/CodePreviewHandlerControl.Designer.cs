namespace CodePreview
{
    partial class CodePreviewHandlerControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodePreviewHandlerControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.fctb = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ruler = new FastColoredTextBoxNS.Ruler();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fctb);
            this.panel1.Controls.Add(this.ruler);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 324);
            this.panel1.TabIndex = 0;
            // 
            // fctb
            // 
            this.fctb.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctb.AutoScrollMinSize = new System.Drawing.Size(40, 15);
            this.fctb.AutoSize = true;
            this.fctb.BackBrush = null;
            this.fctb.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fctb.CharHeight = 15;
            this.fctb.CharWidth = 7;
            this.fctb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctb.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fctb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctb.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fctb.Hotkeys = resources.GetString("fctb.Hotkeys");
            this.fctb.IndentBackColor = System.Drawing.SystemColors.Control;
            this.fctb.IsReplaceMode = false;
            this.fctb.LeftBracket = '(';
            this.fctb.LeftBracket2 = '{';
            this.fctb.LeftPadding = 15;
            this.fctb.Location = new System.Drawing.Point(0, 24);
            this.fctb.Name = "fctb";
            this.fctb.Paddings = new System.Windows.Forms.Padding(0);
            this.fctb.ReadOnly = true;
            this.fctb.RightBracket = ')';
            this.fctb.RightBracket2 = '}';
            this.fctb.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctb.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctb.ServiceColors")));
            this.fctb.Size = new System.Drawing.Size(349, 300);
            this.fctb.TabIndex = 2;
            this.fctb.Zoom = 100;
            // 
            // ruler
            // 
            this.ruler.BackColor = System.Drawing.SystemColors.Control;
            this.ruler.BackColor2 = System.Drawing.SystemColors.Control;
            this.ruler.Dock = System.Windows.Forms.DockStyle.Top;
            this.ruler.Location = new System.Drawing.Point(0, 0);
            this.ruler.MaximumSize = new System.Drawing.Size(1073741824, 24);
            this.ruler.MinimumSize = new System.Drawing.Size(2, 24);
            this.ruler.Name = "ruler";
            this.ruler.Size = new System.Drawing.Size(349, 24);
            this.ruler.TabIndex = 3;
            this.ruler.Target = this.fctb;
            // 
            // CodePreviewHandlerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CodePreviewHandlerControl";
            this.Size = new System.Drawing.Size(349, 324);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FastColoredTextBoxNS.FastColoredTextBox fctb;
        private FastColoredTextBoxNS.Ruler ruler;
    }
}
