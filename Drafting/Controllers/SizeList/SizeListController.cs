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
            //ClearInputs();
            //_inputList = new ObservableCollection<string>();
            //_inputList.CollectionChanged += _inputList_CollectionChanged;
            SetStatusInitialValues();
        }

        #region Events
        public event EventHandler<LoadEventArgs> OnLoad;
        public event EventHandler<SizeListSelectionEventArgs> OnSelection;
        public event EventHandler<SelectEventArgs<SizeList>> OnSelect;
        public event EventHandler<InputStatus> OnIdStatusChange;
        public event EventHandler<InputStatus> OnNameStatusChange;
        public event EventHandler<InputStatus> OnListStatusChange;
        public event EventHandler<ReadyEventArgs> OnReadyStateChange;
        public event EventHandler<PreDraftingEventArgs> OnPreDrafting;
        public event EventHandler<SetEventArgs> OnSet;
        public event EventHandler<CancelEventArgs> OnCancel;
        public event EventHandler<RemoveEventArgs> OnRemove;
        #endregion

        /* Properties */
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
            //_inputList.CollectionChanged -= _inputList_CollectionChanged;
            _inputList = value;
            //_inputList.CollectionChanged += _inputList_CollectionChanged;
            
            CheckListValidity(value);
        }
        #endregion

        #region Status

        public InputStatus StatusID
        {
            get => _statusID; private set
            {
                _statusID = value;

                // raise event for status change
                //  (OnIdStatusChange, value);
                OnIdStatusChange.CheckedInvoke(value, !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusName
        {
            get => _statusName; private set
            {
                _statusName = value;

                // raise event for status change
                OnNameStatusChange.CheckedInvoke(value, !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusList
        {
            get => _statusList; private set
            {
                _statusList = value;

                // raise event for status change
                OnListStatusChange.CheckedInvoke(value, !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }
        #endregion

        #region Methods

        /* public methods */

        public void Save()
        {
            ContextProvider.Save(ContextEntity.Sizes);
        }

        public void Load()
        {
            LoadEventArgs args = new LoadEventArgs
            {
                GenericViewList = sizeDP.GetList().ToGenericView(),
                Count = Count
            };

            // raise #event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string objectId)
        {
            if (objectId == null)
                throw new ArgumentNullException(nameof(objectId));

            selectedObject = (SizeList)broker.Read(objectId);
            selectedEntries = selectedObject.List.ToList();

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
                throw new Exception("Modify state is already set.");

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
                throw new Exception("Modify state is already set.");

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

        public void Remove(string objectId)
        {
            broker.Delete(objectId);
            /* SUGGEST:
             * Instead of using a parameter, we could use the selected object field.
             * This allow us to check whether the ID exists and throws an exception if not.
             */

            RemoveEventArgs args = new RemoveEventArgs
            {
                RemoveID = objectId,
                NewList = sizeDP.GetList().ToGenericView(),
                Count = Count
            };

            // raise #event
            OnRemove?.Invoke(this, args);

            selectedObject = null;
        }

        public void CommitChanges()
        {
            if (!isReady)
                throw new Exception("The draft object is invalid or unchanged.");

            CreateOrUpdate();

            SetEventArgs args = new SetEventArgs
            {
                OldID = selectedObject?.ID,
                NewID = InputID,
                NewList = sizeDP.GetList().ToGenericView(),
            };

            // raise #event
            OnSet?.Invoke(this, args);

            // clear selection
            selectedObject = null;
            editObject = null;
            ClearInputs();

            // set flags
            STATE_MODIFY = false;
        }

        public void CancelChanges()
        {
            if (editObject != null)
                editObject = null;

            CancelEventArgs args = new CancelEventArgs
            {
                RestoreID = selectedObject?.ID,
                EmptyList = Count < 1
            };

            // raise #event
            OnCancel?.Invoke(this, args);

            ClearInputs();

            // set flags
            STATE_MODIFY = false;
        }
        
        /* private methods */

        private void CheckReadyStatus()
        {
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            isReady = isValid && isChanged;
            
            ReadyEventArgs args = new ReadyEventArgs
            {
                Ready = isReady,
                Info = isValid ? (isChanged ? "Ready" : "Unchanged") : "Not Ready"
            };

            // raise #event
            //OnReadyStateChange?.Invoke(this, args);
            OnReadyStateChange?.CheckedInvoke(args, !DISABLE_STATUS_RAISE_EVENT);
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
        /// Clear all inputs without raising the change event of the associated
        /// input status.
        /// </summary>
        private void ClearInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputID = string.Empty;
            InputName = string.Empty;
            _inputList.Clear();

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

        /* private getter methods */
        private SizeList GetEditObject()
        {
            return (SizeList)broker.Read(/*objectID*/selectedObject.ID);
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
            if (_inputList == null || editObject == null)
                return false;

            // compare elements count
            if (_inputList.Count != editObject.List.Count)
                return true;

            return !_inputList.SequenceEqual(editObject.List);
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
                List = new ObservableCollection<string>(_inputList)
            };
        }
        #endregion

        #region Fields

        private readonly SizeListBroker broker = new SizeListBroker();
        private readonly SizeProvider sizeDP = new SizeProvider();

        // inputs
        private string _inputID;
        private string _inputName;
        private List<string> _inputList;

        // inputs status
        private InputStatus _statusID;
        private InputStatus _statusName;
        private InputStatus _statusList;

        // flags
        private bool isReady;
        private bool DISABLE_STATUS_RAISE_EVENT;
        private bool STATE_MODIFY;

        // objects
        private SizeList selectedObject;
        private SizeList editObject;

        #endregion

        #region Unit Test API
        public SizeList _Selected => selectedObject;
        public SizeList _EditObject => editObject;
        public bool _IsReady => isReady;
        public bool _IsChanged => IsDraftChanged();
        #endregion
    }
}