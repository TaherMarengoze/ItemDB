namespace UserInterface.Forms
{
    partial class ItemEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSample1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblItemId = new System.Windows.Forms.Label();
            this.txtItemId = new System.Windows.Forms.TextBox();
            this.grpDescription = new System.Windows.Forms.GroupBox();
            this.btnClearDescription = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.grpCategory = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.action = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblCatName = new System.Windows.Forms.Label();
            this.txtCatName = new System.Windows.Forms.TextBox();
            this.lblCatId = new System.Windows.Forms.Label();
            this.txtCatId = new System.Windows.Forms.TextBox();
            this.tabDataSections = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblValidatorItemId = new System.Windows.Forms.Label();
            this.grpExistingItemId = new System.Windows.Forms.GroupBox();
            this.dgvItemsId = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblDuplicateImage = new System.Windows.Forms.Label();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.btnImageCancel = new System.Windows.Forms.Button();
            this.chkUseDefName = new System.Windows.Forms.CheckBox();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.btnImageAccept = new System.Windows.Forms.Button();
            this.grpImages = new System.Windows.Forms.GroupBox();
            this.btnAddNewImage = new System.Windows.Forms.Button();
            this.chkAutoAddImage = new System.Windows.Forms.CheckBox();
            this.lbxImages = new System.Windows.Forms.ListBox();
            this.btnModifyImage = new System.Windows.Forms.Button();
            this.btnRemoveImage = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblUom = new System.Windows.Forms.Label();
            this.txtUom = new System.Windows.Forms.TextBox();
            this.grpNames = new System.Windows.Forms.GroupBox();
            this.btnUseBaseName = new System.Windows.Forms.Button();
            this.txtCommonNameEntry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNameCommon = new System.Windows.Forms.Label();
            this.lblNameDisplay = new System.Windows.Forms.Label();
            this.lbxNameCommon = new System.Windows.Forms.ListBox();
            this.btnAddCommonName = new System.Windows.Forms.Button();
            this.btnDeleteCommonName = new System.Windows.Forms.Button();
            this.btnEditCommonName = new System.Windows.Forms.Button();
            this.txtNameDisplay = new System.Windows.Forms.TextBox();
            this.lblNameBase = new System.Windows.Forms.Label();
            this.txtNameBase = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvSpecListEntries = new System.Windows.Forms.DataGridView();
            this.dgvSpecsItems = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnassignSpecs = new System.Windows.Forms.Button();
            this.cboSpecsId = new System.Windows.Forms.ComboBox();
            this.chkRequiredSpecs = new System.Windows.Forms.CheckBox();
            this.lblSpecsId = new System.Windows.Forms.Label();
            this.txtSearchSpecs = new System.Windows.Forms.TextBox();
            this.dgvSpecs = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbxSizeListEntries = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbxAltSizeListEntries = new System.Windows.Forms.ListBox();
            this.cboAltListSelector = new System.Windows.Forms.ComboBox();
            this.lblSizeId = new System.Windows.Forms.Label();
            this.btnUnassignSize = new System.Windows.Forms.Button();
            this.txtSearchSizes = new System.Windows.Forms.TextBox();
            this.cboSizeGroupId = new System.Windows.Forms.ComboBox();
            this.chkRequiredSize = new System.Windows.Forms.CheckBox();
            this.dgvSizeGroup = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbxEndsListEntries = new System.Windows.Forms.ListBox();
            this.btnUnassignEnds = new System.Windows.Forms.Button();
            this.cboEndsListId = new System.Windows.Forms.ComboBox();
            this.txtSearchEnds = new System.Windows.Forms.TextBox();
            this.lblEndsId = new System.Windows.Forms.Label();
            this.chkRequiredEnds = new System.Windows.Forms.CheckBox();
            this.dgvEndsLists = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbxBrandListEntries = new System.Windows.Forms.ListBox();
            this.cboBrandListId = new System.Windows.Forms.ComboBox();
            this.btnUnassignBrand = new System.Windows.Forms.Button();
            this.dgvBrandsLists = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtSearchBrands = new System.Windows.Forms.TextBox();
            this.lblBrandId = new System.Windows.Forms.Label();
            this.chkRequiredBrand = new System.Windows.Forms.CheckBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTest_FormSize = new System.Windows.Forms.Label();
            this.lblTest_FloatingLocation = new System.Windows.Forms.Label();
            this.lblTest_FloatingBounds = new System.Windows.Forms.Label();
            this.tsmiItemFields = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSpecs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSizeList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBrandsList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEndsList = new System.Windows.Forms.ToolStripMenuItem();
            this.picFloating = new System.Windows.Forms.PictureBox();
            this.picExistingImage = new System.Windows.Forms.PictureBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.mnuMain.SuspendLayout();
            this.grpDescription.SuspendLayout();
            this.grpCategory.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.tabDataSections.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpExistingItemId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsId)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.grpImages.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.grpNames.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecListEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecsItems)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecs)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeGroup)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndsLists)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrandsLists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFloating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExistingImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tsmiItemFields,
            this.toolStripMenuItem4});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(784, 24);
            this.mnuMain.TabIndex = 4;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripMenuItem2,
            this.toolStripSeparator2,
            this.toolStripMenuItem3});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem1.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem2.Text = "Close";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem3.Text = "Exit Application";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSample1});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem4.Text = "Test";
            this.toolStripMenuItem4.Visible = false;
            // 
            // tsmiSample1
            // 
            this.tsmiSample1.Name = "tsmiSample1";
            this.tsmiSample1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsmiSample1.Size = new System.Drawing.Size(212, 22);
            this.tsmiSample1.Text = "Test Sample Data 1";
            this.tsmiSample1.Click += new System.EventHandler(this.tsmiSample1_Click);
            // 
            // lblItemId
            // 
            this.lblItemId.Location = new System.Drawing.Point(6, 16);
            this.lblItemId.Name = "lblItemId";
            this.lblItemId.Size = new System.Drawing.Size(50, 20);
            this.lblItemId.TabIndex = 0;
            this.lblItemId.Text = "Item ID";
            this.lblItemId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemId
            // 
            this.txtItemId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemId.Location = new System.Drawing.Point(62, 17);
            this.txtItemId.MaxLength = 5;
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.ShortcutsEnabled = false;
            this.txtItemId.Size = new System.Drawing.Size(69, 20);
            this.txtItemId.TabIndex = 1;
            this.txtItemId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemId.TextChanged += new System.EventHandler(this.txtItemId_TextChanged);
            this.txtItemId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IdTextbox_KeyPress);
            // 
            // grpDescription
            // 
            this.grpDescription.Controls.Add(this.btnClearDescription);
            this.grpDescription.Controls.Add(this.txtDescription);
            this.grpDescription.Location = new System.Drawing.Point(219, 6);
            this.grpDescription.Name = "grpDescription";
            this.grpDescription.Size = new System.Drawing.Size(248, 128);
            this.grpDescription.TabIndex = 1;
            this.grpDescription.TabStop = false;
            this.grpDescription.Text = "Description";
            // 
            // btnClearDescription
            // 
            this.btnClearDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearDescription.AutoSize = true;
            this.btnClearDescription.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearDescription.Location = new System.Drawing.Point(200, 16);
            this.btnClearDescription.Name = "btnClearDescription";
            this.btnClearDescription.Size = new System.Drawing.Size(42, 23);
            this.btnClearDescription.TabIndex = 1;
            this.btnClearDescription.Text = "Clear";
            this.btnClearDescription.UseVisualStyleBackColor = true;
            this.btnClearDescription.Click += new System.EventHandler(this.btnClearDescription_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(6, 16);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(188, 106);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EssayTextbox_KeyPress);
            // 
            // grpCategory
            // 
            this.grpCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpCategory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpCategory.Controls.Add(this.groupBox1);
            this.grpCategory.Controls.Add(this.lblCatName);
            this.grpCategory.Controls.Add(this.txtCatName);
            this.grpCategory.Controls.Add(this.lblCatId);
            this.grpCategory.Controls.Add(this.txtCatId);
            this.grpCategory.Location = new System.Drawing.Point(273, 6);
            this.grpCategory.Name = "grpCategory";
            this.grpCategory.Size = new System.Drawing.Size(264, 324);
            this.grpCategory.TabIndex = 1;
            this.grpCategory.TabStop = false;
            this.grpCategory.Text = "Category";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvCategories);
            this.groupBox1.Location = new System.Drawing.Point(6, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 235);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Existing Categories";
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToResizeColumns = false;
            this.dgvCategories.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvCategories.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategories.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.action});
            this.dgvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategories.Location = new System.Drawing.Point(3, 16);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(244, 216);
            this.dgvCategories.TabIndex = 0;
            this.dgvCategories.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategories_CellContentClick);
            // 
            // action
            // 
            this.action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.action.HeaderText = "Action";
            this.action.Name = "action";
            this.action.ReadOnly = true;
            this.action.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.action.Text = "Use";
            this.action.UseColumnTextForButtonValue = true;
            this.action.Width = 43;
            // 
            // lblCatName
            // 
            this.lblCatName.AutoSize = true;
            this.lblCatName.Location = new System.Drawing.Point(11, 49);
            this.lblCatName.Name = "lblCatName";
            this.lblCatName.Size = new System.Drawing.Size(34, 13);
            this.lblCatName.TabIndex = 2;
            this.lblCatName.Text = "Name";
            // 
            // txtCatName
            // 
            this.txtCatName.Location = new System.Drawing.Point(51, 45);
            this.txtCatName.MaxLength = 50;
            this.txtCatName.Name = "txtCatName";
            this.txtCatName.Size = new System.Drawing.Size(202, 20);
            this.txtCatName.TabIndex = 3;
            this.txtCatName.TextChanged += new System.EventHandler(this.txtCatName_TextChanged);
            this.txtCatName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextbox_KeyPress);
            this.txtCatName.Leave += new System.EventHandler(this.txtCatName_Leave);
            // 
            // lblCatId
            // 
            this.lblCatId.AutoSize = true;
            this.lblCatId.Location = new System.Drawing.Point(27, 21);
            this.lblCatId.Name = "lblCatId";
            this.lblCatId.Size = new System.Drawing.Size(18, 13);
            this.lblCatId.TabIndex = 0;
            this.lblCatId.Text = "ID";
            // 
            // txtCatId
            // 
            this.txtCatId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCatId.Location = new System.Drawing.Point(51, 19);
            this.txtCatId.MaxLength = 5;
            this.txtCatId.Name = "txtCatId";
            this.txtCatId.ShortcutsEnabled = false;
            this.txtCatId.Size = new System.Drawing.Size(69, 20);
            this.txtCatId.TabIndex = 1;
            this.txtCatId.TextChanged += new System.EventHandler(this.txtCatId_TextChanged);
            this.txtCatId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IdTextbox_KeyPress);
            // 
            // tabDataSections
            // 
            this.tabDataSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDataSections.Controls.Add(this.tabPage1);
            this.tabDataSections.Controls.Add(this.tabPage2);
            this.tabDataSections.Controls.Add(this.tabPage4);
            this.tabDataSections.Controls.Add(this.tabPage3);
            this.tabDataSections.ItemSize = new System.Drawing.Size(150, 20);
            this.tabDataSections.Location = new System.Drawing.Point(0, 27);
            this.tabDataSections.Name = "tabDataSections";
            this.tabDataSections.SelectedIndex = 0;
            this.tabDataSections.Size = new System.Drawing.Size(784, 364);
            this.tabDataSections.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabDataSections.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.grpCategory);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lblValidatorItemId);
            this.groupBox2.Controls.Add(this.grpExistingItemId);
            this.groupBox2.Controls.Add(this.lblItemId);
            this.groupBox2.Controls.Add(this.txtItemId);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 324);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Metadata";
            // 
            // lblValidatorItemId
            // 
            this.lblValidatorItemId.AutoSize = true;
            this.lblValidatorItemId.ForeColor = System.Drawing.Color.Red;
            this.lblValidatorItemId.Location = new System.Drawing.Point(137, 20);
            this.lblValidatorItemId.Name = "lblValidatorItemId";
            this.lblValidatorItemId.Size = new System.Drawing.Size(0, 13);
            this.lblValidatorItemId.TabIndex = 2;
            // 
            // grpExistingItemId
            // 
            this.grpExistingItemId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpExistingItemId.Controls.Add(this.dgvItemsId);
            this.grpExistingItemId.Location = new System.Drawing.Point(6, 74);
            this.grpExistingItemId.Name = "grpExistingItemId";
            this.grpExistingItemId.Size = new System.Drawing.Size(249, 232);
            this.grpExistingItemId.TabIndex = 3;
            this.grpExistingItemId.TabStop = false;
            this.grpExistingItemId.Text = "Existing Items";
            // 
            // dgvItemsId
            // 
            this.dgvItemsId.AllowUserToResizeColumns = false;
            this.dgvItemsId.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvItemsId.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvItemsId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItemsId.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvItemsId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemsId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemsId.Location = new System.Drawing.Point(3, 16);
            this.dgvItemsId.MultiSelect = false;
            this.dgvItemsId.Name = "dgvItemsId";
            this.dgvItemsId.ReadOnly = true;
            this.dgvItemsId.RowHeadersVisible = false;
            this.dgvItemsId.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemsId.Size = new System.Drawing.Size(243, 213);
            this.dgvItemsId.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.picFloating);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.grpImages);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.grpNames);
            this.tabPage2.Controls.Add(this.grpDescription);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Description";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lblDuplicateImage);
            this.groupBox7.Controls.Add(this.picExistingImage);
            this.groupBox7.Controls.Add(this.btnBrowseImage);
            this.groupBox7.Controls.Add(this.btnImageCancel);
            this.groupBox7.Controls.Add(this.picImage);
            this.groupBox7.Controls.Add(this.chkUseDefName);
            this.groupBox7.Controls.Add(this.txtImageName);
            this.groupBox7.Controls.Add(this.btnImageAccept);
            this.groupBox7.Location = new System.Drawing.Point(473, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(192, 326);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Image Preview";
            // 
            // lblDuplicateImage
            // 
            this.lblDuplicateImage.AutoSize = true;
            this.lblDuplicateImage.ForeColor = System.Drawing.Color.Red;
            this.lblDuplicateImage.Location = new System.Drawing.Point(25, 246);
            this.lblDuplicateImage.Name = "lblDuplicateImage";
            this.lblDuplicateImage.Size = new System.Drawing.Size(105, 13);
            this.lblDuplicateImage.TabIndex = 8;
            this.lblDuplicateImage.Text = "• Duplicate file name";
            this.lblDuplicateImage.Visible = false;
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.AutoSize = true;
            this.btnBrowseImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseImage.Enabled = false;
            this.btnBrowseImage.Location = new System.Drawing.Point(134, 195);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(52, 23);
            this.btnBrowseImage.TabIndex = 3;
            this.btnBrowseImage.Text = "Browse";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // btnImageCancel
            // 
            this.btnImageCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImageCancel.AutoSize = true;
            this.btnImageCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImageCancel.Enabled = false;
            this.btnImageCancel.Location = new System.Drawing.Point(6, 297);
            this.btnImageCancel.Name = "btnImageCancel";
            this.btnImageCancel.Size = new System.Drawing.Size(49, 23);
            this.btnImageCancel.TabIndex = 7;
            this.btnImageCancel.Text = "Cancel";
            this.btnImageCancel.UseVisualStyleBackColor = true;
            this.btnImageCancel.Click += new System.EventHandler(this.btnCancelAddImage_Click);
            // 
            // chkUseDefName
            // 
            this.chkUseDefName.AutoSize = true;
            this.chkUseDefName.Checked = true;
            this.chkUseDefName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDefName.Enabled = false;
            this.chkUseDefName.Location = new System.Drawing.Point(6, 226);
            this.chkUseDefName.Name = "chkUseDefName";
            this.chkUseDefName.Size = new System.Drawing.Size(112, 17);
            this.chkUseDefName.TabIndex = 4;
            this.chkUseDefName.Text = "Use Default Name";
            this.chkUseDefName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUseDefName.UseVisualStyleBackColor = true;
            this.chkUseDefName.CheckedChanged += new System.EventHandler(this.chkUseDefName_CheckedChanged);
            // 
            // txtImageName
            // 
            this.txtImageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImageName.Enabled = false;
            this.txtImageName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtImageName.Location = new System.Drawing.Point(6, 195);
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.ReadOnly = true;
            this.txtImageName.Size = new System.Drawing.Size(122, 22);
            this.txtImageName.TabIndex = 5;
            this.txtImageName.TextChanged += new System.EventHandler(this.txtImageName_TextChanged);
            // 
            // btnImageAccept
            // 
            this.btnImageAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImageAccept.Enabled = false;
            this.btnImageAccept.Location = new System.Drawing.Point(126, 297);
            this.btnImageAccept.Name = "btnImageAccept";
            this.btnImageAccept.Size = new System.Drawing.Size(60, 23);
            this.btnImageAccept.TabIndex = 6;
            this.btnImageAccept.Text = "Accept";
            this.btnImageAccept.UseVisualStyleBackColor = true;
            this.btnImageAccept.Click += new System.EventHandler(this.btnImageAccept_Click);
            // 
            // grpImages
            // 
            this.grpImages.Controls.Add(this.btnAddNewImage);
            this.grpImages.Controls.Add(this.chkAutoAddImage);
            this.grpImages.Controls.Add(this.lbxImages);
            this.grpImages.Controls.Add(this.btnModifyImage);
            this.grpImages.Controls.Add(this.btnRemoveImage);
            this.grpImages.Location = new System.Drawing.Point(219, 193);
            this.grpImages.Name = "grpImages";
            this.grpImages.Size = new System.Drawing.Size(248, 139);
            this.grpImages.TabIndex = 3;
            this.grpImages.TabStop = false;
            this.grpImages.Text = "Images";
            // 
            // btnAddNewImage
            // 
            this.btnAddNewImage.Location = new System.Drawing.Point(185, 19);
            this.btnAddNewImage.Name = "btnAddNewImage";
            this.btnAddNewImage.Size = new System.Drawing.Size(57, 23);
            this.btnAddNewImage.TabIndex = 5;
            this.btnAddNewImage.Text = "Add";
            this.btnAddNewImage.UseVisualStyleBackColor = true;
            this.btnAddNewImage.Click += new System.EventHandler(this.btnAddNewImage_Click);
            // 
            // chkAutoAddImage
            // 
            this.chkAutoAddImage.AutoSize = true;
            this.chkAutoAddImage.Checked = true;
            this.chkAutoAddImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoAddImage.Location = new System.Drawing.Point(171, 116);
            this.chkAutoAddImage.Name = "chkAutoAddImage";
            this.chkAutoAddImage.Size = new System.Drawing.Size(71, 17);
            this.chkAutoAddImage.TabIndex = 4;
            this.chkAutoAddImage.Text = "Auto Add";
            this.chkAutoAddImage.UseVisualStyleBackColor = true;
            this.chkAutoAddImage.CheckedChanged += new System.EventHandler(this.chkAutoAddImage_CheckedChanged);
            // 
            // lbxImages
            // 
            this.lbxImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbxImages.Font = new System.Drawing.Font("Calibri", 9F);
            this.lbxImages.FormattingEnabled = true;
            this.lbxImages.HorizontalScrollbar = true;
            this.lbxImages.IntegralHeight = false;
            this.lbxImages.ItemHeight = 14;
            this.lbxImages.Location = new System.Drawing.Point(6, 19);
            this.lbxImages.Name = "lbxImages";
            this.lbxImages.Size = new System.Drawing.Size(159, 114);
            this.lbxImages.TabIndex = 0;
            this.lbxImages.SelectedIndexChanged += new System.EventHandler(this.lbxImages_SelectedIndexChanged);
            // 
            // btnModifyImage
            // 
            this.btnModifyImage.Enabled = false;
            this.btnModifyImage.Location = new System.Drawing.Point(185, 48);
            this.btnModifyImage.Name = "btnModifyImage";
            this.btnModifyImage.Size = new System.Drawing.Size(57, 23);
            this.btnModifyImage.TabIndex = 1;
            this.btnModifyImage.Text = "Edit";
            this.btnModifyImage.UseVisualStyleBackColor = true;
            this.btnModifyImage.Click += new System.EventHandler(this.btnEditImage_Click);
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.Enabled = false;
            this.btnRemoveImage.Location = new System.Drawing.Point(185, 77);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(57, 23);
            this.btnRemoveImage.TabIndex = 2;
            this.btnRemoveImage.Text = "Remove";
            this.btnRemoveImage.UseVisualStyleBackColor = true;
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblUom);
            this.groupBox6.Controls.Add(this.txtUom);
            this.groupBox6.Location = new System.Drawing.Point(219, 140);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(248, 47);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Unit of Measurment";
            // 
            // lblUom
            // 
            this.lblUom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblUom.Location = new System.Drawing.Point(6, 16);
            this.lblUom.Name = "lblUom";
            this.lblUom.Size = new System.Drawing.Size(75, 20);
            this.lblUom.TabIndex = 0;
            this.lblUom.Text = "UoM";
            this.lblUom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUom
            // 
            this.txtUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUom.Location = new System.Drawing.Point(81, 16);
            this.txtUom.MaxLength = 30;
            this.txtUom.Name = "txtUom";
            this.txtUom.Size = new System.Drawing.Size(69, 20);
            this.txtUom.TabIndex = 1;
            this.txtUom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUom.TextChanged += new System.EventHandler(this.txtUom_TextChanged);
            // 
            // grpNames
            // 
            this.grpNames.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpNames.Controls.Add(this.btnUseBaseName);
            this.grpNames.Controls.Add(this.txtCommonNameEntry);
            this.grpNames.Controls.Add(this.label1);
            this.grpNames.Controls.Add(this.lblNameCommon);
            this.grpNames.Controls.Add(this.lblNameDisplay);
            this.grpNames.Controls.Add(this.lbxNameCommon);
            this.grpNames.Controls.Add(this.btnAddCommonName);
            this.grpNames.Controls.Add(this.btnDeleteCommonName);
            this.grpNames.Controls.Add(this.btnEditCommonName);
            this.grpNames.Controls.Add(this.txtNameDisplay);
            this.grpNames.Controls.Add(this.lblNameBase);
            this.grpNames.Controls.Add(this.txtNameBase);
            this.grpNames.Location = new System.Drawing.Point(8, 6);
            this.grpNames.Name = "grpNames";
            this.grpNames.Size = new System.Drawing.Size(205, 326);
            this.grpNames.TabIndex = 0;
            this.grpNames.TabStop = false;
            this.grpNames.Text = "Names";
            // 
            // btnUseBaseName
            // 
            this.btnUseBaseName.AutoSize = true;
            this.btnUseBaseName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUseBaseName.Location = new System.Drawing.Point(9, 102);
            this.btnUseBaseName.Name = "btnUseBaseName";
            this.btnUseBaseName.Size = new System.Drawing.Size(91, 23);
            this.btnUseBaseName.TabIndex = 4;
            this.btnUseBaseName.Text = "Use Base Name";
            this.btnUseBaseName.UseVisualStyleBackColor = true;
            this.btnUseBaseName.Click += new System.EventHandler(this.btnUseBaseName_Click);
            // 
            // txtCommonNameEntry
            // 
            this.txtCommonNameEntry.Font = new System.Drawing.Font("Calibri", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommonNameEntry.Location = new System.Drawing.Point(9, 297);
            this.txtCommonNameEntry.MaxLength = 50;
            this.txtCommonNameEntry.Name = "txtCommonNameEntry";
            this.txtCommonNameEntry.Size = new System.Drawing.Size(144, 21);
            this.txtCommonNameEntry.TabIndex = 8;
            this.txtCommonNameEntry.TextChanged += new System.EventHandler(this.txtCommonNameEntry_TextChanged);
            this.txtCommonNameEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommonNameEntry_KeyDown);
            this.txtCommonNameEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextbox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 280);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Common Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameCommon
            // 
            this.lblNameCommon.AutoSize = true;
            this.lblNameCommon.Location = new System.Drawing.Point(9, 137);
            this.lblNameCommon.Margin = new System.Windows.Forms.Padding(1);
            this.lblNameCommon.Name = "lblNameCommon";
            this.lblNameCommon.Size = new System.Drawing.Size(102, 13);
            this.lblNameCommon.TabIndex = 5;
            this.lblNameCommon.Text = "Common Names List";
            this.lblNameCommon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameDisplay
            // 
            this.lblNameDisplay.AutoSize = true;
            this.lblNameDisplay.Location = new System.Drawing.Point(9, 59);
            this.lblNameDisplay.Name = "lblNameDisplay";
            this.lblNameDisplay.Size = new System.Drawing.Size(41, 13);
            this.lblNameDisplay.TabIndex = 2;
            this.lblNameDisplay.Text = "Display";
            this.lblNameDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxNameCommon
            // 
            this.lbxNameCommon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbxNameCommon.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxNameCommon.FormattingEnabled = true;
            this.lbxNameCommon.ItemHeight = 14;
            this.lbxNameCommon.Location = new System.Drawing.Point(9, 152);
            this.lbxNameCommon.Margin = new System.Windows.Forms.Padding(1);
            this.lbxNameCommon.Name = "lbxNameCommon";
            this.lbxNameCommon.Size = new System.Drawing.Size(186, 86);
            this.lbxNameCommon.TabIndex = 6;
            this.lbxNameCommon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbxNameCommon_KeyDown);
            this.lbxNameCommon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxNameCommon_MouseDoubleClick);
            // 
            // btnAddCommonName
            // 
            this.btnAddCommonName.AutoSize = true;
            this.btnAddCommonName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddCommonName.Enabled = false;
            this.btnAddCommonName.Location = new System.Drawing.Point(159, 297);
            this.btnAddCommonName.Name = "btnAddCommonName";
            this.btnAddCommonName.Size = new System.Drawing.Size(36, 23);
            this.btnAddCommonName.TabIndex = 9;
            this.btnAddCommonName.Text = "Add";
            this.btnAddCommonName.UseVisualStyleBackColor = true;
            this.btnAddCommonName.Click += new System.EventHandler(this.btnAddCommonName_Click);
            // 
            // btnDeleteCommonName
            // 
            this.btnDeleteCommonName.Enabled = false;
            this.btnDeleteCommonName.Location = new System.Drawing.Point(105, 242);
            this.btnDeleteCommonName.Name = "btnDeleteCommonName";
            this.btnDeleteCommonName.Size = new System.Drawing.Size(90, 21);
            this.btnDeleteCommonName.TabIndex = 11;
            this.btnDeleteCommonName.Text = "Delete Name";
            this.btnDeleteCommonName.UseVisualStyleBackColor = true;
            this.btnDeleteCommonName.Click += new System.EventHandler(this.btnDeleteCommonName_Click);
            // 
            // btnEditCommonName
            // 
            this.btnEditCommonName.Enabled = false;
            this.btnEditCommonName.Location = new System.Drawing.Point(9, 242);
            this.btnEditCommonName.Name = "btnEditCommonName";
            this.btnEditCommonName.Size = new System.Drawing.Size(90, 21);
            this.btnEditCommonName.TabIndex = 10;
            this.btnEditCommonName.Text = "Edit Name";
            this.btnEditCommonName.UseVisualStyleBackColor = true;
            this.btnEditCommonName.Click += new System.EventHandler(this.btnEditCommonName_Click);
            // 
            // txtNameDisplay
            // 
            this.txtNameDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNameDisplay.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.txtNameDisplay.Location = new System.Drawing.Point(9, 75);
            this.txtNameDisplay.MaxLength = 50;
            this.txtNameDisplay.Name = "txtNameDisplay";
            this.txtNameDisplay.Size = new System.Drawing.Size(186, 21);
            this.txtNameDisplay.TabIndex = 3;
            this.txtNameDisplay.TextChanged += new System.EventHandler(this.txtNameDisplay_TextChanged);
            this.txtNameDisplay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextbox_KeyPress);
            // 
            // lblNameBase
            // 
            this.lblNameBase.AutoSize = true;
            this.lblNameBase.Location = new System.Drawing.Point(9, 19);
            this.lblNameBase.Name = "lblNameBase";
            this.lblNameBase.Size = new System.Drawing.Size(60, 13);
            this.lblNameBase.TabIndex = 0;
            this.lblNameBase.Text = "Base Name";
            this.lblNameBase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNameBase
            // 
            this.txtNameBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNameBase.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.txtNameBase.Location = new System.Drawing.Point(9, 35);
            this.txtNameBase.MaxLength = 50;
            this.txtNameBase.Name = "txtNameBase";
            this.txtNameBase.Size = new System.Drawing.Size(186, 21);
            this.txtNameBase.TabIndex = 1;
            this.txtNameBase.TextChanged += new System.EventHandler(this.txtNameBase_TextChanged);
            this.txtNameBase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameTextbox_KeyPress);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvSpecListEntries);
            this.tabPage4.Controls.Add(this.dgvSpecsItems);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(776, 336);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Details (Specs)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvSpecListEntries
            // 
            this.dgvSpecListEntries.AllowUserToResizeColumns = false;
            this.dgvSpecListEntries.AllowUserToResizeRows = false;
            this.dgvSpecListEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecListEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSpecListEntries.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSpecListEntries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpecListEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecListEntries.Location = new System.Drawing.Point(357, 197);
            this.dgvSpecListEntries.MultiSelect = false;
            this.dgvSpecListEntries.Name = "dgvSpecListEntries";
            this.dgvSpecListEntries.ReadOnly = true;
            this.dgvSpecListEntries.RowHeadersVisible = false;
            this.dgvSpecListEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpecListEntries.Size = new System.Drawing.Size(413, 133);
            this.dgvSpecListEntries.TabIndex = 16;
            // 
            // dgvSpecsItems
            // 
            this.dgvSpecsItems.AllowUserToResizeColumns = false;
            this.dgvSpecsItems.AllowUserToResizeRows = false;
            this.dgvSpecsItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecsItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSpecsItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSpecsItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpecsItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecsItems.Location = new System.Drawing.Point(357, 6);
            this.dgvSpecsItems.MultiSelect = false;
            this.dgvSpecsItems.Name = "dgvSpecsItems";
            this.dgvSpecsItems.ReadOnly = true;
            this.dgvSpecsItems.RowHeadersVisible = false;
            this.dgvSpecsItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpecsItems.Size = new System.Drawing.Size(413, 185);
            this.dgvSpecsItems.TabIndex = 16;
            this.dgvSpecsItems.SelectionChanged += new System.EventHandler(this.dgvSpecsItems_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnUnassignSpecs);
            this.panel1.Controls.Add(this.cboSpecsId);
            this.panel1.Controls.Add(this.chkRequiredSpecs);
            this.panel1.Controls.Add(this.lblSpecsId);
            this.panel1.Controls.Add(this.txtSearchSpecs);
            this.panel1.Controls.Add(this.dgvSpecs);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 324);
            this.panel1.TabIndex = 15;
            // 
            // btnUnassignSpecs
            // 
            this.btnUnassignSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnassignSpecs.AutoSize = true;
            this.btnUnassignSpecs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUnassignSpecs.Location = new System.Drawing.Point(280, 22);
            this.btnUnassignSpecs.Name = "btnUnassignSpecs";
            this.btnUnassignSpecs.Size = new System.Drawing.Size(60, 23);
            this.btnUnassignSpecs.TabIndex = 12;
            this.btnUnassignSpecs.Text = "Unassign";
            this.btnUnassignSpecs.UseVisualStyleBackColor = true;
            this.btnUnassignSpecs.Click += new System.EventHandler(this.btnUnassignSpecs_Click);
            // 
            // cboSpecsId
            // 
            this.cboSpecsId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSpecsId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpecsId.FormattingEnabled = true;
            this.cboSpecsId.Location = new System.Drawing.Point(3, 22);
            this.cboSpecsId.Name = "cboSpecsId";
            this.cboSpecsId.Size = new System.Drawing.Size(271, 21);
            this.cboSpecsId.TabIndex = 8;
            this.cboSpecsId.SelectedIndexChanged += new System.EventHandler(this.cboSpecsId_SelectedIndexChanged);
            // 
            // chkRequiredSpecs
            // 
            this.chkRequiredSpecs.AutoSize = true;
            this.chkRequiredSpecs.Location = new System.Drawing.Point(3, 56);
            this.chkRequiredSpecs.Name = "chkRequiredSpecs";
            this.chkRequiredSpecs.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredSpecs.TabIndex = 6;
            this.chkRequiredSpecs.Text = "Required";
            this.chkRequiredSpecs.UseVisualStyleBackColor = true;
            // 
            // lblSpecsId
            // 
            this.lblSpecsId.AutoSize = true;
            this.lblSpecsId.Location = new System.Drawing.Point(3, 6);
            this.lblSpecsId.Name = "lblSpecsId";
            this.lblSpecsId.Size = new System.Drawing.Size(49, 13);
            this.lblSpecsId.TabIndex = 4;
            this.lblSpecsId.Text = "Specs ID";
            // 
            // txtSearchSpecs
            // 
            this.txtSearchSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSpecs.Location = new System.Drawing.Point(3, 299);
            this.txtSearchSpecs.Name = "txtSearchSpecs";
            this.txtSearchSpecs.Size = new System.Drawing.Size(337, 20);
            this.txtSearchSpecs.TabIndex = 10;
            this.txtSearchSpecs.TextChanged += new System.EventHandler(this.txtSearchSpecs_TextChanged);
            // 
            // dgvSpecs
            // 
            this.dgvSpecs.AllowUserToResizeColumns = false;
            this.dgvSpecs.AllowUserToResizeRows = false;
            this.dgvSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSpecs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSpecs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSpecs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn3});
            this.dgvSpecs.Location = new System.Drawing.Point(3, 79);
            this.dgvSpecs.MultiSelect = false;
            this.dgvSpecs.Name = "dgvSpecs";
            this.dgvSpecs.ReadOnly = true;
            this.dgvSpecs.RowHeadersVisible = false;
            this.dgvSpecs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpecs.Size = new System.Drawing.Size(337, 210);
            this.dgvSpecs.TabIndex = 11;
            this.dgvSpecs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecs_CellContentClick);
            this.dgvSpecs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvField_CellFormatting);
            this.dgvSpecs.SelectionChanged += new System.EventHandler(this.dgvSpecs_SelectionChanged);
            // 
            // dataGridViewButtonColumn3
            // 
            this.dataGridViewButtonColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewButtonColumn3.HeaderText = "Action";
            this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
            this.dataGridViewButtonColumn3.ReadOnly = true;
            this.dataGridViewButtonColumn3.Text = "Assign";
            this.dataGridViewButtonColumn3.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn3.Width = 43;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(776, 336);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Details (Others)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(770, 330);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel2);
            this.groupBox5.Controls.Add(this.lblSizeId);
            this.groupBox5.Controls.Add(this.btnUnassignSize);
            this.groupBox5.Controls.Add(this.txtSearchSizes);
            this.groupBox5.Controls.Add(this.cboSizeGroupId);
            this.groupBox5.Controls.Add(this.chkRequiredSize);
            this.groupBox5.Controls.Add(this.dgvSizeGroup);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(250, 324);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Sizes";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbxSizeListEntries, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 208);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(244, 113);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // lbxSizeListEntries
            // 
            this.lbxSizeListEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSizeListEntries.FormattingEnabled = true;
            this.lbxSizeListEntries.IntegralHeight = false;
            this.lbxSizeListEntries.Location = new System.Drawing.Point(4, 4);
            this.lbxSizeListEntries.Name = "lbxSizeListEntries";
            this.lbxSizeListEntries.Size = new System.Drawing.Size(114, 105);
            this.lbxSizeListEntries.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbxAltSizeListEntries);
            this.panel2.Controls.Add(this.cboAltListSelector);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(125, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(115, 105);
            this.panel2.TabIndex = 14;
            // 
            // lbxAltSizeListEntries
            // 
            this.lbxAltSizeListEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxAltSizeListEntries.FormattingEnabled = true;
            this.lbxAltSizeListEntries.IntegralHeight = false;
            this.lbxAltSizeListEntries.Location = new System.Drawing.Point(0, 23);
            this.lbxAltSizeListEntries.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.lbxAltSizeListEntries.Name = "lbxAltSizeListEntries";
            this.lbxAltSizeListEntries.Size = new System.Drawing.Size(115, 82);
            this.lbxAltSizeListEntries.TabIndex = 1;
            // 
            // cboAltListSelector
            // 
            this.cboAltListSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAltListSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAltListSelector.FormattingEnabled = true;
            this.cboAltListSelector.Location = new System.Drawing.Point(0, 0);
            this.cboAltListSelector.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.cboAltListSelector.Name = "cboAltListSelector";
            this.cboAltListSelector.Size = new System.Drawing.Size(115, 21);
            this.cboAltListSelector.TabIndex = 0;
            this.cboAltListSelector.SelectedIndexChanged += new System.EventHandler(this.cboAltListSelector_SelectedIndexChanged);
            // 
            // lblSizeId
            // 
            this.lblSizeId.AutoSize = true;
            this.lblSizeId.Location = new System.Drawing.Point(6, 23);
            this.lblSizeId.Name = "lblSizeId";
            this.lblSizeId.Size = new System.Drawing.Size(72, 13);
            this.lblSizeId.TabIndex = 4;
            this.lblSizeId.Text = "Size Group ID";
            this.lblSizeId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUnassignSize
            // 
            this.btnUnassignSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnassignSize.AutoSize = true;
            this.btnUnassignSize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUnassignSize.Location = new System.Drawing.Point(184, 17);
            this.btnUnassignSize.Name = "btnUnassignSize";
            this.btnUnassignSize.Size = new System.Drawing.Size(60, 23);
            this.btnUnassignSize.TabIndex = 12;
            this.btnUnassignSize.Text = "Unassign";
            this.btnUnassignSize.UseVisualStyleBackColor = true;
            this.btnUnassignSize.Click += new System.EventHandler(this.btnUnassignSize_Click);
            // 
            // txtSearchSizes
            // 
            this.txtSearchSizes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSizes.Location = new System.Drawing.Point(81, 46);
            this.txtSearchSizes.Name = "txtSearchSizes";
            this.txtSearchSizes.Size = new System.Drawing.Size(163, 20);
            this.txtSearchSizes.TabIndex = 10;
            this.txtSearchSizes.TextChanged += new System.EventHandler(this.txtSearchSizes_TextChanged);
            // 
            // cboSizeGroupId
            // 
            this.cboSizeGroupId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSizeGroupId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSizeGroupId.FormattingEnabled = true;
            this.cboSizeGroupId.Location = new System.Drawing.Point(81, 19);
            this.cboSizeGroupId.Name = "cboSizeGroupId";
            this.cboSizeGroupId.Size = new System.Drawing.Size(96, 21);
            this.cboSizeGroupId.TabIndex = 8;
            this.cboSizeGroupId.SelectedIndexChanged += new System.EventHandler(this.cboSizeGroupId_SelectedIndexChanged);
            // 
            // chkRequiredSize
            // 
            this.chkRequiredSize.AutoSize = true;
            this.chkRequiredSize.Location = new System.Drawing.Point(6, 48);
            this.chkRequiredSize.Name = "chkRequiredSize";
            this.chkRequiredSize.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredSize.TabIndex = 6;
            this.chkRequiredSize.Text = "Required";
            this.chkRequiredSize.UseVisualStyleBackColor = true;
            // 
            // dgvSizeGroup
            // 
            this.dgvSizeGroup.AllowUserToResizeColumns = false;
            this.dgvSizeGroup.AllowUserToResizeRows = false;
            this.dgvSizeGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSizeGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSizeGroup.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSizeGroup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSizeGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizeGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn4});
            this.dgvSizeGroup.Location = new System.Drawing.Point(6, 72);
            this.dgvSizeGroup.MultiSelect = false;
            this.dgvSizeGroup.Name = "dgvSizeGroup";
            this.dgvSizeGroup.ReadOnly = true;
            this.dgvSizeGroup.RowHeadersVisible = false;
            this.dgvSizeGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSizeGroup.Size = new System.Drawing.Size(238, 130);
            this.dgvSizeGroup.TabIndex = 11;
            this.dgvSizeGroup.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSizeGroup_CellContentClick);
            this.dgvSizeGroup.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvField_CellFormatting);
            this.dgvSizeGroup.SelectionChanged += new System.EventHandler(this.dgvSizeGroup_SelectionChanged);
            // 
            // dataGridViewButtonColumn4
            // 
            this.dataGridViewButtonColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewButtonColumn4.HeaderText = "Action";
            this.dataGridViewButtonColumn4.Name = "dataGridViewButtonColumn4";
            this.dataGridViewButtonColumn4.ReadOnly = true;
            this.dataGridViewButtonColumn4.Text = "Assign";
            this.dataGridViewButtonColumn4.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn4.Width = 43;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbxEndsListEntries);
            this.groupBox4.Controls.Add(this.btnUnassignEnds);
            this.groupBox4.Controls.Add(this.cboEndsListId);
            this.groupBox4.Controls.Add(this.txtSearchEnds);
            this.groupBox4.Controls.Add(this.lblEndsId);
            this.groupBox4.Controls.Add(this.chkRequiredEnds);
            this.groupBox4.Controls.Add(this.dgvEndsLists);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(515, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(252, 324);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ends / Connection";
            // 
            // lbxEndsListEntries
            // 
            this.lbxEndsListEntries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxEndsListEntries.FormattingEnabled = true;
            this.lbxEndsListEntries.Location = new System.Drawing.Point(6, 208);
            this.lbxEndsListEntries.Name = "lbxEndsListEntries";
            this.lbxEndsListEntries.Size = new System.Drawing.Size(240, 108);
            this.lbxEndsListEntries.TabIndex = 13;
            // 
            // btnUnassignEnds
            // 
            this.btnUnassignEnds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnassignEnds.AutoSize = true;
            this.btnUnassignEnds.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUnassignEnds.Location = new System.Drawing.Point(184, 17);
            this.btnUnassignEnds.Name = "btnUnassignEnds";
            this.btnUnassignEnds.Size = new System.Drawing.Size(60, 23);
            this.btnUnassignEnds.TabIndex = 12;
            this.btnUnassignEnds.Text = "Unassign";
            this.btnUnassignEnds.UseVisualStyleBackColor = true;
            this.btnUnassignEnds.Click += new System.EventHandler(this.btnUnassignEnds_Click);
            // 
            // cboEndsListId
            // 
            this.cboEndsListId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEndsListId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEndsListId.FormattingEnabled = true;
            this.cboEndsListId.Location = new System.Drawing.Point(81, 19);
            this.cboEndsListId.Name = "cboEndsListId";
            this.cboEndsListId.Size = new System.Drawing.Size(98, 21);
            this.cboEndsListId.TabIndex = 8;
            this.cboEndsListId.SelectedIndexChanged += new System.EventHandler(this.cboEndsListId_SelectedIndexChanged);
            // 
            // txtSearchEnds
            // 
            this.txtSearchEnds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchEnds.Location = new System.Drawing.Point(81, 46);
            this.txtSearchEnds.Name = "txtSearchEnds";
            this.txtSearchEnds.Size = new System.Drawing.Size(163, 20);
            this.txtSearchEnds.TabIndex = 10;
            this.txtSearchEnds.TextChanged += new System.EventHandler(this.txtSearchEnds_TextChanged);
            // 
            // lblEndsId
            // 
            this.lblEndsId.AutoSize = true;
            this.lblEndsId.Location = new System.Drawing.Point(6, 23);
            this.lblEndsId.Name = "lblEndsId";
            this.lblEndsId.Size = new System.Drawing.Size(63, 13);
            this.lblEndsId.TabIndex = 4;
            this.lblEndsId.Text = "Ends List ID";
            this.lblEndsId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRequiredEnds
            // 
            this.chkRequiredEnds.AutoSize = true;
            this.chkRequiredEnds.Location = new System.Drawing.Point(6, 48);
            this.chkRequiredEnds.Name = "chkRequiredEnds";
            this.chkRequiredEnds.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredEnds.TabIndex = 6;
            this.chkRequiredEnds.Text = "Required";
            this.chkRequiredEnds.UseVisualStyleBackColor = true;
            // 
            // dgvEndsLists
            // 
            this.dgvEndsLists.AllowUserToResizeColumns = false;
            this.dgvEndsLists.AllowUserToResizeRows = false;
            this.dgvEndsLists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEndsLists.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvEndsLists.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvEndsLists.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEndsLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEndsLists.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn2});
            this.dgvEndsLists.Location = new System.Drawing.Point(6, 72);
            this.dgvEndsLists.MultiSelect = false;
            this.dgvEndsLists.Name = "dgvEndsLists";
            this.dgvEndsLists.ReadOnly = true;
            this.dgvEndsLists.RowHeadersVisible = false;
            this.dgvEndsLists.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEndsLists.Size = new System.Drawing.Size(240, 130);
            this.dgvEndsLists.TabIndex = 11;
            this.dgvEndsLists.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEndsLists_CellContentClick);
            this.dgvEndsLists.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvField_CellFormatting);
            this.dgvEndsLists.SelectionChanged += new System.EventHandler(this.dgvEndsLists_SelectionChanged);
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewButtonColumn2.HeaderText = "Action";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.ReadOnly = true;
            this.dataGridViewButtonColumn2.Text = "Assign";
            this.dataGridViewButtonColumn2.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn2.Width = 43;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbxBrandListEntries);
            this.groupBox3.Controls.Add(this.cboBrandListId);
            this.groupBox3.Controls.Add(this.btnUnassignBrand);
            this.groupBox3.Controls.Add(this.dgvBrandsLists);
            this.groupBox3.Controls.Add(this.txtSearchBrands);
            this.groupBox3.Controls.Add(this.lblBrandId);
            this.groupBox3.Controls.Add(this.chkRequiredBrand);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(259, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 324);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Brands";
            // 
            // lbxBrandListEntries
            // 
            this.lbxBrandListEntries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxBrandListEntries.FormattingEnabled = true;
            this.lbxBrandListEntries.Location = new System.Drawing.Point(6, 208);
            this.lbxBrandListEntries.Name = "lbxBrandListEntries";
            this.lbxBrandListEntries.Size = new System.Drawing.Size(238, 108);
            this.lbxBrandListEntries.TabIndex = 13;
            // 
            // cboBrandListId
            // 
            this.cboBrandListId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBrandListId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBrandListId.FormattingEnabled = true;
            this.cboBrandListId.Location = new System.Drawing.Point(81, 19);
            this.cboBrandListId.Name = "cboBrandListId";
            this.cboBrandListId.Size = new System.Drawing.Size(96, 21);
            this.cboBrandListId.TabIndex = 8;
            this.cboBrandListId.SelectedIndexChanged += new System.EventHandler(this.cboBrandListId_SelectedIndexChanged);
            // 
            // btnUnassignBrand
            // 
            this.btnUnassignBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnassignBrand.AutoSize = true;
            this.btnUnassignBrand.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUnassignBrand.Location = new System.Drawing.Point(184, 17);
            this.btnUnassignBrand.Name = "btnUnassignBrand";
            this.btnUnassignBrand.Size = new System.Drawing.Size(60, 23);
            this.btnUnassignBrand.TabIndex = 12;
            this.btnUnassignBrand.Text = "Unassign";
            this.btnUnassignBrand.UseVisualStyleBackColor = true;
            this.btnUnassignBrand.Click += new System.EventHandler(this.btnUnassignBrand_Click);
            // 
            // dgvBrandsLists
            // 
            this.dgvBrandsLists.AllowUserToResizeColumns = false;
            this.dgvBrandsLists.AllowUserToResizeRows = false;
            this.dgvBrandsLists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBrandsLists.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBrandsLists.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvBrandsLists.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBrandsLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBrandsLists.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn1});
            this.dgvBrandsLists.Location = new System.Drawing.Point(6, 72);
            this.dgvBrandsLists.MultiSelect = false;
            this.dgvBrandsLists.Name = "dgvBrandsLists";
            this.dgvBrandsLists.ReadOnly = true;
            this.dgvBrandsLists.RowHeadersVisible = false;
            this.dgvBrandsLists.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBrandsLists.Size = new System.Drawing.Size(238, 130);
            this.dgvBrandsLists.TabIndex = 11;
            this.dgvBrandsLists.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBrandsLists_CellContentClick);
            this.dgvBrandsLists.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvField_CellFormatting);
            this.dgvBrandsLists.SelectionChanged += new System.EventHandler(this.dgvBrandsLists_SelectionChanged);
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewButtonColumn1.HeaderText = "Action";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Text = "Assign";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 43;
            // 
            // txtSearchBrands
            // 
            this.txtSearchBrands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBrands.Location = new System.Drawing.Point(81, 46);
            this.txtSearchBrands.Name = "txtSearchBrands";
            this.txtSearchBrands.Size = new System.Drawing.Size(163, 20);
            this.txtSearchBrands.TabIndex = 10;
            this.txtSearchBrands.TextChanged += new System.EventHandler(this.txtSearchBrands_TextChanged);
            // 
            // lblBrandId
            // 
            this.lblBrandId.AutoSize = true;
            this.lblBrandId.Location = new System.Drawing.Point(6, 23);
            this.lblBrandId.Name = "lblBrandId";
            this.lblBrandId.Size = new System.Drawing.Size(68, 13);
            this.lblBrandId.TabIndex = 4;
            this.lblBrandId.Text = "Brand List ID";
            this.lblBrandId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRequiredBrand
            // 
            this.chkRequiredBrand.AutoSize = true;
            this.chkRequiredBrand.Location = new System.Drawing.Point(6, 48);
            this.chkRequiredBrand.Name = "chkRequiredBrand";
            this.chkRequiredBrand.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredBrand.TabIndex = 6;
            this.chkRequiredBrand.Text = "Required";
            this.chkRequiredBrand.UseVisualStyleBackColor = true;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(12, 426);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.Location = new System.Drawing.Point(616, 426);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 1;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(697, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTest_FormSize
            // 
            this.lblTest_FormSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTest_FormSize.AutoSize = true;
            this.lblTest_FormSize.Location = new System.Drawing.Point(328, 410);
            this.lblTest_FormSize.Name = "lblTest_FormSize";
            this.lblTest_FormSize.Size = new System.Drawing.Size(35, 13);
            this.lblTest_FormSize.TabIndex = 5;
            this.lblTest_FormSize.Text = "label2";
            this.lblTest_FormSize.Visible = false;
            // 
            // lblTest_FloatingLocation
            // 
            this.lblTest_FloatingLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTest_FloatingLocation.AutoSize = true;
            this.lblTest_FloatingLocation.Location = new System.Drawing.Point(328, 423);
            this.lblTest_FloatingLocation.Name = "lblTest_FloatingLocation";
            this.lblTest_FloatingLocation.Size = new System.Drawing.Size(35, 13);
            this.lblTest_FloatingLocation.TabIndex = 5;
            this.lblTest_FloatingLocation.Text = "label2";
            this.lblTest_FloatingLocation.Visible = false;
            // 
            // lblTest_FloatingBounds
            // 
            this.lblTest_FloatingBounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTest_FloatingBounds.AutoSize = true;
            this.lblTest_FloatingBounds.Location = new System.Drawing.Point(328, 436);
            this.lblTest_FloatingBounds.Name = "lblTest_FloatingBounds";
            this.lblTest_FloatingBounds.Size = new System.Drawing.Size(35, 13);
            this.lblTest_FloatingBounds.TabIndex = 5;
            this.lblTest_FloatingBounds.Text = "label2";
            this.lblTest_FloatingBounds.Visible = false;
            // 
            // tsmiItemFields
            // 
            this.tsmiItemFields.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSpecs,
            this.tsmiSizeList,
            this.tsmiBrandsList,
            this.tsmiEndsList});
            this.tsmiItemFields.Name = "tsmiItemFields";
            this.tsmiItemFields.Size = new System.Drawing.Size(76, 20);
            this.tsmiItemFields.Text = "Item Fields";
            // 
            // tsmiSpecs
            // 
            this.tsmiSpecs.Name = "tsmiSpecs";
            this.tsmiSpecs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
            this.tsmiSpecs.Size = new System.Drawing.Size(180, 22);
            this.tsmiSpecs.Text = "Specs";
            this.tsmiSpecs.Click += new System.EventHandler(this.tsmiSpecs_Click);
            // 
            // tsmiSizeList
            // 
            this.tsmiSizeList.Name = "tsmiSizeList";
            this.tsmiSizeList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.tsmiSizeList.Size = new System.Drawing.Size(180, 22);
            this.tsmiSizeList.Text = "Size List";
            this.tsmiSizeList.Click += new System.EventHandler(this.tsmiSizeList_Click);
            // 
            // tsmiBrandsList
            // 
            this.tsmiBrandsList.Name = "tsmiBrandsList";
            this.tsmiBrandsList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
            this.tsmiBrandsList.Size = new System.Drawing.Size(180, 22);
            this.tsmiBrandsList.Text = "Brands List";
            this.tsmiBrandsList.Click += new System.EventHandler(this.tsmiBrandsList_Click);
            // 
            // tsmiEndsList
            // 
            this.tsmiEndsList.Name = "tsmiEndsList";
            this.tsmiEndsList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D4)));
            this.tsmiEndsList.Size = new System.Drawing.Size(180, 22);
            this.tsmiEndsList.Text = "Ends List";
            this.tsmiEndsList.Click += new System.EventHandler(this.tsmiEndsList_Click);
            // 
            // picFloating
            // 
            this.picFloating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFloating.Location = new System.Drawing.Point(671, 6);
            this.picFloating.Name = "picFloating";
            this.picFloating.Size = new System.Drawing.Size(180, 180);
            this.picFloating.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFloating.TabIndex = 5;
            this.picFloating.TabStop = false;
            this.picFloating.Visible = false;
            // 
            // picExistingImage
            // 
            this.picExistingImage.Location = new System.Drawing.Point(136, 226);
            this.picExistingImage.Name = "picExistingImage";
            this.picExistingImage.Size = new System.Drawing.Size(50, 50);
            this.picExistingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picExistingImage.TabIndex = 5;
            this.picExistingImage.TabStop = false;
            this.picExistingImage.Visible = false;
            this.picExistingImage.MouseEnter += new System.EventHandler(this.picExistingImage_MouseEnter);
            this.picExistingImage.MouseLeave += new System.EventHandler(this.picExistingImage_MouseLeave);
            this.picExistingImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picExistingImage_MouseMove);
            // 
            // picImage
            // 
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Location = new System.Drawing.Point(6, 19);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(180, 175);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 5;
            this.picImage.TabStop = false;
            // 
            // ItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.lblTest_FloatingBounds);
            this.Controls.Add(this.lblTest_FloatingLocation);
            this.Controls.Add(this.lblTest_FormSize);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.tabDataSections);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "ItemEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ItemEditor_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.grpDescription.ResumeLayout(false);
            this.grpDescription.PerformLayout();
            this.grpCategory.ResumeLayout(false);
            this.grpCategory.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.tabDataSections.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpExistingItemId.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsId)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.grpImages.ResumeLayout(false);
            this.grpImages.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.grpNames.ResumeLayout(false);
            this.grpNames.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecListEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecsItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecs)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeGroup)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndsLists)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrandsLists)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFloating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExistingImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Label lblItemId;
        private System.Windows.Forms.TextBox txtItemId;
        private System.Windows.Forms.GroupBox grpDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox grpCategory;
        private System.Windows.Forms.Label lblCatName;
        private System.Windows.Forms.TextBox txtCatName;
        private System.Windows.Forms.Label lblCatId;
        private System.Windows.Forms.TextBox txtCatId;
        private System.Windows.Forms.TabControl tabDataSections;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClearDescription;
        private System.Windows.Forms.GroupBox grpNames;
        private System.Windows.Forms.Button btnUseBaseName;
        private System.Windows.Forms.TextBox txtCommonNameEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNameCommon;
        private System.Windows.Forms.Label lblNameDisplay;
        private System.Windows.Forms.ListBox lbxNameCommon;
        private System.Windows.Forms.Button btnAddCommonName;
        private System.Windows.Forms.Button btnDeleteCommonName;
        private System.Windows.Forms.Button btnEditCommonName;
        private System.Windows.Forms.TextBox txtNameDisplay;
        private System.Windows.Forms.Label lblNameBase;
        private System.Windows.Forms.TextBox txtNameBase;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkRequiredEnds;
        private System.Windows.Forms.CheckBox chkRequiredBrand;
        private System.Windows.Forms.Label lblEndsId;
        private System.Windows.Forms.CheckBox chkRequiredSize;
        private System.Windows.Forms.Label lblBrandId;
        private System.Windows.Forms.Label lblSizeId;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cboEndsListId;
        private System.Windows.Forms.TextBox txtSearchEnds;
        private System.Windows.Forms.ComboBox cboBrandListId;
        private System.Windows.Forms.TextBox txtSearchBrands;
        private System.Windows.Forms.ComboBox cboSizeGroupId;
        private System.Windows.Forms.TextBox txtSearchSizes;
        private System.Windows.Forms.DataGridView dgvItemsId;
        private System.Windows.Forms.GroupBox grpExistingItemId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.DataGridViewButtonColumn action;
        private System.Windows.Forms.DataGridView dgvBrandsLists;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.Button btnUnassignEnds;
        private System.Windows.Forms.Button btnUnassignBrand;
        private System.Windows.Forms.Button btnUnassignSize;
        private System.Windows.Forms.Label lblValidatorItemId;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUnassignSpecs;
        private System.Windows.Forms.ComboBox cboSpecsId;
        private System.Windows.Forms.CheckBox chkRequiredSpecs;
        private System.Windows.Forms.Label lblSpecsId;
        private System.Windows.Forms.TextBox txtSearchSpecs;
        private System.Windows.Forms.DataGridView dgvSpecs;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lbxBrandListEntries;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lbxEndsListEntries;
        private System.Windows.Forms.DataGridView dgvEndsLists;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lbxSizeListEntries;
        private System.Windows.Forms.DataGridView dgvSizeGroup;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbxAltSizeListEntries;
        private System.Windows.Forms.ComboBox cboAltListSelector;
        private System.Windows.Forms.DataGridView dgvSpecsItems;
        private System.Windows.Forms.DataGridView dgvSpecListEntries;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblUom;
        private System.Windows.Forms.TextBox txtUom;
        private System.Windows.Forms.GroupBox grpImages;
        private System.Windows.Forms.CheckBox chkAutoAddImage;
        private System.Windows.Forms.Button btnModifyImage;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Button btnImageAccept;
        private System.Windows.Forms.Button btnRemoveImage;
        private System.Windows.Forms.TextBox txtImageName;
        private System.Windows.Forms.ListBox lbxImages;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmiSample1;
        private System.Windows.Forms.CheckBox chkUseDefName;
        private System.Windows.Forms.Button btnImageCancel;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PictureBox picExistingImage;
        private System.Windows.Forms.Label lblDuplicateImage;
        private System.Windows.Forms.Button btnAddNewImage;
        private System.Windows.Forms.PictureBox picFloating;
        private System.Windows.Forms.Label lblTest_FormSize;
        private System.Windows.Forms.Label lblTest_FloatingLocation;
        private System.Windows.Forms.Label lblTest_FloatingBounds;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private System.Windows.Forms.ToolStripMenuItem tsmiItemFields;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpecs;
        private System.Windows.Forms.ToolStripMenuItem tsmiSizeList;
        private System.Windows.Forms.ToolStripMenuItem tsmiBrandsList;
        private System.Windows.Forms.ToolStripMenuItem tsmiEndsList;
    }
}