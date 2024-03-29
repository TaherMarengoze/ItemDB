﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClientService;
using ClientService.Brokers;
using ClientService.Contracts;
using ClientService.Data;
using Controllers.Common;
using CoreLibrary.Enums;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;
using Modeling.ViewModels;
using Modeling.ViewModels.Item;

#pragma warning disable IDE0090 // Use 'new(...)'

namespace Controllers
{
    public partial class ItemController : IController
    {
        public ItemController()
        {
            SetStatusInitialValues();

            InitDetailInputs();
            InitInputStatusObjects();
        }

        #region Events
        // common events
        public event EventHandler<LoadEventArgs> OnLoad;
        public event EventHandler<SelectEventArgs<IItem>> OnSelect;
        public event EventHandler<ReadyEventArgs> OnReadyStateChange;
        public event EventHandler<PreModifyEventArgs> OnPreDrafting;
        public event EventHandler<SetEventArgs> OnSet;
        public event EventHandler<CancelEventArgs> OnCancel;
        public event EventHandler<RemoveEventArgs> OnRemove;

        // specific events
        public event EventHandler<StatusEventArgs> OnIdStatusChange;
        public event EventHandler<StatusEventArgs> OnBaseNameStatusChange;
        public event EventHandler<StatusEventArgs> OnDisplayNameStatusChange;
        public event EventHandler<StatusEventArgs> OnDescriptionStatusChange;
        public event EventHandler<StatusEventArgs> OnUomStatusChange;
        public event EventHandler<StatusEventArgs> OnCatIdStatusChange;
        public event EventHandler<StatusEventArgs> OnCatNameStatusChange;
        public event EventHandler<StatusEventArgs> OnCommonNamesStatusChange;
        public event EventHandler<StatusEventArgs> OnImageNamesStatusChange;
        #endregion


        #region Inputs

        // string       *CatID
        // string       *CatNam
        // string       *ItemID
        // string       *BaseName
        // string       *DisplayName
        // List<string> *CommonNames
        // string       *Description
        // List<string> ImagesFileName
        // IItemDetails Details
        // string       *UoM

        public string InputID
        {
            get => inputID;
            set
            {
                inputID = value;

                StatusID = Operations.GetInputStatus(value,
                    GetEditObjectId(), provider.GetIDs());
            }
        }

        public string InputBaseName
        {
            get => inputBaseName;
            set
            {
                inputBaseName = value;
                StatusBaseName = Operations.GetInputStatus(value);
            }
        }

        public string InputDisplayName
        {
            get => inputDisplayName;
            set
            {
                inputDisplayName = value;
                StatusDisplayName = Operations.GetInputStatus(value);
            }
        }

        public string InputDescription
        {
            get => inputDescription;
            set
            {
                inputDescription = value;
                StatusDescription = Operations.GetInputStatus(value);
            }
        }

        public string InputUom
        {
            get => inputUom;
            set
            {
                inputUom = value;
                StatusUom = Operations.GetInputStatus(value);
            }
        }

        public string InputCatId
        {
            get => inputCatId;
            set
            {
                inputCatId = value;
                StatusCatId = Operations.GetInputStatus(value);
            }
        }

        public string InputCatName
        {
            get => inputCatName;
            set
            {
                inputCatName = value;
                StatusCatName = Operations.GetInputStatus(value);
            }
        }

        private void SetInputCommonNames(List<string> value)
        {
            inputCommonNames = value;
            SetStatusCommonNames(Operations.GetInputStatus(value));
        }

        private void SetInputImageNames(List<string> value)
        {
            inputImageNames = value;
            SetStatusImageNames(Operations.GetInputStatus(value));
        }
        #endregion

        #region Input Status
        public InputStatus StatusID
        {
            get => statusID;
            private set
            {
                statusID = value;

                StatusEventArgs args = new StatusEventArgs(value, inputID);

                // raise event
                if (!DISABLE_STATUS_RAISE_EVENT)
                    OnIdStatusChange?.Invoke(this, args);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusBaseName
        {
            get => statusBaseName;
            private set
            {
                statusBaseName = value;

                StatusEventArgs args = new StatusEventArgs(value, inputBaseName);

                // raise #event
                OnBaseNameStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusDisplayName
        {
            get => statusDisplayName;
            private set
            {
                statusDisplayName = value;
                StatusEventArgs args = new StatusEventArgs(value, inputDisplayName);

                // raise event
                OnDisplayNameStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusDescription
        {
            get => statusDescription;
            private set
            {
                statusDescription = value;
                StatusEventArgs args = new StatusEventArgs(value, inputDescription);

                // raise event
                OnDescriptionStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusUom
        {
            get => statusUom;
            private set
            {
                statusUom = value;
                StatusEventArgs args = new StatusEventArgs(value, inputUom);

                // raise event
                OnUomStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusCatId
        {
            get => statusCatId;
            private set
            {
                statusCatId = value;
                StatusEventArgs args = new StatusEventArgs(value, inputCatId);

                // raise event
                OnCatIdStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusCatName
        {
            get => statusCatName;
            private set
            {
                statusCatName = value;
                StatusEventArgs args = new StatusEventArgs(value, inputCatName);

                // raise event
                OnCatNameStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        private void SetStatusCommonNames(InputStatus value)
        {
            statusCommonNames = value;

            // common name list can be blank, so an empty list is valid
            if (value == InputStatus.Blank)
                statusCommonNames = InputStatus.Valid;

            var args = new StatusEventArgs(value, inputCommonNames);

            // raise event
            if (!DISABLE_STATUS_RAISE_EVENT)
                OnCommonNamesStatusChange?.Invoke(this, args);

            // check all inputs status
            CheckReadyStatus();
        }

        private void SetStatusImageNames(InputStatus value)
        {
            statusImageNames = value;

            // image names list can be blank, so an empty list is valid
            if (value == InputStatus.Blank)
                statusImageNames = InputStatus.Valid;

            var args = new StatusEventArgs(value, inputImageNames);

            // raise event
            if (!DISABLE_STATUS_RAISE_EVENT)
                OnImageNamesStatusChange?.Invoke(this, args);

            // check all inputs status
            CheckReadyStatus();
        }
        #endregion

        #region Public Methods
        public void Save()
        {
            ContextProvider.Save(ContextEntity.Items);
        }

        public void Load()
        {
            var args = new LoadEventArgs(GetGenericViewList());

            // update item total count
            UpdateTotalCount();

            // raise event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string refId)
        {
            if (refId == null)
                throw new ArgumentNullException(nameof(refId));

            selectedObject = broker.Read(refId);

            var args = new SelectEventArgs<IItem>
            {
                Selected = selectedObject,
                RequestInfo = refId
            };

            // raise event
            OnSelect?.Invoke(this, args);
        }

        public void New()
        {
            var args = new PreModifyEventArgs(provider.View<ItemBasicView>());

            // raise event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Edit()
        {
            SetEditObject();
            FillInputs();

            var args = new PreModifyEventArgs(editObject.Clone(),
                provider.GetIDs());

            // raise #event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Remove()
        {
            if (selectedObject == null)
                throw new InvalidOperationException(
                    "Unable to perform operation before selection");

            broker.Delete(selectedObject.ItemID);

            var args = new RemoveEventArgs(selectedObject.ItemID,
                GetGenericViewList());

            selectedObject = null;

            // raise event
            OnRemove?.Invoke(this, args);
        }

        public void CommitChanges()
        {
            if (!STATE_DRAFT_READY)
                throw new Exception("Invalid or unchanged draft object");

            CreateOrUpdate();

            var args = new SetEventArgs(inputID,
                selectedObject?.ItemID, GetGenericViewList());

            ClearSelection();
            editObject = null;
            ClearInputs();

            // raise event
            OnSet?.Invoke(this, args);
        }

        public void CancelChanges()
        {
            var args = new CancelEventArgs(selectedObject?.ItemID,
                GetGenericViewList());

            editObject = null;
            ClearInputs();

            // raise event
            OnCancel?.Invoke(this, args);
        }

        public event EventHandler<LoadEventArgs<GenericView>> OnFilter;

        public void Filter(string critItemId
                         , string critItemName
                         , string critCategoryId
                          , bool? critHasImage)
        {
            critItemId = critItemId.Trim();
            critItemName = critItemName.Trim().ToLower();
            critCategoryId = critCategoryId.Trim();

            List<Func<IItem, bool>> matchFunctions = new()
            {
                qryItem => critItemId == "" || qryItem.ItemID.Contains(critItemId)
               ,
                qryItem => critItemName == "" || qryItem.DisplayName.ToLower().Contains(critItemName)
               ,
                qryItem => critCategoryId == "*" || qryItem.CatID == critCategoryId
               ,
                qryItem => critHasImage == null || ((bool)critHasImage ? qryItem.ImagesFileName?.Count > 0 : (qryItem.ImagesFileName?.Count ?? 0) <= 0)
            };

            bool _AllCriteriaMatched(IItem qryItem)
            {
                foreach (var criteria in matchFunctions)
                {
                    if (!criteria(qryItem))
                        return false;
                }
                return true;
            }

            var result = provider.GetList()
                .Where(_AllCriteriaMatched)
                .Select(item => item)
                .ToList()
                .ToGenericView();

            var args =
                new LoadEventArgs<GenericView> { ViewList = result };

            OnFilter?.Invoke(this, args);
        }

        #endregion

        #region Private Methods
        private void CheckReadyStatus()
        {
            if (DISABLE_READY_STATUS_CHECK)
                return;

            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            // set flags
            STATE_DRAFT_READY = isValid && isChanged;

            ReadyEventArgs args = new ReadyEventArgs(isValid, isChanged);

            // raise #event
            OnReadyStateChange?.CheckedInvoke(args,
                !DISABLE_STATUS_RAISE_EVENT);
        }

        private void CreateOrUpdate()
        {
            IItem draftObject = CreateDraftObject();

            if (editObject == null)
            {
                broker.Create(draftObject);
            }
            else
            {
                broker.Update(editObject.ItemID, draftObject);
            }
        }

        /// <summary>
        /// Sets the edit object.
        /// </summary>
        private void SetEditObject()
        {
            editObject = broker.Read(selectedObject.ItemID);
        }

        /// <summary>
        /// Copy the data of the edited object to the inputs.
        /// </summary>
        private void FillInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;
            DISABLE_READY_STATUS_CHECK = true;

            //List<string> CommonNames
            //List<string> ImagesFileName
            //IItemDetails Details

            InputID = editObject.ItemID;
            InputBaseName = editObject.BaseName;
            InputDisplayName = editObject.DisplayName;
            InputDescription = editObject.Description;
            InputUom = editObject.UoM;
            InputCatId = editObject.CatID;
            InputCatName = editObject.CatName;

            // item details IDs
            InputSpecs.Id = editObject.Details.SpecsID;
            InputSizeGroup.Id = editObject.Details.SizeGroupID;
            InputBrand.Id = editObject.Details.BrandListID;
            InputEnd.Id = editObject.Details.EndsListID;

            // item detail required flag
            InputSpecs.Required = editObject.Details.SpecsRequired;
            InputSizeGroup.Required = editObject.Details.SizeRequired;
            InputBrand.Required = editObject.Details.BrandRequired;
            InputEnd.Required = editObject.Details.EndsRequired;

            SetInputCommonNames(editObject.CommonNames.ToList());
            SetInputImageNames(editObject.ImagesFileName.ToList());

            DISABLE_STATUS_RAISE_EVENT = false;
            DISABLE_READY_STATUS_CHECK = false;
        }

        /// <summary>
        /// Clears the selected object field.
        /// </summary>
        private void ClearSelection()
        {
            selectedObject = null;
        }

        /// <summary>
        /// Clear all inputs without raising the change event of the associated
        /// input status.
        /// </summary>
        private void ClearInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputCatId = null;
            InputCatName = null;
            InputID = null;
            InputBaseName = null;
            InputDisplayName = null;
            InputDescription = null;
            InputUom = null;

            // clear item details value
            InputSpecs.Id = null;
            InputSizeGroup.Id = null;
            InputBrand.Id = null;
            InputEnd.Id = null;

            InputSpecs.Required = false;
            InputSizeGroup.Required = false;
            InputBrand.Required = false;
            InputEnd.Required = false;

            SetInputCommonNames(null);
            SetInputImageNames(null);
            //inputCommonNames = null;
            //statusCommonNames = InputStatus.Blank;

            // etc ...

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        private void SetStatusInitialValues()
        {
            statusID = InputStatus.Blank;
            statusBaseName = InputStatus.Blank;
            statusDisplayName = InputStatus.Blank;
            statusDescription = InputStatus.Blank;
            statusUom = InputStatus.Blank;
            statusCatId = InputStatus.Blank;
            statusCatName = InputStatus.Blank;
            statusCommonNames = InputStatus.Valid;
            statusImageNames = InputStatus.Valid;
            // etc ...
        }
        #endregion

        #region Private Getters
        private List<GenericView> GetGenericViewList()
        {
            return provider.GetList().ToGenericView();
        }

        private string GetEditObjectId()
        {
            return editObject?.ItemID;
        }

        private bool IsValidInputs()
        {
            InputStatus[] inputStatus = {
                statusID,
                statusBaseName,
                statusDisplayName,
                statusDescription.Validate(InputStatus.Blank),
                statusUom,
                statusCatId,
                statusCatName,
                statusCommonNames.Validate(InputStatus.Blank),
                statusImageNames.Validate(InputStatus.Blank),
                // etc ...
            };

            return inputStatus.All(status => status == InputStatus.Valid)
                && IsValidInputsStatus();
        }

        private bool IsDraftChanged()
        {
            bool[] draftChange = {
                Operations.IsChanged(inputID, GetEditObjectId()),
                Operations.IsChanged(inputBaseName, editObject?.BaseName),
                Operations.IsChanged(inputDisplayName, editObject?.DisplayName),
                Operations.IsChanged(inputDescription, editObject?.Description),
                Operations.IsChanged(inputUom, editObject?.UoM),
                Operations.IsChanged(inputCatId, editObject?.CatID),
                Operations.IsChanged(inputCatName, editObject?.CatName),
                Operations.IsChanged(inputCommonNames, editObject?.CommonNames),
                Operations.IsChanged(inputImageNames, editObject?.ImagesFileName),
            };

            return draftChange.Any(change => change)
                || IsDraftDetailsChanged();
        }

        private IItem CreateDraftObject()
        {
            return new Item
            {
                CatID = inputCatId,
                CatName = inputCatName,
                ItemID = inputID,
                BaseName = inputBaseName,
                DisplayName = inputDisplayName,
                UoM = inputUom,
                Description = inputDescription,
                CommonNames = inputCommonNames?.ToList() ?? new List<string>(),
                ImagesFileName = inputImageNames?.ToList() ?? new List<string>(),
                Details = new ItemDetails
                {
                    SpecsID = InputSpecs.Id,
                    SizeGroupID = InputSizeGroup.Id,
                    BrandListID = InputBrand.Id,
                    EndsListID = InputEnd.Id,

                    SpecsRequired = InputSpecs.Required,
                    SizeRequired = InputSizeGroup.Required,
                    BrandRequired = InputBrand.Required,
                    EndsRequired = InputEnd.Required
                }
            };
        }
        #endregion

        #region Fields

        private readonly IBroker<IItem> broker = new ItemBroker();
        private readonly IProvider<IItem> provider = new ItemProvider();
        private readonly IProvider<IItemCategory> categoryProvider = new CategoryProvider();
        private readonly IProvider<ISpecs> specsProvider = new SpecsProvider();

        // backing fields
        private string inputID;
        private InputStatus statusID;
        private string inputBaseName;
        private InputStatus statusBaseName;
        private string inputDisplayName;
        private InputStatus statusDisplayName;
        private string inputDescription;
        private InputStatus statusDescription;
        private string inputUom;
        private InputStatus statusUom;
        private string inputCatId;
        private InputStatus statusCatId;
        private string inputCatName;
        private InputStatus statusCatName;
        private List<string> inputCommonNames;
        private InputStatus statusCommonNames;
        private List<string> inputImageNames;
        private InputStatus statusImageNames;

        // flags
        private bool STATE_DRAFT_READY;
        private bool DISABLE_STATUS_RAISE_EVENT;
        private bool DISABLE_READY_STATUS_CHECK;

        // objects
        private IItem selectedObject;
        private IItem editObject;
        #endregion

        #region Item Details Code

        public event EventHandler<StatusEventArgs> OnSpecsStatusChange;
        public event EventHandler<StatusEventArgs> OnSizeGroupStatusChange;
        public event EventHandler<StatusEventArgs> OnBrandStatusChange;
        public event EventHandler<StatusEventArgs> OnEndStatusChange;

        public ItemDetailInput InputSpecs;
        public ItemDetailInput InputSizeGroup;
        public ItemDetailInput InputBrand;
        public ItemDetailInput InputEnd;

        private readonly List<InputStatusObject> inputStatuses = new();

        private InputStatusObject StatusSpecs;
        private InputStatusObject StatusSizeGroup;
        private InputStatusObject StatusBrand;
        private InputStatusObject StatusEnd;

        private void InitDetailInputs()
        {
            InputSpecs = new ItemDetailInput(SetStatusSpecs);
            InputSizeGroup = new ItemDetailInput(SetStatusSizeGroup);
            InputBrand = new ItemDetailInput(SetStatusBrand);
            InputEnd = new ItemDetailInput(SetStatusEnd);
        }

        private void InitInputStatusObjects()
        {
            //inputStatuses = new();
            StatusSpecs = new(inputStatuses, InputStatus.Valid);
            StatusSizeGroup = new(inputStatuses, InputStatus.Valid);
            StatusBrand = new(inputStatuses, InputStatus.Valid);
            StatusEnd = new(inputStatuses, InputStatus.Valid);
        }

        private void SetStatusSpecs(InputStatus status)
        {
            StatusSpecs.Status = status.Validate(InputStatus.Blank);

            StatusEventArgs args = new StatusEventArgs(status, null);

            // raise event
            if (!DISABLE_STATUS_RAISE_EVENT)
                OnSpecsStatusChange?.Invoke(this, args);

            // check all inputs status
            CheckReadyStatus();
        }

        private void SetStatusSizeGroup(InputStatus status)
        {
            StatusSizeGroup.Status = status.Validate(InputStatus.Blank);

            StatusEventArgs args = new StatusEventArgs(status, null);

            // raise event
            if (!DISABLE_STATUS_RAISE_EVENT)
                OnSizeGroupStatusChange?.Invoke(this, args);

            // check all inputs status
            CheckReadyStatus();
        }

        private void SetStatusBrand(InputStatus status)
        {
            StatusBrand.Status = status.Validate(InputStatus.Blank);

            StatusEventArgs args = new StatusEventArgs(status, null);

            // raise event
            if (!DISABLE_STATUS_RAISE_EVENT)
                OnBrandStatusChange?.Invoke(this, args);

            // check all inputs status
            CheckReadyStatus();
        }

        private void SetStatusEnd(InputStatus status)
        {
            StatusEnd.Status = status.Validate(InputStatus.Blank);

            StatusEventArgs args = new StatusEventArgs(status, null);

            // raise event
            if (!DISABLE_STATUS_RAISE_EVENT)
                OnEndStatusChange?.Invoke(this, args);

            // check all inputs status
            CheckReadyStatus();
        }

        private bool IsValidInputsStatus()
        {
            return inputStatuses.All(input =>
                input.Status == InputStatus.Valid);
        }

        private bool IsDraftDetailsChanged()
        {
            bool[] detailsChanged = {
                Operations.IsChanged(InputSpecs,
                    editObject?.Details.SpecsID,
                    editObject?.Details.SpecsRequired ?? false),

                Operations.IsChanged(InputSizeGroup,
                    editObject?.Details.SizeGroupID,
                    editObject?.Details.SizeRequired ?? false),

                Operations.IsChanged(InputBrand,
                    editObject?.Details.BrandListID,
                    editObject?.Details.BrandRequired ?? false),

                Operations.IsChanged(InputEnd,
                    editObject?.Details.EndsListID,
                    editObject?.Details.EndsRequired ?? false),
            };

            return detailsChanged.Any(change => change);
        }
        #endregion

        #region Code Under Test
        public List<IItemCategory> ListItemCategories(bool addGenericCat = false)
        {
            List<IItemCategory> categories = new List<IItemCategory>();

            if (addGenericCat)
                categories.Add(new ItemCategory("*", "<All categories>"));

            categories.AddRange(categoryProvider.GetList());

            return categories;
        }

        private int totalCount;
        public int TotalCount => totalCount;
        private void UpdateTotalCount() => totalCount = provider.Count;
        #endregion
    }

    public class InputStatusObject
    {
        public InputStatusObject(List<InputStatusObject> statusCheckList,
            InputStatus defaultValue = InputStatus.Blank)
        {
            statusCheckList.Add(this);
            Status = defaultValue;
        }

        public InputStatus Status { get; set; }
    }
}