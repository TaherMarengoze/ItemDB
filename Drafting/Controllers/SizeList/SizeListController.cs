using ClientService;
using ClientService.Brokers;
using ClientService.Data;
using CoreLibrary.Enums;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;
using Modeling.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class SizeListController : IController
    {
        public SizeListController()
        {
            //ClearInputs();
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
        public event EventHandler<object> OnSet;
        public event EventHandler<CancelEventArgs> OnCancel;
        public event EventHandler<int> OnRemove;
        #endregion

        #region Properties

        public List<SizeList> SizeLists =>
            sizeDP.GetList().As<SizeList>();

        public List<string> SizeListIDs =>
            sizeDP.GetIDs();

        private int Count => SizeLists?.Count ?? 0;

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
                    bool isDuplicate = sizeDP.GetIDs().Contains(value) && value != editObject?.ID;

                    if (isDuplicate)
                        StatusID = InputStatus.Duplicate;
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

        public List<string> InputList
        {
            get => _inputList; set
            {
                _inputList = value;

                bool notNullOrEmpty = value?.Count > 0;
                StatusList = notNullOrEmpty ? InputStatus.Valid : InputStatus.Invalid;
            }
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
                OnIdStatusChange.CheckedInvoke(value, !DISABLE_RAISE_EVENT);

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
                OnNameStatusChange.CheckedInvoke(value, !DISABLE_RAISE_EVENT);

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
                OnListStatusChange.CheckedInvoke(value, !DISABLE_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        #endregion

        #endregion

        #region Methods

        /* public methods */

        public void Save()
        {
            ContextProvider.Save(ContextEntity.Sizes);
        }

        public void Load()

        {
            OnLoad?.Invoke(this,
                new LoadEventArgs
                {
                    GenericViewList = sizeDP.GetList().ToGenericView()
                });
        }

        public void Select(string objectId)
        {
            SizeList selected = (SizeList)broker.Read(objectId);

            // raise event
            //OnSelection?.Invoke(this,
            //    new SizeListSelectionEventArgs
            //    {
            //        Selected = selected
            //    });

            OnSelect?.Invoke(this,
                new SelectEventArgs<SizeList>
                {
                    Selected = selected
                });
        }

        public void New()
        {
            //throw new NotImplementedException();
        }

        public void Edit(string objectID)
        {
            // get and store the original object
            editObject = (SizeList)broker.Read(objectID);

            CopyEditObjectDataToInputs();
        }

        public void Remove(string objectId)
        {
            broker.Delete(objectId);

            // raise event
            OnRemove?.Invoke(this, Count);

            selected = null;
        }

        public void CommitChanges()
        {
            if (!isReady)
            {
                //return;
                throw new Exception("The draft object is invalid or unchanged.");
            }

            SizeList draftObject = new SizeList
            {
                ID = InputID,
                Name = InputName,
                List = new ObservableCollection<string>(InputList)
            };

            if (editObject == null)
            {
                broker.Create(draftObject);
            }
            else
            {
                broker.Update(editObject.ID, draftObject);
                editObject = null;
            }

            selected = null; // unset selection object
            OnSet?.Invoke(this, InputID);
            ClearInputs();
        }

        public void CancelChanges()
        {
            if (editObject != null)
                editObject = null;

            // raise event
            OnCancel?.Invoke(this,
                new CancelEventArgs
                {
                    RestoreID = selected?.ID,
                    EmptyList = Count < 1
                });

            ClearInputs();
        }

        /* private methods */

        private void CheckReadyStatus()
        {
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            isReady = isValid && isChanged;

            // raise event
            OnReadyStateChange?.Invoke(this, new ReadyEventArgs
            {
                Ready = isReady,
                Info = isValid ? (isChanged ? "Ready" : "Unchanged") : "Not Ready"
            });
        }

        private void CopyEditObjectDataToInputs()
        {
            DISABLE_RAISE_EVENT = true;

            InputID = editObject.ID;
            InputName = editObject.Name;
            InputList = editObject.List.ToList();

            DISABLE_RAISE_EVENT = false;
        }

        /// <summary>
        /// Clear all inputs without raising the change event of the associated input status.
        /// </summary>
        private void ClearInputs()
        {
            DISABLE_RAISE_EVENT = true;

            InputID =  string.Empty;
            InputName =  string.Empty;
            InputList = null;

            DISABLE_RAISE_EVENT = false;
        }

        private void SetStatusInitialValues()
        {
            _statusID = InputStatus.Blank;
            _statusName = InputStatus.Blank;
            _statusList = InputStatus.Invalid;
        }

        //private void CheckInvoke<T>(EventHandler<T> handler, T val)
        //{
        //    if (!DISABLE_RAISE_EVENT)
        //        handler?.Invoke(this, val);
        //}

        /* private getter methods */

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
            if (_inputList == null)
                return false;

            // compare elements count
            if (_inputList.Count != editObject.List.Count)
                return true;

            return !_inputList.SequenceEqual(editObject.List);
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
        private bool DISABLE_RAISE_EVENT;

        // objects
        private SizeList selected;
        private SizeList editObject;

        #endregion

        #region Unit Test API
        public SizeList _Selected => selected;
        public SizeList _EditObject => editObject;
        public bool _IsReady => isReady;
        public bool _IsChanged => IsDraftChanged();
        #endregion
    }
}