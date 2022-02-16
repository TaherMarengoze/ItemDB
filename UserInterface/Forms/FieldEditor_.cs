
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;
using Client.Controls;
using Controllers;
using CoreLibrary;
using CoreLibrary.Enums;
using Modeling.DataModels;
using UserInterface.Shared;
using UserService;

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

        private readonly string DT_FORMAT = "yyyy-MM-dd HH:mm:ss.fffffff";
        #endregion

        #region Fields
        private readonly SizeListController uiControl;
        private bool editMode;
        private bool DataGridView_MODIFY_COLUMN_HIDDEN;
        private bool PARTIAL_EDIT;
        private bool SKIP_CONTROLLER_EVENTS;
        private readonly string[] actionColumns = new string[3];
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

            if (id != null)
                uiControl.Select(id);
        }

        private void AddNewObject()
        {
            uiControl.New();
            //response > UiControl_OnPreDrafting
        }

        private void EditObject()
        {
            uiControl.Edit();
            //response > UiControl_OnPreDrafting
        }

        private void AcceptChanges()
        {
            uiControl.CommitChanges();
            //response > UiControl_OnSet
        }

        private void CancelChanges()
        {
            uiControl.CancelChanges();
            //response > UiControl_OnCancel
        }

        private void RemoveObject()
        {
            uiControl.Remove();
            //response > UiControl_OnRemove
        }

        private void EditListEntries()
        {
            PARTIAL_EDIT = true;

            EditObject();
            //response > UiControl_OnPreDrafting

            PARTIAL_EDIT = false;

            uiControl.Load_Entries();
            //response > UiControl_OnLoadEntries
        }

        private void EditListEntriesAccept()
        {
            uiControl.Save_Entries();
            //response > UiControl_OnReadyStateChange
            //response > UiControl_OnSaveEntries
        }

        private void EditListEntriesCancel()
        {
            uiControl.Revert_Entries();
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

        private void BindEntryList(object source)
        {
            lbxFieldListItems.DataSource = source;
        }

        private void UnbindEntryList()
        {
            lbxFieldListItems.DataSource = null;
        }
        #endregion

        #region Getters
        private string GetSelectedEntry()
        {
            if (lbxFieldListItems.SelectedIndex == -1)
                return null;

            return
                (string)lbxFieldListItems.SelectedItem;
        }
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

        private void SelectEntry(string entry)
        {
            lbxFieldListItems.Text = entry;
        }
        #endregion

        #region Controller Event Responses
        private void SubscribeControllerEvents()
        {
            uiControl.OnLoad += UiControl_OnLoad;
            uiControl.OnSelect += UiControl_OnSelect;
            uiControl.OnIdStatusChange += UiControl_OnIdStatusChange;
            uiControl.OnNameStatusChange += UiControl_OnNameStatusChange;
            uiControl.OnListStatusChange += UiControl_OnListStatusChange;
            uiControl.OnReadyStateChange += UiControl_OnReadyStateChange;
            uiControl.OnPreDrafting += UiControl_OnPreDrafting;
            uiControl.OnSet += UiControl_OnSet;
            uiControl.OnCancel += UiControl_OnCancel;
            uiControl.OnRemove += UiControl_OnRemove;
            uiControl.OnEntryStatusChange += UiControl_OnEntryStatusChange;

            uiControl.OnLoadEntries += UiControl_OnLoadEntries;
            uiControl.OnSaveEntries += UiControl_OnSaveEntries;
            uiControl.OnRevertEntries += UiControl_OnRevertEntries;
            uiControl.OnEntryPreDrafting += UiControl_OnEntryPreDrafting;
            uiControl.OnEntrySet += UiControl_OnEntrySet;
            uiControl.OnEntryRemove += UiControl_OnEntryRemove;
        }

        private void UiControl_OnLoad(object sender, LoadEventArgs e)
        {
            if (e.Count > 0)
            {
                //EventHandler handler = dgvListDetails_SelectionChanged;
                //dgvListDetails.SelectionChanged -= handler;
                BindObjectList(e.GenericViewList);
                //dgvListDetails.SelectionChanged += handler;
            }
            else
                UndindObjectList();
        }

        private void UiControl_OnSelect(object sender, SelectEventArgs<SizeList> e)
        {
            BindEntryList(e.Selected?.List);
        }

        private void UiControl_OnIdStatusChange(object sender, StatusEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToString(DT_FORMAT)} [{MethodBase.GetCurrentMethod().Name}] > ID Status: {e.Status}");
        }

        private void UiControl_OnNameStatusChange(object sender, StatusEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToString(DT_FORMAT)} [{MethodBase.GetCurrentMethod().Name}] > Name Status: {e}");
        }

        private void UiControl_OnListStatusChange(object sender, StatusEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToString(DT_FORMAT)} [{MethodBase.GetCurrentMethod().Name}] > List Status: {e}");
        }

        private void UiControl_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            // revise the condition
            //if (e.Ready && !editMode)
            //    AcceptChanges();
        }

        private void UiControl_OnPreDrafting(object sender, PreDraftingEventArgs e)
        {
            if (PARTIAL_EDIT)
                return;

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
                    SKIP_CONTROLLER_EVENTS = true;

                    uiControl.Load_Entries();
                    //response > UiControl_OnLoadEntries

                    //uiControl.New_Entry(); // not needed for now

                    uiControl.InputEntry = editor.OutputList.List[0];
                    //response > UiControl_OnEntryStatusChange

                    uiControl.CommitChanges_Entry();
                    //response > UiControl_OnEntrySet

                    uiControl.Save_Entries();
                    //response > UiControl_OnReadyStateChange
                    //response > UiControl_OnSaveEntries

                    SKIP_CONTROLLER_EVENTS = false;
                }

                AcceptChanges();
                //response > UiControl_OnSet
            }
            else
            {
                CancelChanges();
                //response > UiControl_OnCancel
            }
        }

        private void UiControl_OnSet(object sender, SetEventArgs e)
        {
            BindObjectList(e.NewList);
            //BindObjectList(e.GetType().GetProperty("NewList").GetValue(e));

            // select added or edited object
            dgvListDetails.SelectRow(e.NewID, "ID");

            if (editMode)
            {
                dgvListDetails_SelectionChanged(dgvListDetails,
                    EventArgs.Empty);
            }
        }

        private void UiControl_OnCancel(object sender, CancelEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToString(DT_FORMAT)} [{MethodBase.GetCurrentMethod().Name}] > {e.Restore ?? "No item"}");
        }

        private void UiControl_OnRemove(object sender, RemoveEventArgs e)
        {
            if (e.Count > 0)
                dgvListDetails.SaveAndRestoreSelection(
                    delegate { BindObjectList(e.NewObjects); },
                    dgvListDetails_SelectionChanged);
            else
                UndindObjectList();
        }

        private void UiControl_OnEntryStatusChange(object sender,
            StatusEventArgs e)
        {
            if (SKIP_CONTROLLER_EVENTS)
                return;

            switch (e.Status)
            {
                case InputStatus.Valid:
                    btnAddEntry.Enabled = true;
                    break;
                default:
                    btnAddEntry.Enabled = false;
                    break;
            }
        }

        private void UiControl_OnLoadEntries(object sender, LoadEventArgs e)
        {
            if (SKIP_CONTROLLER_EVENTS)
                return;

            // set entry mode
            SetEditMode(true);
        }

        private void UiControl_OnSaveEntries(object sender, EventArgs e)
        {
            if (SKIP_CONTROLLER_EVENTS)
                return;

            AcceptChanges();
            //response > UiControl_OnSet

            SetEditMode(false);
        }

        private void UiControl_OnRevertEntries(object sender,
            RevertEventArgs e)
        {
            SetEditMode(false);

            // restore the entries list to the old one
            BindEntryList(e.Restored);
        }

        private void UiControl_OnEntryPreDrafting(object sender,
            PreModifyEventArgs e)
        {
            ValueEdit valueEditBox = new ValueEdit(e.Draft.ToString(),
                (List<string>)e.List);

            if (valueEditBox.ShowDialog() == DialogResult.OK)
            {
                uiControl.InputEntry = valueEditBox.NewValue;
                uiControl.CommitChanges_Entry();
                //response > UiControl_OnEntrySet
            }
            else
            {
                uiControl.CancelChanges_Entry();
                //response > 
            }
        }

        private void UiControl_OnEntrySet(object sender, EntrySetEventArgs e)
        {
            if (SKIP_CONTROLLER_EVENTS)
                return;

            // clear entry input control
            //txtEntryValue.Text = string.Empty;

            // update the entries list
            BindEntryList(e.SetList);

            // select added or edited entry
            SelectEntry(e.NewItem);

            if (string.IsNullOrWhiteSpace(e.OldItem))
                // clear entry input control
                txtEntryValue.Text = string.Empty;

            // enable the 'Accept' button, if disabled
            if (!btnAccept.Enabled)
                btnAccept.Enabled = true;
        }

        private void UiControl_OnEntryRemove(object sender, RemoveEventArgs e)
        {
            lbxFieldListItems.SaveAndRestoreSelection(
                delegate { BindEntryList(e.NewObjects); });

            btnAccept.Enabled = true;

            if (1 > e.Count)
            {
                btnEdit.Enabled = false;
                btnDeleteEntry.Enabled = false;
                btnAccept.Enabled = false;
            }
        }
        #endregion

        #endregion

        #region Old Code
        private readonly FieldType fieldType;

        private ObservableCollection<string> listEntries;
        #endregion

        #region General
        private void AskSaveBeforeClose()
        {
            if (MessageBox.Show(
                      caption: "Save Changes",
                         text: "Do you want to save before closing ?",
                      buttons: MessageBoxButtons.YesNo,
                         icon: MessageBoxIcon.Exclamation,
                defaultButton: MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            SaveToSource();
        }

        private void UpdateEntriesList()
        {
            string listId = GetSelectedListId();
            listEntries = Data.FieldListGetEntries(fieldType, listId);
            lbxFieldListItems.DataSource = null;
            lbxFieldListItems.DataSource = listEntries;
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
            uiControl.CommitChanges_Entry();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            uiControl.Edit_Entry();
            //response > UiControl_OnEntryPreDrafting
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (lbxFieldListItems.SelectedIndex == -1)
            {
                MessageBox.Show("No item selected");
                return;
            }

            uiControl.RemoveEntry();
            // response > UiControl_OnEntryRemove
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
            uiControl.InputEntry = ((TextBox)sender).Text;
            // response > UiControl_OnEntryStatusChange
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

            Data.FieldListMoveEntry(fieldType, listId, item, ShiftDirection.UP);

            UpdateEntriesList();
            SelectShiftedItem(selecIndex, ShiftDirection.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string listId = GetSelectedListId();
            string item = (string)lbxFieldListItems.SelectedValue;
            int selecIndex = lbxFieldListItems.SelectedIndex;

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
            // new code
            uiControl.SelectEntry(GetSelectedEntry());

            // old code
            //int selectedIndex = lbxFieldListItems.SelectedIndex;
            //int entriesCount = lbxFieldListItems.Items.Count - 1;

            //if (selectedIndex == 0)
            //{
            //    btnUp.Enabled = false;
            //}
            //else
            //{
            //    btnUp.Enabled = true;
            //}

            //if (selectedIndex == entriesCount)
            //{
            //    btnDown.Enabled = false;
            //}
            //else
            //{
            //    btnDown.Enabled = true;
            //}
        }

        private void dgvListDetails_SelectionChanged(object sender, EventArgs e)
        {
            ObjectSelected();
        }

        private void dgvListDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            switch (e.ColumnIndex)
            {
                case (int)ActionColumn.EDIT:
                    EditObject();
                    break;
                case (int)ActionColumn.DELETE:
                    RemoveObject();
                    break;
                case (int)ActionColumn.LIST:
                    EditListEntries();
                    break;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            EditListEntriesAccept();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EditListEntriesCancel();
        }

        #endregion
#pragma warning restore IDE1006 // Naming Styles
    }
}