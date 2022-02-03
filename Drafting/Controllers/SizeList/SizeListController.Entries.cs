using CoreLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Custom;

namespace Controllers
{
    public partial class SizeListController
    {
        #region Events
        public event EventHandler<LoadEventArgs> OnLoadEntries;
        public event EventHandler OnSaveEntries;
        public event EventHandler OnCancelEntries;
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
                if (!ALLOW_INPUT_Entries)
                {
                    System.Diagnostics.Debug.Print("Input is not allowed in the current state.");
                    throw new Exception("Input is not allowed in the current state.");
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

            SetInputList(selectedEntries.ToList());

            DISABLE_STATUS_RAISE_EVENT = false;
        }
        public void PartialCommit_Entries()
        {
            if (selectedObject == null)
                throw new Exception("No object selected");

            selectedObject.List = inputList.ToObservableCollection();
            broker.Update(selectedObject.ID, selectedObject);

            FlaggedInvoke(delegate { inputList.Clear(); },
                out DISABLE_STATUS_RAISE_EVENT);
        }
        public void Revert_Entries()
        {
            FlaggedInvoke(delegate
            {
                inputList.Clear();
            }, out DISABLE_STATUS_RAISE_EVENT);
        }

        public void Load_Entries()
        {
            if (!STATE_MODIFY)
                throw new InvalidOperationException();

            if (STATE_LOADED_Entries)
                throw new Exception("Operation already called.");

            NewOrCloneInputList();

            LoadEventArgs args = new LoadEventArgs(inputListDraft);

            // raise #event
            OnLoadEntries?.Invoke(this, args);

            // set flags
            STATE_LOADED_Entries = true;
        }

        public void Save_Entries()
        {
            if (!STATE_LOADED_Entries)
                throw new InvalidOperationException();

            SetInputList(inputListDraft.ToList());
            inputListDraft = null;

            // raise #event
            OnSaveEntries?.Invoke(this, EventArgs.Empty);

            // set flags
            STATE_LOADED_Entries = false;
        }
        public void Cancel_Entries()
        {
            if (!STATE_LOADED_Entries)
                throw new InvalidOperationException();

            inputListDraft = null;

            // raise #event
            OnCancelEntries?.Invoke(this, EventArgs.Empty);

            // set flags
            STATE_LOADED_Entries = false;
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
            //FIX: if adding new entry, selectedEntries is null
            selectedEntry = /*selectedEntries*/inputListDraft
                .First(lstEntry => lstEntry == entry);

            SelectEventArgs<string> args = new SelectEventArgs<string>
            {
                Selected = selectedEntry,
                RequestInfo = entry
            };

            // raise #event
            OnEntrySelect?.Invoke(this, args);
        }

        public void New_Entry()
        {
            if (STATE_MODIFY_Entries)
                throw new Exception("Modify state is already set.");

            // set flags
            STATE_MODIFY_Entries = true;
            ALLOW_INPUT_Entries = true;

            // raise #event
        }

        public void Edit_Entry()
        {
            if (STATE_MODIFY_Entries)
                throw new Exception("Modify state is already set.");

            if (selectedEntry == null)
                throw new InvalidOperationException();

            // set flags
            STATE_MODIFY_Entries = true;
            ALLOW_INPUT_Entries = true;

            // get and store the edit entry
            editEntry = selectedEntry;

            //CopyEditObjectDataToInputs_Entry();
            // fill inputs with edit object data
            inputEntry = editEntry;

            // raise #event
        }

        public void RemoveEntry()
        {
            if (selectedEntry == null /*|| _inputList.Count < 1*/)
                throw new InvalidOperationException(); //"No entry selected."

            //if (inputListDraft == null)
            //    throw new InvalidOperationException();

            inputListDraft.Remove(selectedEntry);

            RemoveEventArgs e = new RemoveEventArgs
            {
                RemoveID = selectedEntry,
                NewList = inputListDraft.ToList(),
                Count = inputListDraft.Count
            };

            // raise #event
            OnEntryRemove?.Invoke(this, e);
        }

        public void CommitChanges_Entry()
        {
            if (!isReady_Entry)
                throw new Exception("The entry is invalid or unchanged.");

            CreateOrUpdate_Entry();

            EntrySetEventArgs args = new EntrySetEventArgs
            {
                NewItem = inputEntry,
                OldItem = editEntry,
                SetList = inputListDraft.ToList(),
            };

            // raise #event
            OnEntrySet?.Invoke(this, args);

            // clear selection
            selectedEntry = null;
            editEntry = null;
            ClearInputs_Entry();

            // set flags
            STATE_MODIFY_Entries = false;
            ALLOW_INPUT_Entries = false;
        }

        public void CancelChanges_Entry()
        {
            if (editEntry != null)
                editEntry = null;

            CancelEventArgs args = new CancelEventArgs
            {
                RestoreID = selectedEntry,
                EmptyList = (selectedEntries?.Count ?? 0) <= 0
            };

            // raise #event
            OnEntryCancel?.Invoke(this, args);

            ClearInputs_Entry();

            // set flags
            ALLOW_INPUT_Entries = false;
        }

        // private methods

        private void CheckReadyStatus_Entry()
        {
            bool isValid = IsValidInputs_Entry();
            bool isChanged = IsDraftChanged_Entry();

            isReady_Entry = isValid && isChanged;

            ReadyEventArgs args = new ReadyEventArgs
            {
                Ready = isReady,
                //Info = isValid ? (isChanged ? "Ready" : "Unchanged") : "Not Ready"
            };

            // raise #event
            OnEntryReadyStateChange?.Invoke(this, args);
        }

        private void NewOrCloneInputList()
        {
            inputListDraft =
                inputList?.ToObservableCollection() ??
                new ObservableCollection<string>();
        }

        private void ClearInputs_Entry()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputEntry = string.Empty;

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        // private functions (getter method)

        private bool IsInputEntryDuplicate(string value)
            => inputListDraft.Contains(value);

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
                inputListDraft.Add(draftEntry);
            }
            else
            {
                int i = inputListDraft.IndexOf(editEntry);
                inputListDraft[i] = draftEntry;
            }
        }
        #endregion

        #region Fields
        // parent inputs
        private ObservableCollection<string> inputListDraft;

        // inputs
        private string inputEntry;

        // inputs status
        private InputStatus statusEntry;

        // flags
        private bool ALLOW_INPUT_Entries;
        private bool isReady_Entry;
        private bool STATE_LOADED_Entries;
        private bool STATE_MODIFY_Entries;

        // objects
        private List<string> selectedEntries;
        private string selectedEntry;
        private string editEntry;
        #endregion
    }
}