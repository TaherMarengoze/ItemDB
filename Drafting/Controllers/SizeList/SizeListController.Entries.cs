using CoreLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Controllers
{
    public partial class SizeListController
    {
        #region Events
        public event EventHandler<SelectEventArgs<string>> OnEntrySelect;
        public event EventHandler<InputStatus> OnEntryStatusChange;
        public event EventHandler<ReadyEventArgs> OnEntryReadyStateChange;
        public event EventHandler<EntrySetEventArgs> OnEntrySet;
        public event EventHandler<CancelEventArgs> OnEntryCancel;
        public event EventHandler<RemoveEventArgs> OnEntryRemove;
        #endregion

        /* properties */
        #region Inputs
        public string InputEntry
        {
            get => inputEntry; set
            {
                if (!ALLOW_INPUT)
                {
                    System.Diagnostics.Debug.Print("Input is not allowed.");
                    //Console.WriteLine("Input is not allowed.");
                    return;
                }

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
                OnEntryStatusChange.CheckedInvoke(value,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus_Entry();
            }
        }
        #endregion

        #region Methods

        #region To be moved to (SizeListController.cs)
        public void PartialModify_Entries()
        {
            //SetParentData_Entries();
            DISABLE_STATUS_RAISE_EVENT = true;

            SetInputList(new ObservableCollection<string>(selectedEntries));

            DISABLE_STATUS_RAISE_EVENT = false;
        }
        public void PartialCommit_Entries()
        {
            if (selectedObject == null)
                throw new Exception("No object selected");

            selectedObject.List = new ObservableCollection<string>(_inputList);
            broker.Update(selectedObject.ID, selectedObject);

            FlaggedInvoke(delegate { _inputList.Clear(); },
                out DISABLE_STATUS_RAISE_EVENT);
        }
        public void Revert_Entries()
        {
            FlaggedInvoke(delegate
            {
                _inputList.Clear();
            }, out DISABLE_STATUS_RAISE_EVENT);
        }
        #endregion

        private void FlaggedInvoke(Action action, out bool flag,
            bool initValue = true)
        {
            flag = initValue;
            action.Invoke();
            flag = !initValue;
        }
        
        public void SelectEntry(string entry)
        {
            selectedEntry = selectedEntries/*Object.List*/
                .First(listEntry => listEntry == entry);

            SelectEventArgs<string> args = new SelectEventArgs<string>
            {
                Selected = selectedEntry,
                RequestInfo = entry
            };

            // raise event
            OnEntrySelect?.Invoke(this, args);
        }

        public void New_Entry()
        {
            ALLOW_INPUT = true;

            // raise event
        }

        public void Edit_Entry()
        {
            if (selectedEntry == null)
                throw new InvalidOperationException();
            
            ALLOW_INPUT = true;

            // get and store the edit entry
            editEntry = selectedEntry;

            //CopyEditObjectDataToInputs_Entry();
            // fill inputs with edit object data
            inputEntry = editEntry;

            // raise event
        }

        public void RemoveEntry()
        {
            if (selectedEntry == null || _inputList.Count < 1)
                throw new InvalidOperationException(); //"No entry selected."

            _inputList.Remove(selectedEntry);

            RemoveEventArgs e = new RemoveEventArgs
            {
                RemoveID = selectedEntry,
                NewList = _inputList.ToList(),
            };
            // raise event
            OnEntryRemove?.Invoke(this, e);
        }

        public void CommitChanges_Entry()
        {
            if (!isReady_Entry)
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
            ALLOW_INPUT = false;
        }

        public void CancelChanges_Entry()
        {
            if (editEntry != null)
                editEntry = null;

            CancelEventArgs e = new CancelEventArgs
            {
                RestoreID = selectedEntry,
                EmptyList = (selectedEntries?.Count ?? 0) <= 0
            };
            // raise event
            OnEntryCancel?.Invoke(this, e);

            ClearInputs_Entry();
        }

        /* private methods */
        private void SetParentData_Entries()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            SetInputList(new ObservableCollection<string>(selectedEntries));

            DISABLE_STATUS_RAISE_EVENT = false;
        }

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

        private void ClearInputs_Entry()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputEntry = string.Empty;

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        /* private functions (getter method) */
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
        private bool ALLOW_INPUT;
        private bool isReady_Entry;

        // objects
        private List<string> selectedEntries;
        private string selectedEntry;
        private string editEntry;
        #endregion
    }
}