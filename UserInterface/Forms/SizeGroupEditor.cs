namespace UserInterface.Forms
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using Enums;
    using UserInterface.Operation;

    using MultiColumnCombcs;

    public partial class SizeGroupEditor : Form
    {
        private EntryMode _mode = EntryMode.View;
        private EntryMode Mode
        {
            get => _mode;
            set
            {
                _mode = value;
                if (value == EntryMode.View)
                {
                    tsmiSaveFile.Enabled = true;
                }
                else
                {
                    tsmiSaveFile.Enabled = false;
                }
            }
        }

        #region Field Variables
        private List<SizeList> sizesList;
        private List<SizeGroup> sizeGroups;
        private List<string> sizeGroupsIDs;
        private List<string> custSizesList;

        private SizeGroupDraft draft;

        private bool draft_idGiven = false;
        private bool draft_nameGiven = false;
        private bool draft_defListGiven = false;

        private bool skipEvents = false;
        private int groupSelectionIndex;
        #endregion

        public SizeGroupEditor()
        {
            InitializeComponent();
        }

        #region File Management
        private void SaveXmlFile()
        {
            XDataDocuments.Save(Program.xDataDocs.SizeGroups, Program.fpp.SizeGroups);
            //DataService.UpdateSizeGroups(Program.xDataDocs.SizeGroups);
            DataService.UpdateSizeGroups();
        }
        #endregion

        #region XML Serialization / Deserialization
        private List<SizeGroup> ReadSizeGroups()
        {
            return
                (from sg in Program.xDataDocs.SizeGroups.Descendants("group")
                 let list = sg.Element("altLists").HasElements ?
                 sg.Element("altLists").Elements("listID").Select(l => l.Value).ToList() : null
                 let customId = sg.Element("customSizeDataID").Value
                 select new SizeGroup()
                 {
                     ID = sg.Attribute("groupID").Value,
                     Name = sg.Attribute("groupName").Value,
                     DefaultListID = sg.Element("defaultListID").Value,
                     AltIdList = list,
                     CustomSize = customId != string.Empty ? customId : null
                 }).ToList();
        }

        private List<SizeList> ReadSizes()
        {
            return
                (from sz in Program.xDataDocs.Sizes.Descendants("sizeList")
                 select new SizeList()
                 {
                     ID = sz.Attribute("listID").Value,
                     ListName = sz.Attribute("name").Value,
                     Sizes = sz.Descendants("size").Select(item => item.Value).ToList()
                 }
                    ).ToList();
        }

        private List<string> ReadCustomSizes()
        {
            return
                (from csz in Program.xDataDocs.CustomSizes.Descendants("customSizeData")
                 select csz.Attribute("dataId").Value)
                .ToList();
        }

        private XElement SerializeSavedGroup()
        {
            //Create XElement
            XElement xAltList = new XElement("altLists");
            draft.SavedGroup.AltIdList?.ForEach(id => xAltList.Add(new XElement("listID", id)));

            XElement xGroup =
                new XElement("group",
                new XAttribute("groupID", draft.SavedGroup.ID),
                new XAttribute("groupName", draft.SavedGroup.Name),
                    new XElement("defaultListID", draft.SavedGroup.DefaultListID),
                    xAltList,
                    new XElement("customSizeDataID", draft.SavedGroup.CustomSize));
            return xGroup;
        }
        #endregion

        private void ReadSizeGroupsIDs()
        {
            sizeGroupsIDs = sizeGroups.Select(id => id.ID).ToList();
        }

        private void UpdateSizeGroupsIDs()
        {
            ReadSizeGroupsIDs();
        }

        #region Mode Handling
        private void EnterViewMode() => Mode = EntryMode.View;

        private void EnterNewMode() => Mode = EntryMode.New;

        private void EnterEditMode() => Mode = EntryMode.Edit;
        #endregion

        private void PostLoading()
        {
            sizeGroups = ReadSizeGroups();
            sizesList = ReadSizes();
            custSizesList = ReadCustomSizes();

            ReadSizeGroupsIDs();
            BindSizeSelectors();
            ListSizeGroups();

            EnableGroupModifyUI();
        }

        private void ListSizeGroups()
        {
            dgvGroups.DataSource = sizeGroups;
            ResizeGrid();
        }

        private void RefreshSizeGroups()
        {
            object dataSource = dgvGroups.DataSource;
            dgvGroups.DataSource = null;
            if (dataSource != null)
            {
                dgvGroups.DataSource = dataSource;
            }
            ResizeGrid();
        }

        private void ResizeGrid()
        {
            dgvGroups.AutoResizeColumns();
            dgvGroups.AutoResizeRows();
        }

        private void BindSizeSelectors()
        {
            cboDefaultID.DataSource = sizesList.Select(id => id.ID).ToList();
            cboCustomSizeID.DataSource = custSizesList;
            cboDefaultID.SelectedIndex = -1;
            
        }

        private void DisplaySelectedGroupData(string groupId)
        {
            SizeGroup sGroup = GetSizeGroup(groupId);

            txtGroupID.Text = groupId;
            txtGroupName.Text = sGroup.Name;
            cboDefaultID.Text = sGroup.DefaultListID;

            if (sGroup.AltListsCount > 0)
            {
                lstAltListIDs.DataSource = sGroup.AltIdList;
                lstAltListIDs.SelectedIndex = -1;
            }
            else
            {
                lstAltListIDs.DataSource = null;
            }

            if (sGroup.CustomSize != null)
            {
                cboCustomSizeID.Text = sGroup.CustomSize;
            }
            else
            {
                cboCustomSizeID.SelectedIndex = -1;
            }
        }

        private void ChangeGroupID()
        {
            if (skipEvents)
                return;

            //string inputId = txtGroupID.Text;
            if (Mode != EntryMode.View)
            {
                // Remove invalid characters in case user CnP
                string inputId = ModifyInputToPattern(txtGroupID.Text, "[^A-Z0-9]+"); //RemoveInvalidCharactersFromID(inputId);

                skipEvents = true;
                txtGroupID.Text = inputId;
                skipEvents = false;

                draft_idGiven = IsGroupIdGiven(inputId) && IsGroupIdUnique(inputId);

                if (draft_idGiven)
                    draft.GroupID = inputId;

                CheckDraftValidity();
            }
        }

        private void ChangeGroupName()
        {
            if (skipEvents)
                return;

            //string inputName = txtGroupName.Text;
            if (Mode != EntryMode.View)
            {
                string inputName = ModifyInputToPattern(txtGroupName.Text, "[^A-Za-z0-9 _.-]+");

                skipEvents = true;
                txtGroupName.Text = inputName;
                skipEvents = false;

                draft_nameGiven = IsGroupNameGiven(inputName);

                if (draft_nameGiven)
                    draft.GroupName = inputName.Trim();

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
                    draft.DefaultListID = cboDefaultID.Text;
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
                draft.CustomSizeID = cboCustomSizeID.Text;
                CheckDraftValidity();
            }
        }

        private void NewGroup()
        {
            // Create draft objects
            draft = new SizeGroupDraft();

            // Set Entry Mode
            EnterNewMode();

            // Setup User Interface
            NewEditSetupUI();

            // Clear existing data for new input
            ClearGroupMetadataUI();
            ClearGroupDataUI();
            txtGroupID.Focus();
            //cboDefaultID.SelectedIndex = -1;
        }

        private void EditGroup()
        {
            // Get draft objects
            if (dgvGroups.SelectedRows.Count <= 0)
                return;

            DataGridViewRow row = dgvGroups.SelectedRows[0];

            // Format the row being edited
            row.DefaultCellStyle.BackColor = Color.Aquamarine;
            row.DefaultCellStyle.ForeColor = Color.DarkGray;

            string groupId = row.Cells[0].Value.ToString();

            draft = new SizeGroupDraft(GetSizeGroup(groupId))
            {
                DraftGroupXElement = GetSizeGroupXElement(groupId)
            };

            draft_idGiven = true;
            draft_nameGiven = true;
            draft_defListGiven = true;

            EnterEditMode();

            // Setup User Interface
            NewEditSetupUI();

            // Set checkbox for Alt List & Custom Size, if any.
            if (draft.AltList != null)
            {
                chkAltList.Checked = true;
                btnClearAltList.Enabled = true;
            }
            else
            {
                chkAltList.Checked = false;
                btnClearAltList.Enabled = false;
            }
            //chkAltList.Checked = draft.AltList?.Count > 0;
            chkCustomSize.Checked = draft.CustomSizeID != null;
        }

        private void NewEditSetupUI()
        {
            DisableSizeGroupSelection();
            DisableGroupModifyUI();
            ShowGroupReviewUI();
            //DisableGroupAcceptUI();
            EnableGroupMetadataEntryUI();
            EnableGroupDataUI();
            CheckSizeGroupList();

            //btnAccept.Enabled = false;
        }

        private void CancelDraftingSetupUI()
        {
            EnableSizeGroupSelection();
            EnableGroupModifyUI();
            HideGroupReviewUI();
            DisableGroupMetadataEntryUI();
            DisableGroupDataUI();
            //CheckAltListStatus();
            //SetCustomSizeStatus();
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
                draftDataModified = draft.IsModified();
            }

            btnAccept.Enabled = requiredDataGiven && draftDataModified;
        }

        private void SaveDraftGroup()
        {
            // Save Draft Object
            draft.SaveData();

            if (Mode == EntryMode.New)
            {
                //SaveNewGroup();
                sizeGroups.Add(draft.SavedGroup);
                Program.xDataDocs.SizeGroups.Root.Add(draft.DraftGroupXElement);
            }

            // Reset UI and settings
            EnterViewMode();

            // Clear draft object
            draft = null;

            // Update SizeGroup ID List
            UpdateSizeGroupsIDs();

            // Refresh or Set Size Groups List
            if (sizeGroups.Count > 1)
            {
                RefreshSizeGroups();
            }
            else
            {
                ListSizeGroups();
            }

            // Reset UI
            EnableSizeGroupSelection();
            EnableGroupModifyUI();
            HideGroupReviewUI();
            //ClearGroupMetadataUI();
            //ClearGroupDataUI();
            DisableGroupMetadataEntryUI();
            DisableGroupDataUI();
        }

        private void CancelDrafting()
        {
            // Clear draft object
            draft = null;

            EnterViewMode();

            // Setup UI
            CancelDraftingSetupUI();

            // Reset DGV Style
            RefreshSizeGroups();
        }

        private void RemoveGroup()
        {
            // Get draft objects
            if (dgvGroups.SelectedRows.Count <= 0)
                return;

            DataGridViewRow row = dgvGroups.SelectedRows[0];

            //Save Selection
            groupSelectionIndex = SaveDataGridViewSelection(dgvGroups);

            string groupId = row.Cells[0].Value.ToString();
            sizeGroups.Remove(GetSizeGroup(groupId));

            // Check sizeGroups count
            CheckSizeGroupList();
            UpdateSizeGroupsIDs();
            RefreshSizeGroups();

            //Restore Selection
            RestoreDataGridViewSelection(dgvGroups, groupSelectionIndex);

            //Set the Groups DataGridView focus
            dgvGroups.Focus();
        }

        private void CheckSizeGroupList()
        {
            bool emptySizeGroupList = sizeGroups.Count <= 0;
            //bool singleSizeGroupList = sizeGroups.Count == 1;

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

        #region Getters

        private SizeGroup GetSizeGroup(string groupId)
        {
            return sizeGroups.Find(i => i.ID == groupId);
        }

        private XElement GetSizeGroupXElement(string groupId)
        {
            return Program.xDataDocs.SizeGroups.Descendants("group")
                .Where(g => g.Attribute("groupID").Value == groupId)
                .FirstOrDefault();
        }

        private List<SizeList> GetSizeListExcludeId(string excludeId)
        {
            return sizesList.Where(s => s.ID != excludeId).ToList();
        }

        private List<string> GetSizeListExcludeList(List<string> exList)
        {
            return
                (from x in sizesList where !exList.Contains(x.ID) select x.ID).ToList();
        }
        #endregion

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

            switch (Mode)
            {
                case EntryMode.New:
                    unique = !sizeGroupsIDs.Contains(inputId);

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
                    bool duplicate = sizeGroupsIDs.Contains(inputId) /*|| inputId == draft.SizeGroup.ID*/;
                    bool different = inputId != draft.SavedGroup.ID;

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
            //btnRemoveGroup.Enabled = true;
            //btnEditGroup.Enabled = true;
            EditRemoveUIEnabledStatus(true);
        }

        private void DisableEditRemoveUI()
        {
            //btnRemoveGroup.Enabled = false;
            //btnEditGroup.Enabled = false;
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
        /// Enable the New, Edit and Remove buttons.
        /// </summary>
        private void EnableGroupModifyUI()
        {
            btnNewGroup.Enabled = true;
            btnEditGroup.Enabled = true;
            btnRemoveGroup.Enabled = true;
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

            // Clear validator labels
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
            if (Mode == EntryMode.View)
                return;

            if (chkAltList.Checked)
            {
                draft.HasAltList = true;
                EnableAltListUI();
            }
            else
            {
                //if (draft != null)
                draft.HasAltList = false;
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
                draft.HasCustomSize = true;
                EnableCustomSizeUI();
            }
            else
            {
                //if (draft != null)
                draft.HasCustomSize = false;
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
            //btnClearAltList.Enabled = true;
        }

        private void DisableAltListUI()
        {
            lblListsID.Enabled = false;
            lstAltListIDs.Enabled = false;
            btnModifyAltList.Enabled = false;
            //btnClearAltList.Enabled = false;
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
            List<SizeList> sizeListExcluded = GetSizeListExcludeId(draft.DefaultListID);

            if (draft.AltList == null)
            {
                listSelector = new AltListSelector(sizeListExcluded);
            }
            else
            {
                listSelector = new AltListSelector(sizeListExcluded, draft.AltList);
            }

            if (listSelector.ShowDialog() == DialogResult.OK)
            {
                switch (Mode)
                {
                    case EntryMode.New:
                        draft.AltList = listSelector.OutputAltSizesIDs;
                        lstAltListIDs.DataSource = draft.AltList;
                        CheckDraftValidity();
                        break;

                    case EntryMode.Edit:
                        draft.AltList = listSelector.OutputAltSizesIDs;
                        lstAltListIDs.DataSource = draft.AltList;
                        CheckDraftValidity();
                        break;
                }

                // Enable the Clear button
                btnClearAltList.Enabled = true;

                // Exclude Alt Size ID List from the default Size ID selector
                skipEvents = true;
                cboDefaultID.DataSource = GetSizeListExcludeList(draft.AltList);
                skipEvents = false;
                cboDefaultID.Text = draft.DefaultListID;
            }
        }

        private void ClearAltSizesIdList()
        {
            if (draft.AltList != null)
            {
                draft.AltList = null;
                draft.HasAltList = false;

                chkAltList.Checked = false;
                lstAltListIDs.DataSource = null;

                btnClearAltList.Enabled = false;

                // Re-bind the size ID selector with the full list
                skipEvents = true;
                cboDefaultID.DataSource = sizesList.Select(id => id.ID).ToList();
                skipEvents = false;
                cboDefaultID.Text = draft.DefaultListID;
            }
        }

        #region Selection Handling

        private void SaveSizeGroupSelectionPosition(int itemsCount)
        {
            int selectedIndex = dgvGroups.SelectedRows[0].Index;
            if (selectedIndex == itemsCount - 1)
                groupSelectionIndex = itemsCount - 2;
            else
            {
                groupSelectionIndex = selectedIndex;
            }
        }

        private int SaveDataGridViewSelection(DataGridView dgv)
        {
            if (dgv.DataSource == null)
                return -1;

            int itemsCount = dgv.Rows.Count;
            int selectedIndex = dgv.SelectedRows[0].Index;

            if (selectedIndex == itemsCount - 1)
            {
                return itemsCount - 2;
            }
            else
            {
                return selectedIndex;
            }
        }

        private void RestoreDataGridViewSelection(DataGridView dgv, int selectionIndex)
        {
            if (selectionIndex > -1)
            {
                dgv.CurrentCell = dgv.Rows[selectionIndex].Cells[0];
                dgv.Rows[selectionIndex].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = selectionIndex;
            }
        }

        #endregion

#pragma warning disable IDE1006 // Naming Styles
        private void SizeGroupEditor_Load(object sender, EventArgs e)
        {
            // TEST
            //Runtime.Test.AutoLoad(LoadXmlFile, "SizeGroups.xml");

            //
            PostLoading();
            //

            //dgvGroups.Rows[0].Selected = true;
            //dgvGroups.Select();
            //cboDefaultID.SelectedIndex = -1;
            //button2.PerformClick();
        }

        private void dgvGroups_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGroups.DataSource == null || dgvGroups.SelectedRows.Count <= 0)
                return;

            DataGridViewRow row = dgvGroups.SelectedRows[0];

            if (row == null)
                return;

            string id = (string)row.Cells[0].Value;
            DisplaySelectedGroupData(id);
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
            NewGroup();
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            EditGroup();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SaveDraftGroup();
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
            ChangeGroupID();
        }

        private void txtGroupID_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            ChangeGroupName();
        }

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            txtGroupName.Text = txtGroupName.Text.Trim();
        }

        private void cboDefaultID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipEvents)
                return;
            //if (cboDefaultID.SelectedIndex != -1)
            //{

            // Display selected size list entries
            listBox1.DataSource = DataService.FieldListGetEntries(FieldType.SIZE, cboDefaultID.Text);

            ChangeDefaultListID();
            //}
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
            SaveXmlFile();
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

#pragma warning restore IDE1006 // Naming Styles
    }
}