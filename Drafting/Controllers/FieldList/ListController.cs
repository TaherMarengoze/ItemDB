using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClientService;
using ClientService.Brokers;
using ClientService.Contracts;
using CoreLibrary.Enums;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.ViewModels;

namespace Controllers
{
    public partial class ListController<TField> : IController
        where TField: IFieldList, new()
    {
        public ListController()
        {
            BrokerSelector<TField>.Assign(out broker, out provider);

            SetStatusInitialValues();
        }

        #region Events
        public event EventHandler<LoadEventArgs> OnLoad;
        public event EventHandler<SelectEventArgs<TField>> OnSelect;
        public event EventHandler<StatusEventArgs> OnIdStatusChange;
        public event EventHandler<StatusEventArgs> OnNameStatusChange;
        public event EventHandler<StatusEventArgs> OnListStatusChange;
        public event EventHandler<ReadyEventArgs> OnReadyStateChange;
        public event EventHandler<PreDraftingEventArgs> OnPreDrafting;
        public event EventHandler<SetEventArgs> OnSet;
        public event EventHandler<CancelEventArgs> OnCancel;
        public event EventHandler<RemoveEventArgs> OnRemove;
        #endregion

        // Properties
        #region Inputs

        public string InputID
        {
            get => inputID; set
            {
                inputID = value;
                if (string.IsNullOrWhiteSpace(value))
                    StatusID = InputStatus.Blank;
                else
                {
                    // check for duplicate
                    bool isDuplicate = provider.GetIDs().Contains(value);
                    bool isNotAsEdit = value != editObject?.ID;

                    if (isDuplicate)
                    {
                        if (isNotAsEdit)
                            StatusID = InputStatus.Duplicate;
                        else
                            StatusID = InputStatus.Valid;
                    }
                    else
                    {
                        bool isValidChar = true; // valid characters check

                        if (isValidChar)
                            StatusID = InputStatus.Valid;
                        else
                            StatusID = InputStatus.Invalid;
                    }
                }
            }
        }

        public string InputName
        {
            get => inputName; set
            {
                inputName = value;

                if (string.IsNullOrWhiteSpace(value))
                    StatusName = InputStatus.Blank;
                else
                {
                    bool isValidChar = true; // valid characters check

                    if (isValidChar)
                        StatusName = InputStatus.Valid;
                    else
                        StatusName = InputStatus.Invalid;
                }
            }
        }

        private void SetInputList(List<string> value)
        {
            inputList = value;

            CheckListValidity(value);
        }
        #endregion

        #region Status

        public InputStatus StatusID
        {
            get => statusID; private set
            {
                statusID = value;

                StatusEventArgs args = new StatusEventArgs(value, inputID);

                // raise #event
                OnIdStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusName
        {
            get => statusName; private set
            {
                statusName = value;

                StatusEventArgs args = new StatusEventArgs(value, inputName);

                // raise #event
                OnNameStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusList
        {
            get => statusList; private set
            {
                statusList = value;

                StatusEventArgs args = new StatusEventArgs(value,
                    inputList.ToList());

                // raise #event
                OnListStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }
        #endregion

        #region Methods

        #region Public Methods
        public void Save()
        {
            ContextProvider.Save(ContextEntity.Sizes);
        }

        public void Load()
        {
            LoadEventArgs args =
                new LoadEventArgs(provider.GetList().ToGenericView());
            
            // raise #event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string objectId)
        {
            if (objectId == null)
                throw new ArgumentNullException(nameof(objectId));
            
            selectedObject = (TField)broker.Read(objectId);

            SelectEventArgs<TField> args = new SelectEventArgs<TField>
            {
                Selected = selectedObject,
                RequestInfo = objectId
            };

            // raise #event
            OnSelect?.Invoke(this, args);
        }

        public void New()
        {
            PreDraftingEventArgs args = new PreDraftingEventArgs
            {
                PreList = provider.GetIDs()
            };

            // raise #event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Edit()
        {
            SetEditObject();
            FillInputs();

            PreDraftingEventArgs args = new PreDraftingEventArgs
            {
                DraftObject = editObject.Clone(),
                PreList = provider.GetIDs(),
            };

            // raise #event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Remove()
        {
            if (selectedObject == null)
                throw new InvalidOperationException(
                    "Unable to perform operation before selection");

            broker.Delete(selectedObject.ID);

            RemoveEventArgs args = new RemoveEventArgs(selectedObject.ID,
                provider.GetList().ToGenericView());

            selectedObject = default /*null*/;

            // raise #event
            OnRemove?.Invoke(this, args);
        }

        public void CommitChanges()
        {
            if (!STATE_DRAFT_READY)
                throw new Exception("Invalid or unchanged draft object");

            CreateOrUpdate();

            var args = new SetEventArgs(InputID,
                selectedObject?.ID, provider.GetList().ToGenericView());
            
            ClearSelection();
            editObject = default/*null*/;
            ClearInputs();

            OnSet?.Invoke(this, args);
        }
        
        public void CancelChanges()
        {
            CancelEventArgs args = new CancelEventArgs(selectedObject?.ID,
                provider.GetList().ToGenericView());

            editObject = default/*null*/;
            ClearInputs();

            // raise #event
            OnCancel?.Invoke(this, args);
        }
        #endregion

        #region Private Methods
        private void CheckReadyStatus()
        {
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            // set flags
            STATE_DRAFT_READY = isValid && isChanged;

            ReadyEventArgs args = new ReadyEventArgs(isValid, isChanged);

            // raise #event
            OnReadyStateChange?.CheckedInvoke(args,
                !DISABLE_STATUS_RAISE_EVENT);
        }

        /// <summary>
        /// Sets the edit object.
        /// </summary>
        private void SetEditObject()
        {
            editObject = (TField)broker.Read(selectedObject.ID);
        }

        /// <summary>
        /// Copy the data of the edited object to the inputs.
        /// </summary>
        private void FillInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputID = editObject.ID;
            InputName = editObject.Name;
            SetInputList(editObject.List.ToList());

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        /// <summary>
        /// Clears the selected object field.
        /// </summary>
        private void ClearSelection()
        {
            selectedObject = default/*null*/;
        }

        /// <summary>
        /// Clear all inputs without raising the change event of the associated
        /// input status.
        /// </summary>
        private void ClearInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputID = string.Empty;
            InputName = string.Empty;
            inputList?.Clear();

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        private void SetStatusInitialValues()
        {
            statusID = InputStatus.Blank;
            statusName = InputStatus.Blank;
            statusList = InputStatus.Invalid;
        }

        private void CheckListValidity(List<string> sender)
        {
            bool notNullOrEmpty = sender.Count > 0;
            StatusList =
                notNullOrEmpty ? InputStatus.Valid : InputStatus.Invalid;
        }
        #endregion

        #region Private Getters
        private bool IsValidInputs()
        {
            InputStatus[] inputStatus = {
                StatusID,
                StatusName,
                StatusList
            };

            return inputStatus.All(status => status == InputStatus.Valid);
        }

        private bool IsDraftChanged()
        {
            bool[] draftChange = {
                inputID != null && inputID != editObject?.ID,
                inputName != null && inputName != editObject?.Name,
                IsListChanged()
            };

            return draftChange.Any(change => change);
        }

        private bool IsListChanged()
        {
            if (inputList == null || editObject == null)
                return false;

            // compare elements count
            if (inputList.Count != editObject.List.Count)
                return true;

            return !inputList.SequenceEqual(editObject.List);
        }

        private void CreateOrUpdate()
        {
            TField draftObject = CreateDraftObject();

            if (editObject == null)
            {
                broker.Create(draftObject);
            }
            else
            {
                broker.Update(editObject.ID, draftObject);
            }
        }

        private TField CreateDraftObject()
        {
            return new TField
            {
                ID = inputID,
                Name = inputName,
                List = new ObservableCollection<string>(inputList)
            };
        }
        #endregion
        
        #endregion

        #region Fields

        private readonly IBroker<IFieldList> broker;
        private readonly IProvider<IFieldList> provider;

        // inputs
        private string inputID;
        private string inputName;
        private List<string> inputList;

        // inputs status
        private InputStatus statusID;
        private InputStatus statusName;
        private InputStatus statusList;

        // flags
        private bool STATE_DRAFT_READY;
        private bool DISABLE_STATUS_RAISE_EVENT;

        // objects
        private TField selectedObject;
        private TField editObject;

        #endregion
    }
}