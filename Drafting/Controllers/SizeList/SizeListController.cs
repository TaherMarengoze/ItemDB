using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService;
using ClientService.Brokers;
using ClientService.Data;
using CoreLibrary.Enums;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;
using Modeling.ViewModels;

namespace Controllers
{
    public partial class SizeListController : IController
    {
        public SizeListController()
        {
            SetStatusInitialValues();
        }

        #region Events
        public event EventHandler<LoadEventArgs> OnLoad;
        public event EventHandler<SelectEventArgs<SizeList>> OnSelect;
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
        public List<SizeList> SizeLists => sizeDP.GetList().As<SizeList>();

        public List<string> SizeListIDs =>
            sizeDP.GetIDs();

        private int Count => sizeDP.Count /*SizeLists?.Count ?? 0*/;

        #region Inputs

        public string InputID
        {
            get => _inputID; set
            {
                _inputID = value;
                if (string.IsNullOrWhiteSpace(value))
                    StatusID = InputStatus.Blank;
                else
                {
                    // check for duplicate
                    bool isDuplicate = sizeDP.GetIDs().Contains(value);
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
            get => _inputName; set
            {
                _inputName = value;

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
            get => _statusID; private set
            {
                _statusID = value;

                StatusEventArgs args = new StatusEventArgs(value, _inputID);

                // raise #event
                OnIdStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusName
        {
            get => _statusName; private set
            {
                _statusName = value;

                StatusEventArgs args = new StatusEventArgs(value, _inputName);

                // raise #event
                OnNameStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusList
        {
            get => _statusList; private set
            {
                _statusList = value;

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
            //if (STATE_LOADED)
            //    throw new Exception("Operation already called.");

            // set flags
            STATE_LOADED = true;

            LoadEventArgs args =
                new LoadEventArgs(sizeDP.GetList().ToGenericView());
            
            // raise #event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string objectId)
        {
            if (STATE_MODIFY)
                throw new InvalidOperationException();

            if (objectId == null)
                throw new ArgumentNullException(nameof(objectId));

            //if (!STATE_LOADED)
            //    throw new InvalidOperationException();

            selectedObject = (SizeList)broker.Read(objectId);

            SelectEventArgs<SizeList> args = new SelectEventArgs<SizeList>
            {
                Selected = selectedObject,
                RequestInfo = objectId
            };

            // raise #event
            OnSelect?.Invoke(this, args);
        }

        public void New()
        {
            if (STATE_MODIFY)
                throw new Exception("Modify state is already set");

            // set flags
            STATE_MODIFY = true;

            PreDraftingEventArgs args = new PreDraftingEventArgs
            {
                PreList = sizeDP.GetIDs()
            };

            // raise #event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Edit(string objectID = "")
        {
            if (STATE_MODIFY)
                throw new Exception("Modify state is already set");

            STATE_MODIFY = true;

            editObject = GetEditObject();
            CopyEditObjectDataToInputs();

            // raise #event
            OnPreDrafting?.Invoke(this, new PreDraftingEventArgs
            {
                DraftObject = editObject.Clone(),
                PreList = sizeDP.GetIDs(),
            });
        }

        public void Remove(string objectId = "")
        {
            if (selectedObject == null)
                throw new InvalidOperationException(
                    "Unable to perform operation before selection");

            broker.Delete(selectedObject.ID);

            RemoveEventArgs args = new RemoveEventArgs(selectedObject.ID,
                sizeDP.GetList().ToGenericView());

            // raise #event
            OnRemove?.Invoke(this, args);

            selectedObject = null;
        }

        public void CommitChanges()
        {
            if (!STATE_DRAFT_READY)
                throw new Exception("Draft object is invalid or unchanged");

            CreateOrUpdate();

            SetEventArgs args = new SetEventArgs
            {
                OldID = selectedObject?.ID,
                NewID = InputID,
                NewList = sizeDP.GetList().ToGenericView(),
            };

            // raise #event
            OnSet?.Invoke(this, args);

            ClearSelection();
            editObject = null;
            ClearInputs();

            // set flags
            STATE_MODIFY = false;
        }
        
        public void CancelChanges()
        {
            CancelEventArgs args = new CancelEventArgs(selectedObject?.ID,
                sizeDP.GetList().ToGenericView());
            
            // raise #event
            OnCancel?.Invoke(this, args);

            editObject = null;
            ClearInputs();

            // set flags
            STATE_MODIFY = false;
        }
        #endregion

        #region Private Methods
        private void CheckReadyStatus()
        {
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            // set flags // test: moved at bottom
            //STATE_DRAFT_READY = isValid && isChanged;

            ReadyEventArgs args = new ReadyEventArgs(isValid, isChanged);

            // raise #event
            OnReadyStateChange?.CheckedInvoke(args,
                !DISABLE_STATUS_RAISE_EVENT);

            // set flags
            STATE_DRAFT_READY = isValid && isChanged;
        }

        private void CopyEditObjectDataToInputs()
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
            selectedObject = null;
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
            _statusID = InputStatus.Blank;
            _statusName = InputStatus.Blank;
            _statusList = InputStatus.Invalid;
        }

        private void CheckListValidity(List<string> sender)
        {
            bool notNullOrEmpty = sender.Count > 0;
            StatusList =
                notNullOrEmpty ? InputStatus.Valid : InputStatus.Invalid;
        }
        #endregion

        #region Private Getters
        private SizeList GetEditObject()
        {
            return (SizeList)broker.Read(selectedObject.ID);
        }

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
                _inputID != null ? _inputID != editObject?.ID : false,
                _inputName != null ? _inputName != editObject?.Name : false,
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
            SizeList draftObject = CreateDraftObject();

            if (editObject == null)
            {
                broker.Create(draftObject);
            }
            else
            {
                broker.Update(editObject.ID, draftObject);
            }
        }

        private SizeList CreateDraftObject()
        {
            return new SizeList
            {
                ID = _inputID,
                Name = _inputName,
                List = new ObservableCollection<string>(inputList)
            };
        }
        #endregion
        
        #endregion

        #region Fields

        private readonly SizeListBroker broker = new SizeListBroker();
        private readonly SizeProvider sizeDP = new SizeProvider();

        // inputs
        private string _inputID;
        private string _inputName;
        private List<string> inputList;

        // inputs status
        private InputStatus _statusID;
        private InputStatus _statusName;
        private InputStatus _statusList;

        // flags
        private bool STATE_DRAFT_READY;
        private bool STATE_LOADED;
        private bool DISABLE_STATUS_RAISE_EVENT;
        private bool STATE_MODIFY;

        // objects
        private SizeList selectedObject;
        private SizeList editObject;

        #endregion
    }
}