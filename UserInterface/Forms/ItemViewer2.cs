using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Controllers;
using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Models;
using CoreLibrary.Operation;
using Interfaces.Models;
using UserService;

namespace UserInterface.Forms
{
    public partial class ItemViewer2 : Form
    {
        //List<Item> items;
        private bool skipFilter = false;
        private int itemSelectionIndex;
        private ToolTip helperTip = new ToolTip();

        private ItemController uiController = new ItemController();
        private bool ignoreDataGridViewSelectionFlush;
        private bool prevFilterHasItems = true;

        public ItemViewer2()
        {
            InitializeComponent();

            // new code
            //uiController = new ItemController();
            SubscribeControllerEvents();

            // Reference: https://stackoverflow.com/questions/4941766/picturebox-tooltip-changing-c-sharp
            // The ToolTip setting. You can do this as many times as you want
            helperTip.SetToolTip(picImage, "Right-Click for additional options.\n\nDouble-Click to enlarge image.");

            // Double buffering for DGV to prevent slow/flickering scroll
            dgvItems.DoubleBuffer(true);
        }

        #region Controller Event Responses
        private void SubscribeControllerEvents()
        {
            uiController.OnLoad += UiController_OnLoad;
            uiController.OnFilter += UiController_OnFilter;
            uiController.OnSelect += UiController_OnSelect;
        }

        private void UiController_OnLoad(object sender, LoadEventArgs e)
        {
            string nameOfCatID = nameof(IItemCategory.CatID);

            dgvItems.DataSource = e.GenericViewList;

            DataGridViewArrangeColumns(dgvItems,
                "ID",
                "DisplayName",
                "SpecsID",
                "SizeGroupID",
                "BrandsID",
                "EndsID",
                "CatName",
                "CatID");

            HideItemsDataGridViewColumn(nameOfCatID);

            UpdateStatusBar(e.Count);

            cboFilterCategory.DisplayMember = nameof(IItemCategory.CatName);

            cboFilterCategory.ValueMember = nameOfCatID;

            // bind item categories filter combo-box
            cboFilterCategory.DataSource =
                uiController.ListItemCategories(true);

            EnableUI();

            // auto resize items' DGV rows and columns
            dgvItems.AutoResizeColumns();
            dgvItems.AutoResizeRows();
        }

        private void UiController_OnFilter(object sender, LoadEventArgs<Modeling.ViewModels.Item.GenericView> e)
        {
            bool filterContainItems = e.Count > 0;

            ignoreDataGridViewSelectionFlush = filterContainItems;
            dgvItems.DataSourceResize(e.ViewList);

            if (filterContainItems)
            {
                prevFilterHasItems = true;
                lblNoItems.Visible = false;
                grpData.Enabled = true;
            }
            else
            {
                prevFilterHasItems = false;
                lblNoItems.Visible = true;
                grpData.Enabled = false;
            }

            UpdateStatusBar(e.Count);
        }

        private void UiController_OnSelect(object sender, SelectEventArgs<IItem> e)
        {
            DisplayItemInfo(e.Selected);
        }
        #endregion

        private void HideItemsDataGridViewColumn(string columnName)
        {
            if (dgvItems.Columns.Contains(columnName))
                dgvItems.Columns[columnName].Visible = false;
        }

        /// <summary>
        /// Updates the status bar with the items info.
        /// </summary>
        /// <param name="filteredCount">The number of items currently displayed.</param>
        private void UpdateStatusBar(int filteredCount)
        {
            int totalCount = uiController.TotalCount;

            tslblItemsCount.Text = $"Items: { totalCount }";
            tslblShownItemsCount.Text = $"Shown: { filteredCount }";

            tslblShownItemsCount.ForeColor =
                filteredCount > 0 ? filteredCount == totalCount ?
                    Color.Black : Color.Blue : Color.Red;
        }

        #region File Management
        private void SaveToSource()
        {
            Data.Save(ContextEntity.Items);
        }
        #endregion 

        private void EnableUI()
        {
            grpSearch.Enabled = true;
            grpData.Enabled = true;
            EnableDisableModifyButtons(true);
        }

        /// <summary>
        /// Enables or disable the modify (<i>Add, Edit and Remove</i>) buttons.
        /// </summary>
        /// <param name="enable">True to enable the buttons, otherwise, false.</param>
        private void EnableDisableModifyButtons(bool enable)
        {
            btnAdd.Enabled = enable;
            btnEdit.Enabled = enable;
            btnDelete.Enabled = enable;
        }

        private void DisplayItemInfo(IItem item)
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

            // collect filtering criteria values
            string id = txtFilterId.Text;
            string name = txtFilterName.Text;
            string catId = GetSelectedCategoryID();
            bool? image = null;

            if (rdoFilterHasImage.Checked)
            {
                image = true;
            }

            if (rdoFilterNoImage.Checked)
            {
                image = false;
            }

            if (catId == "*")
                dgvItems.Columns["CatName"].Visible = true;
            else
                dgvItems.Columns["CatName"].Visible = false;

            uiController.Filter(id, name, catId, image);
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

        private void EnableEditUI()
        {
            EnableOrDisableEditUI(true);
        }

        private void DisableEditUI()
        {
            EnableOrDisableEditUI(false);
        }

        private void EnableOrDisableEditUI(bool enable)
        {
            btnEdit.Enabled = enable;
            tsmiEditItem.Enabled = enable;
        }

        private void DataGridViewArrangeColumns(DataGridView dgv, params string[] columnNames)
        {
            int orderIndex = 0;

            for (int i = 0; i < columnNames.Length; i++)
            {
                if (dgv.Columns.Contains(columnNames[i]))
                {
                    dgv.Columns[columnNames[i]].DisplayIndex = orderIndex++;
                }
            }
        }

        #region Getters
        private string GetSelectedCategoryID()
        {
            return cboFilterCategory.SelectedValue.ToString();
        }
        #endregion

#pragma warning disable IDE1006 // Naming Styles

        #region Event Generated

        // Form
        private void ItemEditor_Load(object sender, EventArgs e)
        {
            uiController.Load();
        }

        // DataGridView
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (ignoreDataGridViewSelectionFlush && prevFilterHasItems)
            {
                ignoreDataGridViewSelectionFlush = false;
                return;
            }

            DataGridView dgv = sender as DataGridView;

            if (dgv.SelectedRows.Count > 0)
            {
                string id = (string)dgv.SelectedRows[0].Cells["ID"].Value;
                uiController.Select(id);
                EnableEditUI();
            }
            else
            {
                ClearItemData();
                DisableEditUI();
            }
        }

        private void dgvItems_DataSourceChanged(object sender, EventArgs e)
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
                txtFilterCatId.Text = GetSelectedCategoryID();
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

        // Modify Actions
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Hide();
            new ItemEditor2(uiController).ShowDialog();
            //ItemEditor itemEditor = new ItemEditor();
            //if (itemEditor.ShowDialog() == DialogResult.OK)
            //{
            //    Data.AddNewItem(itemEditor.DraftItemData);
            //}
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

        private void picImage_MouseHover(object sender, EventArgs e)
        {

        }

        // Main Menu
        private void tsmiSaveXmlFile_Click(object sender, EventArgs e)
        {
            SaveToSource();
            CopyService.ExecutePendingCopyOrders();
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

        private void tsmiEditItem_Click(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        #endregion

#pragma warning restore IDE1006 // Naming Styles
    }
}
