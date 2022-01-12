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
using System.Collections.Specialized;
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
            _inputList = new ObservableCollection<string>();
            _inputList.CollectionChanged += _inputList_CollectionChanged;
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

        // Properties

        public List<SizeList> SizeLists =>
            sizeDP.GetList().As<SizeList>();

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

        private void SetInputList(ObservableCollection<string> value)
        {
            _inputList.CollectionChanged -= _inputList_CollectionChanged;
            _inputList = value;
            _inputList.CollectionChanged += _inputList_CollectionChanged;

            CheckListValidity(value);
        }

        public string InputEntry { get; set; }

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
            OnLoad?.Invoke(this,
                new LoadEventArgs
                {
                    GenericViewList = sizeDP.GetList().ToGenericView(),
                    Count = Count
                });
        }

        public void Select(string objectId)
        {
            /*SizeList*/ selected = (SizeList)broker.Read(objectId);

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
            // raise event
            OnPreDrafting?.Invoke(this,
                new PreDraftingEventArgs
                {
                    PreList = sizeDP.GetIDs()
                });
        }

        public void Edit(string objectID)
        {
            // get and store the original object
            editObject = (SizeList)broker.Read(objectID);

            CopyEditObjectDataToInputs();

            // raise event
            OnPreDrafting?.Invoke(this,
                new PreDraftingEventArgs
                {
                    DraftObject = editObject.Clone(),
                    PreList = sizeDP.GetIDs(),
                });
        }

        public void Remove(string objectId)
        {
            broker.Delete(objectId);

            // raise event
            OnRemove?.Invoke(this,
                new RemoveEventArgs
                {
                    RemoveID = objectId,
                    NewList = sizeDP.GetList().ToGenericView(),
                    Count = Count
                });

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
                ID = _inputID,
                Name = _inputName,
                List = new ObservableCollection<string>(_inputList)
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
            OnSet?.Invoke(this,
                new SetEventArgs
                {
                    SetID = InputID,
                    NewList = sizeDP.GetList().ToGenericView(),
                });
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

        // list modification actions
        public void AddEntry(string entry)
        {
            _inputList.Add(entry);
        }

        public void EditEntry(string oldValue, string newValue)
        {
            int i = _inputList.IndexOf(oldValue);
            _inputList[i] = newValue;
        }

        public void RemoveEntry(string entry)
        {
            _inputList.Remove(entry);
        }

        public void MoveEntry(string entry, ShiftDirection direction)
        {
            
        }

        private void _inputList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CheckListValidity((ObservableCollection<string>)sender);
            
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Console.WriteLine("> Collection Changed [{0}: {1}]", e.Action.ToString(), e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Console.WriteLine("> Collection Changed [{0}: {1}]", e.Action.ToString(), e.OldItems[0]);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Console.WriteLine("> Collection Changed [{0}: {1} > {2}]", e.Action.ToString(), e.OldItems[0], e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }

            Console.WriteLine("> Collection New Items:");
            foreach (string item in _inputList)
            {
                Console.WriteLine("> {0}", item);
            }
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
            DISABLE_STATUS_RAISE_EVENT = true;

            InputID = editObject.ID;
            InputName = editObject.Name;
            SetInputList(new ObservableCollection<string>(editObject.List));

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        /// <summary>
        /// Clear all inputs without raising the change event of the associated input status.
        /// </summary>
        private void ClearInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputID =  string.Empty;
            InputName =  string.Empty;
            //InputList = null;
            _inputList.Clear();

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        private void SetStatusInitialValues()
        {
            _statusID = InputStatus.Blank;
            _statusName = InputStatus.Blank;
            _statusList = InputStatus.Invalid;
        }

        private void CheckListValidity(ObservableCollection<string> sender)
        {
            bool notNullOrEmpty = sender.Count > 0;
            StatusList = notNullOrEmpty ? InputStatus.Valid : InputStatus.Invalid;
        }

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
            if (_inputList == null || editObject == null)
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
        private ObservableCollection<string> _inputList;

        // inputs status
        private InputStatus _statusID;
        private InputStatus _statusName;
        private InputStatus _statusList;

        // flags
        private bool isReady;
        private bool DISABLE_STATUS_RAISE_EVENT;

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