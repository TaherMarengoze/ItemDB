
using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Models;
using Controllers.SizeGroupUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UserService;
using UserInterface.Shared;
using Shared.UI;

namespace UserInterface.Forms
{
    public partial class SizeGroupEditor : Form
    {
        public SizeGroupEditor()
        {
            InitializeComponent();
            uiControl = new SizeGroupUiController();
            SubscribeControllerEvents();
        }

        private void SubscribeControllerEvents()
        {
            uiControl.OnSelectionChange += UiControl_OnSelectionChange;
            uiControl.OnIdStatusChange += UiControl_OnIdStatusChange;
            uiControl.OnNameStatusChange += UiControl_OnNameStatusChange;
            uiControl.OnDefaultIdStatusChange += UiControl_OnDefaultIdStatusChange;
            uiControl.OnReadyStateChange += UiControl_OnReadyStateChange;
            uiControl.OnNewEntityAdd += UiControl_OnNewEntityAdd;
        }
        
        private void UiControl_OnSelectionChange(object sender, SizeGroupSelectionEventArgs e)
        {
            //throw new NotImplementedException();
            if (Mode == EntryMode.View)
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

        private void UiControl_OnIdStatusChange(object sender, InputStatus status)
        {
            //throw new NotImplementedException();
            lblValidatorGroupId.ValidityInfo(status);
        }
        
        private void UiControl_OnNameStatusChange(object sender, InputStatus status)
        {
            //throw new NotImplementedException();
            lblValidatorGroupName.ValidityInfo(status);
        }

        private void UiControl_OnDefaultIdStatusChange(object sender, InputStatus status)
        {
            //throw new NotImplementedException();
            lblValidatorDefaultId.ValidityInfo(status);
            if (status == InputStatus.Valid)
            {
                EnableAltListSelection();
            }
        }

        private void UiControl_OnReadyStateChange(object sender, bool ready)
        {
            if (Mode != EntryMode.View)
            {
                if (ready)
                {
                    tsLblReadyState.ForeColor = Color.Green;
                    tsLblReadyState.Text = "Ready";

                    btnAccept.Enabled = true;
                }
                else
                {
                    tsLblReadyState.ForeColor = Color.Red;
                    tsLblReadyState.Text = "Not ready";

                    btnAccept.Enabled = false;
                }
            }
        }

        private void UiControl_OnNewEntityAdd(object sender, string e)
        {
            //throw new NotImplementedException();
            Mode = EntryMode.View;

            // refresh SizeGroup list
            dgvGroups.DataSourceResize(uiControl.SizeGroups, true);

            // select added item
            int i = GetRowIndex(dgvGroups, e, "ID");
            if (i != -1)
            {
                dgvGroups.Rows[i].Cells[0].Selected = true;
            }
        }

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


        private EntryMode Mode
        {
            get => _sizeGroupMode;
            set
            {
                _sizeGroupMode = value;
                if (value == EntryMode.View)
                {
                    // enable Save UI
                    tsmiSaveFile.Enabled = true;

                    // set status bar text
                    tsLblReadyState.Text = string.Empty;

                    // enable SizeGroup selection
                    dgvGroups.Enabled = true;

                    // enable SizeGroup Add, Edit & Remove buttons
                    btnNewGroup.Enabled = true;
                    //btnEditGroup.Enabled = true; => enabled with conditions
                    //btnRemoveGroup.Enabled = true; => enabled with conditions

                    // disable Accept button
                    btnAccept.Enabled = false;

                    // show Accept and Cancel buttons
                    btnAccept.Visible = false;
                    btnCancel.Visible = false;

                    // disable SizeGroup fields entry
                    txtGroupID.ReadOnly = true;
                    txtGroupName.ReadOnly = true;

                    // disable default list selector
                    cboDefaultID.Enabled = false;

                    // hide Alt list & Custom size check boxes
                    chkAltList.Visible = false;
                    chkCustomSize.Visible = false;
                }
                else
                {
                    // disable Save UI
                    tsmiSaveFile.Enabled = false;

                    // setup User Interface

                    // clear and disable SizeGroup selection
                    dgvGroups.ClearSelection();
                    dgvGroups.Enabled = false;

                    // disable SizeGroup Add, Edit & Remove buttons
                    btnNewGroup.Enabled = false;
                    btnEditGroup.Enabled = false;
                    btnRemoveGroup.Enabled = false;

                    // show Accept and Cancel buttons
                    btnAccept.Visible = true;
                    btnCancel.Visible = true;

                    // enable SizeGroup fields entry
                    txtGroupID.ReadOnly = false;
                    txtGroupName.ReadOnly = false;

                    // enable default list selector
                    cboDefaultID.Enabled = true;

                    chkAltList.Visible = true;
                    chkAltList.Checked = false;

                    // disable alt list check box if New mode
                    if (value == EntryMode.New)
                    {
                        chkAltList.Enabled = false;
                    }

                    chkCustomSize.Visible = true;
                    chkCustomSize.Checked = false;
                }
            }
        }

        private EntryMode _sizeGroupMode = EntryMode.View;

        private SizeGroupUiController uiControl;
        // the above line will replace the one below when the controller is fully operational
        private SizeGroupDrafter drafter;

        /// <summary>
        /// A flag indicating a valid ID is given or not.
        /// </summary>
        private bool draft_idGiven = false;

        /// <summary>
        /// A flag indicating a valid Name is given or not.
        /// </summary>
        private bool draft_nameGiven = false;

        /// <summary>
        /// A flag indicating a valid size list ID is given or not.
        /// </summary>
        private bool draft_defListGiven = false;

        private bool skipEvents = false;
        private int groupSelectionIndex;

        private void PostLoading()
        {
            SetupSizeSelectors();
            if (uiControl.Count > 0)
            {
                BindSizeGroupList();
            }
            else
            {
                UnbindSizeGroupList();
            }
        }

        private void SetupSizeSelectors()
        {
            // bind selectors
            BindDefaultSizeSelector();
            BindCustomSizeSelector();

            // clear selectors
            cboDefaultID.SelectedIndex = -1;
        }

        private void BindDefaultSizeSelector()
        {
            cboDefaultID.DataSource = uiControl.SizeIDs;
        }

        private void BindCustomSizeSelector()
        {
            cboCustomSizeID.DataSource = uiControl.CustomSizeIDs;
        }

        private void BindSizeGroupList()
        {
            dgvGroups.DataSourceResize(uiControl.SizeGroups);
        }

        private void UnbindSizeGroupList()
        {
            dgvGroups.UnbindNotify(dgvGroups_DataSourceChanged);
        }

        private void SizeGroupSelected(string id)
        {
            uiControl.SetSelection(id);
        }

        private void AddNewSizeGroup()
        {
            //drafter = new SizeGroupDrafter();
            uiControl.New();

            // set entry mode
            Mode = EntryMode.New;

            // Clear existing data for new input
            ClearSizeGroupFields();
            ClearSizeGroupSelectors();
            //ClearGroupDataUI();
            txtGroupID.Focus();
        }

        private void ClearSizeGroupFields()
        {
            txtGroupID.Clear();
            txtGroupName.Clear();
        }

        private void ClearSizeGroupSelectors()
        {
            //if (cboDefaultID.SelectedIndex != -1)

            // clear default list selector
            cboDefaultID.SelectedIndex = -1;

            // clear alt list
            lstAltListIDs.DataSource = null;

            // clear custom list selector
            cboCustomSizeID.SelectedIndex = -1;
        }

        private void InputSizeGroupID()
        {
            //throw new NotImplementedException();
            
            if (Mode != EntryMode.View)
            {
                uiControl.InputID = txtGroupID.Text;
            }
        }

        private void InputSizeGroupName()
        {
            //throw new NotImplementedException();

            if (Mode != EntryMode.View)
            {
                uiControl.InputName = txtGroupName.Text;
            }
        }

        private void InputDefaultID()
        {
            //throw new NotImplementedException();

            if (Mode != EntryMode.View)
            {
                uiControl.InputDefaultID = GetSelectedDefaultID();
            }
        }

        private string GetSelectedDefaultID()
        {
            // use a method in case we changed from
            // combo box to another control like DataGridView
            return cboDefaultID.Text;
        }

        private void DisplaySelectedListEntries()
        {
            string id = GetSelectedDefaultID();

            lbxSizeListEntries.DataSource = null;
            lbxSizeListEntries.DataSource = uiControl.GetListEntries(id);
        }

        /// <summary>
        /// Enable the New, Edit and Remove buttons.
        /// </summary>
        private void EnableGroupModifyUI()
        {
            btnNewGroup.Enabled = true;
            btnEditGroup.Enabled = true;
            btnRemoveGroup.Enabled = true;
        }

        private void SaveToSource()
        {
            //AppFactory.context.Save(ContextEntity.SizeGroups);
            Data.Save(ContextEntity.SizeGroups);
        }

        private void EnterViewMode() => Mode = EntryMode.View;

        private void EnterNewMode() => Mode = EntryMode.New;

        private void EnterEditMode() => Mode = EntryMode.Edit;



        /// <summary>
        /// Binds the size group list to a <see cref="DataGridView"/>.
        /// </summary>
        private void ListSizeGroups()
        {
            dgvGroups.DataSourceResize(Data.GetSizeGroups());
        }

        private void RefreshSizeGroups()
        {
            object dataSource = dgvGroups.DataSource;
            dgvGroups.DataSource = null;
            if (dataSource != null)
            {
                dgvGroups.DataSource = dataSource;
            }
            dgvGroups.AutoResizeColumns();
            dgvGroups.AutoResizeRows();
        }
        
        private void ChangeGroupID()
        {
            if (skipEvents) return;

            if (Mode != EntryMode.View)
            {
                // Remove invalid characters in case user CnP
                string inputId = ModifyInputToPattern(txtGroupID.Text, "[^A-Z0-9]+"); //RemoveInvalidCharactersFromID(inputId);

                skipEvents = true;
                txtGroupID.Text = inputId;
                skipEvents = false;

                draft_idGiven = IsGroupIdGiven(inputId) && IsGroupIdUnique(inputId);

                if (draft_idGiven)
                {
                    drafter.groupID = inputId;
                }

                CheckDraftValidity();
            }
        }

        private void ChangeGroupName()
        {
            if (skipEvents) return;

            if (Mode != EntryMode.View)
            {
                string inputName = ModifyInputToPattern(txtGroupName.Text, "[^A-Za-z0-9 _.-]+");

                skipEvents = true;
                txtGroupName.Text = inputName;
                skipEvents = false;

                draft_nameGiven = IsGroupNameGiven(inputName);

                if (draft_nameGiven)
                    drafter.groupName = inputName.Trim();

                CheckDraftValidity();
            }
        }

        private void ChangeDefaultListID()
        {
            // only in non-view mode
            if (Mode != EntryMode.View)
            {
                draft_defListGiven = IsGroupDefaultListGiven(cboDefaultID.Text);

                if (draft_defListGiven)
                {
                    drafter.groupDefaultListID = cboDefaultID.Text;
                    EnableAltListSelection();
                }

                CheckDraftValidity();
            }
        }

        private void ChangeCustomSizeID()
        {
            // only in non view mode
            if (Mode != EntryMode.View)
            {
                drafter.groupCustomSizeID = cboCustomSizeID.Text;
                CheckDraftValidity();
            }
        }

        //private void NewGroup()
        //{
        //    // Create draft objects
        //    drafter = new SizeGroupDrafter();

        //    // Set Entry Mode
        //    EnterNewMode();

        //    // Setup User Interface
        //    NewEditSetupUI();

        //    // Clear existing data for new input
        //    ClearGroupMetadataUI();
        //    ClearGroupDataUI();
        //    txtGroupID.Focus();
        //    //cboDefaultID.SelectedIndex = -1;
        //}

        private void EditGroup()
        {
            // Exit if no group exists
            if (dgvGroups.SelectedRows.Count <= 0) return;

            // Get selected row
            DataGridViewRow row = dgvGroups.SelectedRows[0];

            // Format the row being edited
            row.DefaultCellStyle.BackColor = Color.Aquamarine;
            row.DefaultCellStyle.ForeColor = Color.DarkGray;

            // Get selected group id from row
            string groupId = row.Cells[0].Value.ToString();

            drafter = new SizeGroupDrafter(Data.GetSizeGroup(groupId));

            draft_idGiven = true;
            draft_nameGiven = true;

            // For now all concerns on Default List ID
            // and no concern on Custom Size ID
            // so this flag will always be True
            draft_defListGiven = true;

            if (draft_defListGiven) // or if draft_customIdGiven is True --to be added later--
            {
                EnableAltListSelection();
            }

            EnterEditMode();

            // Setup User Interface
            NewEditSetupUI();

            // Set CheckBox for Alt List & Custom Size, if any.
            if (drafter.groupAltList != null)
            {
                chkAltList.Checked = true;
                btnClearAltList.Enabled = true;
            }
            else
            {
                chkAltList.Checked = false;
                btnClearAltList.Enabled = false;
            }

            chkCustomSize.Checked = drafter.groupCustomSizeID != null;
        }

        private void NewEditSetupUI()
        {
            DisableSizeGroupSelection();
            DisableGroupModifyUI();
            ShowGroupReviewUI();
            EnableGroupMetadataEntryUI();
            EnableGroupDataUI();
            CheckSizeGroupList();
        }

        private void CancelDraftingSetupUI()
        {
            EnableSizeGroupSelection();
            EnableGroupModifyUI();
            HideGroupReviewUI();
            DisableGroupMetadataEntryUI();
            DisableGroupDataUI();
            ClearValidationLabels();
            CheckSizeGroupList();
        }

        /// <summary>
        /// Check if the draft SizeGroup data is given and modified in case of edit mode.
        /// </summary>
        private void CheckDraftValidity()
        {
            bool requiredDataGiven = draft_idGiven && draft_nameGiven && draft_defListGiven;
            bool draftDataModified = true;

            // Detect modification in case of edit
            if (Mode == EntryMode.Edit)
            {
                draftDataModified = drafter.IsModified();
            }

            btnAccept.Enabled = requiredDataGiven && draftDataModified;
        }

        private void SaveDraftGroup()
        {
            // Save Draft Object
            drafter.CommitChanges();

            switch (Mode)
            {
                case EntryMode.New:
                    Data.AddSizeGroup(drafter.DraftSizeGroup);
                    break;

                case EntryMode.Edit:
                    Data.UpdateSizeGroup(drafter.refId, drafter.DraftSizeGroup);
                    break;
            }

            EnterViewMode();

            // Clear draft object
            drafter = null;

            // Reset Size Selector data source
            SetupSizeSelectors();

            // Update Size Groups List
            dgvGroups.DataSource = null;
            ListSizeGroups();

            // Reset UI
            EnableSizeGroupSelection();
            EnableGroupModifyUI();
            HideGroupReviewUI();
            DisableGroupMetadataEntryUI();
            DisableGroupDataUI();
        }

        private void CancelDrafting()
        {
            // Clear draft object
            drafter = null;

            // Reset flags values
            draft_idGiven = false;
            draft_nameGiven = false;
            draft_defListGiven = false;

            EnterViewMode();

            // Reset Size Selector data source
            SetupSizeSelectors();

            // Setup UI
            CancelDraftingSetupUI();

            // Reset DGV Style
            RefreshSizeGroups();
        }

        private void RemoveGroup()
        {
            // Check if there is at least a single item
            if (dgvGroups.SelectedRows.Count <= 0) return;

            // Get the selected row
            DataGridViewRow row = dgvGroups.SelectedRows[0];

            //Save Selection
            groupSelectionIndex = Common.GetDataGridViewSelectionIndex(dgvGroups);

            // Get size group id from selected row
            string groupId = row.Cells[0].Value.ToString();

            Data.DeleteSizeGroup(groupId);

            // Check size groups count
            CheckSizeGroupList();

            ListSizeGroups();

            //Restore Selection
            Common.RestoreDataGridViewSelection(dgvGroups, groupSelectionIndex);

            //Set the Groups DataGridView focus
            dgvGroups.Focus();
        }

        private void CheckSizeGroupList()
        {
            List<SizeGroup> szGroups = Data.GetSizeGroups();

            bool emptySizeGroupList = szGroups.Count <= 0;

            switch (Mode)
            {
                case EntryMode.View:
                    if (emptySizeGroupList)
                    {
                        ClearGroupsGrid();
                        ClearGroupMetadataUI();
                        ClearGroupDataUI();
                        DisableEditRemoveUI();
                        DisableGroupContainerUI();
                    }
                    else
                    {
                        EnableEditRemoveUI();
                        EnableGroupContainerUI();
                    }

                    break;
                case EntryMode.New:
                    if (emptySizeGroupList)
                    {
                        EnableGroupContainerUI();
                    }
                    break;
            }
        }

        private void EditRemoveUIEnabledStatus(bool enable)
        {
            btnRemoveGroup.Enabled = enable;
            btnEditGroup.Enabled = enable;
        }

        private void ClearValidationLabels()
        {
            lblValidatorDefaultId.Text = string.Empty;
            lblValidatorGroupId.Text = string.Empty;
            lblValidatorGroupName.Text = string.Empty;
        }

        private string RemoveInvalidCharactersFromID(string inputId)
        {
            return Regex.Replace(inputId, "[^A-Z0-9]+", "", RegexOptions.Compiled);
        }

        private string ModifyInputToPattern(string text, string pattern)
        {
            return Regex.Replace(text, pattern, "", RegexOptions.Compiled);
        }

        private bool IsGroupIdGiven(string inputId)
        {
            bool given = inputId != string.Empty;

            if (!given)
                lblValidatorGroupId.Text = "• Blank";
            else
                lblValidatorGroupId.Text = string.Empty;

            return given;
        }

        private bool IsGroupIdUnique(string inputId)
        {
            bool unique;
            List<string> groupsId = Data.GetSizeGroupsId();

            switch (Mode)
            {
                case EntryMode.New:
                    unique = !groupsId.Contains(inputId);

                    if (!unique)
                    {
                        lblValidatorGroupId.Text = "• Duplicate";

                        if (inputId.Length >= txtGroupID.MaxLength)
                            txtGroupID.SelectAll();

                        txtGroupID.Focus();
                    }
                    else
                        lblValidatorGroupId.Text = string.Empty;

                    return unique;

                case EntryMode.Edit:
                    bool duplicate = groupsId.Contains(inputId);
                    bool different = inputId != drafter.DraftSizeGroup.ID;

                    if (duplicate)
                    {
                        if (different)
                        {
                            lblValidatorGroupId.Text = "• Duplicate";

                            if (inputId.Length >= txtGroupID.MaxLength)
                                txtGroupID.SelectAll();

                            txtGroupID.Focus();
                        }
                        else
                        {
                            lblValidatorGroupId.Text = "• Unchanged";
                        }

                    }
                    else
                        lblValidatorGroupId.Text = string.Empty;

                    return !duplicate || !different;

                default:
                    return false;
            }

        }

        private bool IsGroupNameGiven(string inputName)
        {
            bool given = inputName != string.Empty;

            if (!given)
                lblValidatorGroupName.Text = "• Blank";
            else
                lblValidatorGroupName.Text = string.Empty;

            return given;
        }

        private bool IsGroupDefaultListGiven(string text)
        {
            bool given = text != string.Empty;

            if (given)
            {
                lblValidatorDefaultId.Text = string.Empty;
            }
            else
            {
                lblValidatorDefaultId.Text = "• Blank";
            }

            return given;
        }

        private void EnableGroupContainerUI()
        {
            grpGroupMetadata.Enabled = true;
            grpGroupData.Enabled = true;
        }

        private void DisableGroupContainerUI()
        {
            grpGroupMetadata.Enabled = false;
            grpGroupData.Enabled = false;
        }

        private void EnableEditRemoveUI()
        {
            EditRemoveUIEnabledStatus(true);
        }

        private void DisableEditRemoveUI()
        {
            EditRemoveUIEnabledStatus(false);
        }

        private void EnableSizeGroupSelection()
        {
            dgvGroups.Enabled = true;
        }

        private void DisableSizeGroupSelection()
        {
            dgvGroups.ClearSelection();
            dgvGroups.Enabled = false;
        }

        /// <summary>
        /// Disable the New, Edit and Remove buttons.
        /// </summary>
        private void DisableGroupModifyUI()
        {
            btnNewGroup.Enabled = false;
            btnEditGroup.Enabled = false;
            btnRemoveGroup.Enabled = false;
        }

        private void EnableGroupAcceptUI()
        {
            btnAccept.Enabled = true;
        }

        private void DisableGroupAcceptUI()
        {
            btnAccept.Enabled = false;
        }

        private void EnableGroupMetadataEntryUI()
        {
            txtGroupID.ReadOnly = false;
            txtGroupName.ReadOnly = false;
        }

        private void DisableGroupMetadataEntryUI()
        {
            txtGroupID.ReadOnly = true;
            txtGroupName.ReadOnly = true;

            // Clear validation labels
            lblValidatorGroupId.Text = string.Empty;
            lblValidatorGroupName.Text = string.Empty;
        }

        private void EnableGroupDataUI()
        {
            cboDefaultID.Enabled = true;

            chkAltList.Visible = true;
            chkAltList.Checked = false;

            // Disable alt list if default Id is empty
            if (Mode == EntryMode.New)
            {
                DisableAltListSelection();
            }

            chkCustomSize.Visible = true;
            chkCustomSize.Checked = false;
        }

        private void DisableGroupDataUI()
        {
            cboDefaultID.Enabled = false;
            chkAltList.Visible = false;
            chkCustomSize.Visible = false;
            btnClearAltList.Enabled = false;
            DisableAltListUI();
            DisableCustomSizeUI();
        }

        /// <summary>
        /// Show the Accept and Cancel buttons.
        /// </summary>
        private void ShowGroupReviewUI()
        {
            btnAccept.Visible = true;
            btnCancel.Visible = true;
        }

        /// <summary>
        /// Hide the Accept and Cancel buttons.
        /// </summary>
        private void HideGroupReviewUI()
        {
            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAccept.Enabled = false;
        }

        private void ClearGroupsGrid()
        {
            dgvGroups.DataSource = null;
        }

        private void ClearGroupMetadataUI()
        {
            if (txtGroupID.Text == string.Empty)
            {
                txtGroupID_TextChanged(txtGroupID, null);
            }
            else
            {
                txtGroupID.Clear();
            }

            if (txtGroupName.Text == string.Empty)
            {
                txtGroupName_TextChanged(txtGroupName, null);
            }
            else
            {
                txtGroupName.Clear();
            }
        }

        private void ClearGroupDataUI()
        {
            //cboDefaultID.FormattingEnabled = true;
            if (cboDefaultID.SelectedIndex == -1)
            {
                cboDefaultID_SelectedIndexChanged(cboDefaultID, null);
            }
            else
            {
                cboDefaultID.SelectedIndex = -1;
            }

            cboCustomSizeID.SelectedIndex = -1;
            lstAltListIDs.DataSource = null;
        }

        private void CheckAltListStatus()
        {
            if (Mode != EntryMode.View)
            {
                uiControl.InputAltListRequired = chkAltList.Checked;
            }

            if (Mode == EntryMode.View)
                return;

            if (chkAltList.Checked)
            {
                drafter.HasAltList = true;
                EnableAltListUI();
            }
            else
            {
                drafter.HasAltList = false;
                DisableAltListUI();
            }
            CheckDraftValidity();
        }

        private void SetCustomSizeStatus()
        {
            if (Mode == EntryMode.View)
                return;

            if (chkCustomSize.Checked)
            {
                drafter.HasCustomSize = true;
                EnableCustomSizeUI();
            }
            else
            {
                drafter.HasCustomSize = false;
                DisableCustomSizeUI();
            }
            CheckDraftValidity();
        }

        private void EnableAltListSelection()
        {
            chkAltList.Enabled = true;
        }

        private void DisableAltListSelection()
        {
            chkAltList.Enabled = false;
        }

        private void EnableAltListUI()
        {
            lblListsID.Enabled = true;
            lstAltListIDs.Enabled = true;
            btnModifyAltList.Enabled = true;
        }

        private void DisableAltListUI()
        {
            lblListsID.Enabled = false;
            lstAltListIDs.Enabled = false;
            btnModifyAltList.Enabled = false;
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

        private void ShowListSelector()
        {
            AltListSelector listSelector;

            List<BasicListView> sizeListExcluded =
                Data.GetSizesExclude(drafter.groupDefaultListID);

            if (drafter.groupAltList == null)
            {
                listSelector = new AltListSelector(sizeListExcluded);
            }
            else
            {
                listSelector = new AltListSelector(sizeListExcluded, drafter.groupAltList);
            }

            if (listSelector.ShowDialog() == DialogResult.OK)
            {
                switch (Mode)
                {
                    case EntryMode.New:
                        drafter.groupAltList = listSelector.OutputAltSizesIDs;
                        lstAltListIDs.DataSource = drafter.groupAltList;
                        CheckDraftValidity();
                        break;

                    case EntryMode.Edit:
                        drafter.groupAltList = listSelector.OutputAltSizesIDs;
                        lstAltListIDs.DataSource = drafter.groupAltList;
                        CheckDraftValidity();
                        break;
                }

                // Enable the Clear button
                btnClearAltList.Enabled = true;

                // Exclude Alt Size ID List from the default Size ID selector
                skipEvents = true;
                cboDefaultID.DataSource = Data.GetSizesIdExclude(drafter.groupAltList);
                skipEvents = false;
                cboDefaultID.Text = drafter.groupDefaultListID;
            }
        }

        private void ClearAltSizesIdList()
        {
            if (drafter.groupAltList != null)
            {
                drafter.groupAltList = null;
                drafter.HasAltList = false;

                chkAltList.Checked = false;
                lstAltListIDs.DataSource = null;

                btnClearAltList.Enabled = false;

                // Re-bind the size ID selector with the full list
                skipEvents = true;
                cboDefaultID.DataSource = Data.GetFieldIds(FieldType.SIZE).ToList();
                skipEvents = false;
                cboDefaultID.Text = drafter.groupDefaultListID;
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        private void SizeGroupEditor_Load(object sender, EventArgs e) => PostLoading();
        private void dgvGroups_DataSourceChanged(object sender, EventArgs e)
        {
            if (Mode == EntryMode.View)
            {
                if (dgvGroups.DataSource == null)
                {
                    btnEditGroup.Enabled = false;
                    btnRemoveGroup.Enabled = false;
                }
                else
                {
                    btnEditGroup.Enabled = true;
                    btnRemoveGroup.Enabled = true;
                }
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
                EditGroup();
            }
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            //NewGroup();
            AddNewSizeGroup();
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            EditGroup();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            //SaveDraftGroup();
            uiControl.AddNew();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelDrafting();
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            RemoveGroup();
        }

        private void chkAltList_CheckedChanged(object sender, EventArgs e)
        {
            CheckAltListStatus();
        }

        private void chkCustomSize_CheckedChanged(object sender, EventArgs e)
        {
            SetCustomSizeStatus();
        }

        private void txtGroupID_TextChanged(object sender, EventArgs e)
        {
            //ChangeGroupID();
            InputSizeGroupID();
        }
        
        private void txtGroupID_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            //ChangeGroupName();
            InputSizeGroupName();
        }

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            txtGroupName.Text = txtGroupName.Text.Trim();
        }

        private void cboDefaultID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipEvents)
                return;

            InputDefaultID();

            // display selected size list entries
            DisplaySelectedListEntries();
        }
        
        private void cboCustomSizeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCustomSizeID();
        }

        private void cboCustomSizeID_TextChanged(object sender, EventArgs e)
        {
            ChangeCustomSizeID();
        }

        private void btnModifyAltList_Click(object sender, EventArgs e)
        {
            ShowListSelector();
        }

        private void btnClearAltList_Click(object sender, EventArgs e)
        {
            ClearAltSizesIdList();
        }

        private void msmiSaveFile_Click(object sender, EventArgs e)
        {
            SaveToSource();
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            Close();
        }

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
    }


}