namespace UserInterface.Forms
{
    partial class SizeGroupEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblGroupID = new System.Windows.Forms.Label();
            this.txtGroupID = new System.Windows.Forms.TextBox();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.grpGroupMetadata = new System.Windows.Forms.GroupBox();
            this.lblValidatorGroupName = new System.Windows.Forms.Label();
            this.lblValidatorGroupId = new System.Windows.Forms.Label();
            this.lblDefaultListID = new System.Windows.Forms.Label();
            this.cboDefaultID = new System.Windows.Forms.ComboBox();
            this.lblListsID = new System.Windows.Forms.Label();
            this.lstAltListIDs = new System.Windows.Forms.ListBox();
            this.btnModifyAltList = new System.Windows.Forms.Button();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.grpGroupData = new System.Windows.Forms.GroupBox();
            this.lblValidatorDefaultId = new System.Windows.Forms.Label();
            this.grpCustomSize = new System.Windows.Forms.GroupBox();
            this.chkCustomSize = new System.Windows.Forms.CheckBox();
            this.lblDataID = new System.Windows.Forms.Label();
            this.cboCustomSizeID = new System.Windows.Forms.ComboBox();
            this.grpAltList = new System.Windows.Forms.GroupBox();
            this.btnClearAltList = new System.Windows.Forms.Button();
            this.chkAltList = new System.Windows.Forms.CheckBox();
            this.grpGroupList = new System.Windows.Forms.GroupBox();
            this.dgvGroups = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.btnEditGroup = new System.Windows.Forms.Button();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.grpModiyControlPanel = new System.Windows.Forms.GroupBox();
            this.lbxSizeListEntries = new System.Windows.Forms.ListBox();
            this.grpListEntries = new System.Windows.Forms.GroupBox();
            this.grpGroupMetadata.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.grpGroupData.SuspendLayout();
            this.grpCustomSize.SuspendLayout();
            this.grpAltList.SuspendLayout();
            this.grpGroupList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroups)).BeginInit();
            this.grpModiyControlPanel.SuspendLayout();
            this.grpListEntries.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGroupID
            // 
            this.lblGroupID.AutoSize = true;
            this.lblGroupID.Location = new System.Drawing.Point(9, 22);
            this.lblGroupID.Name = "lblGroupID";
            this.lblGroupID.Size = new System.Drawing.Size(50, 13);
            this.lblGroupID.TabIndex = 9;
            this.lblGroupID.Text = "Group ID";
            // 
            // txtGroupID
            // 
            this.txtGroupID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGroupID.Location = new System.Drawing.Point(81, 19);
            this.txtGroupID.MaxLength = 5;
            this.txtGroupID.Name = "txtGroupID";
            this.txtGroupID.ReadOnly = true;
            this.txtGroupID.Size = new System.Drawing.Size(54, 20);
            this.txtGroupID.TabIndex = 10;
            this.txtGroupID.TextChanged += new System.EventHandler(this.txtGroupID_TextChanged);
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(9, 48);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(66, 13);
            this.lblGroupName.TabIndex = 12;
            this.lblGroupName.Text = "Group Name";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(81, 45);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.ReadOnly = true;
            this.txtGroupName.Size = new System.Drawing.Size(100, 20);
            this.txtGroupName.TabIndex = 13;
            this.txtGroupName.TextChanged += new System.EventHandler(this.txtGroupName_TextChanged);
            this.txtGroupName.Leave += new System.EventHandler(this.txtGroupName_Leave);
            // 
            // grpGroupMetadata
            // 
            this.grpGroupMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGroupMetadata.Controls.Add(this.lblValidatorGroupName);
            this.grpGroupMetadata.Controls.Add(this.lblValidatorGroupId);
            this.grpGroupMetadata.Controls.Add(this.txtGroupID);
            this.grpGroupMetadata.Controls.Add(this.txtGroupName);
            this.grpGroupMetadata.Controls.Add(this.lblGroupID);
            this.grpGroupMetadata.Controls.Add(this.lblGroupName);
            this.grpGroupMetadata.Location = new System.Drawing.Point(428, 27);
            this.grpGroupMetadata.Name = "grpGroupMetadata";
            this.grpGroupMetadata.Size = new System.Drawing.Size(244, 71);
            this.grpGroupMetadata.TabIndex = 8;
            this.grpGroupMetadata.TabStop = false;
            this.grpGroupMetadata.Text = "Group Metadata";
            // 
            // lblValidatorGroupName
            // 
            this.lblValidatorGroupName.AutoSize = true;
            this.lblValidatorGroupName.Font = new System.Drawing.Font("Calibri", 9F);
            this.lblValidatorGroupName.ForeColor = System.Drawing.Color.Red;
            this.lblValidatorGroupName.Location = new System.Drawing.Point(187, 49);
            this.lblValidatorGroupName.Name = "lblValidatorGroupName";
            this.lblValidatorGroupName.Size = new System.Drawing.Size(0, 14);
            this.lblValidatorGroupName.TabIndex = 14;
            // 
            // lblValidatorGroupId
            // 
            this.lblValidatorGroupId.AutoSize = true;
            this.lblValidatorGroupId.Font = new System.Drawing.Font("Calibri", 9F);
            this.lblValidatorGroupId.ForeColor = System.Drawing.Color.Red;
            this.lblValidatorGroupId.Location = new System.Drawing.Point(141, 23);
            this.lblValidatorGroupId.Name = "lblValidatorGroupId";
            this.lblValidatorGroupId.Size = new System.Drawing.Size(0, 14);
            this.lblValidatorGroupId.TabIndex = 11;
            this.lblValidatorGroupId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDefaultListID
            // 
            this.lblDefaultListID.AutoSize = true;
            this.lblDefaultListID.Location = new System.Drawing.Point(9, 22);
            this.lblDefaultListID.Name = "lblDefaultListID";
            this.lblDefaultListID.Size = new System.Drawing.Size(75, 13);
            this.lblDefaultListID.TabIndex = 16;
            this.lblDefaultListID.Text = "Default List ID";
            // 
            // cboDefaultID
            // 
            this.cboDefaultID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDefaultID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDefaultID.Enabled = false;
            this.cboDefaultID.FormattingEnabled = true;
            this.cboDefaultID.Location = new System.Drawing.Point(89, 19);
            this.cboDefaultID.Name = "cboDefaultID";
            this.cboDefaultID.Size = new System.Drawing.Size(92, 21);
            this.cboDefaultID.TabIndex = 17;
            this.cboDefaultID.SelectedIndexChanged += new System.EventHandler(this.cboDefaultID_SelectedIndexChanged);
            // 
            // lblListsID
            // 
            this.lblListsID.AutoSize = true;
            this.lblListsID.Enabled = false;
            this.lblListsID.Location = new System.Drawing.Point(6, 32);
            this.lblListsID.Name = "lblListsID";
            this.lblListsID.Size = new System.Drawing.Size(42, 13);
            this.lblListsID.TabIndex = 22;
            this.lblListsID.Text = "IDs List";
            // 
            // lstAltListIDs
            // 
            this.lstAltListIDs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAltListIDs.Enabled = false;
            this.lstAltListIDs.FormattingEnabled = true;
            this.lstAltListIDs.IntegralHeight = false;
            this.lstAltListIDs.Location = new System.Drawing.Point(6, 48);
            this.lstAltListIDs.Name = "lstAltListIDs";
            this.lstAltListIDs.Size = new System.Drawing.Size(220, 104);
            this.lstAltListIDs.TabIndex = 23;
            // 
            // btnModifyAltList
            // 
            this.btnModifyAltList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyAltList.Enabled = false;
            this.btnModifyAltList.Location = new System.Drawing.Point(70, 19);
            this.btnModifyAltList.Name = "btnModifyAltList";
            this.btnModifyAltList.Size = new System.Drawing.Size(75, 23);
            this.btnModifyAltList.TabIndex = 21;
            this.btnModifyAltList.Text = "Modify";
            this.btnModifyAltList.UseVisualStyleBackColor = true;
            this.btnModifyAltList.Click += new System.EventHandler(this.btnModifyAltList_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(684, 24);
            this.mnuMain.TabIndex = 4;
            this.mnuMain.Text = "mnuMain";
            // 
            // tsmiFile
            // 
            this.tsmiFile.AutoSize = false;
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSaveFile,
            this.tsSeparator1,
            this.tsmiClose,
            this.tsSeparator2,
            this.tsmiExitApp});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(50, 20);
            this.tsmiFile.Text = "File";
            // 
            // tsmiSaveFile
            // 
            this.tsmiSaveFile.Name = "tsmiSaveFile";
            this.tsmiSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSaveFile.Size = new System.Drawing.Size(198, 22);
            this.tsmiSaveFile.Text = "Save";
            this.tsmiSaveFile.Click += new System.EventHandler(this.msmiSaveFile_Click);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiClose.Size = new System.Drawing.Size(198, 22);
            this.tsmiClose.Text = "Close";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiExitApp
            // 
            this.tsmiExitApp.Name = "tsmiExitApp";
            this.tsmiExitApp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiExitApp.Size = new System.Drawing.Size(198, 22);
            this.tsmiExitApp.Text = "Exit Application";
            this.tsmiExitApp.Click += new System.EventHandler(this.tsmiExitApp_Click);
            // 
            // grpGroupData
            // 
            this.grpGroupData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGroupData.Controls.Add(this.grpListEntries);
            this.grpGroupData.Controls.Add(this.lblValidatorDefaultId);
            this.grpGroupData.Controls.Add(this.grpCustomSize);
            this.grpGroupData.Controls.Add(this.grpAltList);
            this.grpGroupData.Controls.Add(this.lblDefaultListID);
            this.grpGroupData.Controls.Add(this.cboDefaultID);
            this.grpGroupData.Location = new System.Drawing.Point(428, 104);
            this.grpGroupData.Name = "grpGroupData";
            this.grpGroupData.Size = new System.Drawing.Size(244, 345);
            this.grpGroupData.TabIndex = 15;
            this.grpGroupData.TabStop = false;
            this.grpGroupData.Text = "Group Data";
            // 
            // lblValidatorDefaultId
            // 
            this.lblValidatorDefaultId.AutoSize = true;
            this.lblValidatorDefaultId.Font = new System.Drawing.Font("Calibri", 9F);
            this.lblValidatorDefaultId.ForeColor = System.Drawing.Color.Red;
            this.lblValidatorDefaultId.Location = new System.Drawing.Point(187, 22);
            this.lblValidatorDefaultId.Name = "lblValidatorDefaultId";
            this.lblValidatorDefaultId.Size = new System.Drawing.Size(0, 14);
            this.lblValidatorDefaultId.TabIndex = 18;
            // 
            // grpCustomSize
            // 
            this.grpCustomSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCustomSize.Controls.Add(this.chkCustomSize);
            this.grpCustomSize.Controls.Add(this.lblDataID);
            this.grpCustomSize.Controls.Add(this.cboCustomSizeID);
            this.grpCustomSize.Location = new System.Drawing.Point(6, 287);
            this.grpCustomSize.Name = "grpCustomSize";
            this.grpCustomSize.Size = new System.Drawing.Size(232, 46);
            this.grpCustomSize.TabIndex = 25;
            this.grpCustomSize.TabStop = false;
            this.grpCustomSize.Text = "Custom Size";
            // 
            // chkCustomSize
            // 
            this.chkCustomSize.AutoSize = true;
            this.chkCustomSize.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCustomSize.Location = new System.Drawing.Point(6, 0);
            this.chkCustomSize.Name = "chkCustomSize";
            this.chkCustomSize.Size = new System.Drawing.Size(84, 17);
            this.chkCustomSize.TabIndex = 26;
            this.chkCustomSize.Text = "Custom Size";
            this.chkCustomSize.UseVisualStyleBackColor = true;
            this.chkCustomSize.Visible = false;
            this.chkCustomSize.CheckedChanged += new System.EventHandler(this.chkCustomSize_CheckedChanged);
            // 
            // lblDataID
            // 
            this.lblDataID.AutoSize = true;
            this.lblDataID.Enabled = false;
            this.lblDataID.Location = new System.Drawing.Point(25, 22);
            this.lblDataID.Name = "lblDataID";
            this.lblDataID.Size = new System.Drawing.Size(44, 13);
            this.lblDataID.TabIndex = 27;
            this.lblDataID.Text = "Data ID";
            // 
            // cboCustomSizeID
            // 
            this.cboCustomSizeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomSizeID.Enabled = false;
            this.cboCustomSizeID.FormattingEnabled = true;
            this.cboCustomSizeID.Location = new System.Drawing.Point(75, 19);
            this.cboCustomSizeID.Name = "cboCustomSizeID";
            this.cboCustomSizeID.Size = new System.Drawing.Size(151, 21);
            this.cboCustomSizeID.TabIndex = 28;
            this.cboCustomSizeID.SelectedIndexChanged += new System.EventHandler(this.cboCustomSizeID_SelectedIndexChanged);
            this.cboCustomSizeID.TextChanged += new System.EventHandler(this.cboCustomSizeID_TextChanged);
            // 
            // grpAltList
            // 
            this.grpAltList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAltList.Controls.Add(this.btnClearAltList);
            this.grpAltList.Controls.Add(this.chkAltList);
            this.grpAltList.Controls.Add(this.lstAltListIDs);
            this.grpAltList.Controls.Add(this.btnModifyAltList);
            this.grpAltList.Controls.Add(this.lblListsID);
            this.grpAltList.Location = new System.Drawing.Point(6, 123);
            this.grpAltList.Name = "grpAltList";
            this.grpAltList.Size = new System.Drawing.Size(232, 158);
            this.grpAltList.TabIndex = 19;
            this.grpAltList.TabStop = false;
            this.grpAltList.Text = "Alternate Lists";
            // 
            // btnClearAltList
            // 
            this.btnClearAltList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAltList.Enabled = false;
            this.btnClearAltList.Location = new System.Drawing.Point(151, 19);
            this.btnClearAltList.Name = "btnClearAltList";
            this.btnClearAltList.Size = new System.Drawing.Size(75, 23);
            this.btnClearAltList.TabIndex = 24;
            this.btnClearAltList.Text = "Clear";
            this.btnClearAltList.UseVisualStyleBackColor = true;
            this.btnClearAltList.Click += new System.EventHandler(this.btnClearAltList_Click);
            // 
            // chkAltList
            // 
            this.chkAltList.AutoSize = true;
            this.chkAltList.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAltList.Location = new System.Drawing.Point(6, 0);
            this.chkAltList.Name = "chkAltList";
            this.chkAltList.Size = new System.Drawing.Size(95, 17);
            this.chkAltList.TabIndex = 20;
            this.chkAltList.Text = "Alternate Lists";
            this.chkAltList.UseVisualStyleBackColor = true;
            this.chkAltList.Visible = false;
            this.chkAltList.CheckedChanged += new System.EventHandler(this.chkAltList_CheckedChanged);
            // 
            // grpGroupList
            // 
            this.grpGroupList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGroupList.Controls.Add(this.dgvGroups);
            this.grpGroupList.Location = new System.Drawing.Point(12, 27);
            this.grpGroupList.Name = "grpGroupList";
            this.grpGroupList.Size = new System.Drawing.Size(410, 279);
            this.grpGroupList.TabIndex = 0;
            this.grpGroupList.TabStop = false;
            this.grpGroupList.Text = "Group List";
            // 
            // dgvGroups
            // 
            this.dgvGroups.AllowUserToAddRows = false;
            this.dgvGroups.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.dgvGroups.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGroups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroups.Location = new System.Drawing.Point(6, 19);
            this.dgvGroups.MultiSelect = false;
            this.dgvGroups.Name = "dgvGroups";
            this.dgvGroups.ReadOnly = true;
            this.dgvGroups.RowHeadersVisible = false;
            this.dgvGroups.RowHeadersWidth = 30;
            this.dgvGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGroups.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGroups.Size = new System.Drawing.Size(398, 254);
            this.dgvGroups.StandardTab = true;
            this.dgvGroups.TabIndex = 1;
            this.dgvGroups.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGroups_CellDoubleClick);
            this.dgvGroups.SelectionChanged += new System.EventHandler(this.dgvGroups_SelectionChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(329, 48);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(329, 19);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Accept";
            this.btnAccept.Visible = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Enabled = false;
            this.btnRemoveGroup.Location = new System.Drawing.Point(6, 77);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveGroup.TabIndex = 7;
            this.btnRemoveGroup.Text = "&Remove";
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
            // 
            // btnEditGroup
            // 
            this.btnEditGroup.Enabled = false;
            this.btnEditGroup.Location = new System.Drawing.Point(6, 48);
            this.btnEditGroup.Name = "btnEditGroup";
            this.btnEditGroup.Size = new System.Drawing.Size(75, 23);
            this.btnEditGroup.TabIndex = 4;
            this.btnEditGroup.Text = "&Edit";
            this.btnEditGroup.UseVisualStyleBackColor = true;
            this.btnEditGroup.Click += new System.EventHandler(this.btnEditGroup_Click);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Enabled = false;
            this.btnNewGroup.Location = new System.Drawing.Point(6, 19);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(75, 23);
            this.btnNewGroup.TabIndex = 3;
            this.btnNewGroup.Text = "&New";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // grpModiyControlPanel
            // 
            this.grpModiyControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModiyControlPanel.Controls.Add(this.btnNewGroup);
            this.grpModiyControlPanel.Controls.Add(this.btnCancel);
            this.grpModiyControlPanel.Controls.Add(this.btnEditGroup);
            this.grpModiyControlPanel.Controls.Add(this.btnAccept);
            this.grpModiyControlPanel.Controls.Add(this.btnRemoveGroup);
            this.grpModiyControlPanel.Location = new System.Drawing.Point(12, 343);
            this.grpModiyControlPanel.Name = "grpModiyControlPanel";
            this.grpModiyControlPanel.Size = new System.Drawing.Size(410, 106);
            this.grpModiyControlPanel.TabIndex = 2;
            this.grpModiyControlPanel.TabStop = false;
            // 
            // listBox1
            // 
            this.lbxSizeListEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSizeListEntries.FormattingEnabled = true;
            this.lbxSizeListEntries.IntegralHeight = false;
            this.lbxSizeListEntries.Location = new System.Drawing.Point(3, 16);
            this.lbxSizeListEntries.Name = "listBox1";
            this.lbxSizeListEntries.Size = new System.Drawing.Size(226, 52);
            this.lbxSizeListEntries.TabIndex = 0;
            // 
            // grpListEntries
            // 
            this.grpListEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpListEntries.Controls.Add(this.lbxSizeListEntries);
            this.grpListEntries.Location = new System.Drawing.Point(6, 46);
            this.grpListEntries.Name = "grpListEntries";
            this.grpListEntries.Size = new System.Drawing.Size(232, 71);
            this.grpListEntries.TabIndex = 26;
            this.grpListEntries.TabStop = false;
            this.grpListEntries.Text = "List Entries";
            // 
            // SizeGroupEditor
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.grpModiyControlPanel);
            this.Controls.Add(this.grpGroupList);
            this.Controls.Add(this.grpGroupData);
            this.Controls.Add(this.grpGroupMetadata);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "SizeGroupEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Size Group Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SizeGroupEditor_Load);
            this.grpGroupMetadata.ResumeLayout(false);
            this.grpGroupMetadata.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.grpGroupData.ResumeLayout(false);
            this.grpGroupData.PerformLayout();
            this.grpCustomSize.ResumeLayout(false);
            this.grpCustomSize.PerformLayout();
            this.grpAltList.ResumeLayout(false);
            this.grpAltList.PerformLayout();
            this.grpGroupList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroups)).EndInit();
            this.grpModiyControlPanel.ResumeLayout(false);
            this.grpListEntries.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGroupID;
        private System.Windows.Forms.TextBox txtGroupID;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.GroupBox grpGroupMetadata;
        private System.Windows.Forms.Label lblDefaultListID;
        private System.Windows.Forms.ComboBox cboDefaultID;
        private System.Windows.Forms.Label lblListsID;
        private System.Windows.Forms.ListBox lstAltListIDs;
        private System.Windows.Forms.Button btnModifyAltList;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveFile;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripSeparator tsSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExitApp;
        private System.Windows.Forms.GroupBox grpGroupData;
        private System.Windows.Forms.GroupBox grpAltList;
        private System.Windows.Forms.GroupBox grpGroupList;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.Button btnEditGroup;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.CheckBox chkAltList;
        private System.Windows.Forms.DataGridView dgvGroups;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.GroupBox grpCustomSize;
        private System.Windows.Forms.CheckBox chkCustomSize;
        private System.Windows.Forms.Label lblDataID;
        private System.Windows.Forms.ComboBox cboCustomSizeID;
        private System.Windows.Forms.Label lblValidatorGroupId;
        private System.Windows.Forms.Label lblValidatorGroupName;
        private System.Windows.Forms.Label lblValidatorDefaultId;
        private System.Windows.Forms.GroupBox grpModiyControlPanel;
        private System.Windows.Forms.Button btnClearAltList;
        private System.Windows.Forms.ListBox lbxSizeListEntries;
        private System.Windows.Forms.GroupBox grpListEntries;
    }
}