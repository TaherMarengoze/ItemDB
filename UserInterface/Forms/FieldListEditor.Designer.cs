namespace UserInterface.Forms
{
    partial class FieldListEditor
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
            this.lblListID = new System.Windows.Forms.Label();
            this.txtListID = new System.Windows.Forms.TextBox();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.lblListName = new System.Windows.Forms.Label();
            this.txtInitialEntry = new System.Windows.Forms.TextBox();
            this.lblInitialEntry = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblValidID = new System.Windows.Forms.Label();
            this.lbxExistingCodes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblListID
            // 
            this.lblListID.AutoSize = true;
            this.lblListID.Location = new System.Drawing.Point(36, 15);
            this.lblListID.Name = "lblListID";
            this.lblListID.Size = new System.Drawing.Size(37, 13);
            this.lblListID.TabIndex = 0;
            this.lblListID.Text = "List ID";
            // 
            // txtListID
            // 
            this.txtListID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtListID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtListID.Location = new System.Drawing.Point(79, 12);
            this.txtListID.MaxLength = 5;
            this.txtListID.Name = "txtListID";
            this.txtListID.Size = new System.Drawing.Size(100, 20);
            this.txtListID.TabIndex = 1;
            this.txtListID.TextChanged += new System.EventHandler(this.txtListID_TextChanged);
            // 
            // txtListName
            // 
            this.txtListName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtListName.Location = new System.Drawing.Point(79, 38);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(206, 20);
            this.txtListName.TabIndex = 2;
            // 
            // lblListName
            // 
            this.lblListName.AutoSize = true;
            this.lblListName.Location = new System.Drawing.Point(20, 41);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(53, 13);
            this.lblListName.TabIndex = 3;
            this.lblListName.Text = "List Name";
            // 
            // txtInitialEntry
            // 
            this.txtInitialEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtInitialEntry.Location = new System.Drawing.Point(79, 64);
            this.txtInitialEntry.Name = "txtInitialEntry";
            this.txtInitialEntry.Size = new System.Drawing.Size(206, 20);
            this.txtInitialEntry.TabIndex = 4;
            this.txtInitialEntry.TextChanged += new System.EventHandler(this.txtInitialEntry_TextChanged);
            // 
            // lblInitialEntry
            // 
            this.lblInitialEntry.AutoSize = true;
            this.lblInitialEntry.Location = new System.Drawing.Point(11, 67);
            this.lblInitialEntry.Name = "lblInitialEntry";
            this.lblInitialEntry.Size = new System.Drawing.Size(62, 13);
            this.lblInitialEntry.TabIndex = 5;
            this.lblInitialEntry.Text = "Initial Entry";
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAdd.Location = new System.Drawing.Point(79, 113);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(185, 113);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblValidID
            // 
            this.lblValidID.AutoSize = true;
            this.lblValidID.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Italic);
            this.lblValidID.ForeColor = System.Drawing.Color.Red;
            this.lblValidID.Location = new System.Drawing.Point(185, 15);
            this.lblValidID.Name = "lblValidID";
            this.lblValidID.Size = new System.Drawing.Size(0, 12);
            this.lblValidID.TabIndex = 7;
            // 
            // lbxExistingCodes
            // 
            this.lbxExistingCodes.FormattingEnabled = true;
            this.lbxExistingCodes.Location = new System.Drawing.Point(291, 28);
            this.lbxExistingCodes.Name = "lbxExistingCodes";
            this.lbxExistingCodes.Size = new System.Drawing.Size(100, 108);
            this.lbxExistingCodes.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label1.Location = new System.Drawing.Point(310, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Existing IDs";
            // 
            // FieldListEditor
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(402, 149);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxExistingCodes);
            this.Controls.Add(this.lblValidID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblInitialEntry);
            this.Controls.Add(this.txtInitialEntry);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.txtListID);
            this.Controls.Add(this.lblListID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FieldListEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "List Editor";
            this.Load += new System.EventHandler(this.FieldListEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblListID;
        private System.Windows.Forms.TextBox txtListID;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.TextBox txtInitialEntry;
        private System.Windows.Forms.Label lblInitialEntry;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblValidID;
        private System.Windows.Forms.ListBox lbxExistingCodes;
        private System.Windows.Forms.Label label1;
    }
}