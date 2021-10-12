
using ClientService;
using Shared.UI;
using CoreLibrary.Enums;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UserInterface.Forms
{
    public partial class SpecsEditor : Form
    {
        private enum IdStatus
        {
            Valid,
            Duplicate,
            Blank
        }

        #region Properties
        private EntryMode _specsMode = EntryMode.View;
        private EntryMode SpecsMode
        {
            get => _specsMode;
            set
            {
                _specsMode = value;
                if (value == EntryMode.View)
                {
                    // Enable Save UI
                    tsmiSaveFile.Enabled = true;
                }
                else
                {
                    // Disable Save UI
                    tsmiSaveFile.Enabled = false;
                }
            }
        }

        private bool _isValidSpecsId;
        private bool IsValidSpecsId
        {
            get { return _isValidSpecsId; }
            set
            {
                _isValidSpecsId = value;
                CheckDraftSpecsReady();
            }
        }

        private bool _isSpecsHasItem;
        private bool IsSpecsHasItem
        {
            get { return _isSpecsHasItem; }
            set
            {
                _isSpecsHasItem = value;
                CheckDraftSpecsReady();
            }
        }

        private bool _isValidSpecName;
        public bool IsValidSpecName
        {
            get => _isValidSpecName;
            set
            {
                _isValidSpecName = value;
                CheckDraftSpecItemReady();
            }
        }

        private bool _isValidSpecData;
        public bool IsValidSpecData
        {
            get { return _isValidSpecData; }
            set
            {
                _isValidSpecData = value;
                CheckDraftSpecItemReady();
            }
        }
        #endregion

        
        private EntryMode specMode = EntryMode.View;
        private Interfaces.Models.ISpecs selectedSpecs;
        private Interfaces.Models.ISpecsItem selSpec;

        private Modeling.DraftModels.SpecsDrafter drafter;

        private string draftSpecsId;
        //private Interfaces.Models.ISpecs draftSpecs;
        //private Interfaces.Models.ISpecsItem draftSpec;
        //private SpecType draftSpecType;
        private List<Interfaces.Models.ISpecListEntry> draftEntries;
        private string draftCustomSpecId;

        private int specsSelectionIndex = 0;
        private int specSelectionIndex;
        private int entrySelectionIndex;
        

        public SpecsEditor() => InitializeComponent();

        private void SaveToDataSource()
        {
            ContextProvider.Save(ContextEntity.Specs);
        }

        #region Processes

        private void PostLoading()
        {
            RefreshSpecsList();

            // Bind Custom Specs Selector
            cboCustomTypeSelector.DataSource =
                DataManager.GetCustomSpecsList();

            ClearCustomTypeSelector();

            EnableSpecsModifyUI();
        }

        private void ReadSelectedSpecsData()
        {
            string specsId = lbxSpecs.Text;
            selectedSpecs = SpecsRepository.Read(specsId);
        }

        private void AddNewSpecs()
        {
            SaveSpecsSelectionPosition();
            // Instantiate new Specs

            // DISABLED
            //draftSpecs = new Modeling.DataModels.Specs();

            drafter = new Modeling.DraftModels.SpecsDrafter();

            // Generate new SpecsID
            draftSpecsId = GenerateNewSpecsID();
            // Sets a flag
            SpecsMode = EntryMode.New;


            // Disable Specs Selection
            DisableSpecsListSelection();
            // Disable Specs main controls
            DisableSpecsModifyUI();
            // Show mode Accept/Cancel controls
            ShowSpecsReviewUI();
            // Setup Specs Meta-data controls
            EnableSpecsMetadataEntryUI();
            btnAccept.Enabled = false;
            btnSiAdd.Enabled = true;

            // Clear Specs Meta-data controls
            ClearSpecsMetadataEntryUI();

            // Set Specs Meta-data initial/default values
            txtSpecsID.Focus();
            txtSpecsID.Text = draftSpecsId;

            // DISABLED
            //txtSpecsPattern.Text = draftSpecs.TextPattern;
            txtSpecsPattern.Text = drafter.DraftSpecs.TextPattern;

            // Setup SpecsItem Meta-data controls
            ClearSpecMetadataEntryUI();

            // Clear DGVs from any data
            ClearSpecItemsGrid();

            //ClearListEntriesGrid();
            ResetSpecTypeUI();

            // Reset SpecsItems list type controls
            DeselectListType();
        }

        private void EditSpecs()
        {
            draftSpecsId = GetSelectedSpecsId();

            // DISABLED
            //draftSpecs = SpecsRepository.Read(draftSpecsId);

            drafter = new Modeling.DraftModels.SpecsDrafter(SpecsRepository.Read(draftSpecsId));

            SaveSpecsSelectionPosition();

            SpecsMode = EntryMode.Edit;
            CheckSpecsID();
            CheckDraftSpecsItemsCount();
            // Disable Specs Selection
            DisableSpecsListSelection();

            // Disable Specs main controls
            DisableSpecsModifyUI();

            // Show mode Accept/Cancel controls
            ShowSpecsReviewUI();

            // Setup Specs Meta-data controls
            EnableSpecsMetadataEntryUI();

            btnCancel.Focus();
            EnableSpecModifyUI();
            DisableListEntryModifyUI();
        }

        private void CancelSpecsDrafting()
        {
            SpecsMode = EntryMode.View;
            specMode = EntryMode.View;

            // DISABLED
            //draftSpecs = null;
            drafter.ClearAllDrafts();

            // DISABLED
            //ClearSpecsDrafts();

            EnableSpecsListSelection();

            if (DataProvider.GetSpecsIds().Count > 0)
            {
                EnableSpecsModifyUI();
            }
            else
            {
                btnNewSpecs.Enabled = true;
                btnEditSpecs.Enabled = false;
                btnRemoveSpecs.Enabled = false;
            }

            HideSpecsReviewUI();
            DisableSpecsMetadataEntryUI();
            ClearSpecsMetadataEntryUI();

            DisableSpecModifyUI();
            dgvSpec.Enabled = true;
            HideSpecReviewUI();
            DisableSpecMetadataEntryUI();
            ClearSpecMetadataEntryUI();

            DisableListEntryModifyUI();

            ClearSpecItemsGrid();
            ClearListEntriesGrid();
            ResetSpecTypeUI();
            DisableSpecTypeUI();
            PopulateSpecsList();

            RestoreSpecsSelection();

            btnNewSpecs.Focus();
        }

        private void RemoveSpecs()
        {
            int rowsCount = lbxSpecs.SelectedItems.Count;
            if (rowsCount == 0)
                return;

            SaveSpecsSelectionPosition(true);

            if (ShowSpecsRemoveConfirmation() == DialogResult.OK)
            {
                string specsId = GetSelectedSpecsId();

                SpecsRepository.Delete(specsId);

                CheckSpecsCount();

                RefreshSpecsList();

                if (DataProvider.GetSpecsIds().Count > 0)
                {
                    ClearSpecsMetadataEntryUI();
                    ClearSpecMetadataEntryUI();
                    ClearSpecItemsGrid();
                    ClearListEntriesGrid();
                    btnNewSpecs.Focus();
                }
            }

            RestoreSpecsSelection();
        }

        private void CheckSpecsCount()
        {
            if (DataProvider.GetSpecsIds().Count < 1)
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

        private void NewSpec()
        {
            specMode = EntryMode.New;

            // Instantiate new Spec
            // DISABLED
            //draftSpec = new Modeling.DataModels.SpecsItem();
            drafter.NewDraftSpec();

            // Get last SpecsItem index
            // DISABLED
            //int lastIdx = draftSpecs.SpecItems.Count();
            int lastIdx = drafter.DraftSpecsItemsCount();

            // Set Initial member values
            int newIdx = lastIdx + 1;
            string name = $"SI{newIdx:000}";

            // Set object members
            // DISABLED
            //draftSpec.Index = newIdx;
            //draftSpec.Name = name;

            drafter.DraftSpec.Index = newIdx;
            drafter.DraftSpec.Name = name;

            // DISABLED
            //SetSpecFieldsValue(newIdx, name, draftSpec.ValuePattern);
            SetSpecFieldsValue(newIdx, name, drafter.DraftSpec.ValuePattern);

            // Setup UI
            DisableSpecModifyUI();
            btnSiAccept.Enabled = false;
            ShowSpecReviewUI();
            EnableSpecMetadataEntryUI();
            txtSiName.Focus();
            grpSpecType.Enabled = true;

            // Clear
            ResetSpecTypeUI();
            ClearListEntriesGrid();
        }

        private void SetSpecFieldsValue(int index, string name, string valPattern)
        {
            txtSiIndex.Text = index.ToString();
            txtSiName.Text = name;
            txtSiValuePattern.Text = valPattern;
        }

        private void DoubleClickEditSpec()
        {
            if (SpecsMode != EntryMode.View && specMode == EntryMode.View)
                EditSpec();
        }

        private void EditSpec()
        {
            specMode = EntryMode.Edit;

            drafter.EditSpec(GetSelectedSpecIndex());

            // Get Spec object being edited
            // DISABLED
            //draftSpec = draftSpecs.SpecItems.ToList()[GetSelectedSpecIndex() - 1];

            //if (draftSpec.ListEntries != null)
            //{
            //    draftSpecType = SpecType.List;
            //    draftEntries =
            //        new List<Interfaces.Models.ISpecListEntry>(draftSpec.ListEntries);
            //}

            //if (draftSpec.CustomInputID != null)
            //{
            //    draftSpecType = SpecType.Custom;
            //    draftCustomSpecId = draftSpec.CustomInputID;
            //}

            // Setup UI
            DisableSpecModifyUI();
            ShowSpecReviewUI();
            EnableSpecMetadataEntryUI();
            txtSiName.Focus();
            grpSpecType.Enabled = true;

            if (rdoListType.Checked)
            {
                grpListEntries.Enabled = true;
                DisplayDraftEntries();
                EnableListEntryModifyUI();
                CheckEntriesCount(draftEntries.Count);
                CheckEntriesCount(drafter.DraftEntries.Count);
            }
            //else Do Nothing

            if (rdoCustomType.Checked)
            {
                cboCustomTypeSelector.Enabled = true;
                cboCustomTypeSelector.Text = draftCustomSpecId;
                cboCustomTypeSelector.Text = drafter.DraftCustomSpecId;
            }
            //else Do Nothing
        }

        private void SaveDraftSpec()
        {
            drafter.SaveDraftSpec(int.Parse(txtSiIndex.Text), txtSiName.Text, txtSiValuePattern.Text);

            // Save new Spec data
            // DISABLED
            //draftSpec.Index = int.Parse(txtSiIndex.Text);
            //draftSpec.Name = txtSiName.Text;
            //draftSpec.ValuePattern = txtSiValuePattern.Text;

            //switch (draftSpecType)
            //{
            //    case SpecType.List:
            //        draftSpec.ListEntries =
            //        new List<Interfaces.Models.ISpecListEntry>(draftEntries);

            //        draftSpec.CustomInputID = null;

            //        break;
            //    case SpecType.Custom:
            //        draftSpec.CustomInputID = draftCustomSpecId;
            //        draftSpec.ListEntries = null;

            //        break;
            //}

            // Add the created Spec to Spec list of the new Specs
            if (specMode == EntryMode.New)
            {
                // DISABLED
                //List<Interfaces.Models.ISpecsItem> tempList = draftSpecs.SpecItems.ToList();
                //tempList.Add(draftSpec);
                //draftSpecs.SpecItems = tempList;

                drafter.AddSpecToSpecsItemsDrafts();
            }

            CheckDraftSpecsItemsCount();

            // Set EntryMode to View
            specMode = EntryMode.View;

            // Null draft objects
            ClearSpecsDrafts();
            ResetSpecUI();
            btnSiAdd.Focus();
        }

        private void ResetSpecUI()
        {
            // Clear Text boxes
            ClearSpecMetadataEntryUI();

            // Setup UI
            CheckDraftSpecsReady();

            EnableSpecModifyUI();

            // DISABLED
            //int siCount = draftSpecs.SpecItems.Count();
            int siCount = drafter.DraftSpecsItemsCount();

            if (siCount <= 1)
                btnSiRemove.Enabled = false;

            HideSpecReviewUI();
            DisableSpecMetadataEntryUI();
            DisableSpecTypeUI();
            ResetSpecTypeUI();
            DisableListEntryModifyUI();

            // Clear the list entries
            ClearListEntriesGrid();

            // Refresh View
            ClearSpecItemsGrid();

            // DISABLED
            //dgvSpec.DataSourceResize(draftSpecs.SpecItems.ToList());
            dgvSpec.DataSourceResize(drafter.DraftSpecs.SpecItems.ToList());
        }

        private void CancelSpecChanges()
        {
            // Set EntryMode to View
            specMode = EntryMode.View;

            // Null draft objects
            ClearSpecsDrafts();

            ResetSpecUI();

            // DISABLED
            //int siCount = draftSpecs.SpecItems.Count();
            int siCount = drafter.DraftSpecs.SpecItems.Count();
            if (siCount <= 0)
            {
                // Set to null to remove columns
                ClearSpecItemsGrid();

                // Disable Edit and Delete buttons for Spec modification
                btnSiAdd.Focus();
                btnSiEdit.Enabled = false;
                btnSiRemove.Enabled = false;

                dgvListEntries.DataSource = null;
            }
        }

        private void RemoveSpec()
        {
            if ((SpecsMode != EntryMode.New || SpecsMode == EntryMode.Edit) && specMode == EntryMode.View)
            {
                // DISABLED
                //int siCount = draftSpecs.SpecItems.Count();
                int siCount = drafter.DraftSpecsItemsCount();

                SaveSpecItemSelectionPosition(siCount);

                if (ShowSpecRemoveConfirmation() == DialogResult.OK)
                {
                    // DISABLED
                    //Interfaces.Models.ISpecsItem _specsItem = draftSpecs.SpecItems
                    //    .FirstOrDefault(idx => idx.Index == GetSelectedSpecIndex());

                    //List<Interfaces.Models.ISpecsItem> tempList = draftSpecs.SpecItems.ToList();
                    //tempList.Remove(_specsItem);
                    //draftSpecs.SpecItems = tempList;

                    //// Renumber SpecItems
                    //int i = 0;
                    //foreach (Interfaces.Models.ISpecsItem spec in draftSpecs.SpecItems)
                    //{
                    //    spec.Index = ++i;
                    //}

                    // using the drafter object
                    drafter.RemoveSpecFromDraftSpecsItems(GetSelectedSpecIndex());

                    ClearSpecItemsGrid();

                    // DISABLED
                    //dgvSpec.DataSourceResize(draftSpecs.SpecItems.ToList());
                    dgvSpec.DataSourceResize(drafter.DraftSpecs.SpecItems.ToList());
                }

                RestoreSpecItemSelection();

                if (siCount <= 0)
                {
                    // Set to null to remove columns
                    ClearSpecItemsGrid();

                    // Disable Edit and Delete buttons for Spec modification
                    btnSiAdd.Focus();
                    btnSiEdit.Enabled = false;

                    ClearSpecMetadataEntryUI();
                    dgvListEntries.DataSource = null;
                }

                if (siCount <= 1)
                    btnSiRemove.Enabled = false;
            }
        }

        private void SaveChanges()
        {
            // Save draft (new) Specs metadata
            // DISABLED
            //draftSpecs.ID = txtSpecsID.Text;
            //draftSpecs.Name = txtSpecsName.Text;
            //draftSpecs.TextPattern = txtSpecsPattern.Text;

            drafter.DraftSpecs.ID = txtSpecsID.Text;
            drafter.DraftSpecs.Name = txtSpecsName.Text;
            drafter.DraftSpecs.TextPattern = txtSpecsPattern.Text;

            drafter.specsId = txtSpecsID.Text;
            drafter.specsName = txtSpecsName.Text;
            drafter.specsTxtPat = txtSpecsPattern.Text;

            drafter.CommitChanges();

            if (SpecsMode == EntryMode.New)
            {
                // DISABLED
                //SpecsRepository.Create(draftSpecs);
                SpecsRepository.Create(drafter.DraftSpecs);
            }

            if (SpecsMode == EntryMode.Edit)
            {
                // DISABLED
                //SpecsRepository.Update(draftSpecsId, draftSpecs);
                SpecsRepository.Update(draftSpecsId, drafter.DraftSpecs);
            }

            // Exit draft (New) mode
            SpecsMode = EntryMode.View;

            // Setup UI
            EnableSpecsListSelection();
            EnableSpecsModifyUI();
            HideSpecsReviewUI();
            DisableSpecsMetadataEntryUI();
            DisableSpecModifyUI();
            HideSpecReviewUI();

            // Disable Spec pattern UI
            btnSiDefaultVal.Visible = false;

            DisableSpecMetadataEntryUI();
            DisableListEntryModifyUI();

            // Reload and Repopulate Specs list
            RefreshSpecsList();

            // DISABLED
            //SelectSpecs(draftSpecs.ID);
            SelectSpecs(drafter.DraftSpecs.ID);

            // TEST
            // DISABLED
            //draftSpecs = null;
            drafter.ClearDraftSpecs();
        }

        private void AddNewListEntry()
        {
            ListEntryEditor listEditor = new ListEntryEditor();

            // Copy list entries of draft spec to draft entries, if any
            // DISABLED
            //if (draftEntries == null)
            //{
            //    draftEntries =
            //        draftSpec.ListEntries == null ?
            //        new List<Interfaces.Models.ISpecListEntry>() :
            //        new List<Interfaces.Models.ISpecListEntry>(draftSpec.ListEntries);
            //}
            drafter.CopyEntriesToDraft();


            if (listEditor.ShowDialog() == DialogResult.OK)
            {
                // Get last entryID
                int lastId = draftEntries.Count;
                lastId = drafter.DraftEntries.Count;

                // Generate new entryID
                int newId = lastId + 1;

                // Set ID for the new entry
                listEditor.ListEntry2.ValueID = newId;

                // Add the new entry to the draft list
                draftEntries.Add(listEditor.ListEntry2);
                drafter.DraftEntries.Add(listEditor.ListEntry2);

                // Enable Edit and Delete buttons
                EnableListEntryModifyUI();

                CheckEntriesCount(draftEntries.Count);
                CheckEntriesCount(drafter.DraftEntries.Count);

                CheckSpecData();

                // Display the list items
                ClearListEntriesGrid();
                DisplayDraftEntries();
            }
        }

        private void DoubleClickEditListEntry()
        {
            if (SpecsMode != EntryMode.View && specMode != EntryMode.View)
                EditListEntry();
        }

        private void EditListEntry()
        {
            // Get Spec ListEntry
            int entryId = GetSelectedListEntryID();
            Interfaces.Models.ISpecListEntry _editListEntry = draftEntries
                .Find(id => id.ValueID == entryId);

            _editListEntry = drafter.GetSpecListEntry(entryId);

            ListEntryEditor _listEditor = new ListEntryEditor(_editListEntry);

            if (_listEditor.ShowDialog() == DialogResult.OK)
            {
                // Refresh the list of Entries
                ClearListEntriesGrid();
                DisplayDraftEntries();
            }
        }

        private void RemoveListEntry()
        {
            // Get Spec ListEntry
            int entryId = GetSelectedListEntryID();

            Interfaces.Models.ISpecListEntry _removeListEntry =
                draftEntries.Find(id => id.ValueID == entryId);

            if (ShowEntryRemoveConfirmation() == DialogResult.OK)
            {
                int entriesCount = draftEntries.Count;
                entriesCount = drafter.DraftEntries.Count;

                SaveEntrySelectionPosition(entriesCount);

                drafter.RemoveEntryFromDraftEntries(entryId);

                // Remove entry from list
                draftEntries.Remove(_removeListEntry);

                // Renumber remaining entries ValueID
                int i = 0;
                foreach (Interfaces.Models.ISpecListEntry entry in draftEntries)
                {
                    entry.ValueID = ++i;
                }

                // Refresh the list of Entries
                ClearListEntriesGrid();
                DisplayDraftEntries();
                RestoreEntrySelection();
                CheckEntriesCount(entriesCount);
            }
        }

        private void ChangeSpecCustomId()
        {
            string selCustSpecId= cboCustomTypeSelector.Text;

            switch (specMode)
            {
                case EntryMode.View:
                    break;
                case EntryMode.New:
                    SetSpecCustomId(selCustSpecId);

                    if (selCustSpecId != string.Empty)
                    {
                        draftCustomSpecId = selCustSpecId;
                    }
                    CheckSpecData();
                    break;

                case EntryMode.Edit:
                    SetSpecCustomId(selCustSpecId);

                    if (selCustSpecId != string.Empty)
                    {
                        draftCustomSpecId = selCustSpecId;
                    }
                    //CheckSpecName();
                    CheckSpecData();
                    break;

                default:
                    break;
            }
        }

        private void SetSpecCustomId(string selCustSpecId)
        {
            if (selCustSpecId != string.Empty)
            {
                drafter.DraftCustomSpecId = selCustSpecId;
            }
            CheckSpecData();
        }

        private void ClearSpecsDrafts()
        {
            // DISABLED
            //draftSpec = null;
            draftEntries = null;

            draftCustomSpecId = string.Empty;

            drafter.ClearDraftSpec();
        }

        private void CheckSpecsID()
        {
            if (SpecsMode != EntryMode.View && specMode == EntryMode.View)
            {
                string inputSpecsId = txtSpecsID.Text;
                if (inputSpecsId != string.Empty)
                {
                    ValidateInputId(inputSpecsId);
                }
                else
                {
                    DisplayIdValidityInfo(IdStatus.Blank);
                    IsValidSpecsId = false;
                    //CheckDraftSpecsReady();
                }
                FilterExistingIDs(inputSpecsId);
            }
        }

        private void ValidateInputId(string inputSpecsId)
        {
            if (inputSpecsId != draftSpecsId && DataProvider.GetSpecsIds().Contains(inputSpecsId))
            {
                DisplayIdValidityInfo(IdStatus.Duplicate);
                txtSpecsID.SelectAll();
                txtSpecsID.Focus();
                IsValidSpecsId = false;
                //CheckDraftSpecsReady
            }
            else
            {
                DisplayIdValidityInfo(IdStatus.Valid);
                IsValidSpecsId = true;
                //CheckDraftSpecsReady();
            }
        }

        private void CheckDraftSpecsItemsCount()
        {
            // DISABLED
            //IsSpecsHasItem = draftSpecs.SpecItems.Count() > 0;
            IsSpecsHasItem = drafter.DraftSpecsItemsCount() > 0;
        }

        private void CheckDraftSpecsReady()
        {
            bool isValidDraftSpecs = IsValidSpecsId && IsSpecsHasItem;

            if (isValidDraftSpecs)
            {
                btnAccept.Enabled = true;
            }
            else
            {
                btnAccept.Enabled = false;
            }
        }

        private void CheckSpecName()
        {
            if (specMode != EntryMode.View)
            {
                string inputSpecName = txtSiName.Text;
                if (inputSpecName != string.Empty)
                {
                    IsValidSpecName = true;
                }
                else
                {
                    IsValidSpecName = false;
                }
            }
        }

        private void CheckSpecData()
        {
            // DISABLED
            //bool specValid = false;

            //switch (draftSpecType)
            //{
            //    case SpecType.List:
            //        specValid = draftEntries != null && draftEntries.Count > 0;

            //        break;
            //    case SpecType.Custom:
            //        specValid = draftCustomSpecId != string.Empty;
            //        break;
            //    default:
            //        break;
            //}

            //IsValidSpecData = specValid;

            IsValidSpecData = drafter.IsSpecValid();
        }

        private void CheckDraftSpecItemReady()
        {
            bool isValidDraftSpecItem = IsValidSpecName && IsValidSpecData;

            btnSiAccept.Enabled = isValidDraftSpecItem;
        }

        private void SetDefaultValuePattern()
        {
            txtSiValuePattern.Text = "{val}";
            txtSiValuePattern.SelectAll();
            txtSiValuePattern.Focus();
            CheckTextPattern();
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
            txtSiValuePattern.SelectAll();
            txtSiValuePattern.Focus();
            CheckTextPattern();
        }

        private void CheckTextPattern()
        {
            string valPattern = txtSiValuePattern.Text;

            if (valPattern.Contains("{val}"))
            {
                btnSiInsertVal.Enabled = false;
            }
            else
            {
                btnSiInsertVal.Enabled = true;
            }
        }
        #endregion

        #region User Interaction
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
        #endregion

        private string GenerateNewSpecsID()
        {
            int idCount = DataProvider.GetSpecsIds().Count;

            string newId = $"S{idCount:0000}";

            if (DataProvider.GetSpecsIds().Contains(newId) == true)
            {
                int i = idCount;
                do
                {
                    i++;
                    newId = $"S{i:0000}";
                }
                while (DataProvider.GetSpecsIds().Contains(newId) == true && i > idCount + 1000);

                return newId;
            }
            return newId;
        }


        #region User Interface

        private void RefreshSpecsList()
        {
            PopulateSpecsList();
            CheckSpecsCount();
        }

        private void PopulateSpecsList()
        {
            lbxSpecs.DataSource = DataProvider.GetSpecsIds();
        }

        private void ViewSelectedSpecsData()
        {
            txtSpecsID.Text = selectedSpecs.ID;
            txtSpecsName.Text = selectedSpecs.Name;
            txtSpecsPattern.Text = selectedSpecs.TextPattern;
            dgvSpec.DataSourceResize(selectedSpecs.SpecItems);
        }

        private void ViewSelectedSpecData(int idx)
        {
            if (specMode == EntryMode.View)
            {
                // DISABLED
                //selSpec =
                //    SpecsMode == EntryMode.View ?
                //    SpecsManiuplator.GetSpecsItem(selectedSpecs, idx) :
                //    SpecsManiuplator.GetSpecsItem(draftSpecs, idx);

                selSpec =
                    SpecsMode == EntryMode.View ?
                    SpecsManiuplator.GetSpecsItem(selectedSpecs, idx) :
                    SpecsManiuplator.GetSpecsItem(drafter.DraftSpecs, idx);

                txtSiIndex.Text = idx.ToString();

                txtSiName.Text = selSpec.Name;
                txtSiValuePattern.Text = selSpec.ValuePattern;

                ChangeSpecTypeSelector();
            }
        }

        private void ChangeSpecTypeSelector()
        {
            // List Type Specs Item
            if (selSpec.ListEntries != null)
            {
                dgvListEntries.DataSourceResize(selSpec.ListEntries.ToList());
                rdoListType.Checked = true;
            }
            else
            {
                ClearListEntriesGrid();
                rdoListType.Checked = false;
            }

            // Custom Type Specs Item
            if (selSpec.CustomInputID != null && selSpec.CustomInputID != "")
            {
                cboCustomTypeSelector.Text = selSpec.CustomInputID;
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

        private void HideSpecsReviewUI()
        {
            btnAccept.Visible = false;
            btnCancel.Visible = false;
        }

        private void EnableSpecsMetadataEntryUI()
        {
            txtSpecsID.ReadOnly = false;
            txtSpecsName.ReadOnly = false;
            txtSpecsPattern.ReadOnly = false;
        }

        private void DisableSpecsMetadataEntryUI()
        {
            txtSpecsID.ReadOnly = true;
            txtSpecsID.BackColor = SystemColors.Control;

            txtSpecsName.ReadOnly = true;
            txtSpecsPattern.ReadOnly = true;
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

        private void EnableSpecsModifyUI()
        {
            btnNewSpecs.Enabled = true;
            btnEditSpecs.Enabled = true;
            btnRemoveSpecs.Enabled = true;
        }

        private void DisableSpecsModifyUI()
        {
            btnNewSpecs.Enabled = false;
            btnEditSpecs.Enabled = false;
            btnRemoveSpecs.Enabled = false;
        }

        private void EnableSpecModifyUI()
        {
            //TEST
            dgvSpec.Enabled = true;

            btnSiAdd.Enabled = true;
            btnSiEdit.Enabled = true;
            btnSiRemove.Enabled = true;
        }

        private void DisableSpecModifyUI()
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
                if (specMode == EntryMode.New || specMode == EntryMode.Edit)
                    SelectListType();
            }
            else
            {
                if (specMode == EntryMode.New || specMode == EntryMode.Edit)
                    DeselectListType();
            }
        }

        private void SelectListType()
        {
            drafter.SetSpecTypeToList();

            // DISABLED
            //draftSpecType = SpecType.List;
            grpListEntries.Enabled = true;
            CheckListEntries();
            //CheckSpecName();
            CheckSpecData();
        }

        private void CheckListEntries()
        {
            bool noEntries;
            int entriesCount;

            noEntries = draftEntries == null;
            noEntries = drafter.DraftEntries == null;

            if (noEntries)
            {
                btnListEntryAdd.Enabled = true;
            }
            else
            {
                DisplayDraftEntries();
                EnableListEntryModifyUI();
                
                entriesCount = draftEntries.Count;
                entriesCount = drafter.DraftEntries.Count;

                CheckEntriesCount(entriesCount);
            }
        }

        private void DeselectListType()
        {
            grpListEntries.Enabled = false;
            ClearListEntriesGrid();
        }
        #endregion

        #region Spec Custom Type
        private void CheckSpecCustomType()
        {
            if (rdoCustomType.Checked)
            {
                if (specMode == EntryMode.New || specMode == EntryMode.Edit)
                    SelectCustomType();
            }
            else
            {
                if (specMode == EntryMode.New || specMode == EntryMode.Edit)
                    DeselectCustomType();
            }
        }

        private void SelectCustomType()
        {
            drafter.SetSpecTypeToCustom();

            // DISABLED
            //draftSpecType = SpecType.Custom;
            cboCustomTypeSelector.Enabled = true;

            if (draftCustomSpecId != null)
            {
                cboCustomTypeSelector.Text = draftCustomSpecId;
            }

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

        private void ResetSpecTypeUI()
        {
            rdoListType.Checked = false;
            rdoCustomType.Checked = false;
        }

        private void ClearSpecsMetadataEntryUI()
        {
            txtSpecsID.Clear();
            txtSpecsName.Clear();
            txtSpecsPattern.Clear();
        }

        private void ClearSpecMetadataEntryUI()
        {
            txtSiIndex.Clear();
            txtSiName.Clear();
            txtSiValuePattern.Clear();
        }

        private void DisplayDraftEntries()
        {
            dgvListEntries.DataSourceResize(draftEntries);
            dgvListEntries.DataSourceResize(drafter.DraftEntries);
        }

        private void ClearSpecItemsGrid()
        {
            dgvSpec.DataSource = null;
        }

        private void ClearListEntriesGrid()
        {
            dgvListEntries.DataSource = null;
        }

        private void SaveSpecsSelectionPosition(bool shiftUp = false)
        {
            int selectedIndex = lbxSpecs.SelectedIndex;

            if (selectedIndex == DataProvider.GetSpecsIds().Count - 1)
            {
                specsSelectionIndex = DataProvider.GetSpecsIds().Count - 1;

                if (shiftUp)
                {
                    specsSelectionIndex -= 1;
                }
            }
            else
                specsSelectionIndex = selectedIndex;
        }

        private void RestoreSpecsSelection()
        {
            if (specsSelectionIndex > -1)
            {
                lbxSpecs.SelectedIndex = specsSelectionIndex;
            }
        }

        private void SaveSpecItemSelectionPosition(int itemsCount)
        {
            int selectedIndex = dgvSpec.SelectedRows[0].Index;
            if (selectedIndex == itemsCount - 1)
                specSelectionIndex = itemsCount - 2;
            else
            {
                specSelectionIndex = selectedIndex;
            }
        }

        private void RestoreSpecItemSelection()
        {
            if (specSelectionIndex > -1)
            {
                dgvSpec.Rows[specSelectionIndex].Selected = true;
                dgvSpec.FirstDisplayedScrollingRowIndex = specSelectionIndex;
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

        private void DisplayIdValidityInfo(IdStatus status)
        {
            switch (status)
            {
                case IdStatus.Valid:
                    lblSpecsIdValidator.Text = string.Empty;
                    txtSpecsID.BackColor = SystemColors.Window;
                    break;
                case IdStatus.Duplicate:
                    lblSpecsIdValidator.Text = "* Duplicate ID";
                    txtSpecsID.BackColor = Color.HotPink;
                    break;
                case IdStatus.Blank:
                    lblSpecsIdValidator.Text = "* Blank ID";
                    txtSpecsID.BackColor = Color.Pink;
                    break;
                default:
                    break;
            }
        }

        private void FilterExistingIDs(string inputSpecsId)
        {
            if (inputSpecsId == string.Empty)
            {
                lbxSpecs.DataSource = DataProvider.GetSpecsIds();
            }
            else
            {
                lbxSpecs.DataSource = DataProvider.FilterSpecsIds(inputSpecsId);
            }

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
            SaveToDataSource();
        }

        private void lbxSpecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SpecsMode == EntryMode.View)
                {
                    if (lbxSpecs.SelectedIndex != -1)
                    {
                        ReadSelectedSpecsData();
                        ViewSelectedSpecsData();
                    }
                }
            }
            catch (Exception) { }
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
            if (e.RowIndex >= 0)
            {
                DoubleClickEditSpec();
            }
        }
        private void dgvListEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DoubleClickEditListEntry();
            }
        }
        private void cboCustomTypeSelector_SelectedIndexChanged(object sender, EventArgs e) => ChangeSpecCustomId();
        private void btnNewSpecs_Click(object sender, EventArgs e) => AddNewSpecs();
        private void btnEditSpecs_Click(object sender, EventArgs e) => EditSpecs();
        private void btnAccept_Click(object sender, EventArgs e) => SaveChanges();
        private void btnCancel_Click(object sender, EventArgs e) => CancelSpecsDrafting();
        private void btnRemoveSpecs_Click(object sender, EventArgs e) => RemoveSpecs();
        private void txtSpecsID_TextChanged(object sender, EventArgs e) => CheckSpecsID();
        private void btnSiAdd_Click(object sender, EventArgs e) => NewSpec();
        private void btnSiEdit_Click(object sender, EventArgs e) => EditSpec();
        private void btnSiRemove_Click(object sender, EventArgs e) => RemoveSpec();
        private void btnSiAccept_Click(object sender, EventArgs e) => SaveDraftSpec();
        private void btnSiCancel_Click(object sender, EventArgs e) => CancelSpecChanges();
        private void txtSiName_TextChanged(object sender, EventArgs e) => CheckSpecName();
        private void txtSiValuePattern_TextChanged(object sender, EventArgs e) => CheckTextPattern();
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
        private void SpecsEditor_Load(object sender, EventArgs e)
        {
            PostLoading();
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
        private void dgvSpec_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void dgvSpec_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dgvSpec.SelectedRows.Count > 0 && SpecsMode != EntryMode.View && specMode == EntryMode.View)
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
        #endregion
#pragma warning restore IDE1006 // Naming Styles

    }
}