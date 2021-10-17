﻿
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

        public SpecsDrafter()
        {
            DraftSpecs = new Specs();
        }

        public SpecsDrafter(string specsId)
        {
            ISpecs editSpecs = SpecsRepository.Read(specsId);

            refId = editSpecs.ID;

            // Save edit object reference to call it on edit cancel
            DraftSpecsId = specsId;
            DraftSpecs = editSpecs;

            // Copy edit object to temporary fields
            //_inputSpecsId = editSpecs.ID; // original
            InputSpecsId = editSpecs.ID;  // test
            _inputSpecsName = editSpecs.Name;
            _inputSpecsTxtPat = editSpecs.TextPattern;
            _inputSpecsItems = editSpecs.SpecItems.Clone();

            // Check draft specs has at least one spec item
            IsSpecsHasItem = DraftSpecs.SpecItems.Count() > 0;
        }

        /// <summary>
        /// Temporary value holder for edit object ID,to refer to old ID value in case it was changed.
        /// </summary>
        private readonly string refId;

        public string DraftSpecsId { get; set; }

        public ISpecs DraftSpecs { get; set; }

        public ISpecsItem DraftSpec { get; private set; }

        public SpecType DraftSpecType { get; private set; }

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
                    IsValidSpecsId = false;
                    IdStatus = ValidityStatus.Blank;
                }
                else
                {
                    bool isInputNotDraft = _inputSpecsId != DraftSpecsId;
                    bool isDuplicateInput = DataProvider.GetSpecsIds().Contains(_inputSpecsId);
                    bool isValidChar = true;

                    if (isInputNotDraft && isDuplicateInput)
                    {
                        IsValidSpecsId = false;
                        IdStatus = ValidityStatus.Duplicate;
                    }
                    else
                    {
                        // Should also check for invalid characters
                        if (isValidChar)
                        {
                            IsValidSpecsId = true;
                            IdStatus = ValidityStatus.Valid;
                        }
                        else
                        {
                            IsValidSpecsId = false;
                            IdStatus = ValidityStatus.Invalid;
                        }

                    }
                }
                ExistingIDs = FilterExistingIDs(_inputSpecsId);
            }
        }

        /// <summary>
        /// Temporary input for specs name.
        /// </summary>
        public string _inputSpecsName;

        /// <summary>
        /// Temporary input for specs text pattern.
        /// </summary>
        public string _inputSpecsTxtPat;

        /// <summary>
        /// Temporary input for specs items.
        /// </summary>
        public List<ISpecsItem> _inputSpecsItems;


        public int specIndex;
        public string specName;
        public string specPattern;


        public int DraftSpecsItemsCount()
        {
            return DraftSpecs.SpecItems.Count();
        }

        public void CommitChanges()
        {
            DraftSpecs.ID = _inputSpecsId;
            DraftSpecs.Name = _inputSpecsName;
            DraftSpecs.TextPattern = _inputSpecsTxtPat;
        }

        public void Clear()
        {
            ClearDraftSpecs();
            ClearDraftSpec();
        }

        public void ClearDraftSpecs()
        {
            DraftSpecs = null;
        }

        public void ClearDraftSpec()
        {
            DraftSpec = null;
            DraftEntries = null;
            DraftCustomSpecId = string.Empty;
        }

        public void NewDraftSpec()
        {
            // Same class attempt
            DraftSpec = new SpecsItem();

            int lastIdx = DraftSpecs.SpecItems.Count();
            int newIdx = lastIdx + 1;

            DraftSpec.Index = newIdx;
            DraftSpec.Name = $"SI{newIdx:000}";
        }

        public void EditSpec(int specIndex)
        {
            DraftSpec = DraftSpecs.SpecItems
                .FirstOrDefault(si => si.Index == specIndex);

            if (DraftSpec.ListEntries != null)
            {
                DraftSpecType = SpecType.List;
                DraftEntries = new List<ISpecListEntry>(DraftSpec.ListEntries);
            }

            if (DraftSpec.CustomInputID != null)
            {
                DraftSpecType = SpecType.Custom;
                DraftCustomSpecId = DraftSpec.CustomInputID;
            }
        }

        public void AddSpecToSpecsItemsDrafts()
        {
            List<ISpecsItem> tempList = DraftSpecs.SpecItems.ToList();
            tempList.Add(DraftSpec);
            DraftSpecs.SpecItems = tempList;
        }

        public void RemoveSpecFromDraftSpecsItems(int specIndex)
        {
            DraftSpecs.SpecItems =
                DraftSpecs.SpecItems.Where(idx => idx.Index != specIndex);

            // Renumber SpecItems
            int i = 0;
            foreach (ISpecsItem spec in DraftSpecs.SpecItems)
            {
                spec.Index = ++i;
            }
        }

        public void CopyEntriesToDraft()
        {
            // Copy list entries of draft spec to draft entries, if any
            if (DraftEntries == null)
            {
                DraftEntries =
                    DraftSpec.ListEntries == null ?
                    new List<ISpecListEntry>() :
                    new List<ISpecListEntry>(DraftSpec.ListEntries);
            }
        }

        public void SaveDraftSpec(int index, string name, string valPattern)
        {
            DraftSpec.Index = index;
            DraftSpec.Name = name;
            DraftSpec.ValuePattern = valPattern;

            switch (DraftSpecType)
            {
                case SpecType.List:
                    DraftSpec.ListEntries = new List<ISpecListEntry>(DraftEntries);
                    DraftSpec.CustomInputID = null;

                    break;
                case SpecType.Custom:
                    DraftSpec.CustomInputID = DraftCustomSpecId;
                    DraftSpec.ListEntries = null;

                    break;
            }
        }

        public bool IsSpecValid()
        {
            switch (DraftSpecType)
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
            DraftSpecType = SpecType.List;
        }

        public void SetSpecTypeToCustom()
        {
            DraftSpecType = SpecType.Custom;
        }

        public ISpecListEntry GetSpecListEntry(int entryId)
        {
            return
                DraftEntries.Find(id => id.ValueID == entryId);
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

        private bool _isValidSpecsId;
        public bool IsValidSpecsId
        {
            get => _isValidSpecsId;
            private set
            {
                _isValidSpecsId = value;
                CheckDraftSpecsReady();
            }
        }

        private bool _isSpecsHasItem;
        public bool IsSpecsHasItem
        {
            get { return _isSpecsHasItem; }
            set
            {
                _isSpecsHasItem = value;
                CheckDraftSpecsReady();
            }
        }

        public ValidityStatus IdStatus { get; private set; }

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

        public event EventHandler<bool> OnSpecsValidityChange;

        private void CheckDraftSpecsReady()
        {
            bool isValidDraftSpecs = IsValidSpecsId && IsSpecsHasItem;

            if (isValidDraftSpecs)
            {
                // raise event for ready state
                OnSpecsValidityChange?.Invoke(this, true);
            }
            else
            {
                // raise event for not ready state
                OnSpecsValidityChange?.Invoke(this, false);
            }
        }
    }
}