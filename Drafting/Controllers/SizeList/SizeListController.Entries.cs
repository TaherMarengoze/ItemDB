﻿using CoreLibrary.Enums;
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
        public event EventHandler<RevertEventArgs> OnRevertEntries;
        public event EventHandler<SelectEventArgs<string>> OnEntrySelect;
        public event EventHandler<StatusEventArgs> OnEntryStatusChange;
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
                    System.Diagnostics.Debug.Print(
                        "Input is not allowed in the current state.");

                    throw new Exception("Input is not allowed in the current state.");
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

                StatusEventArgs args = new StatusEventArgs(value, inputEntry);

                // raise event for status change
                OnEntryStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus_Entry();
            }
        }
        #endregion

        #region Methods

        public void PartialCommit_Entries()
        {
            if (selectedObject == null)
                throw new Exception("No object selected");

            selectedObject.List = inputList.ToObservableCollection();
            broker.Update(selectedObject.ID, selectedObject);

            DISABLE_STATUS_RAISE_EVENT = true;
            inputList.Clear();
            DISABLE_STATUS_RAISE_EVENT = false;
        }

        public void Load_Entries()
        {
            NewOrCloneInputList();

            LoadEventArgs args = new LoadEventArgs(inputListDraft);

            // raise #event
            OnLoadEntries?.Invoke(this, args);
        }

        public void Save_Entries()
        {
            SetInputList(inputListDraft.ToList());
            inputListDraft = null;

            // raise #event
            OnSaveEntries?.Invoke(this, EventArgs.Empty);
        }

        public void Revert_Entries()
        {
            inputListDraft = null;

            RevertEventArgs args = new RevertEventArgs(inputList?.ToList());

            // raise #event
            OnRevertEntries?.Invoke(this, args);
        }

        public void SelectEntry(string entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (inputListDraft == null)
            {
                selectedEntry = selectedObject.List
                    .First(lstEntry => lstEntry == entry);
            }
            else
            {
                selectedEntry = inputListDraft
                    .First(lstEntry => lstEntry == entry);
            }

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
            // set flags
            ALLOW_INPUT_Entries = true;

            // raise #event
        }

        public void Edit_Entry()
        {
            if (selectedEntry == null)
                throw new InvalidOperationException(
                    "Unable to perform operation before selection");

            // set flags
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
            if (selectedEntry == null)
                throw new InvalidOperationException(
                    "Unable to perform the operation before selection");

            inputListDraft.Remove(selectedEntry);

            RemoveEventArgs args = new RemoveEventArgs(selectedEntry,
                inputListDraft.ToList());

            // raise #event
            OnEntryRemove?.Invoke(this, args);
        }

        public void CommitChanges_Entry()
        {
            if (!isReady_Entry)
                throw new Exception("Entry is invalid or unchanged");

            CreateOrUpdate_Entry();

            EntrySetEventArgs args = new EntrySetEventArgs(inputEntry,
                editEntry, inputListDraft.ToList());

            // raise #event
            OnEntrySet?.Invoke(this, args);

            // clear objects
            selectedEntry = null;
            editEntry = null;
            ClearInputs_Entry();

            // set flags
            ALLOW_INPUT_Entries = false;
        }

        public void CancelChanges_Entry()
        {
            if (editEntry != null) editEntry = null;

            CancelEventArgs args = new CancelEventArgs(selectedEntry,
                inputListDraft.ToList());

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

            ReadyEventArgs args = new ReadyEventArgs(isReady_Entry);

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

        // objects
        private string selectedEntry;
        private string editEntry;
        #endregion
    }
}