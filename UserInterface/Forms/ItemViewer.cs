
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using CoreLibrary.Models;
using CoreLibrary.Operation;
using UserService;


namespace UserInterface.Forms
{
    public partial class ItemViewer : Form
    {
        //List<Item> items;
        private bool skipFilter = false;
        private int itemSelectionIndex;
        private ToolTip helperTip = new ToolTip();

        public ItemViewer()
        {
            InitializeComponent();

            // Reference: https://stackoverflow.com/questions/4941766/picturebox-tooltip-changing-c-sharp
            // The ToolTip setting. You can do this as many times as you want
            helperTip.SetToolTip(picImage, "Right-Click for additional options.\n\nDouble-Click to enlarge image.");

            // Double buffering for DGV to prevent slow/flickering scroll
            dgvItems.DoubleBuffer(true);
        }

        #region File Management
        private void SaveToSource()
        {
            //AppFactory.context.Save(ContextEntity.Items);
            Data.Save(ContextEntity.Items);
        }
        #endregion

        private void PostLoading()
        {
            dgvItems.DataSource = Data.GetAllItemsVO();
            dgvItems.Columns["CatID"].Visible = false;

            // Update Status bar
            //UpdateStatusBar();

            cboFilterCategory.DataSource = Data.GetAllCategories();
            cboFilterCategory.DisplayMember = "Name"; //"CatName";
            cboFilterCategory.ValueMember = "ID"; //"CatID";

            EnableUI();

            // Auto Resize Rows and Columns
            dgvItems.AutoResizeColumns();
            dgvItems.AutoResizeRows();
        }

        private void UpdateStatusBar()
        {
            int items = Data.GetItemsCount();
            int filtered = dgvItems.RowCount;

            tslblItemsCount.Text = $"Items: { items }";
            tslblShownItemsCount.Text = $"Shown: { filtered }";

            if (filtered > 0)
            {
                if (filtered == items)
                {
                    tslblShownItemsCount.ForeColor = Color.Black;
                }
                else
                {
                    tslblShownItemsCount.ForeColor = Color.Blue;
                }
            }
            else
            {
                tslblShownItemsCount.ForeColor = Color.Red;
            }

        }

        private void EnableUI()
        {
            grpSearch.Enabled = true;
            grpData.Enabled = true;
            EnableDisableModifyButtons(true);
        }

        /// <summary>
        /// Enables or disable the modify (Add, Edit and Remove) buttons.
        /// </summary>
        /// <param name="enable">True to enable the buttons, otherwise, false.</param>
        private void EnableDisableModifyButtons(bool enable)
        {
            btnAdd.Enabled = enable;
            btnEdit.Enabled = enable;
            btnDelete.Enabled = enable;
        }

        private void ShowItemData(IItem item)
        {
            txtItemId.Text = item.ItemID;

            txtCatId.Text = item.CatID;
            txtCatName.Text = item.CatName;

            txtNameBase.Text = item.BaseName;
            txtNameDisplay.Text = item.DisplayName;

            if (item.CommonNames == null)
            {
                lbxNameCommon.SelectionMode = SelectionMode.One;
            }
            else
            {
                lbxNameCommon.SelectionMode = SelectionMode.None;
            }
            lbxNameCommon.DataSource = item.CommonNames;
            txtDescription.Text = item.Description;

            txtSpecsId.Text = item.Details.SpecsID;
            txtSizeId.Text = item.Details.SizeGroupID;
            txtBrandId.Text = item.Details.BrandListID;
            txtEndsId.Text = item.Details.EndsListID;

            chkRequiredSpecs.Checked = item.Details.SpecsRequired;
            chkRequiredSize.Checked = item.Details.SizeRequired;
            chkRequiredBrand.Checked = item.Details.BrandRequired;
            chkRequiredEnds.Checked = item.Details.EndsRequired;

            txtUom.Text = item.UoM;

            lbxImages.DataSource = item.ImagesFileName;
        }

        private void ClearItemData()
        {
            txtItemId.Clear();

            txtCatId.Clear();
            txtCatName.Clear();

            txtNameBase.Clear();
            txtNameDisplay.Clear();
            lbxNameCommon.SelectionMode = SelectionMode.One;
            lbxNameCommon.DataSource = null;
            lbxNameCommon.SelectionMode = SelectionMode.None;
            txtDescription.Clear();

            txtSpecsId.Clear();
            txtSizeId.Clear();
            txtBrandId.Clear();
            txtEndsId.Clear();

            chkRequiredSpecs.Checked = false;
            chkRequiredSize.Checked = false;
            chkRequiredBrand.Checked = false;
            chkRequiredEnds.Checked = false;

            txtUom.Clear();

            lbxImages.DataSource = null;

            grpData.Enabled = false;
        }

        private void ApplyItemFilter()
        {
            if (skipFilter)
                return;

            string id = txtFilterId.Text;
            string name = txtFilterName.Text;
            string cat = GetSelectedCatId();
            bool? image = null;

            if (rdoFilterHasImage.Checked)
            {
                image = true;
            }

            if (rdoFilterNoImage.Checked)
            {
                image = false;
            }

            if (cat == "*")
            {
                dgvItems.Columns["Category"].Visible = true;
            }
            else
            {
                dgvItems.Columns["Category"].Visible = false;
            }

            List<ItemVO> filteredItems =
                Data.GetFilteredItemsView(id, name, image, cat);

            //dgvItems.DataSource = filteredItems;
            dgvItems.DataSourceResize(filteredItems);
            if (filteredItems.Count <= 0 /* and in filter mode */)
            {
                lblNoItems.Visible = true;
                grpData.Enabled = false;
            }
            else
            {
                lblNoItems.Visible = false;
                grpData.Enabled = true;
            }
            
            //UpdateStatusBar();
        }

        private void DeleteItem()
        {
            //Get the selected item ID
            if (dgvItems.SelectedRows.Count <= 0)
                return;

            DataGridViewRow row = dgvItems.SelectedRows[0];
            string id = row.Cells[0].Value.ToString();

            //Save Selection Position
            itemSelectionIndex = Common.GetDataGridViewSelectionIndex(dgvItems);

            List<ItemVO> modifiedItemList = Data.DeleteItem(id);

            if (modifiedItemList.Count > 0)
            {
                dgvItems.DataSource = modifiedItemList;
                ApplyItemFilter();

                // Restore Selection Position
                Common.RestoreDataGridViewSelection(dgvItems, itemSelectionIndex);

                //btnDelete.Enabled = true;
            }
            else
            {
                dgvItems.DataSource = null;
                ClearItemData();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }

            //UpdateStatusBar();
        }

        private void SetAbilityItemEditUI(bool enable)
        {
            btnEdit.Enabled = enable;
            tsmiEditItem.Enabled = enable;
        }

        #region Getters
        private string GetSelectedCatId()
        {
            return ((ItemCategory)cboFilterCategory.SelectedItem).ID;
        }
        #endregion

#pragma warning disable IDE1006 // Naming Styles
        #region Event Generated

        // Form
        private void ItemEditor_Load(object sender, EventArgs e)
        {
            PostLoading();
        }

        // Main Menu
        private void tsmiSaveXmlFile_Click(object sender, EventArgs e)
        {
            SaveToSource();
            CopyService.ExecutePendingCopyOrders();
        }

        // DataGridView
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count <= 0)
            {
                SetAbilityItemEditUI(enable: false);
                ClearItemData();
            }
            else
            {
                SetAbilityItemEditUI(enable: true);
                string id = (string)dgv.SelectedRows[0].Cells[0].Value;
                ShowItemData(Data.GetItem(id));
            }
        }

        // Filtering
        private void txtFilterId_TextChanged(object sender, EventArgs e)
        {
            ApplyItemFilter();
        }
        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            ApplyItemFilter();
        }
        private void rdoFilterAnyImage_CheckedChanged(object sender, EventArgs e)
        {
            ApplyItemFilter();
        }
        private void rdoFilterHasImage_CheckedChanged(object sender, EventArgs e)
        {
            ApplyItemFilter();
        }
        private void rdoFilterNoImage_CheckedChanged(object sender, EventArgs e)
        {
            ApplyItemFilter();
        }
        private void cboFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilterCategory.SelectedIndex != -1)
            {
                txtFilterCatId.Text = GetSelectedCatId();
                ApplyItemFilter();
            }
        }
        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            skipFilter = true;
            txtFilterId.Clear();
            txtFilterName.Clear();
            rdoFilterAnyImage.Checked = true;
            cboFilterCategory.SelectedIndex = 0;
            skipFilter = false;
            ApplyItemFilter();
        }

        // Modify
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Hide();
            ItemEditor itemEditor = new ItemEditor();
            if (itemEditor.ShowDialog() == DialogResult.OK)
            {
                Data.AddNewItem(itemEditor.DraftItemData);
                PostLoading();
            }
            Show();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count <= 0)
                return;

            DataGridViewRow row = dgvItems.SelectedRows[0];
            string id = row.Cells[0].Value.ToString();

            Hide();
            ItemEditor itemEditor = new ItemEditor(id);
            if (itemEditor.ShowDialog() == DialogResult.OK)
            {
                // Modify edited item with new one
                Data.ModifyItem(id, itemEditor.DraftItemData);
                PostLoading();
            }
            Show();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void lbxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxImages.SelectedItem != null)
            {
                string imageFile = Path.Combine(GlobalsX.fpp.ImageRepos, lbxImages.Text);

                if (File.Exists(imageFile))
                {
                    picImage.Image = Image.FromFile(imageFile);
                }
                else
                {
                    picImage.Image = null;
                }
            }
            else
            {
                picImage.Image = null;
            }
        }

        private void tsmiEnlargeImage_Click(object sender, EventArgs e)
        {
            ImagePreviewer ip = new ImagePreviewer() { ItemImage = picImage.Image };

            ip.ShowDialog();
        }

        private void tsmiCloseForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiExitApp_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void dgvItems_DataSourceChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void picImage_MouseHover(object sender, EventArgs e)
        {

        }

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                btnEdit.PerformClick();
            }
        }

        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bool isFieldIdColumn =
                e.ColumnIndex == dgvItems.Columns["SpecsID"].Index ||
                e.ColumnIndex == dgvItems.Columns["SizeGroupID"].Index ||
                e.ColumnIndex == dgvItems.Columns["BrandsID"].Index ||
                e.ColumnIndex == dgvItems.Columns["EndsID"].Index;

            if (isFieldIdColumn && e.Value != null)
            {
                if (e.Value.ToString().Contains('*'))
                {
                    DataGridViewCell cell = dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Style.ForeColor = Color.Red;
                    cell.ToolTipText = "Required";
                }
            }
        }

        private void tsmiEditItem_Click(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        #endregion

#pragma warning restore IDE1006 // Naming Styles
    }
}
