namespace UserInterface.Forms
{
    partial class XML_Viewer
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
            this.rtbViewerPart = new System.Windows.Forms.RichTextBox();
            this.rtbViewerAll = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbViewerPart
            // 
            this.rtbViewerPart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbViewerPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbViewerPart.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbViewerPart.Location = new System.Drawing.Point(0, 0);
            this.rtbViewerPart.Name = "rtbViewerPart";
            this.rtbViewerPart.Size = new System.Drawing.Size(300, 624);
            this.rtbViewerPart.TabIndex = 0;
            this.rtbViewerPart.Text = "";
            // 
            // rtbViewerAll
            // 
            this.rtbViewerAll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbViewerAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbViewerAll.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbViewerAll.Location = new System.Drawing.Point(0, 0);
            this.rtbViewerAll.Name = "rtbViewerAll";
            this.rtbViewerAll.Size = new System.Drawing.Size(420, 624);
            this.rtbViewerAll.TabIndex = 1;
            this.rtbViewerAll.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtbViewerPart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbViewerAll);
            this.splitContainer1.Size = new System.Drawing.Size(729, 626);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // XML_Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 626);
            this.Controls.Add(this.splitContainer1);
            this.Name = "XML_Viewer";
            this.Text = "XML_Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbViewerPart;
        public System.Windows.Forms.RichTextBox rtbViewerAll;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}