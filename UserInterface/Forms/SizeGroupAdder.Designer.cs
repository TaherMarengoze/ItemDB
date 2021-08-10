namespace UserInterface.Forms
{
    partial class SizeGroupAdder
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
            this.lbxExistingCodes = new System.Windows.Forms.ListBox();
            this.lblValidID = new System.Windows.Forms.Label();
            this.lblInitialEntry = new System.Windows.Forms.Label();
            this.lblListName = new System.Windows.Forms.Label();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.txtListID = new System.Windows.Forms.TextBox();
            this.lblListID = new System.Windows.Forms.Label();
            this.btnDeleteEntry = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbxFieldListItems = new System.Windows.Forms.ListBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnAddEntry = new System.Windows.Forms.Button();
            this.txtEntryValue = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvExistingCodes = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkAutoId = new System.Windows.Forms.CheckBox();
            this.chkAutoName = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSizeGroupId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingCodes)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxExistingCodes
            // 
            this.lbxExistingCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxExistingCodes.FormattingEnabled = true;
            this.lbxExistingCodes.Location = new System.Drawing.Point(3, 16);
            this.lbxExistingCodes.Name = "lbxExistingCodes";
            this.lbxExistingCodes.Size = new System.Drawing.Size(123, 73);
            this.lbxExistingCodes.TabIndex = 17;
            // 
            // lblValidID
            // 
            this.lblValidID.AutoSize = true;
            this.lblValidID.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Italic);
            this.lblValidID.ForeColor = System.Drawing.Color.Red;
            this.lblValidID.Location = new System.Drawing.Point(182, 15);
            this.lblValidID.Name = "lblValidID";
            this.lblValidID.Size = new System.Drawing.Size(0, 12);
            this.lblValidID.TabIndex = 16;
            // 
            // lblInitialEntry
            // 
            this.lblInitialEntry.AutoSize = true;
            this.lblInitialEntry.Location = new System.Drawing.Point(12, 67);
            this.lblInitialEntry.Name = "lblInitialEntry";
            this.lblInitialEntry.Size = new System.Drawing.Size(52, 13);
            this.lblInitialEntry.TabIndex = 15;
            this.lblInitialEntry.Text = "List Entry";
            // 
            // lblListName
            // 
            this.lblListName.AutoSize = true;
            this.lblListName.Location = new System.Drawing.Point(17, 41);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(53, 13);
            this.lblListName.TabIndex = 13;
            this.lblListName.Text = "List Name";
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(76, 38);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(206, 20);
            this.txtListName.TabIndex = 12;
            this.txtListName.TextChanged += new System.EventHandler(this.txtListName_TextChanged);
            // 
            // txtListID
            // 
            this.txtListID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtListID.Location = new System.Drawing.Point(76, 12);
            this.txtListID.MaxLength = 5;
            this.txtListID.Name = "txtListID";
            this.txtListID.Size = new System.Drawing.Size(100, 20);
            this.txtListID.TabIndex = 11;
            this.txtListID.TextChanged += new System.EventHandler(this.txtListID_TextChanged);
            // 
            // lblListID
            // 
            this.lblListID.AutoSize = true;
            this.lblListID.Location = new System.Drawing.Point(33, 15);
            this.lblListID.Name = "lblListID";
            this.lblListID.Size = new System.Drawing.Size(37, 13);
            this.lblListID.TabIndex = 10;
            this.lblListID.Text = "List ID";
            // 
            // btnDeleteEntry
            // 
            this.btnDeleteEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteEntry.Enabled = false;
            this.btnDeleteEntry.Location = new System.Drawing.Point(161, 310);
            this.btnDeleteEntry.Name = "btnDeleteEntry";
            this.btnDeleteEntry.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEntry.TabIndex = 19;
            this.btnDeleteEntry.Text = "Delete Entry";
            this.btnDeleteEntry.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(3, 310);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 20;
            this.btnEdit.Text = "Edit Entry";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lbxFieldListItems);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnDeleteEntry);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Location = new System.Drawing.Point(12, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 339);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List Entries";
            // 
            // lbxFieldListItems
            // 
            this.lbxFieldListItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxFieldListItems.FormattingEnabled = true;
            this.lbxFieldListItems.IntegralHeight = false;
            this.lbxFieldListItems.Location = new System.Drawing.Point(3, 16);
            this.lbxFieldListItems.Name = "lbxFieldListItems";
            this.lbxFieldListItems.Size = new System.Drawing.Size(233, 288);
            this.lbxFieldListItems.TabIndex = 2;
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Enabled = false;
            this.btnUp.Image = global::UserInterface.Properties.Resources.up_arrow_16x;
            this.btnUp.Location = new System.Drawing.Point(239, 16);
            this.btnUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(28, 28);
            this.btnUp.TabIndex = 22;
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Enabled = false;
            this.btnDown.Image = global::UserInterface.Properties.Resources.down_arrow_16x;
            this.btnDown.Location = new System.Drawing.Point(239, 44);
            this.btnDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(28, 28);
            this.btnDown.TabIndex = 23;
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnAddEntry
            // 
            this.btnAddEntry.Enabled = false;
            this.btnAddEntry.Location = new System.Drawing.Point(207, 81);
            this.btnAddEntry.Name = "btnAddEntry";
            this.btnAddEntry.Size = new System.Drawing.Size(75, 23);
            this.btnAddEntry.TabIndex = 25;
            this.btnAddEntry.Text = "Add Entry";
            this.btnAddEntry.UseVisualStyleBackColor = true;
            // 
            // txtEntryValue
            // 
            this.txtEntryValue.Location = new System.Drawing.Point(15, 83);
            this.txtEntryValue.Name = "txtEntryValue";
            this.txtEntryValue.Size = new System.Drawing.Size(186, 20);
            this.txtEntryValue.TabIndex = 24;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(310, 399);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(107, 50);
            this.btnAccept.TabIndex = 26;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(310, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbxExistingCodes);
            this.groupBox2.Location = new System.Drawing.Point(288, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 92);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Existing Lists ID";
            // 
            // dgvExistingCodes
            // 
            this.dgvExistingCodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExistingCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExistingCodes.Location = new System.Drawing.Point(288, 110);
            this.dgvExistingCodes.Name = "dgvExistingCodes";
            this.dgvExistingCodes.Size = new System.Drawing.Size(129, 254);
            this.dgvExistingCodes.TabIndex = 29;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Controls.Add(this.chkAutoId);
            this.groupBox3.Controls.Add(this.chkAutoName);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtSizeGroupId);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(423, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(218, 352);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Size Group Metadata";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 196);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(206, 150);
            this.dataGridView1.TabIndex = 7;
            // 
            // chkAutoId
            // 
            this.chkAutoId.AutoSize = true;
            this.chkAutoId.Checked = true;
            this.chkAutoId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoId.Location = new System.Drawing.Point(6, 68);
            this.chkAutoId.Name = "chkAutoId";
            this.chkAutoId.Size = new System.Drawing.Size(198, 17);
            this.chkAutoId.TabIndex = 6;
            this.chkAutoId.Text = "Auto generate based on Size List ID";
            this.chkAutoId.UseVisualStyleBackColor = true;
            // 
            // chkAutoName
            // 
            this.chkAutoName.AutoSize = true;
            this.chkAutoName.Checked = true;
            this.chkAutoName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoName.Location = new System.Drawing.Point(6, 153);
            this.chkAutoName.Name = "chkAutoName";
            this.chkAutoName.Size = new System.Drawing.Size(137, 17);
            this.chkAutoName.TabIndex = 5;
            this.chkAutoName.Text = "Same as Size List Name";
            this.chkAutoName.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 127);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(206, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "SG List Name";
            // 
            // txtSizeGroupId
            // 
            this.txtSizeGroupId.Location = new System.Drawing.Point(6, 42);
            this.txtSizeGroupId.Name = "txtSizeGroupId";
            this.txtSizeGroupId.Size = new System.Drawing.Size(100, 20);
            this.txtSizeGroupId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SG List ID";
            // 
            // SizeGroupAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(653, 461);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvExistingCodes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnAddEntry);
            this.Controls.Add(this.txtEntryValue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblValidID);
            this.Controls.Add(this.lblInitialEntry);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.txtListID);
            this.Controls.Add(this.lblListID);
            this.Name = "SizeGroupAdder";
            this.Text = "New Field List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FieldListAdder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingCodes)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lbxExistingCodes;
        private System.Windows.Forms.Label lblValidID;
        private System.Windows.Forms.Label lblInitialEntry;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.TextBox txtListID;
        private System.Windows.Forms.Label lblListID;
        private System.Windows.Forms.Button btnDeleteEntry;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbxFieldListItems;
        private System.Windows.Forms.Button btnAddEntry;
        private System.Windows.Forms.TextBox txtEntryValue;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvExistingCodes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkAutoId;
        private System.Windows.Forms.CheckBox chkAutoName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSizeGroupId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}