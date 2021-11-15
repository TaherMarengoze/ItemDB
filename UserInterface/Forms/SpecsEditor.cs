using CoreLibrary.Enums;
using Drafting;
using Shared.UI;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UserInterface.Forms
{
    public partial class SpecsEditor : Form
    {
        private EntryMode _specsMode = EntryMode.View;
        private EntryMode SpecsMode
        {
            get => _specsMode;
            set
            {
                _specsMode = value;

                if (_specsMode == EntryMode.View)
                {
                    //if (SpecsItemMode != EntryMode.View)
                    SpecsItemMode = EntryMode.View;

                    // enable Save UI
                    tsmiSaveFile.Enabled = true;

                    // enable Specs List Selection UI
                    lbxSpecs.SelectionMode = SelectionMode.One;

                    // enable Specs Add, Edit & Remove buttons
                    btnNewSpecs.Enabled = true;
                    btnEditSpecs.Enabled = true;
                    btnRemoveSpecs.Enabled = true;

                    // disable Specs Accept button
                    btnAccept.Enabled = false;
                    // btnCancel: always enabled

                    // hide Specs Review Buttons (Accept & Cancel)
                    btnAccept.Visible = false;
                    btnCancel.Visible = false;

                    // disable SpecsItem Edit & Remove buttons
                    // btnSiAdd: always enabled
                    btnSiEdit.Enabled = false;
                    btnSiRemove.Enabled = false;

                    // hide SpecsItem Add, Edit & Remove buttons
                    btnSiAdd.Visible = false;
                    btnSiEdit.Visible = false;
                    btnSiRemove.Visible = false;

                    chkSpecConfirmRemove.Visible = false;

                    // disable SpecsItem Cancel button
                    btnSiAccept.Enabled = false;
                    // btnSiCancel: always enabled

                    // hide SpecsItem review Buttons (Accept & Cancel)
                    btnSiAccept.Visible = false;
                    btnSiCancel.Visible = false;

                    // disable Specs data entry UI
                    txtSpecsID.ReadOnly = true;
                    txtSpecsName.ReadOnly = true;
                    txtSpecsPattern.ReadOnly = true;

                    // reset SpecsID field color
                    txtSpecsID.BackColor = SystemColors.Control;

                    // reset SpecsID validation label
                    lblSpecsIdValidator.Text = string.Empty;
                }
                else
                {
                    // disable Save UI
                    tsmiSaveFile.Enabled = false;

                    // save the position of selection
                    //SaveSelection(lbxSpecs);

                    // disable Specs List Selection UI
                    lbxSpecs.SelectionMode = SelectionMode.None;

                    // disable Specs Add, Edit & Remove buttons
                    btnNewSpecs.Enabled = false;
                    btnEditSpecs.Enabled = false;
                    btnRemoveSpecs.Enabled = false;

                    // show Specs Review Buttons (Accept & Cancel)
                    btnAccept.Visible = true;
                    btnCancel.Visible = true;

                    // enable Specs Review Buttons
                    btnAccept.Enabled = false;
                    // btnCancel is always enabled

                    // enable Specs data entry UI
                    txtSpecsID.ReadOnly = false;
                    txtSpecsName.ReadOnly = false;
                    txtSpecsPattern.ReadOnly = false;

                    // show SpecsItem Add, Edit & Remove buttons
                    btnSiAdd.Visible = true;
                    btnSiEdit.Visible = true;
                    btnSiRemove.Visible = true;

                    // show SpecsItem remove confirmation check box
                    chkSpecConfirmRemove.Visible = true;
                }
            }
        }

        private EntryMode _specsItemMode = EntryMode.View;
        private EntryMode SpecsItemMode
        {
            get => _specsItemMode;
            set
            {
                if (_specsItemMode == EntryMode.View)
                {
                    if (value == EntryMode.View)
                    {
                        //throw new Exception("Mode is already set to View.");
                    }
                    else
                    {
                        // set mode to non-view
                        _specsItemMode = value;

                        /* DO NON-VIEW MODE STUFF */

                        // disable SpecsItem Add, Edit & Remove buttons
                        dgvSpec.Enabled = false;
                        btnSiAdd.Enabled = false;
                        btnSiEdit.Enabled = false;
                        btnSiRemove.Enabled = false;

                        // show SpecsItem review buttons (Accept & Cancel)
                        btnSiAccept.Visible = true;
                        btnSiCancel.Visible = true;

                        // disable Accept button
                        btnSiAccept.Enabled = false;
                        // btnSiCancel : always enabled

                        // disable Specs fields entry
                        txtSpecsID.ReadOnly = true;
                        txtSpecsName.ReadOnly = true;
                        txtSpecsPattern.ReadOnly = true;

                        // reset SpecsID field color
                        txtSpecsID.BackColor = SystemColors.Control;

                        // enable SpecsItem data entry UI
                        txtSiName.ReadOnly = false;
                        txtSiValuePattern.ReadOnly = false;

                        btnSiDefaultVal.Visible = true;
                        btnSiInsertVal.Visible = true;

                        // enable SpecsItem type UI
                        grpSpecType.Enabled = true;
                    }
                }
                else // New or Edit ode
                {
                    if (value == EntryMode.View)
                    {
                        // set mode to view
                        _specsItemMode = value;

                        /* DO VIEW MODE STUFF THAT SHOULD BE EXECUTED ONCE */

                        // enable SpecsItem Add, Edit & Remove buttons
                        dgvSpec.Enabled = true;
                        btnSiAdd.Enabled = true;
                        btnSiEdit.Enabled = true;
                        btnSiRemove.Enabled = true;

                        // hide SpecsItem review buttons (Accept & Cancel)
                        btnSiAccept.Visible = false;
                        btnSiCancel.Visible = false;

                        // enable Specs fields entry
                        txtSpecsID.ReadOnly = false;
                        txtSpecsName.ReadOnly = false;
                        txtSpecsPattern.ReadOnly = false;

                        // reset SpecsID field color
                        txtSpecsID.BackColor = specsIdFieldBackColor;

                        // disable SpecsItem data entry UI
                        txtSiName.ReadOnly = true;
                        txtSiValuePattern.ReadOnly = true;

                        btnSiDefaultVal.Visible = false;
                        btnSiInsertVal.Visible = false;

                        // disable SpecsItem type UI
                        grpSpecType.Enabled = false;

                        // enable List Type grid UI
                        grpListEntries.Enabled = true;

                        // disable List Type SpecsItem UI
                        DisableListEntryModifyUI();
                    }
                    else
                    {
                        throw new Exception("Unable to switch between non-view modes.");
                    }
                }

                //if (_specsItemMode == EntryMode.View & value != EntryMode.View)
                //{
                //    // set mode to non-view
                //    _specsItemMode = value;

                //    /* DO NON-VIEW MODE STUFF */

                //    // disable SpecsItem Add, Edit & Remove buttons
                //    dgvSpec.Enabled = false;
                //    btnSiAdd.Enabled = false;
                //    btnSiEdit.Enabled = false;
                //    btnSiRemove.Enabled = false;

                //    // show SpecsItem review buttons (Accept & Cancel)
                //    btnSiAccept.Visible = true;
                //    btnSiCancel.Visible = true;

                //    // disable Accept button
                //    btnSiAccept.Enabled = false;
                //    // btnSiCancel : always enabled

                //    // disable Specs fields entry
                //    txtSpecsID.ReadOnly = true;
                //    txtSpecsName.ReadOnly = true;
                //    txtSpecsPattern.ReadOnly = true;

                //    // reset SpecsID field color
                //    txtSpecsID.BackColor = SystemColors.Control;

                //    // enable SpecsItem data entry UI
                //    txtSiName.ReadOnly = false;
                //    txtSiValuePattern.ReadOnly = false;

                //    btnSiDefaultVal.Visible = true;
                //    btnSiInsertVal.Visible = true;

                //    // enable SpecsItem type UI
                //    grpSpecType.Enabled = true;

                //}
                //else if (_specsItemMode != EntryMode.View & value == EntryMode.View)
                //{
                //    // set mode to view
                //    _specsItemMode = value;

                //    /* DO VIEW MODE STUFF THAT SHOULD BE EXECUTED ONCE */

                //    // enable SpecsItem Add, Edit & Remove buttons
                //    dgvSpec.Enabled = true;
                //    btnSiAdd.Enabled = true;
                //    btnSiEdit.Enabled = true;
                //    btnSiRemove.Enabled = true;

                //    // hide SpecsItem review buttons (Accept & Cancel)
                //    btnSiAccept.Visible = false;
                //    btnSiCancel.Visible = false;

                //    // enable Specs fields entry
                //    txtSpecsID.ReadOnly = false;
                //    txtSpecsName.ReadOnly = false;
                //    txtSpecsPattern.ReadOnly = false;

                //    // reset SpecsID field color
                //    //txtSpecsID.BackColor = SystemColors.Window;

                //    // disable SpecsItem data entry UI
                //    txtSiName.ReadOnly = true;
                //    txtSiValuePattern.ReadOnly = true;

                //    btnSiDefaultVal.Visible = false;
                //    btnSiInsertVal.Visible = false;

                //    // disable SpecsItem type UI
                //    grpSpecType.Enabled = false;

                //    // enable List Type grid UI
                //    grpListEntries.Enabled = true;

                //    // disable List Type SpecsItem UI
                //    DisableListEntryModifyUI();
                //}
            }
        }

        private SpecsDrafter drafter;

        //private int specsSelectionIndex = 0;
        //private int specSelectionIndex;
        private int entrySelectionIndex;

        private Color specsIdFieldBackColor;

        public SpecsEditor()
        {
            InitializeComponent();

            drafter = new SpecsDrafter();
            drafter.OnSpecsValidityChange += Drafter_OnSpecsValidityChange;
            drafter.OnSpecsItemValidityChange += Drafter_OnSpecItemValidityChange;
            drafter.OnSpecsIdValidityChange += Drafter_OnSpecsIdValidityChange;
            drafter.OnSpecsItemPatternChange += Drafter_OnSpecsItemPatternChange;

            drafter.OnSpecsSet += Drafter_OnSpecsSet;
            //drafter.OnSpecsAdd += Drafter_OnSpecsAdd;
            //drafter.OnSpecsUpdate += Drafter_OnSpecsUpdate;
            drafter.OnSpecsCancel += Drafter_OnSpecsCancel;
            drafter.OnSpecsRemove += Drafter_OnSpecsRemove;


            drafter.OnSpecsItemSet += Drafter_OnSpecsItemSet;
            drafter.OnSpecsItemCancel += Drafter_OnSpecsItemCancel;
            drafter.OnSpecsItemRemove += Drafter_OnSpecsItemRemove;

        }
        
        private void Drafter_OnSpecsValidityChange(object sender, bool specsReady)
        {
            if (SpecsMode != EntryMode.View)
            {
                if (specsReady == true)
                {
                    btnAccept.Enabled = true;
                }
                else
                {
                    btnAccept.Enabled = false;
                }
            }
        }

        private void Drafter_OnSpecItemValidityChange(object sender, bool specItemReady)
        {
            btnSiAccept.Enabled = specItemReady;
        }

        private void Drafter_OnSpecsIdValidityChange(object sender, SpecsDrafter.ValidityStatus status)
        {
            switch (status)
            {
                case SpecsDrafter.ValidityStatus.Valid:
                    ResetIdValidityInfo();
                    lblSpecsIdValidator.Text = string.Empty;
                    txtSpecsID.BackColor = SystemColors.Window;
                    break;

                case SpecsDrafter.ValidityStatus.Duplicate:
                    lblSpecsIdValidator.Text = "* Duplicate ID";
                    txtSpecsID.BackColor = Color.HotPink;
                    break;

                case SpecsDrafter.ValidityStatus.Blank:
                    lblSpecsIdValidator.Text = "* Blank ID";
                    txtSpecsID.BackColor = Color.Pink;
                    break;

                default:
                    break;
            }
            specsIdFieldBackColor = txtSpecsID.BackColor;
        }

        private void Drafter_OnSpecsItemPatternChange(object sender, string textValue)
        {
            if (textValue.Contains("{val}"))
            {
                btnSiInsertVal.Enabled = false;
            }
            else
            {
                btnSiInsertVal.Enabled = true;
            }
        }

        private void Drafter_OnSpecsSet(object sender, SpecsSetEventArgs e)
        {
            SpecsMode = EntryMode.View;
            BindSpecsList();
            SelectSpecs(e.SetID);
            btnNewSpecs.Focus();
        }

        //private void Drafter_OnSpecsAdd(object sender, string e)
        //{
        //    // exit draft mode (New)
        //    SpecsMode = EntryMode.View;
        //    BindSpecsList();
        //    lbxSpecs.Text = e;
        //    btnNewSpecs.Focus();
        //}

        //private void Drafter_OnSpecsUpdate(object sender, string e)
        //{
        //    // exit draft mode (Edit)
        //    SpecsMode = EntryMode.View;
        //    BindSpecsList();
        //    lbxSpecs.Text = e;
        //    btnNewSpecs.Focus();
        //}

        private void Drafter_OnSpecsCancel(object sender, string e)
        {
            SpecsMode = EntryMode.View;

            // check if no specs exists
            if (drafter.SpecsCount > 0)
            {
                BindSpecsList();
                SelectSpecs(e);
            }
            else
            {
                // disable Specs Edit & Remove buttons
                btnEditSpecs.Enabled = false;
                btnRemoveSpecs.Enabled = false;

                UnbindSpecsList(); // order: 6
                UnbindSpecsItemList(); // order: 3
                UnbindEntriesList(); // order: 4

                ClearSpecsFields(); // order: 1
                ClearSpecsItemFields(); // order: 2
                ResetSpecsItemTypeSelector(); // order: 5
            }

            btnNewSpecs.Focus();
        }

        private void Drafter_OnSpecsRemove(object sender, int count)
        {
            if (count > 0) // has one or more item
            {
                SaveAndRestoreSelection(lbxSpecs, BindSpecsList);
            }
            else // has no item
            {
                ClearSpecsFields();
                ClearSpecsItemFields();
                UnbindSpecsList();
                UnbindSpecsItemList();
                UnbindEntriesList();

                btnNewSpecs.Focus();
            }
        }

        private void Drafter_OnSpecsItemSet(object sender, int e)
        {
            SpecsItemMode = EntryMode.View;
            dgvSpec.DataSourceResize(drafter.InputSpecsItems, true);

            // select added or updated SpecsItem from the list
            dgvSpec.CurrentCell = dgvSpec["Index", e - 1];

            btnSiAdd.Focus();
        }

        private void Drafter_OnSpecsItemCancel(object sender, SpecsItemCancelEventArgs e)
        {
            SpecsItemMode = EntryMode.View;
            
            if (e.HasItems)
            {
                dgvSpec.DataSourceResize(drafter.InputSpecsItems, true);
                dgvSpec.CurrentCell = dgvSpec["Index", e.Index - 1];
            }
            else
            {
                UnbindSpecsItemList();

                // disable SpecsItem Edit and Remove buttons
                btnSiEdit.Enabled = false;
                btnSiRemove.Enabled = false;
            }
            btnSiAdd.Focus();
        }

        private void Drafter_OnSpecsItemRemove(object sender, int count)
        {
            if (count > 0)
            {
                SaveAndRestoreSelection(dgvSpec, BindSpecsItemList);
            }
            else
            {
                // disable SpecsItem Edit & Delete buttons.
                btnSiEdit.Enabled = false;
                btnSiRemove.Enabled = false;

                btnSiAdd.Focus();

                UnbindSpecsItemList();
                ClearSpecsItemFields();
                UnbindEntriesList();
            }
        }

        #region Processes

        private void PostLoading()
        {
            SpecsMode = EntryMode.View;

            if (drafter.SpecsCount > 0)
            {
                BindSpecsList();
            }
            else
            {
                UnbindSpecsList();
            }
            BindCustomSpecsSelector();
            ClearCustomTypeSelector();
        }

        private void AddNewSpecs()
        {
            drafter.NewSpecs();

            // Sets a flag
            SpecsMode = EntryMode.New;

            // Set Specs data initial values
            FillSpecsFields(
                drafter.InputSpecsId,
                drafter.InputSpecsName,
                drafter.InputSpecsTxtPat);

            txtSpecsID.Focus();

            ClearSpecsItemFields();
            UnbindSpecsItemList();
            ResetSpecsItemTypeSelector();
            DisableListTypeUI();
        }

        private void EditSpecs()
        {
            drafter.EditSpecs(GetSelectedSpecsId());

            SpecsMode = EntryMode.Edit;

            lbxSpecs.DataSource = drafter.ExistingIDs;
            btnCancel.Focus();
            btnSiEdit.Enabled = true;
            btnSiRemove.Enabled = true;

            DisableListEntryModifyUI();
        }

        private void SaveSpecsDrafting()
        {
            drafter.CommitChanges();
        }

        private void CancelSpecsDrafting()
        {
            drafter.CancelChanges();
        }

        private void RemoveSpecs()
        {
            if (lbxSpecs.SelectedItems.Count == 0)
                return;

            if (ShowSpecsRemoveConfirmation() == DialogResult.OK)
            {
                drafter.RemoveSpecs();
            }
        }

        private void AddNewSpecsItem()
        {
            drafter.NewSpecsItem();

            SpecsItemMode = EntryMode.New;

            FillSpecsItemFields(
                drafter.InputSpecIndex,
                drafter.InputSpecName,
                drafter.InputSpecPattern);

            txtSiName.Focus();

            ResetSpecsItemTypeSelector();
            UnbindEntriesList();
        }

        private void EditSpecsItem()
        {
            drafter.EditSpecsItem(GetSelectedSpecIndex());

            SpecsItemMode = EntryMode.Edit;

            txtSiName.Focus();

            if (rdoListType.Checked)
            {
                grpListEntries.Enabled = true;
                DisplayDraftEntries();
                EnableListEntryModifyUI();
                CheckEntriesCount(drafter.DraftEntries.Count);
            }//else Do Nothing
            else if (rdoCustomType.Checked)
            {
                cboCustomTypeSelector.Enabled = true;
                cboCustomTypeSelector.Text = drafter.DraftCustomSpecId;
            }//else Do Nothing
        }

        private void SaveSpecsItemChanges()
        {
            drafter.CommitSpecsItemChanges();
        }

        private void CancelSpecsItemDrafting()
        {
            drafter.CancelSpecsItemChanges();
            //ResetSpecsItemUI();
        }

        private void ResetSpecsItemUI()
        {
            SpecsItemMode = EntryMode.View;

            UnbindSpecsItemList();

            if (drafter.InputSpecsItems.Count > 0)
            {
                dgvSpec.DataSourceResize(drafter.InputSpecsItems);
            }
            else
            {
                // disable SpecsItem Edit and Remove buttons
                btnSiEdit.Enabled = false;
                btnSiRemove.Enabled = false;
            }
            btnSiAdd.Focus();
        }

        private void RemoveSpecsItem()
        {
            if (SpecsItemMode == EntryMode.View)
            {
                if (ShowSpecRemoveConfirmation() == DialogResult.OK)
                {
                    drafter.RemoveSpecsItem(GetSelectedSpecIndex());
                }
            }
        }

        private void BindSpecsItemList()
        {
            dgvSpec.DataSourceResize(drafter.InputSpecsItems, true);
        }

        private void AddNewListEntry()
        {
            ListEntryEditor listEditor = new ListEntryEditor();
            drafter.CopyEntriesToDraft();

            if (listEditor.ShowDialog() == DialogResult.OK)
            {
                // Get last entryID
                int lastId = drafter.DraftEntries.Count;

                // Generate new entryID
                int newId = lastId + 1;

                // Set ID for the new entry
                listEditor.ListEntry2.ValueID = newId;

                // Add the new entry to the draft list
                drafter.AddEntryToDraftEntries(listEditor.ListEntry2);

                // Enable Edit and Delete buttons
                EnableListEntryModifyUI();
                CheckEntriesCount(drafter.DraftEntries.Count);

                // Display the list items
                UnbindEntriesList();
                DisplayDraftEntries();
            }
        }

        //private void DoubleClickEditListEntry()
        //{
        //    if (SpecsMode != EntryMode.View && SpecsItemMode != EntryMode.View)
        //        EditListEntry();
        //}

        private void EditListEntry()
        {
            // Get Spec ListEntry
            int entryId = GetSelectedListEntryID();
            Interfaces.Models.ISpecListEntry _editListEntry =
                drafter.GetSpecListEntry(entryId);

            ListEntryEditor _listEditor = new ListEntryEditor(_editListEntry);

            if (_listEditor.ShowDialog() == DialogResult.OK)
            {
                // Refresh the list of Entries
                UnbindEntriesList();
                DisplayDraftEntries();
            }
        }

        private void RemoveListEntry()
        {
            // Get Spec ListEntry
            int entryId = GetSelectedListEntryID();

            if (ShowEntryRemoveConfirmation() == DialogResult.OK)
            {
                int entriesCount = drafter.DraftEntries.Count;

                SaveEntrySelectionPosition(entriesCount);

                // Remove entry from list
                drafter.RemoveEntryFromDraftEntries(entryId);

                // Refresh the list of Entries
                UnbindEntriesList();
                DisplayDraftEntries();
                RestoreEntrySelection();
                CheckEntriesCount(entriesCount);
            }
        }

        private void ChangeSpecCustomId()
        {
            switch (SpecsItemMode)
            {
                case EntryMode.View:
                    break;

                case EntryMode.New:
                    drafter.
                    SetSpecCustomId(cboCustomTypeSelector.Text);
                    break;

                case EntryMode.Edit:
                    drafter.
                    SetSpecCustomId(cboCustomTypeSelector.Text);
                    break;

                default:
                    break;
            }
        }

        private void InputSpecsID()
        {
            if (SpecsMode != EntryMode.View && SpecsItemMode == EntryMode.View)
            {
                drafter.InputSpecsId = txtSpecsID.Text;
                lbxSpecs.DataSource = drafter.ExistingIDs;
            }
        }

        private void InputSpecsName()
        {
            if (SpecsMode != EntryMode.View && SpecsItemMode == EntryMode.View)
            {
                drafter.InputSpecsName = txtSpecsName.Text;
            }
        }

        private void InputSpecsPattern()
        {
            if (SpecsMode != EntryMode.View && SpecsItemMode == EntryMode.View)
            {
                drafter.InputSpecsTxtPat = txtSpecsPattern.Text;
            }
        }

        private void InputSpecsItemName()
        {
            if (SpecsItemMode != EntryMode.View)
            {
                drafter.InputSpecName = txtSiName.Text;
            }
        }

        private void InputSpecsItemTextPattern()
        {
            if (SpecsItemMode != EntryMode.View)
            {
                drafter.InputSpecPattern = txtSiValuePattern.Text;
            }
        }

        private void SetDefaultValuePattern()
        {
            txtSiValuePattern.Text = "{val}";
            txtSiValuePattern.FocusSelectAll();
        }

        private void InsertValueToken()
        {
            string valPattern = txtSiValuePattern.Text;
            int insertLoc = txtSiValuePattern.SelectionStart;
            int selLength = txtSiValuePattern.SelectionLength;

            if (selLength > 0)
                valPattern = valPattern.Replace(txtSiValuePattern.SelectedText, "{val}");
            else
                valPattern = valPattern.Insert(insertLoc, "{val}");

            txtSiValuePattern.Text = valPattern;
            txtSiValuePattern.FocusSelectAll();
        }
        #endregion

        private string GetSelectedSpecsId()
        {
            return (string)lbxSpecs.SelectedValue;
        }

        private int GetSelectedSpecIndex()
        {
            return (int)dgvSpec.SelectedRows[0].Cells["Index"].Value;
        }

        private int GetSelectedListEntryID()
        {
            if (dgvListEntries.Rows.Count <= 0)
                return 0;

            return
                (int)dgvListEntries.SelectedRows[0].Cells["ValueID"].Value;
        }
        
        #region User Interface
        
        private void BindSpecsList()
        {
            lbxSpecs.DataSource = drafter.SpecsIDs;
        }

        private void BindCustomSpecsSelector()
        {
            cboCustomTypeSelector.DataSource = drafter.CustomSpecsIDs;
        }

        private void CheckSpecsListCount()
        {
            if (lbxSpecs.Items.Count < 1)
            {
                btnRemoveSpecs.Enabled = false;
                btnEditSpecs.Enabled = false;
            }
            else
            {
                btnRemoveSpecs.Enabled = true;
                btnEditSpecs.Enabled = true;
            }
        }

        private void ViewSelectedSpecsData(string specsId)
        {
            drafter.SetSelectedSpecs(specsId);

            FillSpecsFields(
                drafter.SelectedSpecs.ID,
                drafter.SelectedSpecs.Name,
                drafter.SelectedSpecs.TextPattern);

            dgvSpec.DataSourceResize(drafter.SelectedSpecs.SpecItems);
        }

        private void FillSpecsFields(string id, string name, string pattern)
        {
            txtSpecsID.Text = id;
            txtSpecsName.Text = name;
            txtSpecsPattern.Text = pattern;
        }

        private void FillSpecsItemFields(int index, string name, string pattern)
        {
            txtSiIndex.Text = index.ToString();
            txtSiName.Text = name;
            txtSiValuePattern.Text = pattern;
        }

        private void ViewSelectedSpecData(int idx)
        {
            if (SpecsItemMode == EntryMode.View)
            {
                drafter.SetSelectedSpec(idx);

                txtSiIndex.Text = idx.ToString();
                txtSiName.Text = drafter.SelectedSpecsItem.Name;
                txtSiValuePattern.Text = drafter.SelectedSpecsItem.ValuePattern;

                ChangeSpecTypeSelector();
            }
        }

        private void ChangeSpecTypeSelector()
        {
            // List Type Specs Item
            ListTypeSpec();

            // Custom Type Specs Item
            CustomTypeSpec();
        }

        private void ListTypeSpec()
        {
            if (drafter.SelectedSpecsItem.ListEntries != null)
            {
                dgvListEntries.DataSourceResize(drafter.SelectedSpecsItem.ListEntries.ToList());
                rdoListType.Checked = true;
            }
            else
            {
                UnbindEntriesList();
                rdoListType.Checked = false;
            }
        }

        private void CustomTypeSpec()
        {
            if (drafter.SelectedSpecsItem.CustomInputID != null && drafter.SelectedSpecsItem.CustomInputID != "")
            {
                cboCustomTypeSelector.Text = drafter.SelectedSpecsItem.CustomInputID;
                rdoCustomType.Checked = true;
            }
            else
            {
                ClearCustomTypeSelector();
                rdoCustomType.Checked = false;
            }
        }

        private void ShowSpecsReviewUI()
        {
            btnAccept.Visible = true;
            btnCancel.Visible = true;
        }

        private void EnableSpecsMetadataEntryUI()
        {
            txtSpecsID.ReadOnly = false;
            txtSpecsName.ReadOnly = false;
            txtSpecsPattern.ReadOnly = false;
        }

        private void ShowSpecReviewUI()
        {
            btnSiAccept.Visible = true;
            btnSiCancel.Visible = true;
        }

        private void HideSpecReviewUI()
        {
            btnSiAccept.Visible = false;
            btnSiCancel.Visible = false;
        }

        private void EnableSpecMetadataEntryUI()
        {
            //txtSiIndex.ReadOnly = false;
            txtSiName.ReadOnly = false;
            txtSiValuePattern.ReadOnly = false;
            btnSiDefaultVal.Visible = true;
            btnSiInsertVal.Visible = true;
        }

        private void DisableSpecMetadataEntryUI()
        {
            //txtSiIndex.ReadOnly = true;
            txtSiName.ReadOnly = true;
            txtSiValuePattern.ReadOnly = true;
            btnSiDefaultVal.Visible = false;
            btnSiInsertVal.Visible = false;
        }

        private void SelectSpecs(string specsId)
        {
            lbxSpecs.Text = specsId;
        }

        private void EnableSpecsListSelection()
        {
            lbxSpecs.SelectionMode = SelectionMode.One;
        }

        private void DisableSpecsListSelection()
        {
            lbxSpecs.SelectionMode = SelectionMode.None;
        }

        private void DisableSpecsModifyUI()
        {
            btnNewSpecs.Enabled = false;
            btnEditSpecs.Enabled = false;
            btnRemoveSpecs.Enabled = false;
        }

        private void EnableSpecsItemModifyUI()
        {
            //TEST
            //dgvSpec.Enabled = true;

            //btnSiAdd.Enabled = true;
            btnSiEdit.Enabled = true;
            btnSiRemove.Enabled = true;
        }

        private void DisableSpecsItemModifyUI()
        {
            //TEST
            if (SpecsMode != EntryMode.View)
                dgvSpec.Enabled = false;

            btnSiAdd.Enabled = false;
            btnSiEdit.Enabled = false;
            btnSiRemove.Enabled = false;
        }

        private void EnableListEntryModifyUI()
        {
            btnListEntryAdd.Enabled = true;
            btnListEntryEdit.Enabled = true;
            btnListEntryRemove.Enabled = true;
            chkListEntryConfirmRemove.Enabled = true;
        }

        private void DisableListEntryModifyUI()
        {
            btnListEntryAdd.Enabled = false;
            btnListEntryEdit.Enabled = false;
            btnListEntryRemove.Enabled = false;
            chkListEntryConfirmRemove.Enabled = false;
        }

        private void CheckEntriesCount(int entryCount)
        {
            if (entryCount < 1)
                btnListEntryEdit.Enabled = false;

            if (entryCount < 2)
                btnListEntryRemove.Enabled = false;
        }

        #region Spec List Type
        private void CheckSpecListType()
        {
            if (rdoListType.Checked)
            {
                if (SpecsItemMode != EntryMode.View)
                    SelectListType();
            }
            else
            {
                if (SpecsItemMode != EntryMode.View)
                    DisableListTypeUI();
            }
        }

        private void SelectListType()
        {
            drafter.SetSpecTypeToList();
            grpListEntries.Enabled = true;
            CheckListEntries();
        }

        private void CheckListEntries()
        {
            if (drafter.DraftEntries == null)
            {
                btnListEntryAdd.Enabled = true;
            }
            else
            {
                DisplayDraftEntries();
                EnableListEntryModifyUI();
                CheckEntriesCount(drafter.DraftEntries.Count);
            }
        }

        private void DisableListTypeUI()
        {
            grpListEntries.Enabled = false;
            UnbindEntriesList();
        }
        #endregion

        #region Spec Custom Type
        private void CheckSpecCustomType()
        {
            if (rdoCustomType.Checked)
            {
                if (SpecsItemMode == EntryMode.New || SpecsItemMode == EntryMode.Edit)
                    SelectCustomType();
            }
            else
            {
                if (SpecsItemMode == EntryMode.New || SpecsItemMode == EntryMode.Edit)
                    DeselectCustomType();
            }
        }

        private void SelectCustomType()
        {
            drafter.SetSpecTypeToCustom();
            cboCustomTypeSelector.Enabled = true;

            if (drafter.DraftCustomSpecId != null)
            {
                cboCustomTypeSelector.Text = drafter.DraftCustomSpecId;
            }

        }

        private void DeselectCustomType()
        {
            cboCustomTypeSelector.Enabled = false;
            ClearCustomTypeSelector();
        }

        private void ClearCustomTypeSelector()
        {
            //cboCustomTypeSelector.Text = "";
            cboCustomTypeSelector.SelectedIndex = -1;
        }
        #endregion

        private void DisableSpecTypeUI()
        {
            grpSpecType.Enabled = false;
        }

        private void ResetSpecsItemTypeSelector()
        {
            rdoListType.Checked = false;
            rdoCustomType.Checked = false;
        }

        private void UnbindSpecsList()
        {
            if (lbxSpecs.DataSource == null)
            {
                lbxSpecs_DataSourceChanged(lbxSpecs, EventArgs.Empty);
                return;
            }
            lbxSpecs.DataSource = null;
        }

        private void UnbindSpecsItemList()
        {
            dgvSpec.DataSource = null;
        }

        private void UnbindEntriesList()
        {
            dgvListEntries.DataSource = null;
        }

        private void ClearSpecsFields()
        {
            txtSpecsID.Clear();
            txtSpecsName.Clear();
            txtSpecsPattern.Clear();
        }

        private void ClearSpecsItemFields()
        {
            txtSiIndex.Clear();
            txtSiName.Clear();
            txtSiValuePattern.Clear();
        }

        private void DisplayDraftEntries()
        {
            dgvListEntries.DataSourceResize(drafter.DraftEntries);
        }
        
        //private void SaveSelection(ListBox listBox)
        //{
        //    specsSelectionIndex = listBox.SelectedIndex;
        //}

        //private void RestoreSelection(ListBox listBox)
        //{
        //    int itemsCount = listBox.Items.Count;

        //    if (specsSelectionIndex > -1 && itemsCount > 0)
        //    {
        //        // Check if selection index exists in the list
        //        if (specsSelectionIndex >= itemsCount)
        //            specsSelectionIndex = itemsCount - 1;

        //        listBox.SelectedIndex = specsSelectionIndex;
        //    }
        //}

        //private void SaveSpecsSelectionPosition(bool shiftUp = false)
        //{
        //    int selectedIndex = lbxSpecs.SelectedIndex;
        //    int count = /*drafter.SpecsCount*/lbxSpecs.Items.Count;
        //    if (selectedIndex == count - 1)
        //    {
        //        specsSelectionIndex = count - 1;

        //        if (shiftUp)
        //        {
        //            specsSelectionIndex -= 1;
        //        }
        //    }
        //    else
        //        specsSelectionIndex = selectedIndex;
        //}

        //private void RestoreSpecsSelectionPosition()
        //{
        //    if (specsSelectionIndex > -1)
        //    {
        //        lbxSpecs.SelectedIndex = specsSelectionIndex;
        //    }
        //}

        //private void SelectLastSpecs()
        //{
        //    lbxSpecs.SelectedIndex = lbxSpecs.Items.Count - 1;
        //}

        private void SaveAndRestoreSelection(ListBox listBox, Action action)
        {
            int _specsSelectionIndex = lbxSpecs.SelectedIndex;

            action?.Invoke();

            int itemsCount = lbxSpecs.Items.Count;

            if (_specsSelectionIndex >= itemsCount)
            {
                _specsSelectionIndex = itemsCount - 1;
            }

            if (_specsSelectionIndex > -1 && itemsCount > 0)
            {
                lbxSpecs.SelectedIndex = _specsSelectionIndex;
            }
        }

        private void SaveAndRestoreSelection(DataGridView dataGridView, Action action)
        {
            int _specSelectionIndex = dataGridView.SelectedRows[0].Index;

            action?.Invoke();

            // Get DGV number of rows
            int itemsCount = dataGridView.RowCount;

            if (_specSelectionIndex > -1 && itemsCount > 0)
            {
                // Check if selection index exists in the list
                if (_specSelectionIndex >= itemsCount)
                    _specSelectionIndex = itemsCount - 1;

                dataGridView.Rows[_specSelectionIndex].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = _specSelectionIndex;
            }
        }

        private void SaveEntrySelectionPosition(int itemsCount)
        {
            int selectedIndex = dgvListEntries.SelectedRows[0].Index;
            if (selectedIndex == itemsCount - 1)
                entrySelectionIndex = itemsCount - 2;
            else
            {
                entrySelectionIndex = selectedIndex;
            }
        }

        private void RestoreEntrySelection()
        {
            if (entrySelectionIndex > -1)
            {
                //if (dgvListEntries.Rows[entrySelectionIndex].Displayed == false)
                dgvListEntries.FirstDisplayedScrollingRowIndex = entrySelectionIndex;
                dgvListEntries.Rows[entrySelectionIndex].Cells[0].Selected = true;
            }
        }

        private DialogResult ShowSpecsRemoveConfirmation()
        {
            if (chkSpecsConfirmRemove.Checked)
            {
                return
                    MessageBox.Show(
                    caption: "Confirm Delete",
                    text: "Are you sure you want to remove the selected specs ?",
                    buttons: MessageBoxButtons.OKCancel,
                    icon: MessageBoxIcon.Exclamation,
                    defaultButton: MessageBoxDefaultButton.Button1);
            }

            return DialogResult.OK;
        }

        private DialogResult ShowSpecRemoveConfirmation()
        {
            if (chkSpecConfirmRemove.Checked)
            {
                return
                    MessageBox.Show(
                    caption: "Confirm Delete",
                    text: "Are you sure you want to remove the selected spec item ?",
                    buttons: MessageBoxButtons.OKCancel,
                    icon: MessageBoxIcon.Exclamation,
                    defaultButton: MessageBoxDefaultButton.Button1);
            }

            return DialogResult.OK;
        }

        private DialogResult ShowEntryRemoveConfirmation()
        {
            if (chkListEntryConfirmRemove.Checked)
            {
                return
                    MessageBox.Show(
                    caption: "Confirm Delete",
                    text: "Are you sure you want to remove the selected entry ?",
                    buttons: MessageBoxButtons.OKCancel,
                    icon: MessageBoxIcon.Exclamation,
                    defaultButton: MessageBoxDefaultButton.Button1);
            }

            return DialogResult.OK;
        }

        private void ResetIdValidityInfo()
        {
            lblSpecsIdValidator.Text = string.Empty;
            txtSpecsID.BackColor = SystemColors.Window;
        }

        private void SelectTextbox(TextBox textBox)
        {
            textBox.SelectAll();
            textBox.Focus();
        }
        #endregion

#pragma warning disable IDE1006 // Naming Styles

        #region Event Responses
        private void mnuItmSaveFile_Click(object sender, EventArgs e)
        {
            drafter?.SaveToDataSource();
        }
        private void lbxSpecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SpecsMode == EntryMode.View)
            {
                if (lbxSpecs.SelectedIndex != -1)
                {
                    ViewSelectedSpecsData(GetSelectedSpecsId());
                }
            }
        }
        private void lbxSpecs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lbxSpecs.IndexFromPoint(e.Location);
            if (lbxSpecs.SelectionMode == SelectionMode.None) return;
            if (index != ListBox.NoMatches) EditSpecs();
        }
        private void dgvSpecsItems_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = dgvSpec.SelectedRows.Count;

            if (rowsCount == 0 || rowsCount > 1)
                return;

            DataGridViewRow row = dgvSpec.SelectedRows[0];

            if (row == null)
                return;

            int idx = (int)row.Cells["Index"].Value;
            ViewSelectedSpecData(idx);
        }
        private void dgvSpec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && SpecsMode != EntryMode.View && SpecsItemMode == EntryMode.View)
            {
                EditSpecsItem();
                //if (SpecsMode != EntryMode.View && SpecsItemMode == EntryMode.View)
                //    EditSpecsItem();
            }
        }
        private void dgvListEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                SpecsMode != EntryMode.View && SpecsItemMode != EntryMode.View)
            {
                //DoubleClickEditListEntry();
                //if (SpecsMode != EntryMode.View && SpecsItemMode != EntryMode.View)
                EditListEntry();
            }
        }
        private void cboCustomTypeSelector_SelectedIndexChanged(object sender, EventArgs e) => ChangeSpecCustomId();
        private void btnNewSpecs_Click(object sender, EventArgs e) => AddNewSpecs();
        private void btnEditSpecs_Click(object sender, EventArgs e) => EditSpecs();
        private void btnAccept_Click(object sender, EventArgs e) => SaveSpecsDrafting();
        private void btnCancel_Click(object sender, EventArgs e) => CancelSpecsDrafting();
        private void btnRemoveSpecs_Click(object sender, EventArgs e) => RemoveSpecs();
        private void txtSpecsID_TextChanged(object sender, EventArgs e) => InputSpecsID();
        private void txtSpecsName_TextChanged(object sender, EventArgs e) => InputSpecsName();
        private void txtSpecsPattern_TextChanged(object sender, EventArgs e) => InputSpecsPattern();
        private void btnSiAdd_Click(object sender, EventArgs e) => AddNewSpecsItem();
        private void btnSiEdit_Click(object sender, EventArgs e) => EditSpecsItem();
        private void btnSiRemove_Click(object sender, EventArgs e) => RemoveSpecsItem();
        private void btnSiAccept_Click(object sender, EventArgs e) => SaveSpecsItemChanges();
        private void btnSiCancel_Click(object sender, EventArgs e) => CancelSpecsItemDrafting();
        private void txtSiName_TextChanged(object sender, EventArgs e) => InputSpecsItemName();
        private void txtSiValuePattern_TextChanged(object sender, EventArgs e) => InputSpecsItemTextPattern();
        private void btnSiDefaultVal_Click(object sender, EventArgs e) => SetDefaultValuePattern();
        private void btnSiInsertVal_Click(object sender, EventArgs e) => InsertValueToken();
        private void rdoListType_CheckedChanged(object sender, EventArgs e) => CheckSpecListType();
        private void rdoCustomType_CheckedChanged(object sender, EventArgs e) => CheckSpecCustomType();
        private void btnListEntryAdd_Click(object sender, EventArgs e) => AddNewListEntry();
        private void btnListEntryEdit_Click(object sender, EventArgs e) => EditListEntry();
        private void btnListEntryRemove_Click(object sender, EventArgs e) => RemoveListEntry();
        private void lblSpecsID_Click(object sender, EventArgs e) => SelectTextbox(txtSpecsID);
        private void lblSpecsPattern_Click(object sender, EventArgs e) => SelectTextbox(txtSpecsPattern);
        private void lblSpecIndex_Click(object sender, EventArgs e) => SelectTextbox(txtSiIndex);
        private void lblSpecName_Click(object sender, EventArgs e) => SelectTextbox(txtSiName);
        private void lblSpecValuePattern_Click(object sender, EventArgs e) => SelectTextbox(txtSiValuePattern);
        private void SpecsEditor_Load(object sender, EventArgs e) => PostLoading();
        private void tsmiClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void tsmiExitApp_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
        private void dgvSpec_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void dgvSpec_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dgvSpec.SelectedRows.Count > 0 && SpecsMode != EntryMode.View && SpecsItemMode == EntryMode.View)
                {
                    tsmiInsertToken.Enabled = true;
                }
                else
                {
                    tsmiInsertToken.Enabled = false;
                }
            }
        }
        private void tsmiInsertToken_Click(object sender, EventArgs e)
        {
            // Reference: https://stackoverflow.com/questions/1718389/right-click-context-menu-for-datagridview

            //int specIndex = (int)dgvSpec.SelectedRows[0].Cells["Index"].Value;
            string specToken = $"{{{dgvSpec.SelectedRows[0].Cells["Index"].Value}}}";

            string existingTextPattern = txtSpecsPattern.Text;
            // Check if token is already inserted
            if (existingTextPattern.Contains(specToken))
            {
                MessageBox.Show("The token is already inserted");
            }
            else
            {
                txtSpecsPattern.Text = $"{txtSpecsPattern.Text}{specToken}";
            }
        }

        private void lbxSpecs_DataSourceChanged(object sender, EventArgs e)
        {
            if (SpecsMode == EntryMode.View)
            {
                if (lbxSpecs.DataSource == null/*Items.Count < 1*/)
                {
                    btnRemoveSpecs.Enabled = false;
                    btnEditSpecs.Enabled = false;
                }
                else
                {
                    btnRemoveSpecs.Enabled = true;
                    btnEditSpecs.Enabled = true;
                }
            }
        }
        #endregion

#pragma warning restore IDE1006 // Naming Styles

    }
}