namespace UserInterface.Forms
{
    partial class Main
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSepFile1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiForms = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItemViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnspForms1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSpecsEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnspForms2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSizeGroupsEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSizeEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnspForms3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiBrandEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnspForms4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEndsEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAutoLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEndsEditor = new System.Windows.Forms.Button();
            this.btnSizesEditor = new System.Windows.Forms.Button();
            this.btnBrandsEditor = new System.Windows.Forms.Button();
            this.btnItemViewer = new System.Windows.Forms.Button();
            this.btnSpecsEditor = new System.Windows.Forms.Button();
            this.btnSizeGroupsEditor = new System.Windows.Forms.Button();
            this.tsmiRestartApp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tsmiForms,
            this.tsmiTest});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(174, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadAll,
            this.mnuSepFile1,
            this.mnuItmExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // tsmiLoadAll
            // 
            this.tsmiLoadAll.Name = "tsmiLoadAll";
            this.tsmiLoadAll.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.tsmiLoadAll.Size = new System.Drawing.Size(210, 22);
            this.tsmiLoadAll.Text = "Load XML Documents";
            this.tsmiLoadAll.Click += new System.EventHandler(this.tsmiLoadAll_Click);
            // 
            // mnuSepFile1
            // 
            this.mnuSepFile1.Name = "mnuSepFile1";
            this.mnuSepFile1.Size = new System.Drawing.Size(207, 6);
            // 
            // mnuItmExit
            // 
            this.mnuItmExit.Name = "mnuItmExit";
            this.mnuItmExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuItmExit.Size = new System.Drawing.Size(210, 22);
            this.mnuItmExit.Text = "E&xit Application";
            this.mnuItmExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // tsmiForms
            // 
            this.tsmiForms.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiItemViewer,
            this.mnspForms1,
            this.tsmiSpecsEditor,
            this.mnspForms2,
            this.tsmiSizeGroupsEditor,
            this.tsmiSizeEditor,
            this.mnspForms3,
            this.tsmiBrandEditor,
            this.mnspForms4,
            this.tsmiEndsEditor});
            this.tsmiForms.Name = "tsmiForms";
            this.tsmiForms.Size = new System.Drawing.Size(52, 20);
            this.tsmiForms.Text = "F&orms";
            // 
            // tsmiItemViewer
            // 
            this.tsmiItemViewer.Name = "tsmiItemViewer";
            this.tsmiItemViewer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmiItemViewer.Size = new System.Drawing.Size(211, 22);
            this.tsmiItemViewer.Text = "Item Viewer";
            this.tsmiItemViewer.Click += new System.EventHandler(this.ItemViewer_Click);
            // 
            // mnspForms1
            // 
            this.mnspForms1.Name = "mnspForms1";
            this.mnspForms1.Size = new System.Drawing.Size(208, 6);
            // 
            // tsmiSpecsEditor
            // 
            this.tsmiSpecsEditor.Name = "tsmiSpecsEditor";
            this.tsmiSpecsEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmiSpecsEditor.Size = new System.Drawing.Size(211, 22);
            this.tsmiSpecsEditor.Text = "S&pecs Editor";
            this.tsmiSpecsEditor.Click += new System.EventHandler(this.SpecsEditor_Click);
            // 
            // mnspForms2
            // 
            this.mnspForms2.Name = "mnspForms2";
            this.mnspForms2.Size = new System.Drawing.Size(208, 6);
            // 
            // tsmiSizeGroupsEditor
            // 
            this.tsmiSizeGroupsEditor.Name = "tsmiSizeGroupsEditor";
            this.tsmiSizeGroupsEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.tsmiSizeGroupsEditor.Size = new System.Drawing.Size(211, 22);
            this.tsmiSizeGroupsEditor.Text = "Size &Groups Editor";
            this.tsmiSizeGroupsEditor.Click += new System.EventHandler(this.SizeGroupsEditor_Click);
            // 
            // tsmiSizeEditor
            // 
            this.tsmiSizeEditor.Name = "tsmiSizeEditor";
            this.tsmiSizeEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSizeEditor.Size = new System.Drawing.Size(211, 22);
            this.tsmiSizeEditor.Text = "&Sizes Editor";
            this.tsmiSizeEditor.Click += new System.EventHandler(this.SizeEditor_Click);
            // 
            // mnspForms3
            // 
            this.mnspForms3.Name = "mnspForms3";
            this.mnspForms3.Size = new System.Drawing.Size(208, 6);
            // 
            // tsmiBrandEditor
            // 
            this.tsmiBrandEditor.Name = "tsmiBrandEditor";
            this.tsmiBrandEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.tsmiBrandEditor.Size = new System.Drawing.Size(211, 22);
            this.tsmiBrandEditor.Text = "&Brands Editor";
            this.tsmiBrandEditor.Click += new System.EventHandler(this.BrandEditor_Click);
            // 
            // mnspForms4
            // 
            this.mnspForms4.Name = "mnspForms4";
            this.mnspForms4.Size = new System.Drawing.Size(208, 6);
            // 
            // tsmiEndsEditor
            // 
            this.tsmiEndsEditor.Name = "tsmiEndsEditor";
            this.tsmiEndsEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmiEndsEditor.Size = new System.Drawing.Size(211, 22);
            this.tsmiEndsEditor.Text = "&Ends Editor";
            this.tsmiEndsEditor.Click += new System.EventHandler(this.EndsEditor_Click);
            // 
            // tsmiTest
            // 
            this.tsmiTest.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAutoLoad,
            this.toolStripSeparator1,
            this.tsmiRestartApp});
            this.tsmiTest.Name = "tsmiTest";
            this.tsmiTest.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
            this.tsmiTest.Size = new System.Drawing.Size(39, 20);
            this.tsmiTest.Text = "Test";
            // 
            // tsmiAutoLoad
            // 
            this.tsmiAutoLoad.CheckOnClick = true;
            this.tsmiAutoLoad.Name = "tsmiAutoLoad";
            this.tsmiAutoLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsmiAutoLoad.Size = new System.Drawing.Size(190, 22);
            this.tsmiAutoLoad.Text = "Auto Load File";
            this.tsmiAutoLoad.CheckedChanged += new System.EventHandler(this.tsmiAutoLoad_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // btnEndsEditor
            // 
            this.btnEndsEditor.Location = new System.Drawing.Point(12, 287);
            this.btnEndsEditor.Name = "btnEndsEditor";
            this.btnEndsEditor.Size = new System.Drawing.Size(150, 46);
            this.btnEndsEditor.TabIndex = 6;
            this.btnEndsEditor.Text = "Open &Ends Editor";
            this.btnEndsEditor.Click += new System.EventHandler(this.EndsEditor_Click);
            // 
            // btnSizesEditor
            // 
            this.btnSizesEditor.Location = new System.Drawing.Point(12, 183);
            this.btnSizesEditor.Name = "btnSizesEditor";
            this.btnSizesEditor.Size = new System.Drawing.Size(150, 46);
            this.btnSizesEditor.TabIndex = 4;
            this.btnSizesEditor.Text = "Open &Sizes Editor";
            this.btnSizesEditor.UseVisualStyleBackColor = true;
            this.btnSizesEditor.Click += new System.EventHandler(this.SizeEditor_Click);
            // 
            // btnBrandsEditor
            // 
            this.btnBrandsEditor.Location = new System.Drawing.Point(12, 235);
            this.btnBrandsEditor.Name = "btnBrandsEditor";
            this.btnBrandsEditor.Size = new System.Drawing.Size(150, 46);
            this.btnBrandsEditor.TabIndex = 5;
            this.btnBrandsEditor.Text = "Open &Brands Editor";
            this.btnBrandsEditor.UseVisualStyleBackColor = true;
            this.btnBrandsEditor.Click += new System.EventHandler(this.BrandEditor_Click);
            // 
            // btnItemViewer
            // 
            this.btnItemViewer.Location = new System.Drawing.Point(12, 27);
            this.btnItemViewer.Name = "btnItemViewer";
            this.btnItemViewer.Size = new System.Drawing.Size(150, 46);
            this.btnItemViewer.TabIndex = 1;
            this.btnItemViewer.Text = "Open &Item Viewer";
            this.btnItemViewer.Click += new System.EventHandler(this.ItemViewer_Click);
            // 
            // btnSpecsEditor
            // 
            this.btnSpecsEditor.Location = new System.Drawing.Point(12, 79);
            this.btnSpecsEditor.Name = "btnSpecsEditor";
            this.btnSpecsEditor.Size = new System.Drawing.Size(150, 46);
            this.btnSpecsEditor.TabIndex = 2;
            this.btnSpecsEditor.Text = "Open S&pecs Editor";
            this.btnSpecsEditor.UseVisualStyleBackColor = true;
            this.btnSpecsEditor.Click += new System.EventHandler(this.SpecsEditor_Click);
            // 
            // btnSizeGroupsEditor
            // 
            this.btnSizeGroupsEditor.Location = new System.Drawing.Point(12, 131);
            this.btnSizeGroupsEditor.Name = "btnSizeGroupsEditor";
            this.btnSizeGroupsEditor.Size = new System.Drawing.Size(150, 46);
            this.btnSizeGroupsEditor.TabIndex = 3;
            this.btnSizeGroupsEditor.Text = "Open Size &Groups Editor";
            this.btnSizeGroupsEditor.UseVisualStyleBackColor = true;
            this.btnSizeGroupsEditor.Click += new System.EventHandler(this.SizeGroupsEditor_Click);
            // 
            // tsmiRestartApp
            // 
            this.tsmiRestartApp.Name = "tsmiRestartApp";
            this.tsmiRestartApp.Size = new System.Drawing.Size(190, 22);
            this.tsmiRestartApp.Text = "Restart Application";
            this.tsmiRestartApp.Click += new System.EventHandler(this.tsmiRestartApp_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 345);
            this.Controls.Add(this.btnBrandsEditor);
            this.Controls.Add(this.btnSizeGroupsEditor);
            this.Controls.Add(this.btnSpecsEditor);
            this.Controls.Add(this.btnItemViewer);
            this.Controls.Add(this.btnSizesEditor);
            this.Controls.Add(this.btnEndsEditor);
            this.Controls.Add(this.mnuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiForms;
        private System.Windows.Forms.ToolStripMenuItem tsmiSizeEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiBrandEditor;
        private System.Windows.Forms.ToolStripSeparator mnuSepFile1;
        private System.Windows.Forms.Button btnEndsEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiEndsEditor;
        private System.Windows.Forms.Button btnSizesEditor;
        private System.Windows.Forms.Button btnBrandsEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpecsEditor;
        private System.Windows.Forms.ToolStripSeparator mnspForms2;
        private System.Windows.Forms.ToolStripMenuItem tsmiItemViewer;
        private System.Windows.Forms.ToolStripSeparator mnspForms1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSizeGroupsEditor;
        private System.Windows.Forms.ToolStripSeparator mnspForms3;
        private System.Windows.Forms.ToolStripSeparator mnspForms4;
        private System.Windows.Forms.Button btnItemViewer;
        private System.Windows.Forms.Button btnSpecsEditor;
        private System.Windows.Forms.Button btnSizeGroupsEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiTest;
        private System.Windows.Forms.ToolStripMenuItem tsmiAutoLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiRestartApp;
    }
}