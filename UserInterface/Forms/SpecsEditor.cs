
using ClientService;
using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Models;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//using UserService;


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

        #region Fields
        private EntryMode specMode = EntryMode.View;
        //private List<string> specsIdList;
        //private List<string> cSpecIdList;
        //private List<string> filteredspecsIdList;
        //private string selectedSpecsId;
        //private CoreLibrary.Interfaces.ISpecs selectedSpecs;
        private Interfaces.Models.ISpecs _selectedSpecs;
        //private CoreLibrary.Interfaces.ISpec selSpec;
        private Interfaces.Models.ISpecsItem _selSpec;
        private string draftSpecsId;
        //private CoreLibrary.Interfaces.ISpecs draftSpecs;
        private Interfaces.Models.ISpecs _draftSpecs;
        //private CoreLibrary.Interfaces.ISpec draftSpec;
        private Interfaces.Models.ISpecsItem _draftSpec;
        private SpecType draftSpecType;
        //private List<CoreLibrary.Interfaces.ISpecListEntry> draftEntries;
        private List<Interfaces.Models.ISpecListEntry> _draftEntries;
        private string draftCustomSpecId;
        private int specsSelectionIndex = 0;
        private int specSelectionIndex;
        private int entrySelectionIndex;
        #endregion

        public SpecsEditor() => InitializeComponent();

        private void SaveToDataSource()
        {
            //UserService.Data.Save(ContextEntity.Specs);
            ContextProvider.Save(ContextEntity.Specs);
        }

        #region Processes

        private void PostLoading()
        {
            RefreshSpecsList();
            //ReadCustomSpecs();

            // Bind Custom Specs Selector
            cboCustomTypeSelector.DataSource =
                DataManager.GetCustomSpecsList();

            ClearCustomTypeSelector();

            EnableSpecsModifyUI();
        }

        //private void InsertEmptySpecId()
        //{
        //    //cSpecIdList.Insert(0, string.Empty);
        //}

        //private void ReadSpecsIDs()
        //{
        //    // old
        //    //specsIdList = UserService.Data.GetSpecsIdList().ToList();

        //    // new
        //    // TODO: this variable should be contained a separate service class
        //    //specsIdList = DataManager.GetSpecsList().Select(e => e.ID).ToList();
        //}

        //private void ReadCustomSpecs()
        //{
        //    // old
        //    //cSpecIdList = UserService.Data.GetCustomSpecs();

        //    // new
        //    //cSpecIdList = DataManager.GetCustomSpecsList();
        //}

        private void ReadSelectedSpecsData()
        {
            string specsId = lbxSpecs.Text;
            //selectedSpecsId = specsId;

            // old
            //selectedSpecs = UserService.Data.GetSpecs(specsId);

            // new
            _selectedSpecs = SpecsRepository.Read(specsId);
        }

        private void CancelSpecsAddOrEdit()
        {
            SpecsMode = EntryMode.View;
            specMode = EntryMode.View;

            // old
            //draftSpecs = null;

            // new
            _draftSpecs = null;

            ClearSpecsDrafts();

            EnableSpecsListSelection();


            // TRY
            //if (specsIdList.Count > 0)
            if (DataProvider.SpecsIds.Count > 0)
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

            //if (draftSpecsId != string.Empty)
            //SelectSpecs(draftSpecsId);
            // TEST
            RestoreSpecsSelection();

            btnNewSpecs.Focus();
        }

        // Blank Lines are left for comparison with EditSpecs method
        private void AddNewSpecs()
        {


            SaveSpecsSelectionPosition();
            // Instantiate new Specs
            //draftSpecs = new Specs();
            _draftSpecs = new Modeling.DataModels.Specs();

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

            // old
            //txtSpecsPattern.Text = draftSpecs.TextPattern;

            // new
            txtSpecsPattern.Text = _draftSpecs.TextPattern;

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

            // old
            //draftSpecs = UserService.Data.GetSpecs(draftSpecsId);

            // new
            _draftSpecs = SpecsRepository.Read(draftSpecsId);

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

        private void RemoveSpecs()
        {
            int rowsCount = lbxSpecs.SelectedItems.Count;
            if (rowsCount == 0)
                return;

            SaveSpecsSelectionPosition(true);

            if (ShowSpecsRemoveConfirmation() == DialogResult.OK)
            {
                string specsId = GetSelectedSpecsId();

                // old
                //UserService.Data.DeleteSpecs(specsId);

                // new
                SpecsRepository.Delete(specsId);

                CheckSpecsCount();

                RefreshSpecsList();

                // TRY
                //if (specsIdList.Count > 0)
                if (DataProvider.SpecsIds.Count > 0)
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
            // TRY
            //if (specsIdList.Count < 1)
            if (DataProvider.SpecsIds.Count < 1)
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

        private void CreateNewSpec()
        {
            specMode = EntryMode.New;

            // Instantiate new Spec
            //draftSpec = new Spec();
            _draftSpec = new SpecsItem();

            // Get last SpecsItem index

            // old
            //int lastIdx = draftSpecs.SpecItems.Count; //GetLastSpecsItemIndex();

            // new
            int lastIdx = _draftSpecs.SpecItems.Count();

            // Set Initial member values
            int newIdx = lastIdx + 1;
            string name = $"SI{newIdx:000}";

            // Set object members
            //draftSpec.Index = newIdx;
            //draftSpec.Name = name;

            _draftSpec.Index = newIdx;
            _draftSpec.Name = name;

            txtSiIndex.Text = newIdx.ToString();
            txtSiName.Text = name;

            //txtSiValuePattern.Text = draftSpec.ValuePattern;
            txtSiValuePattern.Text = _draftSpec.ValuePattern;

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

        private void DoubleClickEditSpec()
        {
            if (SpecsMode != EntryMode.View && specMode == EntryMode.View)
                EditSpec();
        }

        private void EditSpec()
        {
            specMode = EntryMode.Edit;

            // Get Spec object being edited

            // old
            /*
            draftSpec = draftSpecs.SpecItems[GetSelectedSpecIndex() - 1];
            
            draftSpecType = draftSpec.SpecType;

            switch (draftSpec.SpecType)
            {
                case SpecType.List:
                    //old
                    //draftEntries = draftSpec.CopyEntries();
                    break;
                case SpecType.Custom:
                    draftCustomSpecId = draftSpec.CustomInputID;
                    break;
            }
            */

            // new
            _draftSpec =
                _draftSpecs.SpecItems.ToList()[GetSelectedSpecIndex() - 1];

            if (_draftSpec.ListEntries != null)
            {
                draftSpecType = SpecType.List;
                _draftEntries =
                    new List<Interfaces.Models.ISpecListEntry>(_draftSpec.ListEntries);
            }

            if (_draftSpec.CustomInputID != null)
            {
                draftSpecType = SpecType.Custom;
                draftCustomSpecId = _draftSpec.CustomInputID;
            }

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

                // old
                //CheckEntriesCount(draftEntries.Count);

                // new
                CheckEntriesCount(_draftEntries.Count);
            }
            //else Do Nothing

            if (rdoCustomType.Checked)
            {
                cboCustomTypeSelector.Enabled = true;
                cboCustomTypeSelector.Text = draftCustomSpecId;
            }
            //else Do Nothing
        }

        private void SaveDraftSpec()
        {
            // Save new Spec data

            // old
            //draftSpec.Index = int.Parse(txtSiIndex.Text);
            //draftSpec.Name = txtSiName.Text;
            //draftSpec.ValuePattern = txtSiValuePattern.Text;

            // new
            _draftSpec.Index = int.Parse(txtSiIndex.Text);
            _draftSpec.Name = txtSiName.Text;
            _draftSpec.ValuePattern = txtSiValuePattern.Text;

            switch (draftSpecType)
            {
                case SpecType.List:
                    // old
                    //draftSpec.AddEntries(draftEntries);

                    // new
                    _draftSpec.ListEntries =
                    new List<Interfaces.Models.ISpecListEntry>(_draftEntries);

                    _draftSpec.CustomInputID = null;

                    break;
                case SpecType.Custom:
                    // old
                    //draftSpec.SetCustomId(draftCustomSpecId);

                    // new
                    _draftSpec.CustomInputID = draftCustomSpecId;
                    _draftSpec.ListEntries = null;

                    break;
            }

            // Add the created Spec to Spec list of the new Specs
            if (specMode == EntryMode.New)
            {
                //draftSpecs.SpecItems.Add(draftSpec);

                List<Interfaces.Models.ISpecsItem> tempList =
                    _draftSpecs.SpecItems.ToList();

                tempList.Add(_draftSpec);

                _draftSpecs.SpecItems = tempList;
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

            // old
            //if (draftSpecs.SpecItems.Count <= 1) { }

            // new
            if (_draftSpecs.SpecItems.Count() <= 1)
            {
                btnSiRemove.Enabled = false;
            }

            HideSpecReviewUI();
            DisableSpecMetadataEntryUI();
            DisableSpecTypeUI();
            ResetSpecTypeUI();
            DisableListEntryModifyUI();

            // Clear the list entries
            ClearListEntriesGrid();

            // Refresh View
            ClearSpecItemsGrid();

            dgvSpec.DataSource =
                //draftSpecs.SpecItems; // old
                _draftSpecs.SpecItems.ToList(); // new

            dgvSpec.AutoResizeColumns();
        }

        private void CancelSpecChanges()
        {
            // Set EntryMode to View
            specMode = EntryMode.View;

            // Null draft objects
            ClearSpecsDrafts();

            ResetSpecUI();

            // old
            //if (draftSpecs.SpecItems.Count <= 0) { }

            // new
            if (_draftSpecs.SpecItems.Count() <= 0)
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
                // old
                //SaveSpecItemSelectionPosition(draftSpecs.SpecItems.Count);

                // new
                SaveSpecItemSelectionPosition(_draftSpecs.SpecItems.Count());

                if (ShowSpecRemoveConfirmation() == DialogResult.OK)
                {
                    // old
                    /*
                    CoreLibrary.Interfaces.ISpec specsItem = draftSpecs.SpecItems
                        .Find(idx => idx.Index == GetSelectedSpecIndex());

                    draftSpecs.SpecItems.Remove(specsItem);
                    */

                    // new
                    Interfaces.Models.ISpecsItem _specsItem = _draftSpecs.SpecItems
                        .FirstOrDefault(idx => idx.Index == GetSelectedSpecIndex());

                    List<Interfaces.Models.ISpecsItem> tempList = _draftSpecs.SpecItems.ToList();
                    tempList.Remove(_specsItem);
                    _draftSpecs.SpecItems = tempList;

                    // Renumber SpecItems
                    int i = 0;
                    // old
                    /*
                    foreach (CoreLibrary.Interfaces.ISpec spec in draftSpecs.SpecItems)
                    {
                        spec.Index = ++i;
                    }
                    */

                    i = 0;
                    // new
                    foreach (Interfaces.Models.ISpecsItem spec in _draftSpecs.SpecItems)
                    {
                        spec.Index = ++i;
                    }

                    ClearSpecItemsGrid();

                    // old
                    //dgvSpec.DataSource = draftSpecs.SpecItems;

                    // new
                    dgvSpec.DataSource = _draftSpecs.SpecItems.ToList();

                    dgvSpec.AutoResizeColumns();
                }

                RestoreSpecItemSelection();

                // old
                //if (draftSpecs.SpecItems.Count <= 0) { }

                // new
                if (_draftSpecs.SpecItems.Count() <= 0)
                {
                    // Set to null to remove columns
                    ClearSpecItemsGrid();

                    // Disable Edit and Delete buttons for Spec modification
                    btnSiAdd.Focus();
                    btnSiEdit.Enabled = false;

                    ClearSpecMetadataEntryUI();
                    dgvListEntries.DataSource = null;
                }

                // old
                //if (draftSpecs.SpecItems.Count <= 1) { }

                // new
                if (_draftSpecs.SpecItems.Count() <= 1)
                    btnSiRemove.Enabled = false;
            }
        }

        private void SaveChanges()
        {
            // Save draft (new) Specs metadata
            /*draftSpecs.ID = txtSpecsID.Text;
            draftSpecs.Name = txtSpecsName.Text;
            draftSpecs.TextPattern = txtSpecsPattern.Text;*/

            _draftSpecs.ID = txtSpecsID.Text;
            _draftSpecs.Name = txtSpecsName.Text;
            _draftSpecs.TextPattern = txtSpecsPattern.Text;

            if (SpecsMode == EntryMode.New)
            {
                // old
                //UserService.Data.AddSpecs(draftSpecs);

                // new
                SpecsRepository.Create(_draftSpecs);
            }

            if (SpecsMode == EntryMode.Edit)
            {
                // old
                //UserService.Data.UpdateSpecs(draftSpecsId, draftSpecs);

                // new
                SpecsRepository.Update(draftSpecsId, _draftSpecs);
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

            // old
            //SelectSpecs(draftSpecs.ID);

            // new
            SelectSpecs(_draftSpecs.ID);

            // TEST
            // old
            //draftSpecs = null;

            // new
            _draftSpecs = null;
        }

        private void AddNewListEntry()
        {
            ListEntryEditor listEditor = new ListEntryEditor();

            // Copy list entries of draftSpec, if any
            //if (draftEntries == null)
            //    draftEntries = draftSpec.CopyEntries();


            // new
            if (_draftEntries == null)
            {
                _draftEntries =
                    _draftSpec.ListEntries == null ?
                    new List<Interfaces.Models.ISpecListEntry>() :
                    new List<Interfaces.Models.ISpecListEntry>(_draftSpec.ListEntries);
            }

            if (listEditor.ShowDialog() == DialogResult.OK)
            {
                // Get last entryID
                // old
                //int lastId = draftEntries.Count;

                // new
                int lastId = _draftEntries.Count;

                // Generate new entryID
                int newId = lastId + 1;

                // Set ID for the new entry
                // old
                //listEditor.ListEntry.ValueID = newId;

                // new
                listEditor.ListEntry2.ValueID = newId;

                // Add the new entry to the draft list

                // old
                //draftEntries.Add(listEditor.ListEntry);

                // new
                _draftEntries.Add(listEditor.ListEntry2);

                // Enable Edit and Delete buttons
                EnableListEntryModifyUI();

                // old
                //CheckEntriesCount(draftEntries.Count);

                // new
                CheckEntriesCount(_draftEntries.Count);

                //CheckSpecName();
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

            // old
            /*CoreLibrary.Interfaces.ISpecListEntry editListEntry = draftEntries
                .Find(id => id.ValueID == entryId);*/

            // new
            Interfaces.Models.ISpecListEntry _editListEntry = _draftEntries
                .Find(id => id.ValueID == entryId);

            // old
            //ListEntryEditor listEditor = new ListEntryEditor(editListEntry);

            // new
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

            // old
            //CoreLibrary.Interfaces.ISpecListEntry editListEntry = draftEntries.Find(id => id.ValueID == entryId);

            // new
            Interfaces.Models.ISpecListEntry _editListEntry = _draftEntries.Find(id => id.ValueID == entryId);

            if (ShowEntryRemoveConfirmation() == DialogResult.OK)
            {
                // old
                //SaveEntrySelectionPosition(draftEntries.Count);

                // new
                SaveEntrySelectionPosition(_draftEntries.Count);

                // Remove entry from list
                // old
                //draftEntries.Remove(editListEntry);

                // new
                _draftEntries.Remove(_editListEntry);

                // Renumber remaining entries ValueID
                int i = 0;

                // old
                /*
                foreach (CoreLibrary.Interfaces.ISpecListEntry entry in draftEntries)
                {
                    entry.ValueID = ++i;
                }
                */

                i = 0;
                // new
                foreach (Interfaces.Models.ISpecListEntry entry in _draftEntries)
                {
                    entry.ValueID = ++i;
                }

                // Refresh the list of Entries
                ClearListEntriesGrid();
                DisplayDraftEntries();

                RestoreEntrySelection();

                // old
                //CheckEntriesCount(draftEntries.Count);

                // new
                CheckEntriesCount(_draftEntries.Count);
            }
        }

        private void ChangeSpecCustomId()
        {
            string selCustSpecId;

            switch (specMode)
            {
                case EntryMode.View:
                    break;
                case EntryMode.New:
                    selCustSpecId = cboCustomTypeSelector.Text;
                    if (selCustSpecId != string.Empty)
                    {
                        draftCustomSpecId = selCustSpecId;
                    }
                    CheckSpecData();
                    break;
                case EntryMode.Edit:
                    selCustSpecId = cboCustomTypeSelector.Text;
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

        private void ClearSpecsDrafts()
        {
            // old
            //draftSpec = null;
            //draftEntries = null;

            // new
            _draftSpec = null;
            _draftEntries = null;

            draftCustomSpecId = string.Empty;
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
            // TRY
            //if (inputSpecsId != draftSpecsId && specsIdList.Contains(inputSpecsId))
            if (inputSpecsId != draftSpecsId && DataProvider.SpecsIds.Contains(inputSpecsId))
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
            IsSpecsHasItem =
                //draftSpecs.SpecItems.Count > 0;
                _draftSpecs.SpecItems.Count() > 0;
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
            bool specValid = false;

            switch (draftSpecType)
            {
                case SpecType.List:
                    // old
                    //specValid = draftEntries != null && draftEntries.Count > 0;

                    // new
                    specValid = _draftEntries != null && _draftEntries.Count > 0;

                    break;
                case SpecType.Custom:
                    specValid = draftCustomSpecId != string.Empty;
                    break;
                default:
                    break;
            }

            IsValidSpecData = specValid;
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

        #region Getters
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

            return //(int)dgvListEntries.SelectedRows[0].Cells[0].Value;
                (int)dgvListEntries.SelectedRows[0].Cells["ValueID"].Value;
        }
        #endregion

        private string GenerateNewSpecsID()
        {
            // TRY
            int idCount = //specsIdList.Count();
                DataProvider.SpecsIds.Count;

            string newId = $"S{idCount:0000}";

            // if (specsIdList.Contains(newId) == true)
            if (DataProvider.SpecsIds.Contains(newId) == true)
            {
                int i = idCount;
                do
                {
                    i++;
                    newId = $"S{i:0000}";
                }
                while //(specsIdList.Contains(newId) == true && i > idCount + 1000);
                    (DataProvider.SpecsIds.Contains(newId) == true && i > idCount + 1000);

                return newId;
            }
            return newId;
        }


        #region User Interface

        private void RefreshSpecsList()
        {
            //ReadSpecsIDs();
            PopulateSpecsList();
            CheckSpecsCount();
        }

        //private void RefreshSpecsItemsGrid()
        //{
        //    ReadSelectedSpecsData();
        //    ViewSelectedSpecsData();
        //}

        private void PopulateSpecsList() => lbxSpecs.DataSource = DataProvider.SpecsIds;

        //private void PopulateSpecsList()
        //{
        //    // TRY
        //    //if (specsIdList.Count > 0)
        //    if (DataProvider.SpecsIds.Count > 0)
        //    {
        //        //lbxSpecs.DataSource = specsIdList;
        //        lbxSpecs.DataSource = DataProvider.SpecsIds;
        //    }
        //    else
        //    {
        //        // This is a funny redundant useless code :)
        //        lbxSpecs.DataSource = specsIdList;
        //    }
        //}

        private void ViewSelectedSpecsData()
        {
            // old
            //txtSpecsID.Text = selectedSpecs.ID;
            //txtSpecsName.Text = selectedSpecs.Name;
            //txtSpecsPattern.Text = selectedSpecs.TextPattern;
            //dgvSpec.DataSourceResize(selectedSpecs.SpecItems);

            // new
            txtSpecsID.Text = _selectedSpecs.ID;
            txtSpecsName.Text = _selectedSpecs.Name;
            txtSpecsPattern.Text = _selectedSpecs.TextPattern;
            dgvSpec.DataSourceResize(_selectedSpecs.SpecItems);
        }

        private void ViewSelectedSpecData(int idx)
        {
            if (specMode == EntryMode.View)
            {
                //string specsId = GetSelectedSpecsId();
                //Console.WriteLine("{0}, {1}", SpecsMode, specsId);

                // old
                /*selSpec =
                    SpecsMode == EntryMode.View ?
                    UserService.Data.GetSpecsItem(selectedSpecs, idx) :
                    UserService.Data.GetSpecsItem(draftSpecs, idx);*/

                // new
                _selSpec =
                    SpecsMode == EntryMode.View ?
                    SpecsManiuplator.GetSpecsItem(_selectedSpecs, idx) :
                    SpecsManiuplator.GetSpecsItem(_draftSpecs, idx);

                txtSiIndex.Text = idx.ToString();

                // old
                /*txtSiName.Text = selSpec.Name;
                txtSiValuePattern.Text = selSpec.ValuePattern;*/

                // new
                txtSiName.Text = _selSpec.Name;
                txtSiValuePattern.Text = _selSpec.ValuePattern;

                ChangeSpecTypeSelector();
            }
        }

        private void ChangeSpecTypeSelector()
        {
            // List Type Specs Item
            if (/*selSpec.SpecType == SpecType.List*/_selSpec.ListEntries != null)
            {
                dgvListEntries.DataSource = /*selSpec*/_selSpec.ListEntries.ToList();
                dgvListEntries.AutoResizeColumns();
                rdoListType.Checked = true;
            }
            else
            {
                ClearListEntriesGrid();
                rdoListType.Checked = false;
            }

            // Custom Type Specs Item
            if (/*selSpec.SpecType == SpecType.Custom*/_selSpec.CustomInputID != null && _selSpec.CustomInputID != "")
            {
                cboCustomTypeSelector.Text = /*selSpec*/ _selSpec.CustomInputID;
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
            draftSpecType = SpecType.List;
            grpListEntries.Enabled = true;
            CheckListEntries();
            //CheckSpecName();
            CheckSpecData();
        }

        private void CheckListEntries()
        {
            if (/*draftEntries*/_draftEntries == null)
            {
                btnListEntryAdd.Enabled = true;
            }
            else
            {
                DisplayDraftEntries();
                EnableListEntryModifyUI();

                // old
                //CheckEntriesCount(draftEntries.Count);

                // new
                CheckEntriesCount(_draftEntries.Count);
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
            draftSpecType = SpecType.Custom;
            cboCustomTypeSelector.Enabled = true;
            if (draftCustomSpecId != null)
            {
                cboCustomTypeSelector.Text = draftCustomSpecId;
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
            dgvListEntries.DataSource =
                //draftEntries;
                _draftEntries;

            dgvListEntries.AutoResizeColumns();
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

            // TRY
            // if (selectedIndex == specsIdList.Count - 1)
            if (selectedIndex == DataProvider.SpecsIds.Count - 1)
            {
                specsSelectionIndex = //specsIdList.Count - 1;
                    DataProvider.SpecsIds.Count - 1;

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
                // ToBeRemoved
                //lbxSpecs.DataSource = specsIdList;

                // NewCode
                lbxSpecs.DataSource = DataProvider.SpecsIds;
            }
            else
            {
                //filteredspecsIdList = specsIdList.Where(id => id.Contains(inputSpecsId)).ToList();

                lbxSpecs.DataSource = //filteredspecsIdList;
                    DataProvider.FilterSpecsIds(inputSpecsId);
            }

        }

        private void SelectTextbox(TextBox textBox)
        {
            textBox.SelectAll();
            textBox.Focus();
        }
        #endregion // User Interface

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
        private void btnCancel_Click(object sender, EventArgs e) => CancelSpecsAddOrEdit();
        private void btnRemoveSpecs_Click(object sender, EventArgs e) => RemoveSpecs();
        private void txtSpecsID_TextChanged(object sender, EventArgs e) => CheckSpecsID();
        private void btnSiAdd_Click(object sender, EventArgs e) => CreateNewSpec();
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