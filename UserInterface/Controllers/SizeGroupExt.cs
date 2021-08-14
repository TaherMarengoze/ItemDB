
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UserInterface
{
    using Forms;
    using System.Collections.Generic;
    using System.Linq;
    using UserInterface.Models;
    using UserInterface.Operation;

    public class SizeGroupExt
    {
        readonly FieldListAdderExtendable host;
        private IEnumerable<BasicView> existSizeGroupIds;

        #region Host components fields
        private TextBox txtListID;
        private TextBox txtListName;
        #endregion

        #region local variables (fields)
        private GroupBox grpSizeGroup;
        private CheckBox chkAutoId;
        private CheckBox chkAutoName;
        private TextBox txtSizeGroupId;
        private TextBox txtSizeGroupName;
        private Label label1;
        private Label label2;
        private DataGridView dgvSizeGroupIdList;
        #endregion

        public SizeGroupExt(FieldListAdderExtendable source)
        {
            host = source;
            ParentComponentPreSetting();
            InitializeComponent();
            ParentComponentPostSetting();
        }

        private void ParentComponentPreSetting()
        {
            host.Load += Host_Load;

            txtListID = (TextBox)GetHostControl("txtListID");
            txtListName = (TextBox)GetHostControl("txtListName");

            txtListID.TextChanged += Host_txtListID_TextChanged;
            txtListName.TextChanged += Host_txtListName_TextChanged;
        }
        
        private void InitializeComponent()
        {
            #region Instantiate components
            grpSizeGroup = new GroupBox();
            dgvSizeGroupIdList = new DataGridView();
            chkAutoId = new CheckBox();
            chkAutoName = new CheckBox();
            txtSizeGroupName = new TextBox();
            label2 = new Label();
            txtSizeGroupId = new TextBox();
            label1 = new Label();
            #endregion

            int w0 = host.ClientSize.Width;

            #region Initialize
            grpSizeGroup.SuspendLayout();
            ((ISupportInitialize)dgvSizeGroupIdList).BeginInit();
            #endregion

            #region Components Setup

            #region grpSizeGroup
            // 
            // grpSizeGroup
            // 
            grpSizeGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpSizeGroup.Controls.Add(dgvSizeGroupIdList);
            grpSizeGroup.Controls.Add(chkAutoId);
            grpSizeGroup.Controls.Add(chkAutoName);
            grpSizeGroup.Controls.Add(txtSizeGroupName);
            grpSizeGroup.Controls.Add(label2);
            grpSizeGroup.Controls.Add(txtSizeGroupId);
            grpSizeGroup.Controls.Add(label1);
            grpSizeGroup.Location = new Point(w0, 12);
            grpSizeGroup.Name = "grpSizeGroup";
            grpSizeGroup.Size = new Size(218, 352);
            grpSizeGroup.TabIndex = 30;
            grpSizeGroup.TabStop = false;
            grpSizeGroup.Text = "Size Group Metadata";
            #endregion

            #region dgvSizeGroupIdList
            // 
            // dgvSizeGroupIdList
            //
            dgvSizeGroupIdList.Anchor = AnchorStyles.Bottom | AnchorStyles.Top;
            dgvSizeGroupIdList.AllowUserToResizeRows = false;
            dgvSizeGroupIdList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSizeGroupIdList.Location = new Point(6, 196);
            dgvSizeGroupIdList.Name = "dgvSizeGroupIdList";
            dgvSizeGroupIdList.Size = new Size(206, 150);
            dgvSizeGroupIdList.RowHeadersVisible = false;
            dgvSizeGroupIdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSizeGroupIdList.TabIndex = 7;
            #endregion

            #region chkAutoId
            // 
            // chkAutoId
            // 
            chkAutoId.AutoSize = true;
            chkAutoId.Checked = true;
            chkAutoId.CheckState = CheckState.Checked;
            chkAutoId.Location = new Point(6, 68);
            chkAutoId.Name = "chkAutoId";
            chkAutoId.Size = new Size(198, 17);
            chkAutoId.TabIndex = 6;
            chkAutoId.Text = "Auto generate based on Size List ID";
            chkAutoId.UseVisualStyleBackColor = true;
            chkAutoId.CheckedChanged += ChkAutoId_CheckedChanged;
            #endregion

            #region chkAutoName
            // 
            // chkAutoName
            // 
            chkAutoName.AutoSize = true;
            chkAutoName.Checked = true;
            chkAutoName.CheckState = CheckState.Checked;
            chkAutoName.Location = new Point(6, 153);
            chkAutoName.Name = "chkAutoName";
            chkAutoName.Size = new Size(137, 17);
            chkAutoName.TabIndex = 5;
            chkAutoName.Text = "Same as Size List Name";
            chkAutoName.UseVisualStyleBackColor = true;
            chkAutoName.CheckedChanged += ChkAutoName_CheckedChanged;
            #endregion
            
            #region label2
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 111);
            label2.Name = "label2";
            label2.Size = new Size(69, 13);
            label2.TabIndex = 2;
            label2.Text = "SG List Name";
            #endregion

            #region txtSizeGroupId
            // 
            // txtSizeGroupId
            //
            txtSizeGroupId.Location = new Point(6, 42);
            txtSizeGroupId.Name = "txtSizeGroupId";
            txtSizeGroupId.Size = new Size(100, 20);
            txtSizeGroupId.CharacterCasing = CharacterCasing.Upper;
            txtSizeGroupId.MaxLength = 5;
            txtSizeGroupId.TabIndex = 1;
            txtSizeGroupId.Enabled = false;
            txtSizeGroupId.TextChanged += TxtSizeGroupId_TextChanged;
            #endregion

            #region txtSizeGroupName
            // 
            // txtSizeGroupName
            // 
            txtSizeGroupName.Location = new Point(6, 127);
            txtSizeGroupName.Name = "txtSizeGroupName";
            txtSizeGroupName.Size = new Size(206, 20);
            txtSizeGroupName.TabIndex = 3;
            txtSizeGroupName.Enabled = false;
            #endregion

            #region label1
            // 
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(6, 26);
            label1.Name = "label1";
            label1.Size = new Size(53, 13);
            label1.TabIndex = 0;
            label1.Text = "SG List ID";
            #endregion

            #endregion

            
            host.ClientSize = new Size(w0 + 224, 461);
            host.Controls.Add(grpSizeGroup);

            GetHostControl("dgvExistingCodes").Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GetHostControl("btnAccept").Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            GetHostControl("btnCancel").Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            #region Finalizing
            grpSizeGroup.ResumeLayout(false);
            grpSizeGroup.PerformLayout();
            ((ISupportInitialize)dgvSizeGroupIdList).EndInit();
            #endregion
        }

        private void ParentComponentPostSetting()
        {
            existSizeGroupIds = DataService.GetSizeGroupsBasic();
        }

        private void Host_Load(object sender, EventArgs e)
        {
            dgvSizeGroupIdList.DataSource = existSizeGroupIds.ToList();
            dgvSizeGroupIdList.AutoResizeColumns();
            dgvSizeGroupIdList.AutoResizeRows();
        }

        private void Host_txtListID_TextChanged(object sender, EventArgs e)
        {
            if (chkAutoId.Checked)
            {
                txtSizeGroupId.Text = txtListID.Text;
            }
        }

        private void Host_txtListName_TextChanged(object sender, EventArgs e)
        {
            if (chkAutoName.Checked)
            {
                txtSizeGroupName.Text = txtListName.Text;
            }
        }

        private void ChkAutoId_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoId.Checked)
            {
                txtSizeGroupId.Enabled = false;
                txtSizeGroupId.Text = txtListID.Text;
            }
            else
            {
                txtSizeGroupId.Enabled = true;
                txtSizeGroupId.FocusSelectAll();
            }
        }

        private void ChkAutoName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoName.Checked)
            {
                txtSizeGroupName.Enabled = false;
                txtSizeGroupName.Text = txtListName.Text;
            }
            else
            {
                txtSizeGroupName.Enabled = true;
                txtSizeGroupName.FocusSelectAll();
            }
        }

        private void TxtSizeGroupId_TextChanged(object sender, EventArgs e)
        {
            string input = ((TextBox)sender).Text;

            // Check duplicate ID
            bool duplicateFound =
                existSizeGroupIds.Select(entry => entry.ID).Contains(input);

            // if Auto-Generate ID is enabled and a duplicate was found then generate new ID


            #region Validation Indication
            //if (input != string.Empty)
            //{
            //    if (duplicateFound)
            //    {
            //        inputValidator.ValidID = false;
            //        lblValidID.Text = "• Duplicate ID";
            //        if (input.Length >= txtListID.MaxLength)
            //        {
            //            txtListID.SelectAll();
            //            txtListID.Focus();
            //        }
            //    }
            //    else
            //    {
            //        inputValidator.ValidID = true;
            //        lblValidID.Text = string.Empty;
            //    }
            //}
            //else
            //{
            //    inputValidator.ValidID = false;
            //    lblValidID.Text = string.Empty;
            //}
            #endregion

            if (input != string.Empty)
            {
                IEnumerable<BasicView> bvs = existSizeGroupIds
                    .Where(entry => entry.ID.Contains(input));

                SetListItems(bvs.ToList());
            }
            else
            {
                SetListItems(existSizeGroupIds.ToList());
            }
        }

        private void SetListItems(List<BasicView> list)
        {
            dgvSizeGroupIdList.DataSource = list;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private Control GetHostControl(string key)
        {
            Control[] controls = host.Controls.Find(key, true);

            if (controls.Length > 0)
            {
                return controls[0];
            }
            else
            {
                return null;
            }
        }
    }

}
