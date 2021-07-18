namespace UserInterface.Forms
{
    partial class AltListSelector
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
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvSizeLists = new System.Windows.Forms.DataGridView();
            this.lstSizeListItems = new System.Windows.Forms.ListBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.txtFilterId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIncluded = new System.Windows.Forms.CheckBox();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.txtTest = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeLists)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(409, 326);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvSizeLists
            // 
            this.dgvSizeLists.AllowUserToAddRows = false;
            this.dgvSizeLists.AllowUserToDeleteRows = false;
            this.dgvSizeLists.AllowUserToResizeRows = false;
            this.dgvSizeLists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSizeLists.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSizeLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizeLists.Location = new System.Drawing.Point(12, 0);
            this.dgvSizeLists.MultiSelect = false;
            this.dgvSizeLists.Name = "dgvSizeLists";
            this.dgvSizeLists.RowHeadersVisible = false;
            this.dgvSizeLists.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSizeLists.Size = new System.Drawing.Size(472, 239);
            this.dgvSizeLists.StandardTab = true;
            this.dgvSizeLists.TabIndex = 2;
            this.dgvSizeLists.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSizeLists_CellContentClick);
            this.dgvSizeLists.SelectionChanged += new System.EventHandler(this.dgvSizeLists_SelectionChanged);
            // 
            // lstSizeListItems
            // 
            this.lstSizeListItems.Dock = System.Windows.Forms.DockStyle.Right;
            this.lstSizeListItems.FormattingEnabled = true;
            this.lstSizeListItems.Location = new System.Drawing.Point(490, 0);
            this.lstSizeListItems.Name = "lstSizeListItems";
            this.lstSizeListItems.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstSizeListItems.Size = new System.Drawing.Size(194, 361);
            this.lstSizeListItems.TabIndex = 3;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(12, 326);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // txtFilterName
            // 
            this.txtFilterName.Location = new System.Drawing.Point(316, 19);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(150, 20);
            this.txtFilterName.TabIndex = 5;
            this.txtFilterName.TextChanged += new System.EventHandler(this.txtFilterName_TextChanged);
            // 
            // txtFilterId
            // 
            this.txtFilterId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFilterId.Location = new System.Drawing.Point(167, 19);
            this.txtFilterId.MaxLength = 5;
            this.txtFilterId.Name = "txtFilterId";
            this.txtFilterId.Size = new System.Drawing.Size(60, 20);
            this.txtFilterId.TabIndex = 6;
            this.txtFilterId.TextChanged += new System.EventHandler(this.txtFilterId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "By ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "By Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkIncluded);
            this.groupBox1.Controls.Add(this.txtFilterId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFilterName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 274);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 46);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // chkIncluded
            // 
            this.chkIncluded.AutoSize = true;
            this.chkIncluded.Location = new System.Drawing.Point(6, 21);
            this.chkIncluded.Name = "chkIncluded";
            this.chkIncluded.Size = new System.Drawing.Size(67, 17);
            this.chkIncluded.TabIndex = 9;
            this.chkIncluded.Text = "Included";
            this.chkIncluded.UseVisualStyleBackColor = true;
            this.chkIncluded.CheckedChanged += new System.EventHandler(this.chkIncluded_CheckedChanged);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeselectAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeselectAll.Enabled = false;
            this.btnDeselectAll.Location = new System.Drawing.Point(384, 245);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(100, 23);
            this.btnDeselectAll.TabIndex = 11;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // txtTest
            // 
            this.txtTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTest.Location = new System.Drawing.Point(93, 326);
            this.txtTest.Name = "txtTest";
            this.txtTest.ReadOnly = true;
            this.txtTest.Size = new System.Drawing.Size(310, 20);
            this.txtTest.TabIndex = 12;
            // 
            // AltListSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.btnDeselectAll);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lstSizeListItems);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvSizeLists);
            this.Name = "AltListSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alt Lists Selector";
            this.Load += new System.EventHandler(this.AltListSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeLists)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvSizeLists;
        private System.Windows.Forms.ListBox lstSizeListItems;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.TextBox txtFilterName;
        private System.Windows.Forms.TextBox txtFilterId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.TextBox txtTest;
        private System.Windows.Forms.CheckBox chkIncluded;
    }
}