namespace UserInterface.Forms
{
    partial class FieldListAdderExtendable
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxExistingCodes
            // 
            this.lbxExistingCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxExistingCodes.FormattingEnabled = true;
            this.lbxExistingCodes.Location = new System.Drawing.Point(3, 16);
            this.lbxExistingCodes.Name = "lbxExistingCodes";
            this.lbxExistingCodes.Size = new System.Drawing.Size(123, 73);
            this.lbxExistingCodes.TabIndex = 0;
            // 
            // lblValidID
            // 
            this.lblValidID.AutoSize = true;
            this.lblValidID.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Italic);
            this.lblValidID.ForeColor = System.Drawing.Color.Red;
            this.lblValidID.Location = new System.Drawing.Point(182, 15);
            this.lblValidID.Name = "lblValidID";
            this.lblValidID.Size = new System.Drawing.Size(0, 12);
            this.lblValidID.TabIndex = 11;
            // 
            // lblInitialEntry
            // 
            this.lblInitialEntry.AutoSize = true;
            this.lblInitialEntry.Location = new System.Drawing.Point(12, 67);
            this.lblInitialEntry.Name = "lblInitialEntry";
            this.lblInitialEntry.Size = new System.Drawing.Size(52, 13);
            this.lblInitialEntry.TabIndex = 4;
            this.lblInitialEntry.Text = "List Entry";
            // 
            // lblListName
            // 
            this.lblListName.AutoSize = true;
            this.lblListName.Location = new System.Drawing.Point(17, 41);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(53, 13);
            this.lblListName.TabIndex = 2;
            this.lblListName.Text = "List &Name";
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(76, 38);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(206, 20);
            this.txtListName.TabIndex = 3;
            this.txtListName.TextChanged += new System.EventHandler(this.txtListName_TextChanged);
            // 
            // txtListID
            // 
            this.txtListID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtListID.Location = new System.Drawing.Point(76, 12);
            this.txtListID.MaxLength = 5;
            this.txtListID.Name = "txtListID";
            this.txtListID.Size = new System.Drawing.Size(100, 20);
            this.txtListID.TabIndex = 1;
            this.txtListID.TextChanged += new System.EventHandler(this.txtListID_TextChanged);
            // 
            // lblListID
            // 
            this.lblListID.AutoSize = true;
            this.lblListID.Location = new System.Drawing.Point(33, 15);
            this.lblListID.Name = "lblListID";
            this.lblListID.Size = new System.Drawing.Size(37, 13);
            this.lblListID.TabIndex = 0;
            this.lblListID.Text = "List &ID";
            // 
            // btnDeleteEntry
            // 
            this.btnDeleteEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteEntry.Enabled = false;
            this.btnDeleteEntry.Location = new System.Drawing.Point(161, 310);
            this.btnDeleteEntry.Name = "btnDeleteEntry";
            this.btnDeleteEntry.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEntry.TabIndex = 2;
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
            this.btnEdit.TabIndex = 1;
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
            this.groupBox1.TabIndex = 7;
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
            this.lbxFieldListItems.TabIndex = 0;
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
            this.btnUp.TabIndex = 3;
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
            this.btnDown.TabIndex = 4;
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnAddEntry
            // 
            this.btnAddEntry.Enabled = false;
            this.btnAddEntry.Location = new System.Drawing.Point(207, 81);
            this.btnAddEntry.Name = "btnAddEntry";
            this.btnAddEntry.Size = new System.Drawing.Size(75, 23);
            this.btnAddEntry.TabIndex = 6;
            this.btnAddEntry.Text = "Add Entry";
            this.btnAddEntry.UseVisualStyleBackColor = true;
            // 
            // txtEntryValue
            // 
            this.txtEntryValue.Location = new System.Drawing.Point(15, 83);
            this.txtEntryValue.Name = "txtEntryValue";
            this.txtEntryValue.Size = new System.Drawing.Size(186, 20);
            this.txtEntryValue.TabIndex = 5;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(381, 399);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(107, 50);
            this.btnAccept.TabIndex = 10;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(381, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lbxExistingCodes);
            this.groupBox2.Location = new System.Drawing.Point(288, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 92);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Existing Lists ID";
            // 
            // dgvExistingCodes
            // 
            this.dgvExistingCodes.AllowUserToResizeRows = false;
            this.dgvExistingCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvExistingCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExistingCodes.Location = new System.Drawing.Point(288, 110);
            this.dgvExistingCodes.Name = "dgvExistingCodes";
            this.dgvExistingCodes.RowHeadersVisible = false;
            this.dgvExistingCodes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvExistingCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExistingCodes.Size = new System.Drawing.Size(200, 254);
            this.dgvExistingCodes.TabIndex = 8;
            // 
            // FieldListAdderExtendable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(500, 461);
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
            this.Name = "FieldListAdderExtendable";
            this.Text = "New Field List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FieldListAdder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingCodes)).EndInit();
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
    }
}