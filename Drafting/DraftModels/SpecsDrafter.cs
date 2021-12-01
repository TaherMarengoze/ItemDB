
using ClientService;
using CoreLibrary.Enums;
using Interfaces.General;
using Interfaces.Models;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drafting
{
    public class SpecsSetEventArgs : EventArgs
    {
        public string SetID { get; set; }

        public bool Existing { get; set; }
    }

    public class SpecsItemSetEventArgs : EventArgs
    {
        public int Index { get; set; }

        public List<ISpecsItem> SpecsItems { get; set; }
    }

    public class SpecsItemRemoveEventArgs : EventArgs
    {
        public List<ISpecsItem> SpecsItems { get; set; }
        public int Count => SpecsItems?.Count ?? 0;
    }

    public class SpecsItemCancelEventArgs : EventArgs
    {
        public bool NoItem { get; set; }

        public int Index { get; set; }

        public List<ISpecsItem> SpecsItems { get; set; }
    }

    public class ListEntryEventArgs : EventArgs
    {
        //public int EntryID { get; set; }
        public List<SpecListEntry> Entries { get; set; }
        public int Count => Entries?.Count ?? 0;
    }

    public partial class SpecsDrafter : IDraftable
    {
        // events
        public event EventHandler<bool> OnSpecsValidityChange;
        public event EventHandler<ValidityStatus> OnSpecsIdValidityChange;

        public event EventHandler<bool> OnSpecsItemValidityChange;
        public event EventHandler<string> OnSpecsItemPatternChange;

        public event EventHandler<SpecsSetEventArgs> OnSpecsSet;
        public event EventHandler<int> OnSpecsRemove;
        public event EventHandler<string> OnSpecsCancel;

        public event EventHandler<SpecsItemSetEventArgs> OnSpecsItemSet;
        public event EventHandler<SpecsItemRemoveEventArgs> OnSpecsItemRemove;
        public event EventHandler<SpecsItemCancelEventArgs> OnSpecsItemCancel;

        public event EventHandler<ListEntryEventArgs> OnListEntrySet;
        public event EventHandler<ListEntryEventArgs> OnListEntryRemove;

        // properties
        public ISpecs SelectedSpecs { get; private set; }

        public ISpecsItem SelectedSpecsItem { get; private set; }

        public List<SpecListEntry> SelectedListEntries =>
            SelectedSpecsItem.ListEntries.Cast<SpecListEntry>().ToList();

        public ISpecs DraftSpecs { get; private set; }

        public ISpecsItem DraftSpecsItem { get; private set; }

        public SpecType DraftSpecsItemType { get; private set; }

        public List<SpecListEntry> DraftEntries { get; private set; }

        public string DraftCustomSpecId { get; set; }

        public List<string> SpecsIDs => DataProvider.GetSpecsIds();

        public int SpecsCount => DataProvider.SpecsCount;

        public List<string> CustomSpecsIDs => DataManager.GetCustomSpecsList();

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
                    ExistingIDs = FilterSimilarIDs(_inputSpecsId);
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
                else
                {
                    IsValidSpecName = false;
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
                CheckSpecsItemReady();
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
            private set
            {
                _isValidSpecData = value;
                CheckSpecsItemReady();
            }
        }

        // fields
        private string restoreSpecsId;
        private int restoreSpecsItemIdx;
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

            // clear selection object
            ClearSelectedSpecs();

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

            // clear selection object
            ClearSelectedSpecs();

            // Set input fields value to edit object data
            InputSpecsId = DraftSpecs.ID;
            InputSpecsName = DraftSpecs.Name;
            InputSpecsTxtPat = DraftSpecs.TextPattern;
            InputSpecsItems = DraftSpecs.SpecItems.Clone();
        }

        public void NewSpecsItem()
        {
            DraftSpecsItem = new SpecsItem();

            // clear selection object
            ClearSelectedSpecsItem();

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

            // clear selection object
            ClearSelectedSpecsItem();

            if (DraftSpecsItem.ListEntries != null)
            {
                DraftSpecsItemType = SpecType.List;
                DraftEntries =
                    DraftSpecsItem.ListEntries.Cast<SpecListEntry>().ToList();

                _isValidSpecData = true;
            }

            if (DraftSpecsItem.CustomInputID != null)
            {
                DraftSpecsItemType = SpecType.Custom;
                DraftCustomSpecId = DraftSpecsItem.CustomInputID;
                _isValidSpecData = true;
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

            bool isNewSpecs = refId == null || refId == string.Empty;

            if (isNewSpecs)
            {
                AddToRepository();
            }
            else
            {
                UpdateRepository();
            }

            // raise an event: Specs is set
            OnSpecsSet?.Invoke(this,
                new SpecsSetEventArgs()
                {
                    SetID = DraftSpecs.ID,
                    Existing = !isNewSpecs
                });

            Clear();
        }

        public void CancelChanges()
        {
            Clear();

            // raise cancel event
            OnSpecsCancel?.Invoke(this, restoreSpecsId);
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

            OnSpecsItemSet?.Invoke(this,
                new SpecsItemSetEventArgs()
                {
                    Index = DraftSpecsItem.Index,
                    SpecsItems = InputSpecsItems
                });

            IsSpecsHasItem = true;

            ClearDraftSpecsItem();
        }

        public void CancelSpecsItemChanges()
        {
            SpecsItemCancelEventArgs e = new SpecsItemCancelEventArgs()
            {
                NoItem = InputSpecsItems.Count < 1,
                Index = /*refIndex ??*/ restoreSpecsItemIdx,
                SpecsItems = InputSpecsItems
            };

            ClearDraftSpecsItem();

            // raise cancel event
            OnSpecsItemCancel?.Invoke(this, e);
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

            OnSpecsItemRemove?.Invoke(this,
                new SpecsItemRemoveEventArgs
                {
                    SpecsItems = InputSpecsItems,
                });

            NotifyChangeInputSpecsItems();
        }

        private void NotifyChangeInputSpecsItems()
        {
            InputSpecsItems = _inputSpecsItems;
        }

        private void ClearSelectedSpecs()
        {
            // save selected Specs ID in case we need to restore it
            restoreSpecsId = SelectedSpecs.ID;
            SelectedSpecs = null;

            // TEST: will disable cause error ?
            //SelectedSpecsItem = null;
        }

        private void ClearSelectedSpecsItem()
        {
            // save selected SpecsItem index to restore it later, if needed
            restoreSpecsItemIdx = SelectedSpecsItem?.Index ?? 0;
            SelectedSpecsItem = null;
        }

        private void Clear()
        {
            ClearDraftSpecs();
            ClearDraftSpecsItem();
        }

        private void ClearDraftSpecs()
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

        private void ClearDraftSpecsItem()
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

            // reset bool flags
            IsValidSpecData = false;
        }

        public void AddListEntry(IListEntry entry)
        {
            // get last entryID
            int lastId = DraftEntries?.Count ?? 0;

            // generate new entryID
            int newId = lastId + 1;

            // create new SpecsItem list entry
            SpecListEntry specEntry = new SpecListEntry
            {
                ValueID = newId,
                Value = entry.Value,
                Display = entry.Display
            };

            NewOrCloneEntries();
            DraftEntries.Add(specEntry);
            IsValidSpecData = true;

            // raise an event for adding an entry
            OnListEntrySet?.Invoke(this,
                new ListEntryEventArgs { Entries = DraftEntries }); ;
        }

        private void NewOrCloneEntries()
        {
            // copy draft SpecsItem entries to a draft entries list
            // or create a new empty list, if non exists
            if (DraftEntries == null)
            {
                DraftEntries =
                    DraftSpecsItem.ListEntries?.Cast<SpecListEntry>().ToList() ??
                    new List<SpecListEntry>();
            }
        }

        public void EditListEntry()
        {
            IsValidSpecData = true;

            // raise an event for editing an entry
            OnListEntrySet?.Invoke(this,
                new ListEntryEventArgs { Entries = DraftEntries });
        }

        public void RemoveEntry(int entryId)
        {
            SpecListEntry removeEntry = GetSpecListEntry(entryId);
            DraftEntries.Remove(removeEntry);

            // renumber remaining entries ValueID
            for (int i = entryId - 1; i < DraftEntries.Count;)
            {
                DraftEntries[i].ValueID = ++i;
            }

            IsValidSpecData = DraftEntries.Count > 0;

            // raise a remove event
            OnListEntryRemove?.Invoke(this,
                new ListEntryEventArgs
                {
                    Entries = DraftEntries
                });
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

        public void SetSelectedSpecsItem(int idx)
        {
            SelectedSpecsItem =
                SpecsManiuplator.GetSpecsItem(SelectedSpecs?.SpecItems ?? InputSpecsItems, idx);
        }

        public SpecListEntry GetSpecListEntry(int entryId)
        {
            return
                DraftEntries.Find(id => id.ValueID == entryId);
        }

        public void SetSpecCustomId(string id)
        {
            if (id != string.Empty)
            {
                DraftCustomSpecId = id;
            }
            IsValidSpecData = IsSpecValid();
        }

        private List<string> FilterSimilarIDs(string inputSpecsId)
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