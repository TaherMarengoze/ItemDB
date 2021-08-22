
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;


namespace UserInterface.Forms
{
    using CoreLibrary;
    using CoreLibrary.Models;
    using CoreLibrary.Interfaces;
    using CoreLibrary.Enums;
    using UserService;

    public partial class FieldEditor : Form
    {
        private FieldType fieldType;
        private ContextEntity entity;

        private ISchema schema;
        private ObservableCollection<string> listEntries;


        public FieldEditor(FieldType field)
        {
            InitializeComponent();
            fieldType = field;
            schema = new FieldSchema(field);

            switch (field)
            {
                case FieldType.SIZE:
                    entity = ContextEntity.Sizes;
                    Text = "Size List Editor";
                    break;
                case FieldType.BRAND:
                    entity = ContextEntity.Brands;
                    Text = "Brand List Editor";
                    break;
                case FieldType.ENDS:
                    entity = ContextEntity.Ends;
                    Text = "Ends List Editor";
                    break;
                default:
                    break;
            }
        }

        #region General
        private void PostLoading()
        {
            EnableControls();
            //cboFieldLists.ValueMember = "ID";
            //PopulateListSelector();
            PopulateGrid();
            DisableEntryAdd();
        }

        private void AskSaveBeforeClose()
        {
            if (MessageBox.Show(
                      caption: "Save Changes",
                         text: "Do you want to save before closing ?",
                      buttons: MessageBoxButtons.YesNo,
                         icon: MessageBoxIcon.Exclamation,
                defaultButton: MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                SaveToSource();
            }
        }

        private void SaveToSource()
        {
            AppFactory.context.Save(entity);
        }
        
        private void PopulateGrid()
        {
            dgvListDetails.DataSource = null;
            object newDataSource = Data.GetFieldLists(fieldType);
            dgvListDetails.DataSource = newDataSource;

            // Auto-Size Columns and Rows
            dgvListDetails.AutoResizeColumns();
            dgvListDetails.AutoResizeRows();

            // Set the location of the delete column
            int bindDataCount = dgvListDetails.Columns.Count - 1;
            dgvListDetails.Columns["colEdit"].DisplayIndex = bindDataCount;
            dgvListDetails.Columns["colDelete"].DisplayIndex = bindDataCount;
        }

        private void UpdateEntriesList()
        {
            string listId = GetSelectedListId();
            listEntries = Data.FieldListGetEntries(fieldType, listId);
            lbxFieldListItems.DataSource = null;
            lbxFieldListItems.DataSource = listEntries;
        }

        private void PopulateEntryList(string listId)
        {
            listEntries = Data.FieldListGetEntries(fieldType, listId);
            lbxFieldListItems.DataSource = listEntries;
        }

        private void EnableControls()
        {
            grpListData.Enabled = true;
            tsmiSave.Enabled = true;
        }

        private void DisableControls()
        {
            grpListData.Enabled = false;
            tsmiSave.Enabled = false;
        }

        private void SelectFirstListItem()
        {
            try
            {
                if (lbxFieldListItems.Items.Count > 1)
                    lbxFieldListItems.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        private void SelectListItem(string item)
        {
            try
            {
                if (lbxFieldListItems.Items.Count > 1)
                    lbxFieldListItems.Text = item;
            }
            catch (Exception) { }
        }

        private void DisableEntryAdd()
        {
            btnAddEntry.Enabled = false;
        }
        #endregion

        #region File-Specific

        private void AddNewList()
        {
            FieldListEditor listEditor =
                new FieldListEditor(Data.GetFieldIds(fieldType));

            if (listEditor.ShowDialog() == DialogResult.OK)
            {
                Data.AddFieldList(fieldType, listEditor.FieldList);
                PopulateGrid();
            }
        }

        private void DeleteExistingList(string listId)
        {
            if (Common.ShowEntryRemoveConfirmation(false) == DialogResult.OK)
            {
                Data.DeleteFieldList(fieldType, listId);

                PopulateGrid();
                SelectFirstListItem();
            }
        }

        private void EditExistingList(string listId, string listName)
        {
            IBasicList editList = Data.GetFieldList(fieldType, listId);

            FieldListEditor listEditor =
                new FieldListEditor(Data.GetFieldIds(fieldType), editList);

            if (listEditor.ShowDialog() == DialogResult.OK)
            {
                Data.EditFieldList(fieldType, listId, listEditor.FieldList);

                PopulateGrid();
            }
        }

        private void CheckAvailableEntries()
        {
            btnDeleteEntry.Enabled = lbxFieldListItems.Items.Count > 1;
        }

        private void CheckDuplicateEntries()
        {
            string listId = GetSelectedListId();
            string entryValue = txtEntryValue.Text;
            btnAddEntry.Enabled = !listEntries.Contains(entryValue);
        }
        #endregion

        #region Events Responses
#pragma warning disable IDE1006 // Naming Styles
        private void Form_Load(object sender, EventArgs e)
        {
            //FieldSwitch();
            PostLoading();
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            string entryValue = txtEntryValue.Text;

            if (entryValue != string.Empty)
            {
                string fieldId = GetSelectedListId();
                Data.FieldListAddEntry(fieldType, fieldId, entryValue);
                
                txtEntryValue.Text = string.Empty;
                UpdateEntriesList();
                SelectListItem(entryValue);
                CheckAvailableEntries();
                txtEntryValue.Focus();
            }
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (lbxFieldListItems.SelectedIndex == -1)
            {
                MessageBox.Show("No item selected");
            }
            else
            {
                string listId = GetSelectedListId();
                string selectedEntry = lbxFieldListItems.Text;
                //DataService.SizeListDeleteEntry(listId, selectedEntry);
                Data.FieldListDeleteEntry(fieldType, listId, selectedEntry);

                UpdateEntriesList();
                SelectFirstListItem();
                CheckAvailableEntries();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbxFieldListItems.SelectedIndex == -1)
            {
                MessageBox.Show("No item selected");
            }
            else
            {
                string listId = GetSelectedListId();
                string selectedEntry = lbxFieldListItems.Text;
                
                ValueEdit valueEditBox = new ValueEdit(selectedEntry);

                if (valueEditBox.ShowDialog() == DialogResult.OK)
                {
                    //DataService.SizeListEditEntry(listId, selectedEntry, valueEditBox.NewValue);
                    Data.FieldListEditEntry(fieldType, listId, selectedEntry, valueEditBox.NewValue);

                    UpdateEntriesList();
                    lbxFieldListItems.Text = valueEditBox.NewValue;
                }
            }
        }

        private void btnAddNewList_Click(object sender, EventArgs e)
        {
            AddNewList();
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveToSource();
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            AskSaveBeforeClose();
            Close();
        }

        private void tsmiExitApp_Click(object sender, EventArgs e)
        {
            AskSaveBeforeClose();
            Close();
            Application.Exit();
        }

        private void txtEntryValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnAddEntry.Enabled)
            {
                btnAddEntry_Click(this, new EventArgs());
            }
        }

        private void txtEntryValue_TextChanged(object sender, EventArgs e)
        {
            if (txtEntryValue.Text.Length > 0)
            {
                CheckDuplicateEntries();
            }
            else
            {
                DisableEntryAdd();
            }

        }

        private void FieldEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (!dgvListDetails.Focused && e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.Up)
                    btnUp.PerformClick();

                if (e.KeyCode == Keys.Down)
                    btnDown.PerformClick();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            string listId = GetSelectedListId();
            string item = (string)lbxFieldListItems.SelectedValue;
            int selecIndex = lbxFieldListItems.SelectedIndex;

            //DataService.SizeListMoveEntry(listId, item, ShiftDirection.UP);
            Data.FieldListMoveEntry(fieldType, listId, item, ShiftDirection.UP);
            
            UpdateEntriesList();
            SelectShiftedItem(selecIndex, ShiftDirection.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string listId = GetSelectedListId();
            string item = (string)lbxFieldListItems.SelectedValue;
            int selecIndex = lbxFieldListItems.SelectedIndex;

            //DataService.SizeListMoveEntry(listId, item, ShiftDirection.DOWN);
            Data.FieldListMoveEntry(fieldType, listId, item, ShiftDirection.DOWN);

            UpdateEntriesList();
            SelectShiftedItem(selecIndex, ShiftDirection.DOWN);
        }

        private void SelectShiftedItem(int selectionIndex, ShiftDirection shift)
        {
            if (lbxFieldListItems.Focused)
                lbxFieldListItems.SelectedIndex = selectionIndex;
            else
                lbxFieldListItems.SelectedIndex = selectionIndex + (int)shift;
        }

        private string GetSelectedListId()
        {
            return dgvListDetails.SelectedRows[0].Cells["ID"].Value.ToString();
        }

        private void lbxFieldListItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = lbxFieldListItems.SelectedIndex;
            int entriesCount = lbxFieldListItems.Items.Count - 1;

            if (selectedIndex == 0)
            {
                btnUp.Enabled = false;
            }
            else
            {
                btnUp.Enabled = true;
            }

            if (selectedIndex == entriesCount)
            {
                btnDown.Enabled = false;
            }
            else
            {
                btnDown.Enabled = true;
            }
        }

        private void dgvListDetails_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListDetails.SelectedRows.Count > 0)
            {
                string selectedListId = (string)dgvListDetails.SelectedRows[0].Cells["ID"].Value;
                PopulateEntryList(selectedListId);
            }
        }

        private void dgvListDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string selectedListId = (string)dgvListDetails["ID", e.RowIndex].Value;

                BasicListView selectedItem = (BasicListView)dgvListDetails.Rows[e.RowIndex].DataBoundItem;

                if (e.ColumnIndex == 0) //Edit Button Column
                {
                    //selectedListId = (string)dgvListDetails["ID", e.RowIndex].Value;
                    EditExistingList(selectedItem.ID, selectedItem.Name);
                }

                if (e.ColumnIndex == 1) //Delete Button Column
                {
                    DeleteExistingList(selectedListId);
                }
            }
        }
#pragma warning restore IDE1006 // Naming Styles
        #endregion

    }
}