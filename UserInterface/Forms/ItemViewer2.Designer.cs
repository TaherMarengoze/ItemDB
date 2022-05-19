namespace UserInterface.Forms
{
    partial class ItemViewer2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.msmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveXmlFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCloseForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.grpImages = new System.Windows.Forms.GroupBox();
            this.lbxImages = new System.Windows.Forms.ListBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.cmsImageOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEnlargeImage = new System.Windows.Forms.ToolStripMenuItem();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblUom = new System.Windows.Forms.Label();
            this.txtUom = new System.Windows.Forms.TextBox();
            this.txtEndsId = new System.Windows.Forms.TextBox();
            this.chkRequiredEnds = new System.Windows.Forms.CheckBox();
            this.txtBrandId = new System.Windows.Forms.TextBox();
            this.chkRequiredBrand = new System.Windows.Forms.CheckBox();
            this.txtSizeId = new System.Windows.Forms.TextBox();
            this.lblEndsId = new System.Windows.Forms.Label();
            this.chkRequiredSize = new System.Windows.Forms.CheckBox();
            this.lblBrandId = new System.Windows.Forms.Label();
            this.txtSpecsId = new System.Windows.Forms.TextBox();
            this.lblSizeId = new System.Windows.Forms.Label();
            this.chkRequiredSpecs = new System.Windows.Forms.CheckBox();
            this.lblSpecsId = new System.Windows.Forms.Label();
            this.lblItemId = new System.Windows.Forms.Label();
            this.txtItemId = new System.Windows.Forms.TextBox();
            this.grpDescription = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.grpCategory = new System.Windows.Forms.GroupBox();
            this.lblCatName = new System.Windows.Forms.Label();
            this.txtCatName = new System.Windows.Forms.TextBox();
            this.lblCatId = new System.Windows.Forms.Label();
            this.txtCatId = new System.Windows.Forms.TextBox();
            this.grpNames = new System.Windows.Forms.GroupBox();
            this.lblNameCommon = new System.Windows.Forms.Label();
            this.lblNameDisplay = new System.Windows.Forms.Label();
            this.lbxNameCommon = new System.Windows.Forms.ListBox();
            this.txtNameDisplay = new System.Windows.Forms.TextBox();
            this.lblNameBase = new System.Windows.Forms.Label();
            this.txtNameBase = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.chkConfirmDelete = new System.Windows.Forms.CheckBox();
            this.txtFilterId = new System.Windows.Forms.TextBox();
            this.lblSearchId = new System.Windows.Forms.Label();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.lblSearchName = new System.Windows.Forms.Label();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnFilterClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboFilterCategory = new System.Windows.Forms.ComboBox();
            this.txtFilterCatId = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.rdoFilterNoImage = new System.Windows.Forms.RadioButton();
            this.rdoFilterHasImage = new System.Windows.Forms.RadioButton();
            this.rdoFilterAnyImage = new System.Windows.Forms.RadioButton();
            this.lblSearchImage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNoItems = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblShownItemsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.grpData.SuspendLayout();
            this.grpImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.cmsImageOptions.SuspendLayout();
            this.grpDetails.SuspendLayout();
            this.grpDescription.SuspendLayout();
            this.grpCategory.SuspendLayout();
            this.grpNames.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msmiFile,
            this.tsmiItems});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(784, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // msmiFile
            // 
            this.msmiFile.AutoSize = false;
            this.msmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSaveXmlFile,
            this.toolStripSeparator1,
            this.tsmiCloseForm,
            this.toolStripSeparator2,
            this.tsmiExitApp});
            this.msmiFile.Name = "msmiFile";
            this.msmiFile.Size = new System.Drawing.Size(50, 20);
            this.msmiFile.Text = "File";
            // 
            // tsmiSaveXmlFile
            // 
            this.tsmiSaveXmlFile.Name = "tsmiSaveXmlFile";
            this.tsmiSaveXmlFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSaveXmlFile.Size = new System.Drawing.Size(198, 22);
            this.tsmiSaveXmlFile.Text = "Save XML File";
            this.tsmiSaveXmlFile.Click += new System.EventHandler(this.tsmiSaveXmlFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiCloseForm
            // 
            this.tsmiCloseForm.Name = "tsmiCloseForm";
            this.tsmiCloseForm.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiCloseForm.Size = new System.Drawing.Size(198, 22);
            this.tsmiCloseForm.Text = "Close Form";
            this.tsmiCloseForm.Click += new System.EventHandler(this.tsmiCloseForm_Click);
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
            this.tsmiExitApp.Text = "Exit Application";
            this.tsmiExitApp.Click += new System.EventHandler(this.tsmiExitApp_Click);
            // 
            // tsmiItems
            // 
            this.tsmiItems.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewItem,
            this.tsmiEditItem});
            this.tsmiItems.Name = "tsmiItems";
            this.tsmiItems.Size = new System.Drawing.Size(48, 20);
            this.tsmiItems.Text = "Items";
            // 
            // tsmiNewItem
            // 
            this.tsmiNewItem.Name = "tsmiNewItem";
            this.tsmiNewItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNewItem.Size = new System.Drawing.Size(168, 22);
            this.tsmiNewItem.Text = "New Item";
            this.tsmiNewItem.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tsmiEditItem
            // 
            this.tsmiEditItem.Name = "tsmiEditItem";
            this.tsmiEditItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmiEditItem.Size = new System.Drawing.Size(168, 22);
            this.tsmiEditItem.Text = "Edit Item";
            this.tsmiEditItem.Click += new System.EventHandler(this.tsmiEditItem_Click);
            // 
            // dgvItems
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItems.Location = new System.Drawing.Point(-1, 172);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersWidth = 25;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(360, 367);
            this.dgvItems.TabIndex = 1;
            this.dgvItems.DataSourceChanged += new System.EventHandler(this.dgvItems_DataSourceChanged);
            this.dgvItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellDoubleClick);
            this.dgvItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvItems_CellFormatting);
            this.dgvItems.SelectionChanged += new System.EventHandler(this.dgvItems_SelectionChanged);
            // 
            // grpData
            // 
            this.grpData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpData.Controls.Add(this.grpImages);
            this.grpData.Controls.Add(this.grpDetails);
            this.grpData.Controls.Add(this.lblItemId);
            this.grpData.Controls.Add(this.txtItemId);
            this.grpData.Controls.Add(this.grpDescription);
            this.grpData.Controls.Add(this.grpCategory);
            this.grpData.Controls.Add(this.grpNames);
            this.grpData.Enabled = false;
            this.grpData.Location = new System.Drawing.Point(365, 27);
            this.grpData.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(419, 512);
            this.grpData.TabIndex = 4;
            this.grpData.TabStop = false;
            this.grpData.Text = "Item Data";
            // 
            // grpImages
            // 
            this.grpImages.Controls.Add(this.lbxImages);
            this.grpImages.Controls.Add(this.picImage);
            this.grpImages.Location = new System.Drawing.Point(257, 91);
            this.grpImages.Name = "grpImages";
            this.grpImages.Size = new System.Drawing.Size(156, 244);
            this.grpImages.TabIndex = 0;
            this.grpImages.TabStop = false;
            this.grpImages.Text = "Images";
            // 
            // lbxImages
            // 
            this.lbxImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbxImages.Font = new System.Drawing.Font("Calibri", 9F);
            this.lbxImages.FormattingEnabled = true;
            this.lbxImages.HorizontalScrollbar = true;
            this.lbxImages.ItemHeight = 14;
            this.lbxImages.Location = new System.Drawing.Point(3, 19);
            this.lbxImages.Name = "lbxImages";
            this.lbxImages.Size = new System.Drawing.Size(150, 58);
            this.lbxImages.TabIndex = 6;
            this.lbxImages.SelectedIndexChanged += new System.EventHandler(this.lbxImages_SelectedIndexChanged);
            // 
            // picImage
            // 
            this.picImage.ContextMenuStrip = this.cmsImageOptions;
            this.picImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picImage.Location = new System.Drawing.Point(3, 91);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(150, 150);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 5;
            this.picImage.TabStop = false;
            this.picImage.DoubleClick += new System.EventHandler(this.tsmiEnlargeImage_Click);
            this.picImage.MouseHover += new System.EventHandler(this.picImage_MouseHover);
            // 
            // cmsImageOptions
            // 
            this.cmsImageOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEnlargeImage});
            this.cmsImageOptions.Name = "cmsImageOptions";
            this.cmsImageOptions.Size = new System.Drawing.Size(150, 26);
            // 
            // tsmiEnlargeImage
            // 
            this.tsmiEnlargeImage.Name = "tsmiEnlargeImage";
            this.tsmiEnlargeImage.Size = new System.Drawing.Size(149, 22);
            this.tsmiEnlargeImage.Text = "Enlarge Image";
            this.tsmiEnlargeImage.Click += new System.EventHandler(this.tsmiEnlargeImage_Click);
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.lblUom);
            this.grpDetails.Controls.Add(this.txtUom);
            this.grpDetails.Controls.Add(this.txtEndsId);
            this.grpDetails.Controls.Add(this.chkRequiredEnds);
            this.grpDetails.Controls.Add(this.txtBrandId);
            this.grpDetails.Controls.Add(this.chkRequiredBrand);
            this.grpDetails.Controls.Add(this.txtSizeId);
            this.grpDetails.Controls.Add(this.lblEndsId);
            this.grpDetails.Controls.Add(this.chkRequiredSize);
            this.grpDetails.Controls.Add(this.lblBrandId);
            this.grpDetails.Controls.Add(this.txtSpecsId);
            this.grpDetails.Controls.Add(this.lblSizeId);
            this.grpDetails.Controls.Add(this.chkRequiredSpecs);
            this.grpDetails.Controls.Add(this.lblSpecsId);
            this.grpDetails.Location = new System.Drawing.Point(6, 335);
            this.grpDetails.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(248, 161);
            this.grpDetails.TabIndex = 0;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Details";
            // 
            // lblUom
            // 
            this.lblUom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUom.Location = new System.Drawing.Point(12, 131);
            this.lblUom.Name = "lblUom";
            this.lblUom.Size = new System.Drawing.Size(75, 20);
            this.lblUom.TabIndex = 4;
            this.lblUom.Text = "UoM";
            this.lblUom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUom
            // 
            this.txtUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUom.Location = new System.Drawing.Point(86, 131);
            this.txtUom.Name = "txtUom";
            this.txtUom.ReadOnly = true;
            this.txtUom.Size = new System.Drawing.Size(69, 20);
            this.txtUom.TabIndex = 0;
            this.txtUom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndsId
            // 
            this.txtEndsId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEndsId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndsId.Location = new System.Drawing.Point(86, 95);
            this.txtEndsId.Name = "txtEndsId";
            this.txtEndsId.ReadOnly = true;
            this.txtEndsId.Size = new System.Drawing.Size(69, 20);
            this.txtEndsId.TabIndex = 7;
            // 
            // chkRequiredEnds
            // 
            this.chkRequiredEnds.AutoCheck = false;
            this.chkRequiredEnds.AutoSize = true;
            this.chkRequiredEnds.Location = new System.Drawing.Point(161, 98);
            this.chkRequiredEnds.Name = "chkRequiredEnds";
            this.chkRequiredEnds.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredEnds.TabIndex = 6;
            this.chkRequiredEnds.Text = "Required";
            this.chkRequiredEnds.UseVisualStyleBackColor = true;
            // 
            // txtBrandId
            // 
            this.txtBrandId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBrandId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBrandId.Location = new System.Drawing.Point(86, 69);
            this.txtBrandId.Name = "txtBrandId";
            this.txtBrandId.ReadOnly = true;
            this.txtBrandId.Size = new System.Drawing.Size(69, 20);
            this.txtBrandId.TabIndex = 7;
            // 
            // chkRequiredBrand
            // 
            this.chkRequiredBrand.AutoCheck = false;
            this.chkRequiredBrand.AutoSize = true;
            this.chkRequiredBrand.Location = new System.Drawing.Point(161, 72);
            this.chkRequiredBrand.Name = "chkRequiredBrand";
            this.chkRequiredBrand.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredBrand.TabIndex = 6;
            this.chkRequiredBrand.Text = "Required";
            this.chkRequiredBrand.UseVisualStyleBackColor = true;
            // 
            // txtSizeId
            // 
            this.txtSizeId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSizeId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSizeId.Location = new System.Drawing.Point(86, 42);
            this.txtSizeId.Name = "txtSizeId";
            this.txtSizeId.ReadOnly = true;
            this.txtSizeId.Size = new System.Drawing.Size(69, 20);
            this.txtSizeId.TabIndex = 7;
            // 
            // lblEndsId
            // 
            this.lblEndsId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEndsId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEndsId.Location = new System.Drawing.Point(12, 95);
            this.lblEndsId.Name = "lblEndsId";
            this.lblEndsId.Size = new System.Drawing.Size(75, 20);
            this.lblEndsId.TabIndex = 4;
            this.lblEndsId.Text = "Ends List ID";
            this.lblEndsId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRequiredSize
            // 
            this.chkRequiredSize.AutoCheck = false;
            this.chkRequiredSize.AutoSize = true;
            this.chkRequiredSize.Location = new System.Drawing.Point(161, 45);
            this.chkRequiredSize.Name = "chkRequiredSize";
            this.chkRequiredSize.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredSize.TabIndex = 6;
            this.chkRequiredSize.Text = "Required";
            this.chkRequiredSize.UseVisualStyleBackColor = true;
            // 
            // lblBrandId
            // 
            this.lblBrandId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBrandId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBrandId.Location = new System.Drawing.Point(12, 69);
            this.lblBrandId.Name = "lblBrandId";
            this.lblBrandId.Size = new System.Drawing.Size(75, 20);
            this.lblBrandId.TabIndex = 4;
            this.lblBrandId.Text = "Brand List ID";
            this.lblBrandId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSpecsId
            // 
            this.txtSpecsId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpecsId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSpecsId.Location = new System.Drawing.Point(86, 16);
            this.txtSpecsId.Name = "txtSpecsId";
            this.txtSpecsId.ReadOnly = true;
            this.txtSpecsId.Size = new System.Drawing.Size(69, 20);
            this.txtSpecsId.TabIndex = 7;
            // 
            // lblSizeId
            // 
            this.lblSizeId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSizeId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSizeId.Location = new System.Drawing.Point(12, 42);
            this.lblSizeId.Name = "lblSizeId";
            this.lblSizeId.Size = new System.Drawing.Size(75, 20);
            this.lblSizeId.TabIndex = 4;
            this.lblSizeId.Text = "Size Group ID";
            this.lblSizeId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRequiredSpecs
            // 
            this.chkRequiredSpecs.AutoCheck = false;
            this.chkRequiredSpecs.AutoSize = true;
            this.chkRequiredSpecs.Location = new System.Drawing.Point(161, 19);
            this.chkRequiredSpecs.Name = "chkRequiredSpecs";
            this.chkRequiredSpecs.Size = new System.Drawing.Size(69, 17);
            this.chkRequiredSpecs.TabIndex = 6;
            this.chkRequiredSpecs.Text = "Required";
            this.chkRequiredSpecs.UseVisualStyleBackColor = true;
            // 
            // lblSpecsId
            // 
            this.lblSpecsId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSpecsId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSpecsId.Location = new System.Drawing.Point(12, 16);
            this.lblSpecsId.Name = "lblSpecsId";
            this.lblSpecsId.Size = new System.Drawing.Size(75, 20);
            this.lblSpecsId.TabIndex = 4;
            this.lblSpecsId.Text = "Specs ID";
            this.lblSpecsId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblItemId
            // 
            this.lblItemId.Location = new System.Drawing.Point(294, 19);
            this.lblItemId.Name = "lblItemId";
            this.lblItemId.Size = new System.Drawing.Size(50, 20);
            this.lblItemId.TabIndex = 4;
            this.lblItemId.Text = "Item ID";
            this.lblItemId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtItemId
            // 
            this.txtItemId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtItemId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemId.Location = new System.Drawing.Point(344, 19);
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.ReadOnly = true;
            this.txtItemId.Size = new System.Drawing.Size(69, 20);
            this.txtItemId.TabIndex = 0;
            this.txtItemId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpDescription
            // 
            this.grpDescription.Controls.Add(this.txtDescription);
            this.grpDescription.Location = new System.Drawing.Point(6, 250);
            this.grpDescription.Name = "grpDescription";
            this.grpDescription.Size = new System.Drawing.Size(248, 85);
            this.grpDescription.TabIndex = 0;
            this.grpDescription.TabStop = false;
            this.grpDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Calibri", 9F);
            this.txtDescription.Location = new System.Drawing.Point(6, 16);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(235, 63);
            this.txtDescription.TabIndex = 1;
            // 
            // grpCategory
            // 
            this.grpCategory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpCategory.Controls.Add(this.lblCatName);
            this.grpCategory.Controls.Add(this.txtCatName);
            this.grpCategory.Controls.Add(this.lblCatId);
            this.grpCategory.Controls.Add(this.txtCatId);
            this.grpCategory.Location = new System.Drawing.Point(6, 38);
            this.grpCategory.Name = "grpCategory";
            this.grpCategory.Size = new System.Drawing.Size(407, 42);
            this.grpCategory.TabIndex = 0;
            this.grpCategory.TabStop = false;
            this.grpCategory.Text = "Category";
            // 
            // lblCatName
            // 
            this.lblCatName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCatName.Location = new System.Drawing.Point(130, 15);
            this.lblCatName.Name = "lblCatName";
            this.lblCatName.Size = new System.Drawing.Size(50, 20);
            this.lblCatName.TabIndex = 4;
            this.lblCatName.Text = "Name";
            this.lblCatName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCatName
            // 
            this.txtCatName.Location = new System.Drawing.Point(179, 15);
            this.txtCatName.Name = "txtCatName";
            this.txtCatName.ReadOnly = true;
            this.txtCatName.Size = new System.Drawing.Size(222, 20);
            this.txtCatName.TabIndex = 1;
            // 
            // lblCatId
            // 
            this.lblCatId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCatId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCatId.Location = new System.Drawing.Point(6, 16);
            this.lblCatId.Name = "lblCatId";
            this.lblCatId.Size = new System.Drawing.Size(50, 20);
            this.lblCatId.TabIndex = 4;
            this.lblCatId.Text = "ID";
            this.lblCatId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCatId
            // 
            this.txtCatId.Location = new System.Drawing.Point(55, 16);
            this.txtCatId.Name = "txtCatId";
            this.txtCatId.ReadOnly = true;
            this.txtCatId.Size = new System.Drawing.Size(69, 20);
            this.txtCatId.TabIndex = 1;
            // 
            // grpNames
            // 
            this.grpNames.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpNames.Controls.Add(this.lblNameCommon);
            this.grpNames.Controls.Add(this.lblNameDisplay);
            this.grpNames.Controls.Add(this.lbxNameCommon);
            this.grpNames.Controls.Add(this.txtNameDisplay);
            this.grpNames.Controls.Add(this.lblNameBase);
            this.grpNames.Controls.Add(this.txtNameBase);
            this.grpNames.Location = new System.Drawing.Point(6, 91);
            this.grpNames.Name = "grpNames";
            this.grpNames.Size = new System.Drawing.Size(248, 150);
            this.grpNames.TabIndex = 0;
            this.grpNames.TabStop = false;
            this.grpNames.Text = "Names";
            // 
            // lblNameCommon
            // 
            this.lblNameCommon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNameCommon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblNameCommon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNameCommon.Location = new System.Drawing.Point(6, 54);
            this.lblNameCommon.Margin = new System.Windows.Forms.Padding(1);
            this.lblNameCommon.Name = "lblNameCommon";
            this.lblNameCommon.Size = new System.Drawing.Size(50, 86);
            this.lblNameCommon.TabIndex = 4;
            this.lblNameCommon.Text = "Common";
            this.lblNameCommon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameDisplay
            // 
            this.lblNameDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblNameDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNameDisplay.Location = new System.Drawing.Point(6, 35);
            this.lblNameDisplay.Name = "lblNameDisplay";
            this.lblNameDisplay.Size = new System.Drawing.Size(50, 20);
            this.lblNameDisplay.TabIndex = 4;
            this.lblNameDisplay.Text = "Display";
            this.lblNameDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxNameCommon
            // 
            this.lbxNameCommon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxNameCommon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lbxNameCommon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbxNameCommon.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxNameCommon.FormattingEnabled = true;
            this.lbxNameCommon.ItemHeight = 14;
            this.lbxNameCommon.Location = new System.Drawing.Point(55, 54);
            this.lbxNameCommon.Margin = new System.Windows.Forms.Padding(1);
            this.lbxNameCommon.Name = "lbxNameCommon";
            this.lbxNameCommon.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbxNameCommon.Size = new System.Drawing.Size(186, 86);
            this.lbxNameCommon.TabIndex = 8;
            // 
            // txtNameDisplay
            // 
            this.txtNameDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNameDisplay.Location = new System.Drawing.Point(55, 35);
            this.txtNameDisplay.Name = "txtNameDisplay";
            this.txtNameDisplay.ReadOnly = true;
            this.txtNameDisplay.Size = new System.Drawing.Size(186, 20);
            this.txtNameDisplay.TabIndex = 2;
            // 
            // lblNameBase
            // 
            this.lblNameBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblNameBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNameBase.Location = new System.Drawing.Point(6, 16);
            this.lblNameBase.Name = "lblNameBase";
            this.lblNameBase.Size = new System.Drawing.Size(50, 20);
            this.lblNameBase.TabIndex = 4;
            this.lblNameBase.Text = "Base";
            this.lblNameBase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNameBase
            // 
            this.txtNameBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNameBase.Location = new System.Drawing.Point(55, 16);
            this.txtNameBase.Name = "txtNameBase";
            this.txtNameBase.ReadOnly = true;
            this.txtNameBase.Size = new System.Drawing.Size(186, 20);
            this.txtNameBase.TabIndex = 1;
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(93, 39);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(12, 39);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(183, 39);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // chkConfirmDelete
            // 
            this.chkConfirmDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkConfirmDelete.AutoSize = true;
            this.chkConfirmDelete.Checked = true;
            this.chkConfirmDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConfirmDelete.Location = new System.Drawing.Point(262, 43);
            this.chkConfirmDelete.Name = "chkConfirmDelete";
            this.chkConfirmDelete.Size = new System.Drawing.Size(97, 17);
            this.chkConfirmDelete.TabIndex = 7;
            this.chkConfirmDelete.Text = "Confirm Delete";
            this.chkConfirmDelete.UseVisualStyleBackColor = true;
            // 
            // txtFilterId
            // 
            this.txtFilterId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFilterId.Location = new System.Drawing.Point(55, 16);
            this.txtFilterId.Name = "txtFilterId";
            this.txtFilterId.Size = new System.Drawing.Size(69, 20);
            this.txtFilterId.TabIndex = 0;
            this.txtFilterId.TextChanged += new System.EventHandler(this.txtFilterId_TextChanged);
            // 
            // lblSearchId
            // 
            this.lblSearchId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblSearchId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchId.Location = new System.Drawing.Point(6, 16);
            this.lblSearchId.Name = "lblSearchId";
            this.lblSearchId.Size = new System.Drawing.Size(50, 20);
            this.lblSearchId.TabIndex = 4;
            this.lblSearchId.Text = "Item ID";
            this.lblSearchId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFilterName
            // 
            this.txtFilterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterName.Location = new System.Drawing.Point(179, 16);
            this.txtFilterName.MaximumSize = new System.Drawing.Size(250, 20);
            this.txtFilterName.MinimumSize = new System.Drawing.Size(162, 20);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(162, 20);
            this.txtFilterName.TabIndex = 1;
            this.txtFilterName.TextChanged += new System.EventHandler(this.txtFilterName_TextChanged);
            // 
            // lblSearchName
            // 
            this.lblSearchName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblSearchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchName.Location = new System.Drawing.Point(130, 16);
            this.lblSearchName.Name = "lblSearchName";
            this.lblSearchName.Size = new System.Drawing.Size(50, 20);
            this.lblSearchName.TabIndex = 4;
            this.lblSearchName.Text = "Name";
            this.lblSearchName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnFilterClear);
            this.grpSearch.Controls.Add(this.panel1);
            this.grpSearch.Controls.Add(this.txtFilterCatId);
            this.grpSearch.Controls.Add(this.panel8);
            this.grpSearch.Controls.Add(this.label1);
            this.grpSearch.Controls.Add(this.lblSearchId);
            this.grpSearch.Controls.Add(this.lblSearchName);
            this.grpSearch.Controls.Add(this.txtFilterId);
            this.grpSearch.Controls.Add(this.txtFilterName);
            this.grpSearch.Enabled = false;
            this.grpSearch.Location = new System.Drawing.Point(12, 68);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(347, 101);
            this.grpSearch.TabIndex = 8;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Filter";
            // 
            // btnFilterClear
            // 
            this.btnFilterClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterClear.AutoSize = true;
            this.btnFilterClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFilterClear.Location = new System.Drawing.Point(299, 72);
            this.btnFilterClear.Name = "btnFilterClear";
            this.btnFilterClear.Size = new System.Drawing.Size(42, 23);
            this.btnFilterClear.TabIndex = 14;
            this.btnFilterClear.Text = "Clear";
            this.btnFilterClear.UseVisualStyleBackColor = true;
            this.btnFilterClear.Click += new System.EventHandler(this.btnFilterClear_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cboFilterCategory);
            this.panel1.Location = new System.Drawing.Point(59, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 23);
            this.panel1.TabIndex = 10;
            // 
            // cboFilterCategory
            // 
            this.cboFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFilterCategory.FormattingEnabled = true;
            this.cboFilterCategory.Location = new System.Drawing.Point(0, 0);
            this.cboFilterCategory.Margin = new System.Windows.Forms.Padding(0);
            this.cboFilterCategory.Name = "cboFilterCategory";
            this.cboFilterCategory.Size = new System.Drawing.Size(211, 21);
            this.cboFilterCategory.TabIndex = 14;
            this.cboFilterCategory.SelectedIndexChanged += new System.EventHandler(this.cboFilterCategory_SelectedIndexChanged);
            // 
            // txtFilterCatId
            // 
            this.txtFilterCatId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterCatId.Location = new System.Drawing.Point(278, 44);
            this.txtFilterCatId.Name = "txtFilterCatId";
            this.txtFilterCatId.ReadOnly = true;
            this.txtFilterCatId.Size = new System.Drawing.Size(63, 20);
            this.txtFilterCatId.TabIndex = 1;
            this.txtFilterCatId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel8
            // 
            this.panel8.AutoSize = true;
            this.panel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.rdoFilterNoImage);
            this.panel8.Controls.Add(this.rdoFilterHasImage);
            this.panel8.Controls.Add(this.rdoFilterAnyImage);
            this.panel8.Controls.Add(this.lblSearchImage);
            this.panel8.Location = new System.Drawing.Point(6, 69);
            this.panel8.Margin = new System.Windows.Forms.Padding(2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(266, 27);
            this.panel8.TabIndex = 13;
            // 
            // rdoFilterNoImage
            // 
            this.rdoFilterNoImage.AutoSize = true;
            this.rdoFilterNoImage.Location = new System.Drawing.Point(190, 5);
            this.rdoFilterNoImage.Name = "rdoFilterNoImage";
            this.rdoFilterNoImage.Size = new System.Drawing.Size(71, 17);
            this.rdoFilterNoImage.TabIndex = 7;
            this.rdoFilterNoImage.Text = "No Image";
            this.rdoFilterNoImage.UseVisualStyleBackColor = true;
            this.rdoFilterNoImage.CheckedChanged += new System.EventHandler(this.rdoFilterNoImage_CheckedChanged);
            // 
            // rdoFilterHasImage
            // 
            this.rdoFilterHasImage.AutoSize = true;
            this.rdoFilterHasImage.Location = new System.Drawing.Point(108, 5);
            this.rdoFilterHasImage.Name = "rdoFilterHasImage";
            this.rdoFilterHasImage.Size = new System.Drawing.Size(76, 17);
            this.rdoFilterHasImage.TabIndex = 6;
            this.rdoFilterHasImage.Text = "Has Image";
            this.rdoFilterHasImage.UseVisualStyleBackColor = true;
            this.rdoFilterHasImage.CheckedChanged += new System.EventHandler(this.rdoFilterHasImage_CheckedChanged);
            // 
            // rdoFilterAnyImage
            // 
            this.rdoFilterAnyImage.AutoSize = true;
            this.rdoFilterAnyImage.Checked = true;
            this.rdoFilterAnyImage.Location = new System.Drawing.Point(59, 5);
            this.rdoFilterAnyImage.Name = "rdoFilterAnyImage";
            this.rdoFilterAnyImage.Size = new System.Drawing.Size(44, 17);
            this.rdoFilterAnyImage.TabIndex = 5;
            this.rdoFilterAnyImage.TabStop = true;
            this.rdoFilterAnyImage.Text = "Any";
            this.rdoFilterAnyImage.UseVisualStyleBackColor = true;
            this.rdoFilterAnyImage.CheckedChanged += new System.EventHandler(this.rdoFilterAnyImage_CheckedChanged);
            // 
            // lblSearchImage
            // 
            this.lblSearchImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblSearchImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchImage.Location = new System.Drawing.Point(3, 3);
            this.lblSearchImage.Name = "lblSearchImage";
            this.lblSearchImage.Size = new System.Drawing.Size(50, 20);
            this.lblSearchImage.TabIndex = 4;
            this.lblSearchImage.Text = "Image";
            this.lblSearchImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Category";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoItems
            // 
            this.lblNoItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoItems.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblNoItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoItems.Location = new System.Drawing.Point(0, 172);
            this.lblNoItems.Name = "lblNoItems";
            this.lblNoItems.Size = new System.Drawing.Size(359, 21);
            this.lblNoItems.TabIndex = 9;
            this.lblNoItems.Text = "No items found";
            this.lblNoItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoItems.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblItemsCount,
            this.tslblShownItemsCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 24);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblItemsCount
            // 
            this.tslblItemsCount.AutoSize = false;
            this.tslblItemsCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslblItemsCount.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tslblItemsCount.Name = "tslblItemsCount";
            this.tslblItemsCount.Size = new System.Drawing.Size(80, 19);
            this.tslblItemsCount.Text = "Items: 0";
            this.tslblItemsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslblShownItemsCount
            // 
            this.tslblShownItemsCount.AutoSize = false;
            this.tslblShownItemsCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslblShownItemsCount.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tslblShownItemsCount.Name = "tslblShownItemsCount";
            this.tslblShownItemsCount.Size = new System.Drawing.Size(80, 19);
            this.tslblShownItemsCount.Text = "Shown: 0";
            this.tslblShownItemsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ItemViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblNoItems);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.chkConfirmDelete);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.grpData);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ItemViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ItemEditor_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.grpImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.cmsImageOptions.ResumeLayout(false);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.grpDescription.ResumeLayout(false);
            this.grpDescription.PerformLayout();
            this.grpCategory.ResumeLayout(false);
            this.grpCategory.PerformLayout();
            this.grpNames.ResumeLayout(false);
            this.grpNames.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem msmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveXmlFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseForm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExitApp;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.Label lblUom;
        private System.Windows.Forms.TextBox txtUom;
        private System.Windows.Forms.GroupBox grpImages;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.GroupBox grpDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox grpNames;
        private System.Windows.Forms.Label lblNameCommon;
        private System.Windows.Forms.Label lblNameDisplay;
        private System.Windows.Forms.ListBox lbxNameCommon;
        private System.Windows.Forms.TextBox txtNameDisplay;
        private System.Windows.Forms.Label lblNameBase;
        private System.Windows.Forms.TextBox txtNameBase;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblItemId;
        private System.Windows.Forms.TextBox txtItemId;
        private System.Windows.Forms.GroupBox grpCategory;
        private System.Windows.Forms.Label lblCatName;
        private System.Windows.Forms.TextBox txtCatName;
        private System.Windows.Forms.Label lblCatId;
        private System.Windows.Forms.TextBox txtCatId;
        private System.Windows.Forms.ListBox lbxImages;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.CheckBox chkConfirmDelete;
        private System.Windows.Forms.TextBox txtEndsId;
        private System.Windows.Forms.CheckBox chkRequiredEnds;
        private System.Windows.Forms.TextBox txtBrandId;
        private System.Windows.Forms.CheckBox chkRequiredBrand;
        private System.Windows.Forms.TextBox txtSizeId;
        private System.Windows.Forms.Label lblEndsId;
        private System.Windows.Forms.CheckBox chkRequiredSize;
        private System.Windows.Forms.Label lblBrandId;
        private System.Windows.Forms.TextBox txtSpecsId;
        private System.Windows.Forms.Label lblSizeId;
        private System.Windows.Forms.CheckBox chkRequiredSpecs;
        private System.Windows.Forms.Label lblSpecsId;
        private System.Windows.Forms.TextBox txtFilterId;
        private System.Windows.Forms.Label lblSearchId;
        private System.Windows.Forms.TextBox txtFilterName;
        private System.Windows.Forms.Label lblSearchName;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton rdoFilterNoImage;
        private System.Windows.Forms.RadioButton rdoFilterHasImage;
        private System.Windows.Forms.RadioButton rdoFilterAnyImage;
        private System.Windows.Forms.Label lblSearchImage;
        private System.Windows.Forms.Label lblNoItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilterCatId;
        private System.Windows.Forms.Button btnFilterClear;
        private System.Windows.Forms.ContextMenuStrip cmsImageOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiEnlargeImage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblItemsCount;
        private System.Windows.Forms.ToolStripStatusLabel tslblShownItemsCount;
        private System.Windows.Forms.ToolStripMenuItem tsmiItems;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditItem;
    }
}