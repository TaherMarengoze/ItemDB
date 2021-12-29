
using CoreLibrary;
using CoreLibrary.Enums;
using Controllers.SizeGroupUI;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UserInterface.Shared;
using Shared.UI;

namespace UserInterface.Forms
{
    public partial class SizeGroupEditor : Form
    {
        #region Fields
        private SizeGroupUiController uiControl;
        private EntryMode _mode = EntryMode.View;

        // flags
        private bool SKIP_EVENTS = false;
        private bool PREVENT_INPUT = false;
        #endregion

        public SizeGroupEditor()
        {
            InitializeComponent();
            dgvGroups.DoubleBuffered(true);
            uiControl = new SizeGroupUiController();
            SubscribeControllerEvents();
        }
        
        #region Commands to Controller
        private void SaveToSource()
        {
            uiControl?.Save();
        }

        private void PostLoading()
        {
            BindDefaultSizeSelector();

            if (uiControl.Count > 0)
                BindSizeGroupList();
            else
                UnbindSizeGroupList(true);

            BindCustomSizeSelector();
            UpdateStatusBar();
        }

        private void SizeGroupSelected(string id)
        {
            uiControl.SetSelection(id);
        }

        private void AddNewSizeGroup()
        {
            uiControl.New();

            // set entry mode
            SetMode(EntryMode.New);

            txtGroupID.Focus();
        }

        private void EditSizeGroup()
        {
            // get id of edit object
            string id = (string)dgvGroups.SelectedObjectID();

            uiControl.Edit(id);

            // set entry mode
            SetMode(EntryMode.Edit);

            // set check-boxes check value
            PREVENT_INPUT = true;
            chkAltList.NotifyCheck(uiControl.InputAltListRequired, chkAltList_CheckedChanged);
            chkCustomSize.NotifyCheck(uiControl.InputCustomIdRequired, chkCustomSize_CheckedChanged);
            PREVENT_INPUT = false;
        }

        private void AcceptChanges()
        {
            uiControl.CommitChanges();
        }

        private void CancelDrafting()
        {
            uiControl.CancelChanges();
        }

        private void RemoveGroup()
        {
            // get id of edit object
            string id = (string)dgvGroups.SelectedObjectID();

            uiControl.Remove(id);
        }
        #endregion

        #region Input to Controller
        private void InputSizeGroupID()
        {
            if (_mode != EntryMode.View)
                uiControl.InputID = txtGroupID.Text;
        }

        private void InputSizeGroupName()
        {
            if (_mode != EntryMode.View)
                uiControl.InputName = txtGroupName.Text;
        }

        private void InputDefaultID()
        {
            if (_mode != EntryMode.View)
                uiControl.InputDefaultID = GetSelectedDefaultID();
        }

        private void InputAltListRequirement()
        {
            if (_mode != EntryMode.View)
            {
                uiControl.InputAltListRequired = chkAltList.Checked;

                //if (chkAltList.Checked)
                //    EnableAltListUI();
                //else
                //    DisableAltListUI();
            }
        }

        private void InputCustomSizeRequirement()
        {
            if (_mode != EntryMode.View)
            {
                uiControl.InputCustomIdRequired = chkCustomSize.Checked;
            }
        }

        private void InputAltListIDs()
        {
            ListSelector selector =
                new ListSelector(uiControl.SizeListsDefaultEx, uiControl.InputAltList);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                if (_mode != EntryMode.View)
                    uiControl.InputAltList = selector.OutputList;
            }
        }

        private void InputCustomSizeID()
        {
            if (_mode != EntryMode.View)
                uiControl.InputCustomID = GetSelectedCustomID();
        }

        private void ClearAltSizeList()
        {
            if (_mode != EntryMode.View)
                uiControl.InputAltList = null;
        }
        #endregion

        #region Binding
        private void BindDefaultSizeSelector()
        {
            cboDefaultID.DataSource = uiControl.SizeIDs;
            cboDefaultID.SelectedIndex = -1;
        }

        private void BindCustomSizeSelector()
        {
            cboCustomSizeID.DataSource = uiControl.CustomSizeIDs;
            cboCustomSizeID.SelectedIndex = -1;
        }

        private void BindSizeGroupList()
        {
            dgvGroups.DataSourceResize(uiControl.SizeGroups);
        }

        private void UnbindSizeGroupList(bool notify = false)
        {
            if (notify)
            {
                dgvGroups.UnbindNotify(dgvGroups_DataSourceChanged);
            }
            else
            {
                dgvGroups.DataSource = null;
            }
        }

        private void DisplaySelectedListEntries()
        {
            string id = GetSelectedDefaultID();

            lbxSizeListEntries.DataSource = null;
            lbxSizeListEntries.DataSource = uiControl.GetListEntries(id);
        }
        #endregion

        #region Getters
        public static int GetRowIndex(DataGridView dgv, string value, string field = "")
        {
            int rowIndex = -1;

            DataGridViewRow row;

            if (field == string.Empty)
            {
                row = dgv.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[0].Value.ToString().Equals(value))
                .FirstOrDefault();
            }
            else
            {
                row = dgv.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[field].Value.ToString().Equals(value))
                .FirstOrDefault();
            }

            return row?.Index ?? rowIndex;
        }

        private string GetSelectedDefaultID()
        {
            return cboDefaultID.Text;
        }

        private string GetSelectedCustomID()
        {
            return cboCustomSizeID.Text;
        }
        #endregion

        #region UI

        private void SetMode(EntryMode value)
        {
            _mode = value;
            if (value == EntryMode.View)
            {
                EnableSaveUI();

                // set status bar text
                tslReadyState.Text = string.Empty;

                EnableObjectSelection();
                btnNewGroup.Enabled = true; // enable add button
                                            //EnableModifyUI();
                DisableAcceptUI();
                HideReviewUI();
                DisableFieldsEntry();
                DisableDefaultIdEntryUI();
                DisableAltListEntryUI();
                DisableCustomIdEntryUI();
                ClearValidationInfo();
            }
            else
            {
                DisableSaveUI();
                EnableContainerUI();
                FormatSelectedObjectUI();
                FormatObjectList();
                ClearSelection();
                DisableObjectSelection();
                DisableModifyUI();
                ShowReviewUI();
                EnableFieldsEntry();
                EnableDefaultIdEntryUI();
                EnableAltListEntryUI();
                EnableCustomIdEntryUI();
            }
            UpdateStatusBar();
        }

        /// <summary>
        /// Formats the object list hosting control.
        /// </summary>
        private void FormatObjectList()
        {
            dgvGroups.RowHeadersDefaultCellStyle.Padding =
                                    new Padding(dgvGroups.RowHeadersWidth);
        }

        /// <summary>
        /// Formats the hosting control of the selected object.
        /// </summary>
        private void FormatSelectedObjectUI()
        {
            if (_mode == EntryMode.Edit)
            {
                DataGridViewRow row = dgvGroups.SelectedRows[0];

                // format the row being edited
                row.DefaultCellStyle.BackColor = Color.Aquamarine;
                row.DefaultCellStyle.ForeColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// Restores the selection of the object in its hosting control.
        /// </summary>
        /// <param name="objectID"></param>
        private void RestoreSelection(string objectID)
        {
            int i = GetRowIndex(dgvGroups, objectID, "ID");
            if (i != -1)
            {
                dgvGroups.Rows[i].Cells[0].Selected = true;
            }
        }

        /// <summary>
        /// Clears the selection of the object from the hosting control.
        /// </summary>
        private void ClearSelection()
        {
            dgvGroups.ClearSelection();

            if (_mode == EntryMode.New)
            {
                ClearFieldsUI();
                ClearSelectors();
            }
        }

        private void ClearFieldsUI()
        {
            txtGroupID.Clear();
            txtGroupName.Clear();
        }

        private void ClearSelectors()
        {
            // clear default list selector
            cboDefaultID.SelectedIndex = -1;

            // clear alt list
            lstAltListIDs.DataSource = null;

            // clear custom list selector
            cboCustomSizeID.SelectedIndex = -1;
        }

        /// <summary>
        /// Enable the controls related to saving to the data source.
        /// </summary>
        private void EnableSaveUI()
        {
            tsmiSaveFile.Enabled = true;
        }

        /// <summary>
        /// Disable the controls related to saving to the data source.
        /// </summary>
        private void DisableSaveUI()
        {
            tsmiSaveFile.Enabled = false;
        }

        /// <summary>
        /// Enables the selection of objects from the hosting control.
        /// </summary>
        private void EnableObjectSelection()
        {
            dgvGroups.Enabled = true;
        }

        /// <summary>
        /// Disables the selection of objects from the hosting control.
        /// </summary>
        private void DisableObjectSelection()
        {
            dgvGroups.Enabled = false;
        }

        /// <summary>
        /// Enables modification for the entity list by disabling the Add, Edit and Remove UI.
        /// </summary>
        private void EnableModifyUI()
        {
            btnNewGroup.Enabled = true;

            if (dgvGroups.DataSource != null)
            {
                btnEditGroup.Enabled = true;
                btnRemoveGroup.Enabled = true;
            }
        }

        /// <summary>
        /// Disables any modification for the entity list by disabling the Add, Edit and Remove UI.
        /// </summary>
        private void DisableModifyUI()
        {
            btnNewGroup.Enabled = false;
            btnEditGroup.Enabled = false;
            btnRemoveGroup.Enabled = false;
        }

        /// <summary>
        /// Disables the Accept button.
        /// </summary>
        private void DisableAcceptUI()
        {
            btnAccept.Enabled = false;
        }

        /// <summary>
        /// Shows the review controls, enabling acceptance or cancellation of any changes made during drafting.
        /// </summary>
        private void ShowReviewUI()
        {
            btnAccept.Visible = true;
            btnCancel.Visible = true;
        }

        /// <summary>
        /// Hide both Accept and Cancel buttons.
        /// </summary>
        private void HideReviewUI()
        {
            btnAccept.Visible = false;
            btnCancel.Visible = false;
        }

        /// <summary>
        /// Enable data entry for text fields of the object by enabling text entry controls.
        /// </summary>
        private void EnableFieldsEntry()
        {
            txtGroupID.ReadOnly = false;
            txtGroupName.ReadOnly = false;
        }

        /// <summary>
        /// Disable data entry for text fields of the object by setting text entry controls to read-only, for view only.
        /// </summary>
        private void DisableFieldsEntry()
        {
            txtGroupID.ReadOnly = true;
            txtGroupName.ReadOnly = true;
        }

        /// <summary>
        /// Enables entry of default list ID, by showing and enabling the related controls.
        /// </summary>
        private void EnableDefaultIdEntryUI()
        {
            cboDefaultID.Enabled = true;
        }

        /// <summary>
        /// Disables entry of default list ID, by hiding and disabling the related controls.
        /// </summary>
        private void DisableDefaultIdEntryUI()
        {
            cboDefaultID.Enabled = false;
        }

        /// <summary>
        /// Enables entry of alternate list ID, by showing and enabling the related controls.
        /// </summary>
        private void EnableAltListEntryUI()
        {
            chkAltList.Visible = true;

            if (_mode == EntryMode.New)
            {
                chkAltList.Checked = false; // uncheck required checkbox
                DisableAltListRequiredUI();
            }
        }

        /// <summary>
        /// Disables entry of alternate list ID, by hiding and disabling the related controls.
        /// </summary>
        private void DisableAltListEntryUI()
        {
            // hide alt list required checkbox
            chkAltList.Visible = false;

            HideAltListModifyUI();
            DisableAltListModifyUI();

            // disable alt list label
            lblListsID.Enabled = false;

            // disable alt list viewer control
            lstAltListIDs.Enabled = false;
        }

        /// <summary>
        /// Enables the control related to turning ON or OFF, the flag of whether an alternate list is needed.
        /// </summary>
        private void EnableAltListRequiredUI()
        {
            chkAltList.Enabled = true;
        }

        /// <summary>
        /// Disables the control related to turning ON or OFF, the flag of whether an alternate list is needed.
        /// </summary>
        private void DisableAltListRequiredUI()
        {
            chkAltList.Enabled = false;
        }

        /// <summary>
        /// Enable the controls related to modifying the alternate list.
        /// </summary>
        private void EnableAltListSelectionUI()
        {
            // enable info label
            lblListsID.Enabled = true;

            // enable the list control hosting the alt list IDs
            lstAltListIDs.Enabled = true;
            ShowAltListModifyUI();
            EnableAltListModifyUI();
        }

        /// <summary>
        /// Disable the controls related to modifying the alternate list.
        /// </summary>
        private void DisableAltListSelectionUI()
        {
            lblListsID.Enabled = false;
            lstAltListIDs.Enabled = false;
            HideAltListModifyUI();
            DisableAltListModifyUI();
        }

        /// <summary>
        /// Show the controls related to modifying the alternate ID list.
        /// </summary>
        private void ShowAltListModifyUI()
        {
            btnModifyAltList.Visible = true;
            btnClearAltList.Visible = true;
        }

        /// <summary>
        /// Hide the controls related to modifying the alternate ID list.
        /// </summary>
        private void HideAltListModifyUI()
        {
            btnModifyAltList.Visible = false;
            btnClearAltList.Visible = false;
        }

        /// <summary>
        /// Enable the controls related to modifying the alternate ID list.
        /// </summary>
        private void EnableAltListModifyUI()
        {
            // enable the modify button
            btnModifyAltList.Enabled = true;

            // if list is not empty then enable clear button
            if (lstAltListIDs.DataSource != null)
                btnClearAltList.Enabled = true;
            // else, leave it unchanged
        }

        /// <summary>
        /// Disable the controls related to modifying the alternate ID list.
        /// </summary>
        private void DisableAltListModifyUI()
        {
            btnModifyAltList.Enabled = false;
            btnClearAltList.Enabled = false;
        }

        /// <summary>
        /// Enables entry of custom input ID, by showing and enabling the related controls.
        /// </summary>
        private void EnableCustomIdEntryUI()
        {
            // hide required checkbox
            chkCustomSize.Visible = true;

            if (_mode == EntryMode.New)
            {
                chkCustomSize.Checked = false;
            }
        }

        /// <summary>
        /// Disables entry of custom input ID, by hiding and disabling the related controls.
        /// </summary>
        private void DisableCustomIdEntryUI()
        {
            // hide required checkbox
            chkCustomSize.Visible = false;

            // disable custom id label
            lblDataID.Enabled = false;

            // disable custom id selector
            cboCustomSizeID.Enabled = false;
        }

        private void EnableCustomSizeUI()
        {
            lblDataID.Enabled = true;
            cboCustomSizeID.Enabled = true;
        }

        private void DisableCustomSizeUI()
        {
            lblDataID.Enabled = false;
            cboCustomSizeID.Enabled = false;
        }

        /// <summary>
        /// Clear the validation info label controls from any value.
        /// </summary>
        private void ClearValidationInfo()
        {
            lblValidatorGroupId.Text = string.Empty;
            lblValidatorGroupName.Text = string.Empty;
            lblValidatorDefaultId.Text = string.Empty;
            lblValidatorGroupId.Text = string.Empty;
            lblValidatorGroupName.Text = string.Empty;
        }

        /// <summary>
        /// Enable all container controls, enabling its child controls.
        /// </summary>
        private void EnableContainerUI()
        {
            grpGroupList.Enabled = true;
            grpGroupMetadata.Enabled = true;
            grpGroupData.Enabled = true;
        }

        /// <summary>
        /// Disable all container controls, disabling its child controls.
        /// </summary>
        private void DisableContainersUI()
        {
            grpGroupList.Enabled = false;
            grpGroupMetadata.Enabled = false;
            grpGroupData.Enabled = false;
        }

        /// <summary>
        /// Updates information provided in the status bar.
        /// </summary>
        private void UpdateStatusBar()
        {
            if (_mode == EntryMode.View)
            {
                // ready state label
                tslReadyState.Text = string.Empty;
            }
            UpdateStatusLabelMode();
            UpdateStatusLabelCount();
        }

        private void UpdateStatusLabelMode()
        {
            switch (_mode)
            {
                case EntryMode.View:
                    tslMode.Text = "View";
                    tslMode.ForeColor = SystemColors.ControlText;
                    break;

                case EntryMode.New:
                    tslMode.Text = "Adding";
                    tslMode.ForeColor = Color.Green;
                    break;

                case EntryMode.Edit:
                    tslMode.Text = "Editing";
                    tslMode.ForeColor = Color.Red;
                    break;

                default:
                    break;
            }
        }

        private void UpdateStatusLabelCount()
        {
            int count = dgvGroups.RowCount;

            if (count > 0)
            {
                tslCount.Text = $"Count: { count }";
                tslCount.ForeColor = SystemColors.ControlText;
            }
            else
            {
                tslCount.Text = "No item";
                tslCount.ForeColor = Color.Red;
            }
        }
        #endregion

        #region Controller Event Responses
        private void SubscribeControllerEvents()
        {
            uiControl.OnSelectionChange += UiControl_OnSelectionChange;
            uiControl.OnInputAltListSet += UiControl_OnInputAltListSet;
            uiControl.OnIdStatusChange += UiControl_OnIdStatusChange;
            uiControl.OnNameStatusChange += UiControl_OnNameStatusChange;
            uiControl.OnDefaultIdStatusChange += UiControl_OnDefaultIdStatusChange;
            uiControl.OnAltListStatusChange += UiControl_OnAltListStatusChange;
            uiControl.OnReadyStateChange += UiControl_OnReadyStateChange;
            uiControl.OnEntitySet += UiControl_OnEntitySet;
            uiControl.OnDraftCancel += UiControl_OnDraftCancel;
            uiControl.OnEntityRemove += UiControl_OnEntityRemove;
        }

        private void UiControl_OnSelectionChange(object sender, SizeGroupSelectionEventArgs e)
        {
            if (_mode == EntryMode.View)
            {
                txtGroupID.Text = e.ID;
                txtGroupName.Text = e.Name;
                cboDefaultID.Text = e.Default;

                if (e.AltListCount > 0)
                {
                    lstAltListIDs.DataSource = e.AltList;
                    lstAltListIDs.SelectedIndex = -1;
                }
                else
                {
                    lstAltListIDs.DataSource = null;
                }

                if (e.Custom != null)
                {
                    cboCustomSizeID.Text = e.Custom;
                }
                else
                {
                    cboCustomSizeID.SelectedIndex = -1;
                }
            }
        }

        private void UiControl_OnInputAltListSet(object sender, SizeGroupAltListSetEventArgs e)
        {
            lstAltListIDs.DataSource = e.SelectedSizeLists;

            // exclude Alt Size ID List from the default Size ID selector
            SKIP_EVENTS = true;
            cboDefaultID.DataSource = e.AvailableSizeLists;
            cboDefaultID.Text = uiControl.InputDefaultID;
            SKIP_EVENTS = false;
        }

        private void UiControl_OnIdStatusChange(object sender, InputStatus status)
        {
            lblValidatorGroupId.ValidityInfo(status);
        }

        private void UiControl_OnNameStatusChange(object sender, InputStatus status)
        {
            lblValidatorGroupName.ValidityInfo(status);
        }

        private void UiControl_OnDefaultIdStatusChange(object sender, InputStatus status)
        {
            lblValidatorDefaultId.ValidityInfo(status);
            if (status == InputStatus.Valid)
            {
                EnableAltListRequiredUI();
            }
        }

        private void UiControl_OnAltListStatusChange(object sender, InputStatus status)
        {
            if (status == InputStatus.Valid)
            {
                // enable the Clear button
                btnClearAltList.Enabled = true;
            }
            else
            {
                // disable the Clear button
                btnClearAltList.Enabled = false;
            }
        }

        private void UiControl_OnReadyStateChange(object sender, SizeGroupReadyEventArgs e)
        {
            if (_mode != EntryMode.View)
            {
                if (e.Ready)
                {
                    tslReadyState.ForeColor = Color.Green;
                    //tsLblReadyState.Text = "Ready";
                    btnAccept.Enabled = true;
                }
                else
                {
                    tslReadyState.ForeColor = Color.Red;
                    //tsLblReadyState.Text = "Not ready";
                    btnAccept.Enabled = false;
                }
                tslReadyState.Text = e.Info;
            }
        }

        private void UiControl_OnEntitySet(object sender, string e)
        {
            SetMode(EntryMode.View);

            BindSizeGroupList();

            // select added item
            int i = GetRowIndex(dgvGroups, e, "ID");
            if (i != -1)
            {
                dgvGroups.Rows[i].Cells[0].Selected = true;
            }
            //RestoreSelection(e);
        }

        private void UiControl_OnDraftCancel(object sender, SizeGroupCancelEventArgs e)
        {
            SetMode(EntryMode.View);

            // reset selectors data-source
            BindDefaultSizeSelector();

            // clear selector
            cboCustomSizeID.SelectedIndex = -1;

            if (e.EmptyList)
            {
                UnbindSizeGroupList(true);
            }
            else
            {
                BindSizeGroupList();
                RestoreSelection(e.RestoreID);
                dgvGroups.Focus();
            }
        }

        private void UiControl_OnEntityRemove(object sender, int e)
        {
            if (e > 0)
            {
                dgvGroups.RestoreSelection(uiControl.SizeGroups);
            }
            else
            {
                ClearFieldsUI();
                ClearSelectors();
                UnbindSizeGroupList();
            }
        }
        #endregion

        #region Form Objects Event Responses
#pragma warning disable IDE1006 // Naming Styles
        private void SizeGroupEditor_Load(object sender, EventArgs e) => PostLoading();
        private void dgvGroups_DataSourceChanged(object sender, EventArgs e)
        {
            if (_mode == EntryMode.View)
            {
                if (dgvGroups.DataSource == null)
                {
                    btnEditGroup.Enabled = false;
                    btnRemoveGroup.Enabled = false;
                    DisableContainersUI();
                }
                else
                {
                    btnEditGroup.Enabled = true;
                    btnRemoveGroup.Enabled = true;
                    EnableContainerUI();
                }
                UpdateStatusLabelCount();
            }
        }
        private void dgvGroups_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvGroups.SelectedFirstRow();

            if (row != null)
            {
                string id = (string)row.Cells[0].Value;
                SizeGroupSelected(id);
            }
        }
        private void dgvGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                EditSizeGroup();
            }
        }
        private void btnNewGroup_Click(object sender, EventArgs e) => AddNewSizeGroup();
        private void btnEditGroup_Click(object sender, EventArgs e) => EditSizeGroup();
        private void btnAccept_Click(object sender, EventArgs e) => AcceptChanges();
        private void btnCancel_Click(object sender, EventArgs e) => CancelDrafting();
        private void btnRemoveGroup_Click(object sender, EventArgs e) => RemoveGroup();
        private void chkAltList_CheckedChanged(object sender, EventArgs e)
        {
            if (!PREVENT_INPUT)
                InputAltListRequirement();

            if (chkAltList.Checked)
                EnableAltListSelectionUI();
            else
                DisableAltListSelectionUI();
        }
        private void chkCustomSize_CheckedChanged(object sender, EventArgs e)
        {
            if (!PREVENT_INPUT)
                InputCustomSizeRequirement();

            if (chkCustomSize.Checked)
                EnableCustomSizeUI();
            else
                DisableCustomSizeUI();
        }
        private void txtGroupID_TextChanged(object sender, EventArgs e) => InputSizeGroupID();
        private void txtGroupID_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtGroupName_TextChanged(object sender, EventArgs e) => InputSizeGroupName();
        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            txtGroupName.Text = txtGroupName.Text.Trim();
        }
        private void cboDefaultID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SKIP_EVENTS)
                return;

            InputDefaultID();

            // display selected size list entries
            DisplaySelectedListEntries();
        }
        private void cboCustomSizeID_SelectedIndexChanged(object sender, EventArgs e) => InputCustomSizeID();
        private void cboCustomSizeID_TextChanged(object sender, EventArgs e) => InputCustomSizeID();
        private void btnModifyAltList_Click(object sender, EventArgs e) => InputAltListIDs();
        private void btnClearAltList_Click(object sender, EventArgs e) => ClearAltSizeList();
        private void msmiSaveFile_Click(object sender, EventArgs e) => SaveToSource();
        private void tsmiClose_Click(object sender, EventArgs e) => Close();
        private void tsmiExitApp_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
        private void tsmiSimNew_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //uiControl.Simulate_New();
        }

#pragma warning restore IDE1006 // Naming Styles
        #endregion
    }
}