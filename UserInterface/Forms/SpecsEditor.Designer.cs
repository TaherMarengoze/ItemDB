namespace UserInterface.Forms
{
    partial class SpecsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpecsEditor));
            this.lblSpecsID = new System.Windows.Forms.Label();
            this.txtSpecsID = new System.Windows.Forms.TextBox();
            this.txtSpecsPattern = new System.Windows.Forms.TextBox();
            this.lblSpecsPattern = new System.Windows.Forms.Label();
            this.dgvSpec = new System.Windows.Forms.DataGridView();
            this.cmsSpecsItemOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiInsertToken = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSpecsItems = new System.Windows.Forms.GroupBox();
            this.btnSiAccept = new System.Windows.Forms.Button();
            this.chkSpecConfirmRemove = new System.Windows.Forms.CheckBox();
            this.btnSiCancel = new System.Windows.Forms.Button();
            this.btnSiRemove = new System.Windows.Forms.Button();
            this.btnSiEdit = new System.Windows.Forms.Button();
            this.btnSiAdd = new System.Windows.Forms.Button();
            this.txtSiIndex = new System.Windows.Forms.TextBox();
            this.grpSpecItemData = new System.Windows.Forms.GroupBox();
            this.btnSiInsertVal = new System.Windows.Forms.Button();
            this.btnSiDefaultVal = new System.Windows.Forms.Button();
            this.grpSpecType = new System.Windows.Forms.GroupBox();
            this.cboCustomTypeSelector = new System.Windows.Forms.ComboBox();
            this.rdoListType = new System.Windows.Forms.RadioButton();
            this.rdoCustomType = new System.Windows.Forms.RadioButton();
            this.grpListEntries = new System.Windows.Forms.GroupBox();
            this.chkListEntryConfirmRemove = new System.Windows.Forms.CheckBox();
            this.btnListEntryRemove = new System.Windows.Forms.Button();
            this.btnListEntryEdit = new System.Windows.Forms.Button();
            this.btnListEntryAdd = new System.Windows.Forms.Button();
            this.dgvListEntries = new System.Windows.Forms.DataGridView();
            this.lblSpecValuePattern = new System.Windows.Forms.Label();
            this.lblSpecName = new System.Windows.Forms.Label();
            this.lblSpecIndex = new System.Windows.Forms.Label();
            this.txtSiValuePattern = new System.Windows.Forms.TextBox();
            this.txtSiName = new System.Windows.Forms.TextBox();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.lbxSpecs = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnRemoveSpecs = new System.Windows.Forms.Button();
            this.btnNewSpecs = new System.Windows.Forms.Button();
            this.btnEditSpecs = new System.Windows.Forms.Button();
            this.chkSpecsConfirmRemove = new System.Windows.Forms.CheckBox();
            this.lblSpecsIdValidator = new System.Windows.Forms.Label();
            this.txtSpecsName = new System.Windows.Forms.TextBox();
            this.lblSpecsName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
            this.cmsSpecsItemOptions.SuspendLayout();
            this.grpSpecsItems.SuspendLayout();
            this.grpSpecItemData.SuspendLayout();
            this.grpSpecType.SuspendLayout();
            this.grpListEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListEntries)).BeginInit();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSpecsID
            // 
            this.lblSpecsID.AutoSize = true;
            this.lblSpecsID.Location = new System.Drawing.Point(14, 30);
            this.lblSpecsID.Name = "lblSpecsID";
            this.lblSpecsID.Size = new System.Drawing.Size(49, 13);
            this.lblSpecsID.TabIndex = 6;
            this.lblSpecsID.Text = "Specs I&D";
            this.lblSpecsID.Click += new System.EventHandler(this.lblSpecsID_Click);
            // 
            // txtSpecsID
            // 
            this.txtSpecsID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSpecsID.Location = new System.Drawing.Point(68, 27);
            this.txtSpecsID.MaxLength = 5;
            this.txtSpecsID.Name = "txtSpecsID";
            this.txtSpecsID.ReadOnly = true;
            this.txtSpecsID.Size = new System.Drawing.Size(82, 20);
            this.txtSpecsID.TabIndex = 7;
            this.txtSpecsID.TextChanged += new System.EventHandler(this.txtSpecsID_TextChanged);
            // 
            // txtSpecsPattern
            // 
            this.txtSpecsPattern.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtSpecsPattern.Location = new System.Drawing.Point(316, 27);
            this.txtSpecsPattern.Name = "txtSpecsPattern";
            this.txtSpecsPattern.ReadOnly = true;
            this.txtSpecsPattern.Size = new System.Drawing.Size(162, 20);
            this.txtSpecsPattern.TabIndex = 9;
            // 
            // lblSpecsPattern
            // 
            this.lblSpecsPattern.AutoSize = true;
            this.lblSpecsPattern.Location = new System.Drawing.Point(242, 30);
            this.lblSpecsPattern.Name = "lblSpecsPattern";
            this.lblSpecsPattern.Size = new System.Drawing.Size(68, 13);
            this.lblSpecsPattern.TabIndex = 8;
            this.lblSpecsPattern.Text = "Te&xt Pattern";
            this.lblSpecsPattern.Click += new System.EventHandler(this.lblSpecsPattern_Click);
            // 
            // dgvSpec
            // 
            this.dgvSpec.AllowUserToAddRows = false;
            this.dgvSpec.AllowUserToDeleteRows = false;
            this.dgvSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpec.ContextMenuStrip = this.cmsSpecsItemOptions;
            this.dgvSpec.Location = new System.Drawing.Point(6, 16);
            this.dgvSpec.MultiSelect = false;
            this.dgvSpec.Name = "dgvSpec";
            this.dgvSpec.ReadOnly = true;
            this.dgvSpec.RowHeadersWidth = 30;
            this.dgvSpec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpec.Size = new System.Drawing.Size(589, 127);
            this.dgvSpec.TabIndex = 0;
            this.dgvSpec.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellDoubleClick);
            this.dgvSpec.SelectionChanged += new System.EventHandler(this.dgvSpecsItems_SelectionChanged);
            this.dgvSpec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvSpec_MouseDown);
            // 
            // cmsSpecsItemOptions
            // 
            this.cmsSpecsItemOptions.DropShadowEnabled = false;
            this.cmsSpecsItemOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInsertToken});
            this.cmsSpecsItemOptions.Name = "cmsSpecsItemOptions";
            this.cmsSpecsItemOptions.Size = new System.Drawing.Size(230, 26);
            this.cmsSpecsItemOptions.Text = "Tokens";
            // 
            // tsmiInsertToken
            // 
            this.tsmiInsertToken.Name = "tsmiInsertToken";
            this.tsmiInsertToken.Size = new System.Drawing.Size(229, 22);
            this.tsmiInsertToken.Text = "Insert Token for Selected Item";
            this.tsmiInsertToken.Click += new System.EventHandler(this.tsmiInsertToken_Click);
            // 
            // grpSpecsItems
            // 
            this.grpSpecsItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSpecsItems.Controls.Add(this.btnSiAccept);
            this.grpSpecsItems.Controls.Add(this.chkSpecConfirmRemove);
            this.grpSpecsItems.Controls.Add(this.btnSiCancel);
            this.grpSpecsItems.Controls.Add(this.btnSiRemove);
            this.grpSpecsItems.Controls.Add(this.btnSiEdit);
            this.grpSpecsItems.Controls.Add(this.btnSiAdd);
            this.grpSpecsItems.Controls.Add(this.dgvSpec);
            this.grpSpecsItems.Location = new System.Drawing.Point(11, 79);
            this.grpSpecsItems.Name = "grpSpecsItems";
            this.grpSpecsItems.Size = new System.Drawing.Size(602, 180);
            this.grpSpecsItems.TabIndex = 10;
            this.grpSpecsItems.TabStop = false;
            this.grpSpecsItems.Text = "Spec Ite&ms";
            // 
            // btnSiAccept
            // 
            this.btnSiAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSiAccept.Image = global::UserInterface.Properties.Resources.Accept_icon_16x;
            this.btnSiAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSiAccept.Location = new System.Drawing.Point(198, 149);
            this.btnSiAccept.Name = "btnSiAccept";
            this.btnSiAccept.Size = new System.Drawing.Size(75, 25);
            this.btnSiAccept.TabIndex = 4;
            this.btnSiAccept.Text = "Accep&t";
            this.btnSiAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSiAccept.UseVisualStyleBackColor = true;
            this.btnSiAccept.Visible = false;
            this.btnSiAccept.Click += new System.EventHandler(this.btnSiAccept_Click);
            // 
            // chkSpecConfirmRemove
            // 
            this.chkSpecConfirmRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSpecConfirmRemove.Checked = true;
            this.chkSpecConfirmRemove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpecConfirmRemove.Image = global::UserInterface.Properties.Resources.faq_icon2_16x;
            this.chkSpecConfirmRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSpecConfirmRemove.Location = new System.Drawing.Point(367, 147);
            this.chkSpecConfirmRemove.Name = "chkSpecConfirmRemove";
            this.chkSpecConfirmRemove.Size = new System.Drawing.Size(147, 27);
            this.chkSpecConfirmRemove.TabIndex = 13;
            this.chkSpecConfirmRemove.Text = "Confirm &before delete";
            this.chkSpecConfirmRemove.UseVisualStyleBackColor = true;
            // 
            // btnSiCancel
            // 
            this.btnSiCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSiCancel.Image = global::UserInterface.Properties.Resources.cancel_icon_16x;
            this.btnSiCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSiCancel.Location = new System.Drawing.Point(279, 149);
            this.btnSiCancel.Name = "btnSiCancel";
            this.btnSiCancel.Size = new System.Drawing.Size(75, 25);
            this.btnSiCancel.TabIndex = 5;
            this.btnSiCancel.Text = "Ca&ncel";
            this.btnSiCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSiCancel.UseVisualStyleBackColor = true;
            this.btnSiCancel.Visible = false;
            this.btnSiCancel.Click += new System.EventHandler(this.btnSiCancel_Click);
            // 
            // btnSiRemove
            // 
            this.btnSiRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSiRemove.Enabled = false;
            this.btnSiRemove.Image = global::UserInterface.Properties.Resources.delete_icon_16x;
            this.btnSiRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSiRemove.Location = new System.Drawing.Point(520, 149);
            this.btnSiRemove.Name = "btnSiRemove";
            this.btnSiRemove.Size = new System.Drawing.Size(75, 25);
            this.btnSiRemove.TabIndex = 3;
            this.btnSiRemove.Text = "&Remove";
            this.btnSiRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSiRemove.UseVisualStyleBackColor = true;
            this.btnSiRemove.Click += new System.EventHandler(this.btnSiRemove_Click);
            // 
            // btnSiEdit
            // 
            this.btnSiEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSiEdit.Enabled = false;
            this.btnSiEdit.Image = global::UserInterface.Properties.Resources.Edit_icon_16x;
            this.btnSiEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSiEdit.Location = new System.Drawing.Point(102, 149);
            this.btnSiEdit.Name = "btnSiEdit";
            this.btnSiEdit.Size = new System.Drawing.Size(90, 25);
            this.btnSiEdit.TabIndex = 2;
            this.btnSiEdit.Text = "&Edit";
            this.btnSiEdit.UseVisualStyleBackColor = true;
            this.btnSiEdit.Click += new System.EventHandler(this.btnSiEdit_Click);
            // 
            // btnSiAdd
            // 
            this.btnSiAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSiAdd.Enabled = false;
            this.btnSiAdd.Image = global::UserInterface.Properties.Resources.add_icon_16x;
            this.btnSiAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSiAdd.Location = new System.Drawing.Point(6, 149);
            this.btnSiAdd.Name = "btnSiAdd";
            this.btnSiAdd.Size = new System.Drawing.Size(90, 25);
            this.btnSiAdd.TabIndex = 1;
            this.btnSiAdd.Text = "&Add";
            this.btnSiAdd.UseVisualStyleBackColor = true;
            this.btnSiAdd.Click += new System.EventHandler(this.btnSiAdd_Click);
            // 
            // txtSiIndex
            // 
            this.txtSiIndex.Location = new System.Drawing.Point(87, 19);
            this.txtSiIndex.MaxLength = 7;
            this.txtSiIndex.Name = "txtSiIndex";
            this.txtSiIndex.ReadOnly = true;
            this.txtSiIndex.Size = new System.Drawing.Size(53, 20);
            this.txtSiIndex.TabIndex = 1;
            // 
            // grpSpecItemData
            // 
            this.grpSpecItemData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSpecItemData.Controls.Add(this.btnSiInsertVal);
            this.grpSpecItemData.Controls.Add(this.btnSiDefaultVal);
            this.grpSpecItemData.Controls.Add(this.grpSpecType);
            this.grpSpecItemData.Controls.Add(this.grpListEntries);
            this.grpSpecItemData.Controls.Add(this.lblSpecValuePattern);
            this.grpSpecItemData.Controls.Add(this.lblSpecName);
            this.grpSpecItemData.Controls.Add(this.lblSpecIndex);
            this.grpSpecItemData.Controls.Add(this.txtSiValuePattern);
            this.grpSpecItemData.Controls.Add(this.txtSiName);
            this.grpSpecItemData.Controls.Add(this.txtSiIndex);
            this.grpSpecItemData.Location = new System.Drawing.Point(11, 265);
            this.grpSpecItemData.Name = "grpSpecItemData";
            this.grpSpecItemData.Size = new System.Drawing.Size(602, 273);
            this.grpSpecItemData.TabIndex = 11;
            this.grpSpecItemData.TabStop = false;
            this.grpSpecItemData.Text = "Spec Item &Data";
            // 
            // btnSiInsertVal
            // 
            this.btnSiInsertVal.Location = new System.Drawing.Point(271, 69);
            this.btnSiInsertVal.Name = "btnSiInsertVal";
            this.btnSiInsertVal.Size = new System.Drawing.Size(73, 23);
            this.btnSiInsertVal.TabIndex = 9;
            this.btnSiInsertVal.Text = "Insert Value";
            this.btnSiInsertVal.UseVisualStyleBackColor = true;
            this.btnSiInsertVal.Visible = false;
            this.btnSiInsertVal.Click += new System.EventHandler(this.btnSiInsertVal_Click);
            // 
            // btnSiDefaultVal
            // 
            this.btnSiDefaultVal.Location = new System.Drawing.Point(205, 69);
            this.btnSiDefaultVal.Name = "btnSiDefaultVal";
            this.btnSiDefaultVal.Size = new System.Drawing.Size(60, 23);
            this.btnSiDefaultVal.TabIndex = 6;
            this.btnSiDefaultVal.Text = "Default";
            this.btnSiDefaultVal.UseVisualStyleBackColor = true;
            this.btnSiDefaultVal.Visible = false;
            this.btnSiDefaultVal.Click += new System.EventHandler(this.btnSiDefaultVal_Click);
            // 
            // grpSpecType
            // 
            this.grpSpecType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSpecType.Controls.Add(this.cboCustomTypeSelector);
            this.grpSpecType.Controls.Add(this.rdoListType);
            this.grpSpecType.Controls.Add(this.rdoCustomType);
            this.grpSpecType.Enabled = false;
            this.grpSpecType.Location = new System.Drawing.Point(361, 19);
            this.grpSpecType.Name = "grpSpecType";
            this.grpSpecType.Size = new System.Drawing.Size(235, 72);
            this.grpSpecType.TabIndex = 7;
            this.grpSpecType.TabStop = false;
            this.grpSpecType.Text = "Type";
            // 
            // cboCustomTypeSelector
            // 
            this.cboCustomTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomTypeSelector.Enabled = false;
            this.cboCustomTypeSelector.FormattingEnabled = true;
            this.cboCustomTypeSelector.Location = new System.Drawing.Point(73, 18);
            this.cboCustomTypeSelector.Name = "cboCustomTypeSelector";
            this.cboCustomTypeSelector.Size = new System.Drawing.Size(156, 21);
            this.cboCustomTypeSelector.TabIndex = 2;
            this.cboCustomTypeSelector.SelectedIndexChanged += new System.EventHandler(this.cboCustomTypeSelector_SelectedIndexChanged);
            // 
            // rdoListType
            // 
            this.rdoListType.Location = new System.Drawing.Point(6, 49);
            this.rdoListType.Name = "rdoListType";
            this.rdoListType.Size = new System.Drawing.Size(61, 17);
            this.rdoListType.TabIndex = 1;
            this.rdoListType.TabStop = true;
            this.rdoListType.Text = "&List";
            this.rdoListType.UseVisualStyleBackColor = true;
            this.rdoListType.CheckedChanged += new System.EventHandler(this.rdoListType_CheckedChanged);
            // 
            // rdoCustomType
            // 
            this.rdoCustomType.Location = new System.Drawing.Point(6, 19);
            this.rdoCustomType.Name = "rdoCustomType";
            this.rdoCustomType.Size = new System.Drawing.Size(61, 17);
            this.rdoCustomType.TabIndex = 0;
            this.rdoCustomType.TabStop = true;
            this.rdoCustomType.Text = "C&ustom";
            this.rdoCustomType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoCustomType.UseVisualStyleBackColor = true;
            this.rdoCustomType.CheckedChanged += new System.EventHandler(this.rdoCustomType_CheckedChanged);
            // 
            // grpListEntries
            // 
            this.grpListEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpListEntries.Controls.Add(this.chkListEntryConfirmRemove);
            this.grpListEntries.Controls.Add(this.btnListEntryRemove);
            this.grpListEntries.Controls.Add(this.btnListEntryEdit);
            this.grpListEntries.Controls.Add(this.btnListEntryAdd);
            this.grpListEntries.Controls.Add(this.dgvListEntries);
            this.grpListEntries.Location = new System.Drawing.Point(6, 97);
            this.grpListEntries.Name = "grpListEntries";
            this.grpListEntries.Size = new System.Drawing.Size(590, 155);
            this.grpListEntries.TabIndex = 8;
            this.grpListEntries.TabStop = false;
            this.grpListEntries.Text = "L&ist Entries";
            // 
            // chkListEntryConfirmRemove
            // 
            this.chkListEntryConfirmRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListEntryConfirmRemove.Checked = true;
            this.chkListEntryConfirmRemove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkListEntryConfirmRemove.Enabled = false;
            this.chkListEntryConfirmRemove.Image = global::UserInterface.Properties.Resources.faq_icon2_16x;
            this.chkListEntryConfirmRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkListEntryConfirmRemove.Location = new System.Drawing.Point(355, 123);
            this.chkListEntryConfirmRemove.Name = "chkListEntryConfirmRemove";
            this.chkListEntryConfirmRemove.Size = new System.Drawing.Size(148, 26);
            this.chkListEntryConfirmRemove.TabIndex = 4;
            this.chkListEntryConfirmRemove.Text = "Confirm &before delete";
            this.chkListEntryConfirmRemove.UseVisualStyleBackColor = true;
            // 
            // btnListEntryRemove
            // 
            this.btnListEntryRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListEntryRemove.Enabled = false;
            this.btnListEntryRemove.Image = global::UserInterface.Properties.Resources.delete_icon_16x;
            this.btnListEntryRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnListEntryRemove.Location = new System.Drawing.Point(509, 123);
            this.btnListEntryRemove.Name = "btnListEntryRemove";
            this.btnListEntryRemove.Size = new System.Drawing.Size(75, 26);
            this.btnListEntryRemove.TabIndex = 3;
            this.btnListEntryRemove.Text = "&Remove";
            this.btnListEntryRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnListEntryRemove.UseVisualStyleBackColor = true;
            this.btnListEntryRemove.Click += new System.EventHandler(this.btnListEntryRemove_Click);
            // 
            // btnListEntryEdit
            // 
            this.btnListEntryEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnListEntryEdit.Enabled = false;
            this.btnListEntryEdit.Image = global::UserInterface.Properties.Resources.Edit_icon_16x;
            this.btnListEntryEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnListEntryEdit.Location = new System.Drawing.Point(102, 123);
            this.btnListEntryEdit.Name = "btnListEntryEdit";
            this.btnListEntryEdit.Size = new System.Drawing.Size(90, 26);
            this.btnListEntryEdit.TabIndex = 2;
            this.btnListEntryEdit.Text = "&Edit";
            this.btnListEntryEdit.UseVisualStyleBackColor = true;
            this.btnListEntryEdit.Click += new System.EventHandler(this.btnListEntryEdit_Click);
            // 
            // btnListEntryAdd
            // 
            this.btnListEntryAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnListEntryAdd.Enabled = false;
            this.btnListEntryAdd.Image = global::UserInterface.Properties.Resources.add_icon_16x;
            this.btnListEntryAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnListEntryAdd.Location = new System.Drawing.Point(6, 123);
            this.btnListEntryAdd.Name = "btnListEntryAdd";
            this.btnListEntryAdd.Size = new System.Drawing.Size(90, 26);
            this.btnListEntryAdd.TabIndex = 1;
            this.btnListEntryAdd.Text = "&Add";
            this.btnListEntryAdd.UseVisualStyleBackColor = true;
            this.btnListEntryAdd.Click += new System.EventHandler(this.btnListEntryAdd_Click);
            // 
            // dgvListEntries
            // 
            this.dgvListEntries.AllowUserToAddRows = false;
            this.dgvListEntries.AllowUserToDeleteRows = false;
            this.dgvListEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListEntries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvListEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListEntries.Location = new System.Drawing.Point(6, 19);
            this.dgvListEntries.MultiSelect = false;
            this.dgvListEntries.Name = "dgvListEntries";
            this.dgvListEntries.ReadOnly = true;
            this.dgvListEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListEntries.Size = new System.Drawing.Size(578, 98);
            this.dgvListEntries.TabIndex = 0;
            this.dgvListEntries.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListEntries_CellDoubleClick);
            // 
            // lblSpecValuePattern
            // 
            this.lblSpecValuePattern.AutoSize = true;
            this.lblSpecValuePattern.Location = new System.Drawing.Point(9, 74);
            this.lblSpecValuePattern.Name = "lblSpecValuePattern";
            this.lblSpecValuePattern.Size = new System.Drawing.Size(72, 13);
            this.lblSpecValuePattern.TabIndex = 4;
            this.lblSpecValuePattern.Text = "&Value Pattern";
            this.lblSpecValuePattern.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpecValuePattern.Click += new System.EventHandler(this.lblSpecValuePattern_Click);
            // 
            // lblSpecName
            // 
            this.lblSpecName.AutoSize = true;
            this.lblSpecName.Location = new System.Drawing.Point(47, 48);
            this.lblSpecName.Name = "lblSpecName";
            this.lblSpecName.Size = new System.Drawing.Size(34, 13);
            this.lblSpecName.TabIndex = 2;
            this.lblSpecName.Text = "Na&me";
            this.lblSpecName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpecName.Click += new System.EventHandler(this.lblSpecName_Click);
            // 
            // lblSpecIndex
            // 
            this.lblSpecIndex.AutoSize = true;
            this.lblSpecIndex.Location = new System.Drawing.Point(46, 22);
            this.lblSpecIndex.Name = "lblSpecIndex";
            this.lblSpecIndex.Size = new System.Drawing.Size(35, 13);
            this.lblSpecIndex.TabIndex = 0;
            this.lblSpecIndex.Text = "Index";
            this.lblSpecIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpecIndex.Click += new System.EventHandler(this.lblSpecIndex_Click);
            // 
            // txtSiValuePattern
            // 
            this.txtSiValuePattern.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtSiValuePattern.Location = new System.Drawing.Point(87, 71);
            this.txtSiValuePattern.Name = "txtSiValuePattern";
            this.txtSiValuePattern.ReadOnly = true;
            this.txtSiValuePattern.Size = new System.Drawing.Size(112, 20);
            this.txtSiValuePattern.TabIndex = 5;
            this.txtSiValuePattern.TextChanged += new System.EventHandler(this.txtSiValuePattern_TextChanged);
            // 
            // txtSiName
            // 
            this.txtSiName.Location = new System.Drawing.Point(87, 45);
            this.txtSiName.Name = "txtSiName";
            this.txtSiName.ReadOnly = true;
            this.txtSiName.Size = new System.Drawing.Size(112, 20);
            this.txtSiName.TabIndex = 3;
            this.txtSiName.TextChanged += new System.EventHandler(this.txtSiName_TextChanged);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(771, 24);
            this.mnuMain.TabIndex = 12;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSaveFile,
            this.toolStripSeparator1,
            this.tsmiClose,
            this.toolStripSeparator2,
            this.tsmiExitApp});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // tsmiSaveFile
            // 
            this.tsmiSaveFile.Name = "tsmiSaveFile";
            this.tsmiSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSaveFile.Size = new System.Drawing.Size(194, 22);
            this.tsmiSaveFile.Text = "&Save";
            this.tsmiSaveFile.Click += new System.EventHandler(this.mnuItmSaveFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.tsmiClose.Size = new System.Drawing.Size(194, 22);
            this.tsmiClose.Text = "Close";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiExitApp
            // 
            this.tsmiExitApp.Name = "tsmiExitApp";
            this.tsmiExitApp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.tsmiExitApp.Size = new System.Drawing.Size(194, 22);
            this.tsmiExitApp.Text = "E&xit Application";
            this.tsmiExitApp.Click += new System.EventHandler(this.tsmiExitApp_Click);
            // 
            // lbxSpecs
            // 
            this.lbxSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxSpecs.FormattingEnabled = true;
            this.lbxSpecs.Location = new System.Drawing.Point(619, 27);
            this.lbxSpecs.Name = "lbxSpecs";
            this.lbxSpecs.Size = new System.Drawing.Size(152, 355);
            this.lbxSpecs.TabIndex = 0;
            this.lbxSpecs.SelectedIndexChanged += new System.EventHandler(this.lbxSpecs_SelectedIndexChanged);
            this.lbxSpecs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxSpecs_MouseDoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::UserInterface.Properties.Resources.cancel_icon_16x;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(701, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Image = global::UserInterface.Properties.Resources.Accept_icon_16x;
            this.btnAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccept.Location = new System.Drawing.Point(619, 513);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(76, 25);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Acce&pt";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Visible = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnRemoveSpecs
            // 
            this.btnRemoveSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSpecs.Enabled = false;
            this.btnRemoveSpecs.Image = global::UserInterface.Properties.Resources.delete_icon_16x;
            this.btnRemoveSpecs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveSpecs.Location = new System.Drawing.Point(619, 450);
            this.btnRemoveSpecs.Name = "btnRemoveSpecs";
            this.btnRemoveSpecs.Size = new System.Drawing.Size(152, 25);
            this.btnRemoveSpecs.TabIndex = 3;
            this.btnRemoveSpecs.Text = "&Remove Specs";
            this.btnRemoveSpecs.UseVisualStyleBackColor = true;
            this.btnRemoveSpecs.Click += new System.EventHandler(this.btnRemoveSpecs_Click);
            // 
            // btnNewSpecs
            // 
            this.btnNewSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSpecs.Enabled = false;
            this.btnNewSpecs.Image = global::UserInterface.Properties.Resources.add_icon_16x;
            this.btnNewSpecs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewSpecs.Location = new System.Drawing.Point(619, 388);
            this.btnNewSpecs.Name = "btnNewSpecs";
            this.btnNewSpecs.Size = new System.Drawing.Size(152, 25);
            this.btnNewSpecs.TabIndex = 1;
            this.btnNewSpecs.Text = "Add &New Specs";
            this.btnNewSpecs.UseVisualStyleBackColor = true;
            this.btnNewSpecs.Click += new System.EventHandler(this.btnNewSpecs_Click);
            // 
            // btnEditSpecs
            // 
            this.btnEditSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSpecs.Enabled = false;
            this.btnEditSpecs.Image = global::UserInterface.Properties.Resources.Edit_icon_16x;
            this.btnEditSpecs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditSpecs.Location = new System.Drawing.Point(619, 419);
            this.btnEditSpecs.Name = "btnEditSpecs";
            this.btnEditSpecs.Size = new System.Drawing.Size(152, 25);
            this.btnEditSpecs.TabIndex = 2;
            this.btnEditSpecs.Text = "&Edit Specs";
            this.btnEditSpecs.UseVisualStyleBackColor = true;
            this.btnEditSpecs.Click += new System.EventHandler(this.btnEditSpecs_Click);
            // 
            // chkSpecsConfirmRemove
            // 
            this.chkSpecsConfirmRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSpecsConfirmRemove.Checked = true;
            this.chkSpecsConfirmRemove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpecsConfirmRemove.Image = global::UserInterface.Properties.Resources.faq_icon2_16x;
            this.chkSpecsConfirmRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSpecsConfirmRemove.Location = new System.Drawing.Point(619, 481);
            this.chkSpecsConfirmRemove.Name = "chkSpecsConfirmRemove";
            this.chkSpecsConfirmRemove.Size = new System.Drawing.Size(152, 27);
            this.chkSpecsConfirmRemove.TabIndex = 13;
            this.chkSpecsConfirmRemove.Text = "Confirm &before delete";
            this.chkSpecsConfirmRemove.UseVisualStyleBackColor = true;
            // 
            // lblSpecsIdValidator
            // 
            this.lblSpecsIdValidator.ForeColor = System.Drawing.Color.Red;
            this.lblSpecsIdValidator.Location = new System.Drawing.Point(156, 27);
            this.lblSpecsIdValidator.Name = "lblSpecsIdValidator";
            this.lblSpecsIdValidator.Size = new System.Drawing.Size(75, 20);
            this.lblSpecsIdValidator.TabIndex = 14;
            this.lblSpecsIdValidator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSpecsName
            // 
            this.txtSpecsName.Location = new System.Drawing.Point(85, 53);
            this.txtSpecsName.Name = "txtSpecsName";
            this.txtSpecsName.ReadOnly = true;
            this.txtSpecsName.Size = new System.Drawing.Size(162, 20);
            this.txtSpecsName.TabIndex = 7;
            this.txtSpecsName.TextChanged += new System.EventHandler(this.txtSpecsID_TextChanged);
            // 
            // lblSpecsName
            // 
            this.lblSpecsName.AutoSize = true;
            this.lblSpecsName.Location = new System.Drawing.Point(14, 56);
            this.lblSpecsName.Name = "lblSpecsName";
            this.lblSpecsName.Size = new System.Drawing.Size(65, 13);
            this.lblSpecsName.TabIndex = 6;
            this.lblSpecsName.Text = "Specs Name";
            this.lblSpecsName.Click += new System.EventHandler(this.lblSpecsID_Click);
            // 
            // SpecsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 550);
            this.Controls.Add(this.lblSpecsIdValidator);
            this.Controls.Add(this.chkSpecsConfirmRemove);
            this.Controls.Add(this.lblSpecsName);
            this.Controls.Add(this.lblSpecsID);
            this.Controls.Add(this.txtSpecsName);
            this.Controls.Add(this.txtSpecsID);
            this.Controls.Add(this.txtSpecsPattern);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblSpecsPattern);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnRemoveSpecs);
            this.Controls.Add(this.btnNewSpecs);
            this.Controls.Add(this.lbxSpecs);
            this.Controls.Add(this.grpSpecItemData);
            this.Controls.Add(this.grpSpecsItems);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.btnEditSpecs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(787, 589);
            this.Name = "SpecsEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Specs Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SpecsEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
            this.cmsSpecsItemOptions.ResumeLayout(false);
            this.grpSpecsItems.ResumeLayout(false);
            this.grpSpecItemData.ResumeLayout(false);
            this.grpSpecItemData.PerformLayout();
            this.grpSpecType.ResumeLayout(false);
            this.grpListEntries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListEntries)).EndInit();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSpecsID;
        private System.Windows.Forms.TextBox txtSpecsID;
        private System.Windows.Forms.TextBox txtSpecsPattern;
        private System.Windows.Forms.Label lblSpecsPattern;
        private System.Windows.Forms.DataGridView dgvSpec;
        private System.Windows.Forms.GroupBox grpSpecsItems;
        private System.Windows.Forms.Button btnSiRemove;
        private System.Windows.Forms.Button btnSiEdit;
        private System.Windows.Forms.Button btnSiAdd;
        private System.Windows.Forms.TextBox txtSiIndex;
        private System.Windows.Forms.GroupBox grpSpecItemData;
        private System.Windows.Forms.Label lblSpecIndex;
        private System.Windows.Forms.Label lblSpecName;
        private System.Windows.Forms.TextBox txtSiName;
        private System.Windows.Forms.RadioButton rdoCustomType;
        private System.Windows.Forms.RadioButton rdoListType;
        private System.Windows.Forms.Label lblSpecValuePattern;
        private System.Windows.Forms.TextBox txtSiValuePattern;
        private System.Windows.Forms.GroupBox grpListEntries;
        private System.Windows.Forms.DataGridView dgvListEntries;
        private System.Windows.Forms.GroupBox grpSpecType;
        private System.Windows.Forms.Button btnListEntryRemove;
        private System.Windows.Forms.Button btnListEntryEdit;
        private System.Windows.Forms.Button btnListEntryAdd;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExitApp;
        private System.Windows.Forms.ListBox lbxSpecs;
        private System.Windows.Forms.Button btnNewSpecs;
        private System.Windows.Forms.Button btnEditSpecs;
        private System.Windows.Forms.Button btnRemoveSpecs;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSiCancel;
        private System.Windows.Forms.Button btnSiDefaultVal;
        private System.Windows.Forms.Button btnSiAccept;
        private System.Windows.Forms.ComboBox cboCustomTypeSelector;
        private System.Windows.Forms.CheckBox chkListEntryConfirmRemove;
        private System.Windows.Forms.CheckBox chkSpecsConfirmRemove;
        private System.Windows.Forms.Label lblSpecsIdValidator;
        private System.Windows.Forms.CheckBox chkSpecConfirmRemove;
        private System.Windows.Forms.Button btnSiInsertVal;
        private System.Windows.Forms.TextBox txtSpecsName;
        private System.Windows.Forms.Label lblSpecsName;
        private System.Windows.Forms.ContextMenuStrip cmsSpecsItemOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertToken;
    }
}