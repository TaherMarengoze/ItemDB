using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using CoreLibrary.Enums;

namespace Controllers
{
    public partial class SizeListController
    {
        #region Events
        public event EventHandler<SelectEventArgs<string>> OnEntrySelect;
        public event EventHandler<InputStatus> OnEntryStatusChange;
        public event EventHandler<ReadyEventArgs> OnEntryReadyStateChange;
        public event EventHandler<EntrySetEventArgs> OnEntrySet;
        #endregion

        #region Inputs
        public string InputEntry
        {
            get => inputEntry; set
            {
                inputEntry = value;

                if (string.IsNullOrWhiteSpace(value))
                    StatusEntry = InputStatus.Blank;
                else
                {
                    bool isDuplicate = IsInputEntryDuplicate(value);
                    bool isNotAsEdit = IsNotInputEntryAsEdit(value);

                    if (isDuplicate)
                    {
                        if (isNotAsEdit)
                            StatusEntry = InputStatus.Duplicate;
                        else
                            StatusEntry = InputStatus.Valid;
                    }
                    else
                    {
                        bool isValidChar = true; // valid characters check

                        if (isValidChar)
                            StatusEntry = InputStatus.Valid;
                        else
                            StatusEntry = InputStatus.Invalid;
                    }
                }
            }
        }

        #endregion

        #region Status
        public InputStatus StatusEntry
        {
            get => statusEntry; private set
            {
                statusEntry = value;

                // raise event for status change
                OnEntryStatusChange.CheckedInvoke(value, !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus_Entry();
            }
        }
        #endregion

        #region Methods
        public void Save_Entry()
        {
            if (!isReady)
                throw new Exception("The draft object is invalid or unchanged.");

            CreateOrUpdate();

            // raise event
            OnSet?.Invoke(this, new SetEventArgs
            {
                SetID = InputID,
                NewList = sizeDP.GetList().ToGenericView(),
            });

            // clear selection
            selected = null;
            editObject = null;
            ClearInputs();
        }

        public void Load_Entry() => throw new NotImplementedException();

        public void SelectEntry(string entry)
        {
            selectedEntry = selected?.List.FirstOrDefault(e => e == entry);

            // raise event
            OnEntrySelect?.Invoke(this, new SelectEventArgs<string>
            {
                Selected = entry
            });
        }

        public void New_Entry()
        {
            // raise event
        }

        public void Edit_Entry()
        {
            // get and store the edit entry
            editEntry = selectedEntry;

            CopyEditObjectDataToInputs_Entry();

            // raise event
        }

        public void RemoveEntry()
        {
            if (selectedEntry == null)
                throw new Exception("No entry selected.");

            _inputList.Remove(selectedEntry);
        }

        public void CommitChanges_Entry()
        {
            if (isReady_Entry)
                throw new Exception("The entry is invalid or unchanged.");

            CreateOrUpdate_Entry();

            // raise event
            OnEntrySet?.Invoke(this, new EntrySetEventArgs
            {
                NewItem = inputEntry,
                OldItem = editEntry
            });

            // clear selection
            selectedEntry = null;
            editEntry = null;
            ClearInputs_Entry();
        }

        // private methods
        private void CheckReadyStatus_Entry()
        {
            bool isValid = IsValidInputs_Entry();
            bool isChanged = IsDraftChanged_Entry();

            isReady_Entry = isValid && isChanged;

            // raise event
            OnEntryReadyStateChange?.Invoke(this, new ReadyEventArgs
            {
                Ready = isReady,
                //Info = isValid ? (isChanged ? "Ready" : "Unchanged") : "Not Ready"
            });
        }

        private void CopyEditObjectDataToInputs_Entry()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            SetInputList(new ObservableCollection<string>(selectedEntries));

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        private void ClearInputs_Entry()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputEntry = string.Empty;

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        // private functions (getter method)
        private bool IsInputEntryDuplicate(string value)
            => selectedEntries.Contains(value);

        private bool IsNotInputEntryAsEdit(string value)
            => value != editEntry;

        private bool IsValidInputs_Entry()
        {
            InputStatus[] inputStatus = {
                statusEntry,
            };

            return inputStatus.All(status => status == InputStatus.Valid);
        }

        private bool IsDraftChanged_Entry()
        {
            bool[] draftChange = {
                inputEntry != null ? inputEntry != editEntry : false,
            };

            return draftChange.Any(change => change);
        }

        private void CreateOrUpdate_Entry()
        {
            string draftEntry = inputEntry;

            if (editEntry == null)
            {
                _inputList.Add(draftEntry);
            }
            else
            {
                int i = _inputList.IndexOf(editEntry);
                _inputList[i] = draftEntry;
            }
        }
        #endregion

        #region Fields
        // inputs
        private string inputEntry;

        // inputs status
        private InputStatus statusEntry;

        // flags
        private bool isReady_Entry;

        // objects
        private List<string> selectedEntries;
        private string selectedEntry;
        private string editEntry;
        #endregion
    }
}