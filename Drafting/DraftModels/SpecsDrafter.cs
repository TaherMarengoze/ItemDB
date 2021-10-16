﻿
//using CoreLibrary.Enums;
using ClientService;
using Interfaces.Models;
using Modeling.DataModels;
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

        public SpecsDrafter()
        {
            DraftSpecs = new Specs();
        }

        public SpecsDrafter(ISpecs editSpecs)
        {
            refId = editSpecs.ID;

            // Save edit object reference to call it on edit cancel
            DraftSpecs = editSpecs;

            // Copy edit object to temporary fields
            inputSpecsId = editSpecs.ID;
            specsName = editSpecs.Name;
            specsTxtPat = editSpecs.TextPattern;
            specsItems = editSpecs.SpecItems.Clone();
        }

        public SpecsDrafter(string specsId)
        {
            ISpecs editSpecs = SpecsRepository.Read(specsId);

            refId = editSpecs.ID;

            // Save edit object reference to call it on edit cancel
            DraftSpecs = editSpecs;

            // Copy edit object to temporary fields
            inputSpecsId = editSpecs.ID;
            specsName = editSpecs.Name;
            specsTxtPat = editSpecs.TextPattern;
            specsItems = editSpecs.SpecItems.Clone();
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

        /// <summary>
        /// Temporary input for specs ID.
        /// </summary>
        private string inputSpecsId;

        /// <summary>
        /// Temporary input for specs name.
        /// </summary>
        public string specsName;

        /// <summary>
        /// Temporary input for specs text pattern.
        /// </summary>
        public string specsTxtPat;

        /// <summary>
        /// Temporary input for specs items.
        /// </summary>
        public List<ISpecsItem> specsItems;


        public int specIndex;
        public string specName;
        public string specPattern;


        public int DraftSpecsItemsCount()
        {
            return DraftSpecs.SpecItems.Count();
        }

        public void CommitChanges()
        {
            DraftSpecs.ID = inputSpecsId;
            DraftSpecs.Name = specsName;
            DraftSpecs.TextPattern = specsTxtPat;
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

        public bool IsValidSpecsId { get; private set; }

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

        public ValidityStatus IdStatus { get; private set; }

        public List<string> ExistingIDs { get; private set; }

        public string InputSpecsId
        {
            get => inputSpecsId;
            set
            {
                inputSpecsId = value;
                if (inputSpecsId == string.Empty)
                {
                    IsValidSpecsId = false;
                    IdStatus = ValidityStatus.Blank;
                }
                else
                {
                    bool isInputNotDraft = inputSpecsId != DraftSpecsId;
                    bool isDuplicateInput = DataProvider.GetSpecsIds().Contains(inputSpecsId);
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
                ExistingIDs = FilterExistingIDs(inputSpecsId);
            }
        }

        public enum ValidityStatus
        {
            Valid,
            Duplicate,
            Blank,
            Invalid
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

        private void CheckDraftSpecsReady()
        {
            bool isValidDraftSpecs = IsValidSpecsId && IsSpecsHasItem;

            if (isValidDraftSpecs)
            {
                // raise event for ready state
            }
            else
            {
                // raise event for not ready state
            }
        }
    }
}