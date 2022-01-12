
using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using CoreLibrary.Models;
using Client.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using UserService;
using Controllers;
using Modeling.DataModels;
using System.Collections.Generic;
using System.Linq;
using UserInterface.Shared;
using System.Reflection;

namespace UserInterface.Forms
{
    public partial class FieldEditor_ : Form
    {
        #region New Code

        #region Constants
        enum ActionColumn
        {
            EDIT = 0,
            DELETE = 1,
            LIST = 2
        }
        #endregion

        #region Fields
        SizeListController uiControl;
        private bool editMode;
        bool DataGridView_MODIFY_COLUMN_HIDDEN;
        string[] actionColumns = new string[3];

        #endregion

        #region Constructors and Initialization
        public FieldEditor_()
        {
            InitializeComponent();
            ConfigureComponent();
            InitializeFields();
            uiControl = new SizeListController();
            SubscribeControllerEvents();
        }

        private void ConfigureComponent()
        {
            dgvListDetails.DoubleBuffered(true);
            lbxFieldListItems.DoubleBuffered(true);
        }

        private void InitializeFields()
        {
            actionColumns[(int)ActionColumn.EDIT] = colEdit.Name;
            actionColumns[(int)ActionColumn.DELETE] = colDelete.Name;
            actionColumns[(int)ActionColumn.LIST] = colListModify.Name;
        }
        #endregion

        #region Controller Commands
        private void SaveToSource()
        {
            uiControl.Save();
        }

        private void ObjectSelected()
        {
            string id = (string)dgvListDetails.SelectedObjectID();

            uiControl.Select(id);
        }

        private void AddNewObject()
        {
            uiControl.New();
        }

        private void EditObject(string id)
        {
            uiControl.Edit(id);
        }

        private void AcceptChanges()
        {
            uiControl.CommitChanges();
        }

        private void CancelChanges()
        {
            uiControl.CancelChanges();
        }

        private void RemoveObject(string id)
        {
            uiControl.Remove(id);
        }

        private void EditListEntry(string id)
        {
            SetEditMode(true);
        }
        #endregion

        #region Input to Controller

        #endregion

        #region Binding
        private void BindObjectList(object source)
        {
            dgvListDetails.DataSourceResize(source);
            PositionDataGridViewButtons();
        }

        private void UndindObjectList()
        {
            dgvListDetails.DataSource = null;
            HideModifyActionsColumns();
        }

        #endregion

        #region Getters

        #endregion

        #region UI

        public void SetEditMode(bool value)
        {
            editMode = value;

            if (value == true)
            {
                // do edit mode stuff

                // disable object selection
                dgvListDetails.Enabled = false;

                // modify entries list
                lbxFieldListItems.Dock = DockStyle.Left;

                // show list modify controls
                grpListData.Visible = true;
                //btnAddEntry.Visible = true;
                btnEdit.Visible = true;
                btnDeleteEntry.Visible = true;
                btnUp.Visible = true;
                btnDown.Visible = true;

                // show review controls
                btnAccept.Visible = true;
                btnCancel.Visible = true;

                // enable list entry add controls
                grpListData.Enabled = true;

                // disable main modify controls
                btnAddNewList.Enabled = false;
            }
            else
            {
                // enable object selection
                dgvListDetails.Enabled = true;

                // modify entries list
                lbxFieldListItems.Dock = DockStyle.Fill;

                // hide list modify controls
                grpListData.Visible = false;
                //btnAddEntry.Visible = false;
                btnEdit.Visible = false;
                btnDeleteEntry.Visible = false;
                btnUp.Visible = false;
                btnDown.Visible = false;

                // hide review controls
                btnAccept.Visible = false;
                btnCancel.Visible = false;

                // disable list entry add controls
                grpListData.Enabled = false;

                // enable main modify controls
                btnAddNewList.Enabled = true;
            }
        }

        private void ShowModifyActionsColumns()
        {
            foreach (string column in actionColumns)
                dgvListDetails.Columns[column].Visible = true;

            DataGridView_MODIFY_COLUMN_HIDDEN = false;
        }

        private void HideModifyActionsColumns()
        {
            foreach (string column in actionColumns)
                dgvListDetails.Columns[column].Visible = false;

            DataGridView_MODIFY_COLUMN_HIDDEN = true;
        }

        private void PositionDataGridViewButtons()
        {
            if (DataGridView_MODIFY_COLUMN_HIDDEN)
                ShowModifyActionsColumns();

            // Set the location of the delete column
            int bindDataCount = dgvListDetails.Columns.Count - 1;

            foreach (string column in actionColumns)
                dgvListDetails.Columns[column].DisplayIndex = bindDataCount;
        }
        #endregion

        #region Controller Event Responses
        private void SubscribeControllerEvents()
        {
            uiControl.OnLoad += UiControl_OnLoad;
            //uiControl.OnSelection += UiControl_OnSelection;
            uiControl.OnSelect += UiControl_OnSelect;
            uiControl.OnIdStatusChange += UiControl_OnIdStatusChange;
            uiControl.OnNameStatusChange += UiControl_OnNameStatusChange;
            uiControl.OnListStatusChange += UiControl_OnListStatusChange;
            uiControl.OnReadyStateChange += UiControl_OnReadyStateChange;
            uiControl.OnPreDrafting += UiControl_OnPreDrafting;
            uiControl.OnSet += UiControl_OnSet;
            uiControl.OnCancel += UiControl_OnCancel;
            uiControl.OnRemove += UiControl_OnRemove;
        }

        private void UiControl_OnLoad(object sender, LoadEventArgs e)
        {
            if (e.Count > 0)
                BindObjectList(e.GenericViewList);
            else
                UndindObjectList();
        }

        //private void UiControl_OnSelection(object sender, SizeListSelectionEventArgs e)
        //{
        //    lbxFieldListItems.DataSource = e.Selected?.List;
        //}

        private void UiControl_OnSelect(object sender, SelectEventArgs<SizeList> e)
        {
            lbxFieldListItems.DataSource = e.Selected?.List;
        }

        private void UiControl_OnIdStatusChange(object sender, InputStatus e)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")} [{MethodBase.GetCurrentMethod().Name}] > ID Status: {e.ToString()}");
        }

        private void UiControl_OnNameStatusChange(object sender, InputStatus e)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")} [{MethodBase.GetCurrentMethod().Name}] > Name Status: {e.ToString()}");
        }

        private void UiControl_OnListStatusChange(object sender, InputStatus e)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")} [{MethodBase.GetCurrentMethod().Name}] > List Status: {e.ToString()}");
        }

        private void UiControl_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            if (e.Ready)
                AcceptChanges();
        }

        private void UiControl_OnPreDrafting(object sender, PreDraftingEventArgs e)
        {
            bool isNewObject = e.DraftObject == null;

            ListEditor_ editor = isNewObject ?
                new ListEditor_(e.PreList) :
                new ListEditor_(e.PreList, e.DraftObject);

            if (editor.ShowDialog() == DialogResult.OK)
            {
                uiControl.InputID = editor.OutputList.ID;
                uiControl.InputName = editor.OutputList.Name;

                if (isNewObject)
                {
                    //uiControl.InputList = editor.OutputList.List; // clone or not ?
                    uiControl.AddEntry(editor.OutputList.List[0]);
                }
            }
            else
            {
                CancelChanges();
            }
        }

        private void UiControl_OnSet(object sender, SetEventArgs e)
        {
            BindObjectList(e.NewList);
            //BindObjectList(e.GetType().GetProperty("NewList").GetValue(e));

            // select added object
            dgvListDetails.SelectRow(e.SetID, "ID");
        }

        private void UiControl_OnCancel(object sender, CancelEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")} [{MethodBase.GetCurrentMethod().Name}] > {e.RestoreID ?? "No item"}");
        }

        private void UiControl_OnRemove(object sender, RemoveEventArgs e)
        {
            if (e.Count > 0)
            {
                dgvListDetails.SaveAndRestoreSelection(delegate
                {
                    BindObjectList(e.NewList);
                });
            }
            else
            {
                UndindObjectList();
            }

            Console.WriteLine("{0} [{1}] > {2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                MethodBase.GetCurrentMethod().Name,
                $"Removed Object ID: {e.RemoveID}");
        }
        #endregion

        #endregion

        #region Old Code
        private FieldType fieldType;

        private ObservableCollection<string> listEntries;
        #endregion

        public FieldEditor_(FieldType field)
        {
            InitializeComponent();
            fieldType = field;
        }

        #region General
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

        private void UpdateEntriesList()
        {
            string listId = GetSelectedListId();
            listEntries = Data.FieldListGetEntries(fieldType, listId);
            lbxFieldListItems.DataSource = null;
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
            uiControl.Load();
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
            AddNewObject();
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
            ObjectSelected();
        }

        private void dgvListDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string id = (string)dgvListDetails.SelectedObjectID();

            if (e.ColumnIndex == (int)ActionColumn.EDIT)
                EditObject(id);

            if (e.ColumnIndex == (int)ActionColumn.DELETE)
                RemoveObject(id);

            if (e.ColumnIndex == (int)ActionColumn.LIST)
                EditListEntry(id);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
        }
#pragma warning restore IDE1006 // Naming Styles
        #endregion

        
    }
}