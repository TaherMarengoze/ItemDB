namespace UserInterface.Forms
{
    partial class FieldEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpListData = new System.Windows.Forms.GroupBox();
            this.btnAddEntry = new System.Windows.Forms.Button();
            this.txtEntryValue = new System.Windows.Forms.TextBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbxFieldListItems = new System.Windows.Forms.ListBox();
            this.btnAddNewList = new System.Windows.Forms.Button();
            this.btnDeleteEntry = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItmSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListDetails = new System.Windows.Forms.DataGridView();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.grpListData.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // grpListData
            // 
            this.grpListData.AutoSize = true;
            this.grpListData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpListData.Controls.Add(this.btnAddEntry);
            this.grpListData.Controls.Add(this.txtEntryValue);
            this.grpListData.Enabled = false;
            this.grpListData.Location = new System.Drawing.Point(12, 27);
            this.grpListData.Name = "grpListData";
            this.grpListData.Size = new System.Drawing.Size(270, 59);
            this.grpListData.TabIndex = 7;
            this.grpListData.TabStop = false;
            this.grpListData.Text = "List Entry";
            // 
            // btnAddEntry
            // 
            this.btnAddEntry.Location = new System.Drawing.Point(189, 17);
            this.btnAddEntry.Name = "btnAddEntry";
            this.btnAddEntry.Size = new System.Drawing.Size(75, 23);
            this.btnAddEntry.TabIndex = 4;
            this.btnAddEntry.Text = "Add Entry";
            this.btnAddEntry.UseVisualStyleBackColor = true;
            this.btnAddEntry.Click += new System.EventHandler(this.btnAddEntry_Click);
            // 
            // txtEntryValue
            // 
            this.txtEntryValue.Location = new System.Drawing.Point(6, 19);
            this.txtEntryValue.Name = "txtEntryValue";
            this.txtEntryValue.Size = new System.Drawing.Size(177, 20);
            this.txtEntryValue.TabIndex = 3;
            this.txtEntryValue.TextChanged += new System.EventHandler(this.txtEntryValue_TextChanged);
            this.txtEntryValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEntryValue_KeyDown);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Image = global::UserInterface.Properties.Resources.down_arrow_16x;
            this.btnDown.Location = new System.Drawing.Point(241, 437);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(41, 23);
            this.btnDown.TabIndex = 15;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.Image = global::UserInterface.Properties.Resources.up_arrow_16x;
            this.btnUp.Location = new System.Drawing.Point(241, 414);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(41, 23);
            this.btnUp.TabIndex = 14;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lbxFieldListItems);
            this.groupBox1.Location = new System.Drawing.Point(12, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 316);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List Items";
            // 
            // lbxFieldListItems
            // 
            this.lbxFieldListItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxFieldListItems.FormattingEnabled = true;
            this.lbxFieldListItems.Location = new System.Drawing.Point(3, 16);
            this.lbxFieldListItems.Name = "lbxFieldListItems";
            this.lbxFieldListItems.Size = new System.Drawing.Size(264, 297);
            this.lbxFieldListItems.TabIndex = 2;
            this.lbxFieldListItems.SelectedIndexChanged += new System.EventHandler(this.lbxFieldListItems_SelectedIndexChanged);
            // 
            // btnAddNewList
            // 
            this.btnAddNewList.Location = new System.Drawing.Point(288, 27);
            this.btnAddNewList.Name = "btnAddNewList";
            this.btnAddNewList.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewList.TabIndex = 11;
            this.btnAddNewList.Text = "Add List";
            this.btnAddNewList.UseVisualStyleBackColor = true;
            this.btnAddNewList.Click += new System.EventHandler(this.btnAddNewList_Click);
            // 
            // btnDeleteEntry
            // 
            this.btnDeleteEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteEntry.Location = new System.Drawing.Point(93, 414);
            this.btnDeleteEntry.Name = "btnDeleteEntry";
            this.btnDeleteEntry.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEntry.TabIndex = 7;
            this.btnDeleteEntry.Text = "Delete Entry";
            this.btnDeleteEntry.UseVisualStyleBackColor = true;
            this.btnDeleteEntry.Click += new System.EventHandler(this.btnDeleteEntry_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(12, 414);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Edit Entry";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItmSaveFile,
            this.toolStripSeparator1,
            this.tsmiClose,
            this.toolStripSeparator2,
            this.tsmiExitApp});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuItmSaveFile
            // 
            this.mnuItmSaveFile.Enabled = false;
            this.mnuItmSaveFile.Name = "mnuItmSaveFile";
            this.mnuItmSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuItmSaveFile.Size = new System.Drawing.Size(198, 22);
            this.mnuItmSaveFile.Text = "&Save File";
            this.mnuItmSaveFile.Click += new System.EventHandler(this.mnuItmSaveFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiClose.Size = new System.Drawing.Size(198, 22);
            this.tsmiClose.Text = "&Close Editor";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiExitApp
            // 
            this.tsmiExitApp.Name = "tsmiExitApp";
            this.tsmiExitApp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiExitApp.Size = new System.Drawing.Size(198, 22);
            this.tsmiExitApp.Text = "E&xit Application";
            this.tsmiExitApp.Click += new System.EventHandler(this.tsmiExitApp_Click);
            // 
            // dgvListDetails
            // 
            this.dgvListDetails.AllowUserToAddRows = false;
            this.dgvListDetails.AllowUserToDeleteRows = false;
            this.dgvListDetails.AllowUserToResizeColumns = false;
            this.dgvListDetails.AllowUserToResizeRows = false;
            this.dgvListDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvListDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEdit,
            this.colDelete});
            this.dgvListDetails.Location = new System.Drawing.Point(288, 56);
            this.dgvListDetails.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.dgvListDetails.MultiSelect = false;
            this.dgvListDetails.Name = "dgvListDetails";
            this.dgvListDetails.ReadOnly = true;
            this.dgvListDetails.RowHeadersVisible = false;
            this.dgvListDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvListDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListDetails.Size = new System.Drawing.Size(396, 404);
            this.dgvListDetails.TabIndex = 11;
            this.dgvListDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListDetails_CellContentClick);
            this.dgvListDetails.SelectionChanged += new System.EventHandler(this.dgvListDetails_SelectionChanged);
            // 
            // colEdit
            // 
            this.colEdit.HeaderText = "Edit";
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            this.colEdit.Text = "Edit";
            this.colEdit.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            this.colDelete.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDelete.HeaderText = "Delete";
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Text = "Delete";
            this.colDelete.UseColumnTextForButtonValue = true;
            // 
            // FieldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.btnDeleteEntry);
            this.Controls.Add(this.btnAddNewList);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.dgvListDetails);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.grpListData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FieldEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Field Lists Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FieldEditor_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FieldEditor_KeyDown);
            this.grpListData.ResumeLayout(false);
            this.grpListData.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpListData;
        private System.Windows.Forms.Button btnDeleteEntry;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ListBox lbxFieldListItems;
        private System.Windows.Forms.Button btnAddEntry;
        private System.Windows.Forms.TextBox txtEntryValue;
        private System.Windows.Forms.Button btnAddNewList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItmSaveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExitApp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.DataGridView dgvListDetails;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
    }
}