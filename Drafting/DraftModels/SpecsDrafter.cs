
using CoreLibrary.Enums;
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

        public event EventHandler<bool> OnSpecsValidityChange;
        public event EventHandler<bool> OnSpecsItemValidityChange;
        public event EventHandler<ValidityStatus> OnSpecsIdValidityChange;

        public event EventHandler<int> OnSpecsRemove;
        public event EventHandler<int> OnSpecsItemRemove;

        public event EventHandler<string> OnSpecsItemPatternChange;

        public ISpecs SelectedSpecs { get; private set; }

        public ISpecsItem SelectedSpecsItem { get; private set; }

        public ISpecs DraftSpecs { get; private set; }

        public ISpecsItem DraftSpecsItem { get; private set; }

        public SpecType DraftSpecsItemType { get; private set; }

        public List<ISpecListEntry> DraftEntries { get; set; }

        public string DraftCustomSpecId { get; set; }

        public List<string> SpecsIDs => DataProvider.GetSpecsIds();

        public List<string> ExistingIDs { get; private set; }

        /// <summary>
        /// Temporary parameter that represents the input value from the UI that represents the <see cref="Specs.ID"/>.
        /// </summary>
        public string InputSpecsId
        {
            get => _inputSpecsId;
            set
            {
                _inputSpecsId = value;
                if (value != null)
                {
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
        }

        /// <summary>
        /// Temporary input for specs name.
        /// </summary>
        public string InputSpecsName
        {
            get => _inputSpecsName; set => _inputSpecsName = value;
        }

        /// <summary>
        /// Temporary input for specs text pattern.
        /// </summary>
        public string InputSpecsTxtPat
        {
            get => _inputSpecsTxtPat; set => _inputSpecsTxtPat = value;
        }

        /// <summary>
        /// Temporary input for specs items.
        /// </summary>
        public List<ISpecsItem> InputSpecsItems
        {
            get => _inputSpecsItems;
            set
            {
                _inputSpecsItems = value;
                if (value != null)
                {
                    IsSpecsHasItem = value.Count > 0 ? true : false;
                }
            }
        }

        public int InputSpecIndex { get => _inputSpecIndex; set => _inputSpecIndex = value; }

        public string InputSpecName
        {
            get => _inputSpecName;
            set
            {
                _inputSpecName = value;

                if (value != null)
                {
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
        }

        public string InputSpecPattern
        {
            get => _inputSpecPattern;
            set
            {
                _inputSpecPattern = value;
                if (value != null)
                {
                    OnSpecsItemPatternChange?.Invoke(this, value);
                }
            }
        }

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

        public bool IsSpecsHasItem
        {
            get { return _isSpecsHasItem; }
            set
            {
                _isSpecsHasItem = value;
                CheckSpecsReady();
            }
        }

        public bool IsValidSpecName
        {
            get => _isValidSpecName; private set
            {
                _isValidSpecName = value;
                CheckSpecsItemReady();
            }
        }

        public bool IsValidSpecData
        {
            get { return _isValidSpecData; }
            set
            {
                _isValidSpecData = value;
                CheckSpecsItemReady();
            }
        }

        /// <summary>
        /// Temporary value holder for edit object ID,to refer to old ID value in case it was changed.
        /// </summary>
        private string refId;
        private int? refIndex;
        private string _inputSpecsId;
        private string _inputSpecsName;
        private string _inputSpecsTxtPat;
        private List<ISpecsItem> _inputSpecsItems;
        private int _inputSpecIndex;
        private string _inputSpecName;
        private ValidityStatus _idStatus;
        private bool _isSpecsHasItem;
        private bool _isValidSpecName;
        private string _inputSpecPattern;
        private bool _isValidSpecData;

        #region Drafting Initializers

        public void NewSpecs()
        {
            DraftSpecs = new Specs();

            // TEST
            ClearSelectionObjects();

            // Set input fields initial value
            InputSpecsId = GenerateNewSpecsID();
            InputSpecsName = GenerateSpecsName(InputSpecsId);
            InputSpecsTxtPat = DraftSpecs.TextPattern;
            InputSpecsItems = new List<ISpecsItem>();
        }

        public void EditSpecs(string specsId)
        {
            DraftSpecs = SpecsRepository.Read(specsId).Clone();
            refId = specsId;

            // TEST
            ClearSelectionObjects();

            // Set input fields value to edit object data
            InputSpecsId = DraftSpecs.ID;
            InputSpecsName = DraftSpecs.Name;
            InputSpecsTxtPat = DraftSpecs.TextPattern;
            InputSpecsItems = DraftSpecs.SpecItems.Clone();
        }

        public void NewSpecsItem()
        {
            DraftSpecsItem = new SpecsItem();

            int lastIdx = InputSpecsItems.Count;
            int newIdx = lastIdx + 1;

            // Set input fields initial value
            InputSpecIndex = newIdx;
            InputSpecName = $"Specs Item #{newIdx:000}";
            InputSpecPattern = DraftSpecsItem.ValuePattern;
        }

        public void EditSpecsItem(int specsItemIndex)
        {
            DraftSpecsItem = InputSpecsItems
                .FirstOrDefault(si => si.Index == specsItemIndex).Clone();

            refIndex = specsItemIndex;

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

            // Set input fields value to edit object data
            InputSpecIndex = DraftSpecsItem.Index;
            InputSpecName = DraftSpecsItem.Name;
            InputSpecPattern = DraftSpecsItem.ValuePattern;
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

            // raise event for ready state
            OnSpecsItemValidityChange?.Invoke(this, isValidDraftSpecItem);
        }

        public int DraftSpecsItemsCount()
        {
            return DraftSpecs.SpecItems.Count();
        }

        public void CommitChanges()
        {
            DraftSpecs.ID = _inputSpecsId;
            DraftSpecs.Name = _inputSpecsName;
            DraftSpecs.TextPattern = _inputSpecsTxtPat;
            DraftSpecs.SpecItems = InputSpecsItems;

            if (refId == null || refId == string.Empty)
            {
                AddToRepository();
            }
            else
            {
                UpdateRepository();
            }
        }

        public void CommitSpecsItemChanges()
        {
            DraftSpecsItem.Index = InputSpecIndex;
            DraftSpecsItem.Name = InputSpecName;
            DraftSpecsItem.ValuePattern = InputSpecPattern;

            switch (DraftSpecsItemType)
            {
                case SpecType.List:
                    DraftSpecsItem.ListEntries = DraftEntries.ToList();
                    DraftSpecsItem.CustomInputID = null;

                    break;
                case SpecType.Custom:
                    DraftSpecsItem.CustomInputID = DraftCustomSpecId;
                    DraftSpecsItem.ListEntries = null;

                    break;
            }

            if (refIndex == null || refIndex == 0)
            {
                AddSpecsItem();
            }
            else
            {
                UpdateSpecsItem();
            }

            InputSpecsItems = _inputSpecsItems;
        }

        private void AddToRepository()
        {
            SpecsRepository.Create(DraftSpecs);
        }

        private void UpdateRepository()
        {
            SpecsRepository.Update(refId, DraftSpecs);
        }

        private void AddSpecsItem()
        {
            _inputSpecsItems.Add(DraftSpecsItem);
        }

        private void UpdateSpecsItem()
        {
            int index = (int)refIndex - 1;
            _inputSpecsItems.RemoveAt(index);
            _inputSpecsItems.Insert(index, DraftSpecsItem);
        }

        private void ClearSelectionObjects()
        {
            SelectedSpecs = null;
            SelectedSpecsItem = null;
        }

        public void Clear()
        {
            ClearDraftSpecs();
            ClearDraftSpecsItem();
        }

        public void ClearDraftSpecs()
        {
            // Clear draft object
            DraftSpecs = null;

            // Null edit object reference id
            refId = null;

            // Clear/Null input objects
            InputSpecsId = null;
            InputSpecsName = null;
            InputSpecsTxtPat = null;
            InputSpecsItems = null;
        }

        public void ClearDraftSpecsItem()
        {
            // Clear draft object
            DraftSpecsItem = null;
            DraftEntries = null;
            DraftCustomSpecId = string.Empty;

            // Null edit object reference index
            refIndex = null;

            // Clear / Null input objects
            InputSpecIndex = 0;
            InputSpecName = null;
            InputSpecPattern = null;
        }

        public void RemoveSpecs()
        {
            SpecsRepository.Delete(SelectedSpecs.ID);

            OnSpecsRemove?.Invoke(this, DataProvider.SpecsCount);
        }

        /// <summary>
        /// Removes a <see cref="ISpecsItem"/> from the <see cref="InputSpecsItems"/> list.
        /// </summary>
        /// <param name="specIndex">The index of the <see cref="ISpecsItem"/> to remove.</param>
        public void RemoveSpecsItem(int specIndex)
        {
            InputSpecsItems.RemoveAll(idx => idx.Index == specIndex);

            // Renumber SpecItems
            for (int i = 0; i < InputSpecsItems.Count; i++)
            {
                InputSpecsItems[i].Index = i + 1;
            }

            OnSpecsItemRemove?.Invoke(this, InputSpecsItems.Count);
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

        public void SetSelectedSpecs(string specsId)
        {
            SelectedSpecs = SpecsRepository.Read(specsId);
        }

        public void SetSelectedSpec(int idx)
        {
            SelectedSpecsItem =
                SpecsManiuplator.GetSpecsItem(SelectedSpecs?.SpecItems ?? InputSpecsItems, idx);
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

        private string GenerateSpecsName(string specsId)
        {
            return
                $"Specs_{ specsId }";
        }

        public void SaveToDataSource()
        {
            /*
             * The type 'ContextEntity' is defined in an assembly that is not referenced.
             * You must add a reference to assembly 'CoreLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
             */

            ContextProvider.Save(ContextEntity.Specs);
        }
    }
}