using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using UserInterface.Factory;
using UserInterface.Interfaces;
using UserInterface.Models;
using UserInterface.Operation;
using UserInterface.Enums;

namespace UserInterface.Forms
{
    public partial class FieldEditor : Form
    {
        private IListStructure xn;
        private XDocument fieldXDoc;
        private List<string> listEntries;
        private string fieldFilePath;

        FieldType editorField;

        public FieldEditor(FieldType type)
        {
            InitializeComponent();
            editorField = type;
            switch (type)
            {
                case FieldType.SIZE:
                    xn = new ListStructure("listID", "name", "sizeList", "size", "sizes");
                    Text = "Size List Editor";
                    break;
                case FieldType.BRAND:
                    xn = new ListStructure("listID", "name", "brandList", "brand", "brands");
                    Text = "Brand List Editor";
                    break;
                case FieldType.ENDS:
                    xn = new ListStructure("listID", "name", "endsList", "end", "ends");
                    Text = "Ends List Editor";
                    break;
                default:
                    break;
            }
        }

        #region General

        private void FieldSwitch()
        {
            fieldFilePath = FilePathProcessor.FieldFilePath(editorField);

            Delegators.FieldActionCallback(editorField,
                delegate { fieldXDoc = Program.xDataDocs.Sizes; },
                delegate { fieldXDoc = Program.xDataDocs.Brands; },
                delegate { fieldXDoc = Program.xDataDocs.Ends; });
        }

        private void PostLoading()
        {
            EnableControls();
            //cboFieldLists.ValueMember = "ID";
            //PopulateListSelector();
            PopulateGrid();
            DisableEntryAdd();
            ViewXmlTextAll();
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
                SaveXmlFile(fieldFilePath);
            }
        }

        private void SaveXmlFile(string savePath)
        {
            try
            {
                fieldXDoc.Save(savePath);
                MessageBox.Show("File saved successfully");
                DataService.UpdateFieldList(editorField);
            }
            catch (Exception)
            {
                MessageBox.Show("Error saving the file.");
            }
        }

        private void PopulateGrid()
        {
            dgvListDetails.DataSource = null;
            object newDataSource = DataService.GetFieldLists(editorField);
            dgvListDetails.DataSource = newDataSource;

            // Auto-Size Columns and Rows
            dgvListDetails.AutoResizeColumns();
            dgvListDetails.AutoResizeRows();

            // Set the location of the delete column
            int bindDataCount = dgvListDetails.Columns.Count - 1;
            dgvListDetails.Columns["colEdit"].DisplayIndex = bindDataCount;
            dgvListDetails.Columns["colDelete"].DisplayIndex = bindDataCount;
        }

        private void PopulateFieldEntryList()
        {
            string listId = GetSelectedListId();
            listEntries = GetListEntries(listId);
            lbxFieldListItems.DataSource = listEntries;
        }

        private void PopulateFieldEntryList(string listId)
        {
            listEntries = GetListEntries(listId);
            lbxFieldListItems.DataSource = listEntries;
        }

        private void EnableControls()
        {
            grpListData.Enabled = true;
            mnuItmSaveFile.Enabled = true;
        }

        private void DisableControls()
        {
            grpListData.Enabled = false;
            mnuItmSaveFile.Enabled = false;
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

        private List<string> GetAllListIDs()
        {
            return fieldXDoc.Descendants(xn.ListParent)
                .Select(id => id.Attribute(xn.ListId).Value).ToList();
        }

        private string GetListName(string listId)
        {
            try
            {
                XAttribute xaListName = GetSpecificList(listId).Attribute(xn.ListName);
                if (xaListName != null)
                    return xaListName.Value;

                return string.Empty;
            }
            catch (Exception) { return string.Empty; }

        }

        private List<string> GetListEntries(string listId)
        {
            return (from itm in fieldXDoc.Descendants(xn.ListChild)
                    where itm.Ancestors(xn.ListParent).First().Attribute(xn.ListId).Value == listId
                    select itm.Value).ToList();
        }

        private void AddNewEntry(string fieldId, string entryValue)
        {
            XElement dataNode =
                (from listSizes in fieldXDoc.Descendants(xn.ListParent)
                 where listSizes.Attribute(xn.ListId).Value == fieldId
                 select listSizes).First();

            dataNode.Element(xn.ChildGroup).Add(new XElement(xn.ListChild) { Value = entryValue });
        }

        private XElement GetSpecificList(string listId)
        {
            return
                (from fieldList in fieldXDoc.Descendants(xn.ListParent)
                 where fieldList.Attribute(xn.ListId).Value == listId
                 select fieldList).First();
        }

        private XElement GetListEntry(string listId, string item)
        {
            XElement fieldList = GetSpecificList(listId);
            return
                (from entry in fieldList.Descendants(xn.ListChild)
                 where entry.Value == item
                 select entry).First();
        }

        private void AddNewList()
        {
            FieldListEditor listEditor = new FieldListEditor(GetAllListIDs(), xn);

            if (listEditor.ShowDialog() == DialogResult.OK)
            {
                //fieldXDoc.Root.Add(listEditor.ListItem);
                fieldXDoc = XDataService.AddFieldItemToXDocument(editorField, listEditor.ListItem);
                //PopulateListSelector();
                PopulateGrid();
                //SelectComboboxItem(listEditor.ListItem.Attribute(xn.ListId).Value);
                //CheckAvailableLists();
            }
        }

        private void DeleteExistingList(string listId)
        {
            if (Common.ShowEntryRemoveConfirmation(false) == DialogResult.OK)
            {
                DataService.DeleteFieldList(editorField, listId);

                //PopulateListSelector();
                PopulateGrid();
                SelectFirstListItem();
                //CheckAvailableLists();
                //
            }
        }

        private void EditExistingList(string listId, string listName)
        {
            ListMetadata editMeta = new ListMetadata(listId, listName);

            FieldListEditor listEditor = new FieldListEditor(GetAllListIDs(), editMeta);
            if (listEditor.ShowDialog() == DialogResult.OK)
            {
                // Modify the list metadata
                XDataService.ModifyFieldXDocument(editorField, editMeta.ID, listEditor.ListMetadata, xn);

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

        #region Events Reponses
#pragma warning disable IDE1006 // Naming Styles
        private void Form_Load(object sender, EventArgs e)
        {
            FieldSwitch();
            PostLoading();
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            string entryValue = txtEntryValue.Text;

            if (entryValue != string.Empty)
            {
                string fieldId = GetSelectedListId();
                AddNewEntry(fieldId, entryValue);
                txtEntryValue.Text = string.Empty;
                PopulateFieldEntryList();
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
                string selectedItem = lbxFieldListItems.Text;

                GetListEntry(listId, selectedItem).Remove();
                PopulateFieldEntryList();
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
                string dataId = GetSelectedListId();
                XElement dataItem = GetListEntry(dataId, lbxFieldListItems.Text);

                ValueEdit valueEditBox = new ValueEdit(dataItem.Value);
                if (valueEditBox.ShowDialog() == DialogResult.OK)
                {
                    dataItem.Value = valueEditBox.NewValue;
                    PopulateFieldEntryList();
                    lbxFieldListItems.Text = valueEditBox.NewValue;
                }
            }
        }

        private void btnAddNewList_Click(object sender, EventArgs e)
        {
            AddNewList();
        }

        private void mnuItmSaveFile_Click(object sender, EventArgs e)
        {
            SaveXmlFile(fieldFilePath);
            ViewXmlTextAll();
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
            XElement moveXItem = GetListEntry(listId, item);
            moveXItem.PreviousNode.AddBeforeSelf(moveXItem);
            moveXItem.Remove();
            PopulateFieldEntryList();
            SelectShiftedItem(selecIndex, SelectionShift.UP);
            ViewXmlText(listId);
            ViewXmlTextAll();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string listId = GetSelectedListId();
            string item = (string)lbxFieldListItems.SelectedValue;
            int selecIndex = lbxFieldListItems.SelectedIndex;
            XElement moveXItem = GetListEntry(listId, item);
            moveXItem.NextNode.AddAfterSelf(moveXItem);
            moveXItem.Remove();
            PopulateFieldEntryList();
            SelectShiftedItem(selecIndex, SelectionShift.DOWN);
            ViewXmlText(listId);
            ViewXmlTextAll();
        }

        private void SelectShiftedItem(int selectionIndex, SelectionShift shift)
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
                PopulateFieldEntryList(selectedListId);
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

        XML_Viewer viewer = new XML_Viewer();
        private void ViewXmlTextAll()
        {
            //viewer.rtbViewerAll.Text = fieldXDoc.ToString();
        }
        private void ViewXmlText(string listId)
        {
            //if (!viewer.Visible)
            //{
            //    viewer.Show();
            //}

            //viewer.rtbViewerPart.Text = GetSpecificList(listId).ToString();
        }
        private void FieldEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewer.Close();
        }

#pragma warning restore IDE1006 // Naming Styles
        #endregion

    }
}