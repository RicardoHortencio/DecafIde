namespace DecafIde
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CursorPositionLbl = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.indentLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeVisualizer = new System.Windows.Forms.TreeView();
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.CodeTB = new FastColoredTextBoxNS.IronyFCTB();
            this.errorTB = new System.Windows.Forms.TextBox();
            this.ErrorLbl = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.MainContainer.Panel1.SuspendLayout();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CodeTB)).BeginInit();
            this.SuspendLayout();
            // 
            // CursorPositionLbl
            // 
            this.CursorPositionLbl.AutoSize = true;
            this.CursorPositionLbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CursorPositionLbl.Location = new System.Drawing.Point(0, 300);
            this.CursorPositionLbl.Name = "CursorPositionLbl";
            this.CursorPositionLbl.Size = new System.Drawing.Size(44, 13);
            this.CursorPositionLbl.TabIndex = 0;
            this.CursorPositionLbl.Text = "Line: Nr";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.editToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(516, 24);
            this.mainMenu.TabIndex = 4;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem});
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.indentLinesToolStripMenuItem,
            this.tryTemplateToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // indentLinesToolStripMenuItem
            // 
            this.indentLinesToolStripMenuItem.Name = "indentLinesToolStripMenuItem";
            this.indentLinesToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.indentLinesToolStripMenuItem.Text = "Indent Lines";
            this.indentLinesToolStripMenuItem.Click += new System.EventHandler(this.indentLinesToolStripMenuItem_Click);
            // 
            // tryTemplateToolStripMenuItem
            // 
            this.tryTemplateToolStripMenuItem.Name = "tryTemplateToolStripMenuItem";
            this.tryTemplateToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tryTemplateToolStripMenuItem.Text = "TryTemplate";
            this.tryTemplateToolStripMenuItem.Click += new System.EventHandler(this.tryTemplateToolStripMenuItem_Click);
            // 
            // TreeVisualizer
            // 
            this.TreeVisualizer.Dock = System.Windows.Forms.DockStyle.Right;
            this.TreeVisualizer.Location = new System.Drawing.Point(395, 24);
            this.TreeVisualizer.Name = "TreeVisualizer";
            this.TreeVisualizer.Size = new System.Drawing.Size(121, 276);
            this.TreeVisualizer.TabIndex = 5;
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 24);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainContainer.Panel1
            // 
            this.MainContainer.Panel1.Controls.Add(this.CodeTB);
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.errorTB);
            this.MainContainer.Panel2.Controls.Add(this.ErrorLbl);
            this.MainContainer.Size = new System.Drawing.Size(395, 276);
            this.MainContainer.SplitterDistance = 200;
            this.MainContainer.TabIndex = 6;
            // 
            // CodeTB
            // 
            this.CodeTB.AutoScrollMinSize = new System.Drawing.Size(107, 14);
            this.CodeTB.BackBrush = null;
            this.CodeTB.CharHeight = 14;
            this.CodeTB.CharWidth = 8;
            this.CodeTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CodeTB.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CodeTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeTB.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.CodeTB.IsReplaceMode = false;
            this.CodeTB.Location = new System.Drawing.Point(0, 0);
            this.CodeTB.Name = "CodeTB";
            this.CodeTB.Paddings = new System.Windows.Forms.Padding(0);
            this.CodeTB.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.CodeTB.Size = new System.Drawing.Size(395, 200);
            this.CodeTB.TabIndex = 0;
            this.CodeTB.Text = "ironyFCTB1";
            this.CodeTB.Zoom = 100;
            this.CodeTB.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.CodeTB_TextChanged);
            // 
            // errorTB
            // 
            this.errorTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorTB.Location = new System.Drawing.Point(0, 13);
            this.errorTB.Multiline = true;
            this.errorTB.Name = "errorTB";
            this.errorTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorTB.Size = new System.Drawing.Size(395, 59);
            this.errorTB.TabIndex = 1;
            // 
            // ErrorLbl
            // 
            this.ErrorLbl.AutoSize = true;
            this.ErrorLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ErrorLbl.Location = new System.Drawing.Point(0, 0);
            this.ErrorLbl.Name = "ErrorLbl";
            this.ErrorLbl.Size = new System.Drawing.Size(48, 13);
            this.ErrorLbl.TabIndex = 0;
            this.ErrorLbl.Text = "Error List";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 313);
            this.Controls.Add(this.MainContainer);
            this.Controls.Add(this.TreeVisualizer);
            this.Controls.Add(this.CursorPositionLbl);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "CustomIde";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.MainContainer.Panel1.ResumeLayout(false);
            this.MainContainer.Panel2.ResumeLayout(false);
            this.MainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.MainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CodeTB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CursorPositionLbl;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.TreeView TreeVisualizer;
        private System.Windows.Forms.SplitContainer MainContainer;
        private System.Windows.Forms.TextBox errorTB;
        private System.Windows.Forms.Label ErrorLbl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private FastColoredTextBoxNS.IronyFCTB CodeTB;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem indentLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tryTemplateToolStripMenuItem;
    }
}

