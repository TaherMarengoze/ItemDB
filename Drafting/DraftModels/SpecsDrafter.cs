
//using CoreLibrary.Enums;
using ClientService;
using Interfaces.Models;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drafting
{
    public partial class SpecsDrafter : Interfaces.General.IDraftable
    {
        public enum SpecType
        {
            List = 1,
            Custom = 2
        }

        public enum ValidityStatus
        {
            Valid,
            Duplicate,
            Blank,
            Invalid
        }

        /// <summary>
        /// Temporary value holder for edit object ID,to refer to old ID value in case it was changed.
        /// </summary>
        private string refId;


        public event EventHandler<bool> OnSpecsValidityChange;
        public event EventHandler<bool> OnSpecItemValidityChange;
        public event EventHandler<ValidityStatus> OnSpecsIdValidityChange;

        public ISpecs SelectedSpecs { get; private set; }

        public ISpecsItem SelectedSpecsItem { get; private set; }

        public ISpecs DraftSpecs { get; private set; }

        public ISpecsItem DraftSpecsItem { get; private set; }

        public SpecType DraftSpecsItemType { get; private set; }

        public List<ISpecListEntry> DraftEntries { get; set; }

        public string DraftCustomSpecId { get; set; }

        private string _inputSpecsId;
        /// <summary>
        /// Temporary input for specs ID.
        /// </summary>
        public string InputSpecsId
        {
            get => _inputSpecsId;
            set
            {
                _inputSpecsId = value;
                if (_inputSpecsId == string.Empty)
                {
                    IdStatus = ValidityStatus.Blank;
                }
                else
                {
                    bool isInputNotDraft = _inputSpecsId != DraftSpecs.ID;
                    bool isDuplicateInput = DataProvider.GetSpecsIds().Contains(_inputSpecsId);

                    if (isInputNotDraft && isDuplicateInput)
                    {
                        IdStatus = ValidityStatus.Duplicate;
                    }
                    else
                    {
                        bool isValidChar = true;
                        // Should also check for invalid characters
                        if (isValidChar)
                            IdStatus = ValidityStatus.Valid;
                        else
                            IdStatus = ValidityStatus.Invalid;
                    }
                }
                ExistingIDs = FilterExistingIDs(_inputSpecsId);
            }
        }

        private string inputSpecsName;
        /// <summary>
        /// Temporary input for specs name.
        /// </summary>
        public string InputSpecsName
        {
            get => inputSpecsName; set => inputSpecsName = value;
        }

        private string inputSpecsTxtPat;
        /// <summary>
        /// Temporary input for specs text pattern.
        /// </summary>
        public string InputSpecsTxtPat
        {
            get => inputSpecsTxtPat; set => inputSpecsTxtPat = value;
        }

        private List<ISpecsItem> _inputSpecsItems;
        /// <summary>
        /// Temporary input for specs items.
        /// </summary>
        public List<ISpecsItem> InputSpecsItems
        {
            get => _inputSpecsItems;
            set
            {
                _inputSpecsItems = value;
                IsSpecsHasItem = value.Count > 0 ? true : false;
            }
        }

        // TEST / TRIAL
        public System.Collections.ObjectModel.ObservableCollection<ISpecsItem> SpecsItems { get; set; }

        private void TestMethod()
        {
            SpecsItems.CollectionChanged += SpecsItems_CollectionChanged;
        }

        private void SpecsItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsSpecsHasItem =
            ((System.Collections.ObjectModel.ObservableCollection<ISpecsItem>)sender).Count > 0 ? true : false;
        }
        // END TEST / TRIAL

        private string _inputSpecName;
        public string InputSpecName
        {
            get => _inputSpecName; set
            {
                _inputSpecName = value;
                if (_inputSpecName != string.Empty)
                {
                    // Set a valid name flag to true
                    IsValidSpecName = true;
                }
                else
                {
                    // Set a valid name flag to false
                    IsValidSpecName = false;
                }
            }
        }

        public int specIndex;
        public string specPattern;

        private ValidityStatus _idStatus;
        public ValidityStatus IdStatus
        {
            get => _idStatus;
            private set
            {
                _idStatus = value;

                // Raise an event on ID status change
                OnSpecsIdValidityChange?.Invoke(this, value);

                CheckSpecsReady();
            }
        }

        private bool _isSpecsHasItem;
        public bool IsSpecsHasItem
        {
            get { return _isSpecsHasItem; }
            set
            {
                _isSpecsHasItem = value;
                CheckSpecsReady();
            }
        }

        private bool _isValidSpecName;
        public bool IsValidSpecName
        {
            get => _isValidSpecName; private set
            {
                _isValidSpecName = value;
                CheckSpecsItemReady();
            }
        }

        private bool _isValidSpecData;
        public bool IsValidSpecData
        {
            get { return _isValidSpecData; }
            set
            {
                _isValidSpecData = value;
                CheckSpecsItemReady();
            }
        }

        #region Drafting Initializers

        public void NewDraftSpecs()
        {
            DraftSpecs = new Specs() /*{ ID = GenerateNewSpecsID() }*/;

            InputSpecsId = GenerateNewSpecsID();
            InputSpecsName = string.Empty;
            InputSpecsTxtPat = DraftSpecs.TextPattern;
            InputSpecsItems = new List<ISpecsItem>();

            // TEST
            ClearSelectionObjects();
        }

        public void EditSpecs(string specsId)
        {
            DraftSpecs = SpecsRepository.Read(specsId);

            // TEST
            ClearSelectionObjects();

            refId = DraftSpecs.ID;

            // Copy edit object to temporary fields
            InputSpecsId = DraftSpecs.ID;
            InputSpecsName = DraftSpecs.Name;
            InputSpecsTxtPat = DraftSpecs.TextPattern;
            InputSpecsItems = DraftSpecs.SpecItems.Clone();
        }

        public void NewDraftSpecsItem()
        {
            DraftSpecsItem = new SpecsItem();

            int lastIdx = DraftSpecs.SpecItems.Count();
            int newIdx = lastIdx + 1;

            DraftSpecsItem.Index = newIdx;
            DraftSpecsItem.Name = $"SI{newIdx:000}";
        }

        public void EditSpecsItem(int specsItemIndex)
        {
            DraftSpecsItem = DraftSpecs.SpecItems
                .FirstOrDefault(si => si.Index == specsItemIndex);

            if (DraftSpecsItem.ListEntries != null)
            {
                DraftSpecsItemType = SpecType.List;
                DraftEntries = new List<ISpecListEntry>(DraftSpecsItem.ListEntries);
            }

            if (DraftSpecsItem.CustomInputID != null)
            {
                DraftSpecsItemType = SpecType.Custom;
                DraftCustomSpecId = DraftSpecsItem.CustomInputID;
            }
        }

        #endregion

        private void CheckSpecsReady()
        {
            bool isValidDraftSpecs = IdStatus == ValidityStatus.Valid && IsSpecsHasItem;

            // raise event for ready state
            OnSpecsValidityChange?.Invoke(this, isValidDraftSpecs);
        }

        private void CheckSpecsItemReady()
        {
            bool isValidDraftSpecItem = IsValidSpecName && IsValidSpecData;

            if (isValidDraftSpecItem)
            {
                // raise event for ready state
                OnSpecItemValidityChange?.Invoke(this, true);
            }
            else
            {
                // raise event for not ready state
                OnSpecItemValidityChange?.Invoke(this, false);
            }
        }

        public int DraftSpecsItemsCount()
        {
            return DraftSpecs.SpecItems.Count();
        }

        public void CommitChanges()
        {
            DraftSpecs.ID = _inputSpecsId;
            DraftSpecs.Name = InputSpecsName;
            DraftSpecs.TextPattern = InputSpecsTxtPat;
        }

        public void Clear()
        {
            ClearDraftSpecs();
            ClearDraftSpec();
        }

        private void ClearSelectionObjects()
        {
            SelectedSpecs = null;
            SelectedSpecsItem = null;
        }

        public void ClearDraftSpecs()
        {
            DraftSpecs = null;
        }

        public void ClearDraftSpec()
        {
            DraftSpecsItem = null;
            DraftEntries = null;
            DraftCustomSpecId = string.Empty;
        }

        /// <summary>
        /// Adds the draft spec item to the draft specs items list.
        /// </summary>
        public void AddSpecToSpecsItemsDrafts()
        {
            //List<ISpecsItem> tempList = DraftSpecs.SpecItems.ToList();
            //tempList.Add(DraftSpec);
            //InputSpecsItems = tempList;
            //DraftSpecs.SpecItems = tempList;
            AddSpec();
            DraftSpecs.SpecItems = InputSpecsItems;
        }

        /// <summary>
        /// Removes a spec item (<see cref="ISpecsItem"/>) from the spec list of the draft specs (<see cref="DraftSpecs"/>).
        /// </summary>
        /// <param name="specIndex">The index of the spec to remove</param>
        public void RemoveSpecFromDraftSpecsItems(int specIndex)
        {
            // FIX: its wrong to remove the spec from the draft specs; its should be removed from the temporary input
            List<ISpecsItem> specItemsList =
                DraftSpecs.SpecItems.ToList();

            ISpecsItem specItem =
                DraftSpecs.SpecItems.FirstOrDefault(idx => idx.Index == specIndex);

            specItemsList.Remove(specItem);

            // Renumber SpecItems
            int i = 0;
            foreach (ISpecsItem spec in specItemsList) { spec.Index = ++i; }

            DraftSpecs.SpecItems = specItemsList;
        }

        public void AddSpec()
        {
            InputSpecsItems.Add(DraftSpecsItem);
            IsSpecsHasItem = InputSpecsItems.Count > 0 ? true : false;
        }

        // FIX: its wrong to remove the spec from the draft specs; its should be removed from the temporary input
        public void RemoveSpec(int specIndex)
        {
            //int itemIndex = InputSpecsItems.FindIndex(idx => idx.Index == specIndex);
            //InputSpecsItems.RemoveAt(itemIndex);
            InputSpecsItems.RemoveAll(idx => idx.Index == specIndex);

            // Renumber SpecItems
            for (int i = 0; i < InputSpecsItems.Count; i++)
            {
                InputSpecsItems[i].Index = i + 1;
            }
        }

        public void CopyEntriesToDraft()
        {
            // Copy list entries of draft spec to draft entries, if any
            if (DraftEntries == null)
            {
                DraftEntries =
                    DraftSpecsItem.ListEntries == null ?
                    new List<ISpecListEntry>() :
                    new List<ISpecListEntry>(DraftSpecsItem.ListEntries);
            }
        }

        public void SaveDraftSpec(int index, string name, string valPattern)
        {
            DraftSpecsItem.Index = index;
            DraftSpecsItem.Name = name;
            DraftSpecsItem.ValuePattern = valPattern;

            switch (DraftSpecsItemType)
            {
                case SpecType.List:
                    DraftSpecsItem.ListEntries = new List<ISpecListEntry>(DraftEntries);
                    DraftSpecsItem.CustomInputID = null;

                    break;
                case SpecType.Custom:
                    DraftSpecsItem.CustomInputID = DraftCustomSpecId;
                    DraftSpecsItem.ListEntries = null;

                    break;
            }
        }

        public bool IsSpecValid()
        {
            switch (DraftSpecsItemType)
            {
                case SpecType.List:
                    return DraftEntries != null && DraftEntries.Count > 0;

                case SpecType.Custom:
                    return DraftCustomSpecId != string.Empty;

                default:
                    return false;
            }
        }

        public void SetSpecTypeToList()
        {
            DraftSpecsItemType = SpecType.List;
            IsValidSpecData = IsSpecValid();
        }

        public void SetSpecTypeToCustom()
        {
            DraftSpecsItemType = SpecType.Custom;
        }

        public static ISpecs GetSpecs(string specsId)
        {
            return SpecsRepository.Read(specsId);
        }

        public void SetSelectedSpecs(string specsId)
        {
            SelectedSpecs = SpecsRepository.Read(specsId);
        }

        public void SetSelectedSpec(int idx)
        {
            SelectedSpecsItem = SpecsManiuplator.GetSpecsItem(SelectedSpecs ?? DraftSpecs, idx);
        }

        public ISpecListEntry GetSpecListEntry(int entryId)
        {
            return
                DraftEntries.Find(id => id.ValueID == entryId);
        }

        public void AddEntryToDraftEntries(ISpecListEntry entry)
        {
            DraftEntries.Add(entry);
            IsValidSpecData = IsSpecValid();
        }

        public void RemoveEntryFromDraftEntries(int entryId)
        {
            ISpecListEntry _removeListEntry = GetSpecListEntry(entryId);

            // Remove entry from list
            DraftEntries.Remove(_removeListEntry);

            // Renumber remaining entries ValueID
            int i = 0;
            foreach (ISpecListEntry entry in DraftEntries)
            {
                entry.ValueID = ++i;
            }
        }

        public void SetSpecCustomId(string id)
        {
            if (id != string.Empty)
            {
                DraftCustomSpecId = id;
            }
            IsValidSpecData = IsSpecValid();
        }



        public List<string> ExistingIDs { get; private set; }


        private List<string> FilterExistingIDs(string inputSpecsId)
        {
            if (inputSpecsId == string.Empty)
            {
                return DataProvider.GetSpecsIds();
            }
            else
            {
                return DataProvider.FilterSpecsIds(inputSpecsId);
            }

        }

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

        public void AddToRepository()
        {
            SpecsRepository.Create(DraftSpecs);
        }

        public void UpdateRepository()
        {
            SpecsRepository.Update(refId, DraftSpecs);
        }

        //public void SaveToDataSource()
        //{
        //    /*
        //     * The type 'ContextEntity' is defined in an assembly that is not referenced.
        //     * You must add a reference to assembly 'CoreLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
        //     */

        //    ContextProvider.Save(ContextEntity.Specs);
        //}
    }
}