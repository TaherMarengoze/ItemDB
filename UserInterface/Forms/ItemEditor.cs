
using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using CoreLibrary.Models;
using CoreLibrary.Operation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using UserService;


namespace UserInterface.Forms
{
    public partial class ItemEditor : Form
    {
        public ItemRawData DraftItemData { get; private set; }

        #region Field Variables
        string imagesReposPath;
        //private bool ImageBrowseMode = false;
        private EntryMode imageDraftMode = EntryMode.View;
        private string draftCatName;
        private List<string> commonNames = new List<string>();
        private List<string> existingImages;
        private List<ItemImage> itemImages = new List<ItemImage>();
        private ItemCheckList checkList;

        private ItemImage browsedImage;
        private ItemImage draftImage;

        private string editItemId;
        //private Item editItem;
        #endregion

        /// <summary>
        /// Constructor for a new Item addition.
        /// </summary>
        public ItemEditor()
        {
            CommonInitialization();
        }

        /// <summary>
        /// Constructor for editing an existing item.
        /// </summary>
        /// <param name="editId">The ID of the item being edited.</param>
        public ItemEditor(string editId)
        {
            CommonInitialization();
            IItem item = Data.GetItem(editId);

            //Set variables
            editItemId = editId;
            commonNames = new List<string>();
            if (item.CommonNames != null)
            {
                commonNames.AddRange(item.CommonNames);
            }

            txtItemId.Text = item.ItemID;
            txtCatId.Text = item.CatID;
            if (txtCatName.ReadOnly == false)
            {
                txtCatName.Text = item.CatName;
            }
            txtNameBase.Text = item.BaseName;
            txtNameDisplay.Text = item.DisplayName;
            txtDescription.Text = item.Description;
            txtUom.Text = item.UoM;

            // Set images ListBox
            lbxNameCommon.DataSource = commonNames;

            if (!string.IsNullOrEmpty(item.Details.SpecsID))
            {
                cboSpecsId.Text = item.Details.SpecsID;
                chkRequiredSpecs.Checked = item.Details.SpecsRequired;
            }

            if (!string.IsNullOrEmpty(item.Details.SizeGroupID))
            {
                cboSizeGroupId.Text = item.Details.SizeGroupID;
                chkRequiredSize.Checked = item.Details.SizeRequired;
            }

            if (!string.IsNullOrEmpty(item.Details.BrandListID))
            {
                cboBrandListId.Text = item.Details.BrandListID;
                chkRequiredBrand.Checked = item.Details.BrandRequired;
            }

            if (!string.IsNullOrEmpty(item.Details.EndsListID))
            {
                cboEndsListId.Text = item.Details.EndsListID;
                chkRequiredEnds.Checked = item.Details.EndsRequired;
            }

            if (item.ImagesFileName != null)
            {
                int i = 0;
                foreach (string imageName in item.ImagesFileName)
                {
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        string filePath = Path.Combine(imagesReposPath, imageName);
                        string defaultName = $"{item.ItemID}-{i++:00}"; ;
                        itemImages.Add(new ItemImage(filePath, defaultName));
                    }
                }
            }

            CheckCommonNamesList();
            UpdateImagesList();
            //lbxImages.DataSource = itemImages;
        }

        private void CommonInitialization()
        {
            InitializeComponent();
            checkList = new ItemCheckList();
            checkList.OnComplete += CheckList_OnComplete;
            checkList.OnIncomplete += CheckList_OnIncomplete;

            existingImages = GlobalsX.reader.GetImageNames().ToList();
            imagesReposPath = GlobalsX.fpp.ImageRepos;
            BindControlsToDatasources();
        }

        private void BindControlsToDatasources()
        {
            // Bind items ID DGV
            //Common.SetDataGridViewDataSource(dgvItemsId, Data.GetAllItemsBrief());
            dgvItemsId.DataSourceResize(Data.GetAllItemsBrief());

            // Bind item categories DGV
            //Common.SetDataGridViewDataSource(dgvCategories, Data.GetCategories());
            dgvCategories.DataSourceResize(Data.GetCategories());

            dgvCategories.Columns[0].DisplayIndex = 2;

            // Bind Specs ID selector combobox
            cboSpecsId.DataSource = ClientService.DataProvider.GetIDs();
            cboSpecsId.SelectedIndex = -1;
            dgvSpecs.DataSource = ClientService.SpecsVMProvider.Brief();

            // Bind Size Groups ID selector combobox
            cboSizeGroupId.DataSource = Data.GetSizeGroupsId();
            cboSizeGroupId.SelectedIndex = -1;
            dgvSizeGroup.DataSource = Data.GetSizeGroupsVO();

            // Bind Brands ID selector combobox
            cboBrandListId.DataSource = Data.GetFieldIds(FieldType.BRAND).ToList();
            cboBrandListId.SelectedIndex = -1;
            dgvBrandsLists.DataSource = Data.GetFieldLists(FieldType.BRAND);

            // Bind Ends List ID selector combobox
            cboEndsListId.DataSource = Data.GetFieldIds(FieldType.ENDS).ToList();
            cboEndsListId.SelectedIndex = -1;
            dgvEndsLists.DataSource = Data.GetFieldLists(FieldType.ENDS);
                //GetEndsInterface();

            // Images ListBox
            lbxImages.DisplayMember = "DraftDisplayName";
        }

        private void CheckList_OnComplete()
        {
            btnAccept.Enabled = true;
        }

        private void CheckList_OnIncomplete()
        {
            btnAccept.Enabled = false;
        }

        private OpenFileDialog ShowImageFileBrowser()
        {
            return
                new OpenFileDialog()
                {
                    Title = "Browse Image File (Joint Photographic Experts Group)",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = "jpg",
                    Filter = "JPEG Image files(*.jpg; *.jpeg) | *.jpg; *.jpeg"
                };
        }

        private void BrowseImages()
        {
            OpenFileDialog imageSelector = ShowImageFileBrowser();
            if (imageSelector.ShowDialog() == DialogResult.OK)
            {
                browsedImage = new ItemImage(imageSelector.FileName);

                // Check if same image was added
                DuplicateImageCheck();
            }
            else
            {
                if (draftImage == null && imageDraftMode == EntryMode.New)
                {
                    ExitImageDrafting();
                }
            }
        }

        private void DuplicateImageCheck()
        {
            if (IsDuplicateImage(browsedImage))
            {
                MessageBox.Show(
                    "The selected image is already added to the list\n\nSelect a different one.",
                    "Duplicate Image");

                // TEST Recursive
                //BrowseImages();
            }
            else
            {
                // Set Draft Image Object to Browsed Image Object
                //if (draftMode == EntryMode.New)
                //{
                //    draftImage = browsedImage;
                //}

                picImage.Image = browsedImage.ImagePreview;
                txtImageName.Text = browsedImage.FileName;

                if (chkAutoAddImage.Checked)
                {
                    btnImageAccept_Click(btnImageAccept, EventArgs.Empty);
                }
                else
                {
                    btnImageAccept.Enabled = true;
                    chkUseDefName.Enabled = true;
                }
            }
        }

        private bool IsDuplicateImage(ItemImage image)
        {
            return itemImages.Count(img => img.ImageFullName == image.ImageFullName) > 0;
        }

        private void EnterImageDrafting(EntryMode mode)
        {
            //ImageBrowseMode = true;
            imageDraftMode = mode;

            btnAddNewImage.Enabled = false;
            btnModifyImage.Enabled = false;
            btnRemoveImage.Enabled = false;

            btnBrowseImage.Enabled = true;
            //btnImageAccept.Enabled = true; // Accept should be available if image selection was valid
            btnImageCancel.Enabled = true;

            txtImageName.Enabled = true;
            chkUseDefName.Enabled = true;
        }

        private void ExitImageDrafting()
        {
            //ImageBrowseMode = false;
            imageDraftMode = EntryMode.View;

            btnAddNewImage.Enabled = true;
            btnModifyImage.Enabled = true;
            btnRemoveImage.Enabled = true;

            btnBrowseImage.Enabled = false;
            btnImageAccept.Enabled = false;
            btnImageCancel.Enabled = false;

            txtImageName.Enabled = false;
            chkUseDefName.Enabled = false;

            // Additional Cleanup
            txtImageName.Clear();
            ClearItemPicturebox();

            lblDuplicateImage.Visible = false;
            picExistingImage.Visible = false;
            picExistingImage.Image = null;
        }

        private void UpdateImagesList()
        {
            lbxImages.DataSource = null;
            lbxImages.DataSource = itemImages;
            lbxImages.DisplayMember = "DraftDisplayName";

            // TEST
            CheckImagesCount();
        }

        private void CheckImagesCount()
        {
            if (lbxImages.Items.Count > 0)
            {
                btnRemoveImage.Enabled = true;
                btnModifyImage.Enabled = true;
            }
            if (lbxImages.Items.Count <= 0)
            {
                btnRemoveImage.Enabled = false;
                btnModifyImage.Enabled = false;
                ClearItemPicturebox();
            }
        }

        private void ClearItemPicturebox()
        {
            picImage.Image = null;
        }

        private void UpdateCommonNamesListbox()
        {
            lbxNameCommon.DataSource = null;
            lbxNameCommon.DataSource = commonNames;
        }

        private void CheckCommonNamesList()
        {
            if (commonNames.Count > 0)
            {
                btnEditCommonName.Enabled = true;
                btnDeleteCommonName.Enabled = true;
            }
            else
            {
                btnEditCommonName.Enabled = false;
                btnDeleteCommonName.Enabled = false;
            }
        }

        private ItemRawData CollectInputData()
        {
            string id = txtItemId.Text;

            ItemRawData inputData = new ItemRawData()
            {
                ItemID = id,
                CatID = txtCatId.Text,
                CatName = txtCatName.Text,
                BaseName = txtNameBase.Text,
                DisplayName = txtNameDisplay.Text,
                CommonNames = commonNames,
                Description = txtDescription.Text,
                //ImagesNames = imagesFileNames,
                UoM = txtUom.Text
            };

            //Process Images file name
            string targetName;
            List<string> imagesNames = new List<string>();
            int i = 0;
            foreach (ItemImage img in itemImages)
            {
                if (string.IsNullOrEmpty(img.CustomName))
                {
                    targetName = $"{id}-{i++:00}";
                }
                else
                {
                    targetName = img.CustomName;
                }

                imagesNames.Add(targetName + ".jpg");

                // Create a Copy Order and add it to the Copy Orders List, if needed
                CopyService.CreateCopyOrder(img.ImageFullName, Path.Combine(imagesReposPath, $"{ targetName }.jpg"));
            }
            inputData.ImagesNames = imagesNames;

            //Create Copy Order to be executed on save
            //MessageBox.Show($"{CopyService.GetOrdersCount()} copy order(s) pending");

            // • Process Item Details

            // Specs
            if (cboSpecsId.SelectedIndex != -1)
            {
                inputData.SpecsID = cboSpecsId.Text;
                inputData.SpecsRequired = chkRequiredSpecs.Checked;
            }

            // Size
            if (cboSizeGroupId.SelectedIndex != -1)
            {
                inputData.SizeGroupID = cboSizeGroupId.Text;
                inputData.SizeRequired = chkRequiredSize.Checked;
            }

            // Brand
            if (cboBrandListId.SelectedIndex != -1)
            {
                inputData.BrandListID = cboBrandListId.Text;
                inputData.BrandRequired = chkRequiredBrand.Checked;
            }

            // Ends
            if (cboEndsListId.SelectedIndex != -1)
            {
                inputData.EndsListID = cboEndsListId.Text;
                inputData.EndsRequired = chkRequiredEnds.Checked;
            }

            return inputData;
        }

        private void CheckRequiredModifierKey(CheckBox requiredField)
        {
            // Reference: https://stackoverflow.com/questions/973721/c-sharp-detecting-if-the-shift-key-is-held-when-opening-a-context-menu
            if (ModifierKeys == Keys.Shift)
            {
                requiredField.Checked = true;
            }
        }

#pragma warning disable IDE1006 // Naming Styles

        private void ItemEditor_Load(object sender, EventArgs e)
        {
            //SetDatasources();
            dgvItemsId.AutoResizeColumns();
            dgvCategories.AutoResizeColumns();
        }

        private void IdTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !(e.KeyChar == (char)8))
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }
        private void NameTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsInvalidCharacter(e.KeyChar) && !(@"-_()./\@").Contains(e.KeyChar))
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
            else
            {

            }
        }

        private static bool IsInvalidCharacter(char chr)
        {
            return
                !char.IsLetterOrDigit(chr) && // Alphanumeric
                !char.IsWhiteSpace(chr) &&    // Space
                chr != (char)8;
        }

        private void EssayTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '<' || e.KeyChar == '>')
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void txtUom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '<' || e.KeyChar == '>')
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }

        private void txtItemId_TextChanged(object sender, EventArgs e)
        {
            string inputId = txtItemId.Text;

            if (inputId != string.Empty)
            {
                checkList.ItemIdGiven = true;
                dgvItemsId.DataSource = Data.GetAllItemsBrief(txtItemId.Text);

                // Check for duplicate
                if (inputId != editItemId && Data.IsDuplicateItemId(inputId))
                {
                    checkList.ItemIdUnique = false;
                    ((TextBox)sender).BackColor = Color.LightPink;
                    lblValidatorItemId.Text = "• Duplicate";
                }
                else
                {
                    checkList.ItemIdUnique = true;
                    ((TextBox)sender).BackColor = SystemColors.Window;
                    lblValidatorItemId.Text = "";
                }
            }
            else
            {
                checkList.ItemIdGiven = false;
                dgvItemsId.DataSource = Data.GetAllItemsBrief();
            }
        }
        private void txtCatId_TextChanged(object sender, EventArgs e)
        {
            string catId = txtCatId.Text;

            if (catId != string.Empty)
            {
                checkList.CatIdGiven = true;
                dgvCategories.DataSource = Data.FilterCategoriesById(catId);

                string catName = Data.GetCategoryName(catId);

                if (catName != null)
                {
                    checkList.CatNameGiven = true;
                    txtCatName.ReadOnly = true;
                    draftCatName = txtCatName.Text;
                    txtCatName.Text = catName;
                }
                else
                {
                    txtCatName.ReadOnly = false;
                    txtCatName.Text = draftCatName;
                }

            }
            else
            {
                checkList.CatIdGiven = false;
                dgvCategories.DataSource = Data.GetCategories();
                txtCatName.ReadOnly = false;
                txtCatName.Text = draftCatName;
            }
        }

        private void txtCatName_TextChanged(object sender, EventArgs e)
        {
            if (txtCatName.ReadOnly == false)
            {
                string name = txtCatName.Text;

                if (name != string.Empty)
                {
                    checkList.CatNameGiven = true;
                    dgvCategories.DataSource = Data.FilterCategoriesByName(name);
                }
                else
                {
                    checkList.CatNameGiven = false;
                    dgvCategories.DataSource = Data.GetCategories();
                }
            }
            else
            {
                dgvCategories.DataSource = Data.GetCategories();
            }
        }
        private void txtCatName_Leave(object sender, EventArgs e)
        {
            if (txtCatName.ReadOnly == false)
            {
                draftCatName = txtCatName.Text;
            }

        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                txtCatId.Text = dgvCategories[1, e.RowIndex].Value.ToString();
            }
        }

        private void dgvSpecs_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count > 0)
            {
                string specsId = (string)dgv.SelectedRows[0].Cells[1].Value;
                dgvSpecsItems.DataSource = //Data.GetSpecsItems(specsId).ToList();
                    ClientService.SpecsManiuplator.GetSpecsItems(specsId)?.ToList();
            }
        }

        private void dgvSpecs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                cboSpecsId.Text = dgvSpecs[1, e.RowIndex].Value.ToString();
                CheckRequiredModifierKey(chkRequiredSpecs);
            }
        }

        private void dgvSpecsItems_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgvSpecs.SelectedRows.Count > 0 && dgv.SelectedRows.Count > 0)
            {
                string specsId = (string)dgvSpecs.SelectedRows[0].Cells[1].Value;
                int specIndex = (int)dgv.SelectedRows[0].Cells[0].Value;
                dgvSpecListEntries.DataSource = //Data.GetSpecListEntries(specsId, specIndex);
                    ClientService.SpecsManiuplator.GetSpecsItemListEntries(specsId, specIndex)?.ToList();
            }
        }

        private void dgvSizeGroup_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSizeGroup.SelectedRows.Count > 0)
            {
                string sizeId = dgvSizeGroup.SelectedRows[0].Cells["DefaultListID"].Value.ToString();
                lbxSizeListEntries.DataSource = //DataService.SizeListGetEntries(sizeId);
                    Data.FieldListGetEntries(FieldType.SIZE, sizeId);

                SizeGroupView sgv = dgvSizeGroup.SelectedRows[0].DataBoundItem as SizeGroupView;
                cboAltListSelector.DataSource = sgv.AltIdList;
            }
        }
        private void dgvSizeGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                cboSizeGroupId.Text = dgvSizeGroup[1, e.RowIndex].Value.ToString();
                CheckRequiredModifierKey(chkRequiredSize);
            }
        }

        private void dgvBrandsLists_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBrandsLists.SelectedRows.Count > 0)
            {
                BasicListView blv = dgvBrandsLists.SelectedRows[0].DataBoundItem as BasicListView;
                lbxBrandListEntries.DataSource = blv.List;
            }
        }
        private void dgvBrandsLists_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                cboBrandListId.Text = dgvBrandsLists[1, e.RowIndex].Value.ToString();
                CheckRequiredModifierKey(chkRequiredBrand);
            }
        }

        private void dgvEndsLists_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEndsLists.SelectedRows.Count > 0)
            {
                BasicListView blv = dgvEndsLists.SelectedRows[0].DataBoundItem as BasicListView;
                lbxEndsListEntries.DataSource = blv.List;
            }
        }
        private void dgvEndsLists_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                cboEndsListId.Text = dgvEndsLists[1, e.RowIndex].Value.ToString();
                CheckRequiredModifierKey(chkRequiredEnds);
            }
        }

        private void btnUseBaseName_Click(object sender, EventArgs e)
        {
            txtNameDisplay.Text = txtNameBase.Text;
            txtNameDisplay.SelectAll();
            txtNameDisplay.Focus();
        }

        private void txtCommonNameEntry_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            btnAddCommonName.Enabled = !string.IsNullOrEmpty(text.Trim());
        }
        private void txtCommonNameEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnAddCommonName.Enabled)
            {
                btnAddCommonName_Click(this, new EventArgs());
            }
        }

        private void btnAddCommonName_Click(object sender, EventArgs e)
        {
            string newName = txtCommonNameEntry.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Name can not be empty.",
                    "No Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check for duplicate entry
            if (commonNames.Where(s => s.ToLower() == newName.ToLower()).Count() > 0)
            {
                MessageBox.Show("Name already exists, please, enter a new one.",
                    "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCommonNameEntry.SelectAll();
                txtCommonNameEntry.Focus();
            }
            else
            {
                //Add new name to the list
                commonNames.Add(newName.Trim());

                //Update list
                UpdateCommonNamesListbox();

                //Clear Textbox
                txtCommonNameEntry.Clear();

                //Check if list is empty or not
                CheckCommonNamesList();
            }
        }
        private void btnEditCommonName_Click(object sender, EventArgs e)
        {
            if (lbxNameCommon.SelectedItem == null)
                return;

            string editName = (string)lbxNameCommon.SelectedItem;

            ValueEdit valueEditor = new ValueEdit(editName);
            if (valueEditor.ShowDialog() == DialogResult.OK)
            {
                //Get the index of the old value
                int idx = commonNames.FindIndex(s => s == editName);

                //Update the value to the new one
                commonNames[idx] = valueEditor.NewValue;

                //Update list
                UpdateCommonNamesListbox();
            }
        }
        private void btnDeleteCommonName_Click(object sender, EventArgs e)
        {
            if (lbxNameCommon.SelectedItem == null)
                return;

            string deleteName = (string)lbxNameCommon.SelectedItem;

            //Remove selected name from the list
            commonNames.Remove(deleteName);

            //Update list
            UpdateCommonNamesListbox();

            //Check if list is empty or not
            CheckCommonNamesList();
        }

        private void lbxNameCommon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && btnDeleteCommonName.Enabled)
            {
                btnDeleteCommonName_Click(this, new EventArgs());
            }
        }
        private void lbxNameCommon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = ((ListBox)sender).IndexFromPoint(e.Location);

            if (btnEditCommonName.Enabled == false)
                return;

            if (index != ListBox.NoMatches)
                btnEditCommonName_Click(this, new EventArgs());
        }

        private void btnClearDescription_Click(object sender, EventArgs e)
        {
            txtDescription.Clear();
        }

        private void btnUnassignSpecs_Click(object sender, EventArgs e)
        {
            cboSpecsId.SelectedIndex = -1;
            chkRequiredSpecs.Checked = false;
        }
        private void btnUnassignSize_Click(object sender, EventArgs e)
        {
            cboSizeGroupId.SelectedIndex = -1;
            chkRequiredSize.Checked = false;
        }
        private void btnUnassignBrand_Click(object sender, EventArgs e)
        {
            cboBrandListId.SelectedIndex = -1;
            chkRequiredBrand.Checked = false;
        }
        private void btnUnassignEnds_Click(object sender, EventArgs e)
        {
            cboEndsListId.SelectedIndex = -1;
            chkRequiredEnds.Checked = false;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DraftItemData = CollectInputData();
        }

        private void txtNameBase_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((TextBox)sender).Text.Trim()))
            {
                checkList.BaseNameGiven = true;
            }
            else
            {
                checkList.BaseNameGiven = false;
            }
        }
        private void txtNameDisplay_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Trim() != string.Empty)
            {
                checkList.DisplayNameGiven = true;
            }
            else
            {
                checkList.DisplayNameGiven = false;
            }
        }

        private void txtUom_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Trim() != string.Empty)
            {
                checkList.UomGiven = true;
            }
            else
            {
                checkList.UomGiven = false;
            }
        }

        private void cboAltListSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAltListSelector.SelectedIndex != -1)
            {
                string sizeId = cboAltListSelector.Text;
                lbxAltSizeListEntries.DataSource = //DataService.SizeListGetEntries(sizeId);
                    Data.FieldListGetEntries(FieldType.SIZE, sizeId);
            }
            else
            {
                lbxAltSizeListEntries.DataSource = null;
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            BrowseImages();
        }

        private void btnImageAccept_Click(object sender, EventArgs e)
        {
            string inputName = txtImageName.Text;
            //draftImage.Modify(browsedImage);

            switch (imageDraftMode)
            {
                case EntryMode.New:

                    draftImage = browsedImage;
                    if (chkUseDefName.Checked == false && inputName.Trim() != string.Empty)
                    {
                        draftImage.CustomName = inputName.Trim();
                    }

                    itemImages.Add(draftImage);
                    //itemImages.Add(browsedImage);
                    break;

                case EntryMode.Edit:


                    // Modify edit Image Object
                    if (browsedImage != null)
                    {
                        draftImage.Modify(browsedImage);
                    }

                    if (chkUseDefName.Checked == false && inputName.Trim() != string.Empty)
                    {
                        draftImage.CustomName = inputName.Trim();
                    }

                    break;

                default:
                    break;
            }

            UpdateImagesList();
            ExitImageDrafting();

            // •• Selection •• //
            lbxImages.Text = draftImage.DraftDisplayName;//lbxImages.Text = browsedImage.DraftDisplayName;
            if (lbxImages.Items.Count == 1)
            {
                lbxImages_SelectedIndexChanged(sender, e);
            }

            // Null Image Object Reference
            draftImage = null;//browsedImage = null;
            browsedImage = null;
        }

        private void btnAddNewImage_Click(object sender, EventArgs e)
        {
            EnterImageDrafting(EntryMode.New);
            BrowseImages();
        }

        private void btnEditImage_Click(object sender, EventArgs e)
        {
            object selectedImage = lbxImages.SelectedItem;
            if (selectedImage != null)
            {
                EnterImageDrafting(EntryMode.Edit);
                draftImage = (ItemImage)selectedImage;

                // Update Image Previewer
                picImage.Image = draftImage.ImagePreview;
                txtImageName.Text = draftImage.FileName;

                if (!string.IsNullOrEmpty(draftImage.CustomName))
                {
                    chkUseDefName.Checked = false;
                }
                else
                {
                    chkUseDefName.Checked = true;
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            object selectedImage = lbxImages.SelectedItem;
            if (selectedImage != null)
            {
                //ItemImage image = (ItemImage)selectedImage;
                itemImages.Remove((ItemImage)selectedImage);

                // Save ListBox selection
                int selIdx = Common.SaveListboxSelection(lbxImages);

                UpdateImagesList();

                // Restore ListBox selection
                Common.RestoreListboxSelection(lbxImages, selIdx);

                // Disable Edit & Remove buttons if no image remains
                //CheckImagesCount();
            }
        }

        private void btnCancelAddImage_Click(object sender, EventArgs e)
        {
            ExitImageDrafting();

            // Null Image Object Reference
            draftImage = null; //browsedImage = null;
        }

        private void chkAutoAddImage_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAutoAddImage.Checked)
            //{
            //    btnAddImage.Enabled = false;
            //}
            //else
            //{
            //    btnAddImage.Enabled = true;
            //}
        }

        private void tsmiSample1_Click(object sender, EventArgs e)
        {
            txtItemId.Text = "TEST1";
            txtCatId.Text = "TSCAT";
            txtCatName.Text = "Test Category";
            txtNameBase.Text = "Test Item Base Name";
            txtNameDisplay.Text = "Test Item Display Name";
            txtUom.Text = "each";

            tabDataSections.SelectedTab = tabDataSections.TabPages[1];

            chkAutoAddImage.Checked = false;
            //btnAddNewImage.PerformClick();
        }

        private void chkUseDefName_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.Checked)
            {
                txtImageName.ReadOnly = true;
                lblDuplicateImage.Visible = false;
                picExistingImage.Visible = false;
                picExistingImage.Image = null;
                btnImageAccept.Enabled = true;
            }
            else
            {
                txtImageName.ReadOnly = false;
                txtImageName.SelectAll();
                txtImageName.Focus();
                txtImageName_TextChanged(sender, e);
            }
        }

        private void txtImageName_TextChanged(object sender, EventArgs e)
        {
            string inputName = txtImageName.Text.Trim();
            if (txtImageName.ReadOnly == false && inputName != string.Empty)
            {
                if (existingImages.Count(name => name.ToUpper() == inputName.ToUpper()) > 0)
                {
                    btnImageAccept.Enabled = false;
                    lblDuplicateImage.Visible = true;
                    string temp1 = Path.Combine(imagesReposPath, inputName + ".jpg");
                    string temp2 = Path.Combine(imagesReposPath, inputName + ".jpeg");
                    string temp = File.Exists(temp1) ? temp1 : temp2;
                    picExistingImage.Visible = true;
                    if (File.Exists(temp))
                    {
                        picExistingImage.Image = Image.FromFile(temp);
                    }
                }
                else
                {
                    btnImageAccept.Enabled = true;
                    lblDuplicateImage.Visible = false;
                    picExistingImage.Visible = false;
                    picExistingImage.Image = null;
                }
            }
            else
            {
                btnImageAccept.Enabled = false;
                lblDuplicateImage.Visible = false;
                picExistingImage.Visible = false;
                picExistingImage.Image = null;
            }
        }

        private void lbxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxImages.DataSource != null && imageDraftMode == EntryMode.View)
            {
                ItemImage selectedImage = (ItemImage)lbxImages.SelectedItem;
                picImage.Image = selectedImage.ImagePreview;
            }
        }

        private void cboSpecsId_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkRequiredSpecs.Enabled = ((ComboBox)sender).SelectedIndex != -1;
        }

        private void cboSizeGroupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkRequiredSize.Enabled = ((ComboBox)sender).SelectedIndex != -1;
        }

        private void cboBrandListId_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkRequiredBrand.Enabled = ((ComboBox)sender).SelectedIndex != -1;
        }

        private void cboEndsListId_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkRequiredEnds.Enabled = ((ComboBox)sender).SelectedIndex != -1;
        }

        private void picExistingImage_MouseEnter(object sender, EventArgs e)
        {
            if (picExistingImage.Image != null)
            {
                picFloating.Visible = true;
                picFloating.Image = picExistingImage.Image;
            }
        }

        private void picExistingImage_MouseLeave(object sender, EventArgs e)
        {
            picFloating.Visible = false;
            picFloating.Image = null;
        }

        private void picExistingImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (picFloating.Image != null)
            {
                int x = groupBox7.Location.X + picExistingImage.Location.X + e.X + Cursor.Size.Width;
                int y = groupBox7.Location.Y + picExistingImage.Location.Y + e.Y + base.Cursor.Size.Height;

                int boundsX = tabDataSections.Size.Width - tabDataSections.Location.X;
                int boundsY = tabDataSections.Size.Height - tabDataSections.Location.Y;

                int imgBoundsX = x + picFloating.Size.Width;
                int imgBoundsY = y + picFloating.Size.Height;

                lblTest_FormSize.Text = $"Container Bounds: { boundsX },{ boundsY }";
                lblTest_FloatingLocation.Text = $"Image Location: { x },{ y }";
                lblTest_FloatingBounds.Text = $"Image Bounds: { imgBoundsX }, { imgBoundsY }";

                if (imgBoundsX > boundsX - 6)
                {
                    x = x - picFloating.Size.Width - Cursor.Size.Width;
                }

                if (imgBoundsY > boundsY - 6)
                {
                    y = y - picFloating.Size.Height - Cursor.Size.Height;
                }

                picFloating.Location = new Point(x, y);
            }

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            // Reset state(s)
            imageDraftMode = EntryMode.View;

            // Clear Objects
            commonNames.Clear();
            itemImages.Clear();

            // Reset Controls
            txtItemId.Clear();
            txtCatId.Clear();
            txtCatName.Clear();
            txtNameBase.Clear();
            txtNameDisplay.Clear();
            txtCommonNameEntry.Clear();
            txtDescription.Clear();
            txtUom.Clear();

            lbxNameCommon.DataSource = null;
            lbxImages.DataSource = null;

            cboSpecsId.SelectedIndex = -1;
            cboSizeGroupId.SelectedIndex = -1;
            cboBrandListId.SelectedIndex = -1;
            cboEndsListId.SelectedIndex = -1;

            chkAutoAddImage.Checked = true;
            chkUseDefName.Checked = true;
            chkRequiredSpecs.Checked = false;
            chkRequiredSize.Checked = false;
            chkRequiredBrand.Checked = false;
            chkRequiredEnds.Checked = false;

            ExitImageDrafting();

            // Reset Views and Selections
            tabDataSections.SelectedTab = tabDataSections.TabPages[0];
            Common.SelectDataGridViewFirstRow(dgvItemsId);
            Common.SelectDataGridViewFirstRow(dgvCategories);
            Common.SelectDataGridViewFirstRow(dgvSpecs);
            Common.SelectDataGridViewFirstRow(dgvSizeGroup);
            Common.SelectDataGridViewFirstRow(dgvBrandsLists);
            Common.SelectDataGridViewFirstRow(dgvEndsLists);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSearchSpecs_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchSizes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchBrands_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchEnds_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvField_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Reference: https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/add-tooltips-to-individual-cells-in-a-wf-datagridview-control?view=netframeworkdesktop-4.8

            // ((DataGridView)sender).Columns["Rating"].Index)
            if (e.ColumnIndex == 0 && e.Value != null)
            {
                DataGridViewCell cell = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];

                cell.ToolTipText = "Click to assign the ID to the field.\n\n(Hold SHIFT key to make it a required field)";
            }
        }

        private void tsmiSpecs_Click(object sender, EventArgs e)
        {

        }

        private void tsmiSizeList_Click(object sender, EventArgs e)
        {

            var adder = //new SizeGroupAdder(FieldType.SIZE);
                new FieldListAdderExtendable(FieldType.SIZE, out SizeGroupExt groupExt);

            if (adder.ShowDialog() == DialogResult.OK)
            {
                Data.AddFieldList(FieldType.SIZE, adder.FieldListItem);
                Data.AddSizeGroup(new SizeGroup()
                {
                    ID = groupExt.ListData.ID,
                    Name = groupExt.ListData.Name,
                    DefaultListID = adder.FieldListItem.ID
                });
            }

            UpdateSizeGroupUI();
        }

        private void tsmiBrandsList_Click(object sender, EventArgs e)
        {
            FieldType fieldType = FieldType.BRAND;
            QuickAddField(fieldType);
            UpdateFieldUI(fieldType, cboBrandListId, dgvBrandsLists);
        }

        private void tsmiEndsList_Click(object sender, EventArgs e)
        {
            FieldType fieldType = FieldType.ENDS;
            QuickAddField(fieldType);
            UpdateFieldUI(fieldType, cboEndsListId, dgvEndsLists);
        }

        void QuickAddField(FieldType fieldType)
        {
            var adder = //new FieldListAdder(fieldType);
                new FieldListAdderExtendable(fieldType);

            if (adder.ShowDialog() == DialogResult.OK)
            {
                Data.AddFieldList(fieldType, adder.FieldListItem);
            }
        }

        private void UpdateSizeGroupUI()
        {
            cboSizeGroupId.DataSource = null;
            dgvSizeGroup.DataSource = null;
            cboSizeGroupId.DataSource = Data.GetSizeGroupsId();
            dgvSizeGroup.DataSource = Data.GetSizeGroupsVO();
        }

        private void UpdateFieldUI(FieldType fieldType, ComboBox fieldSelector, DataGridView fieldDataView)
        {
            fieldSelector.DataSource = null;
            fieldDataView.DataSource = null;
            fieldSelector.DataSource = Data.GetFieldIds(fieldType).ToList();
            fieldDataView.DataSource = Data.GetFieldLists(fieldType);
        }

#pragma warning restore IDE1006 // Naming Styles
    }
}