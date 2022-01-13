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
    public partial class SizeListController
    {
        #region Events
        public event EventHandler<InputStatus> OnEntryStatusChange;
        public event EventHandler<SetEventArgs> OnEntrySet;
        #endregion

        #region Inputs
        public string InputEntry
        {
            get => _inputEntry; set
            {
                _inputEntry = value;

                if (string.IsNullOrWhiteSpace(value))
                    StatusEntry = InputStatus.Blank;
                else
                {
                    // check for duplicate
                    bool isDuplicate = IsInputEntryDuplicate(value);
                    bool isNotAsEdit = IsNotInputEntryAsEdit(value);

                    if (isDuplicate)
                    {
                        StatusEntry = InputStatus.Duplicate;

                        if (isNotAsEdit)
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
            get => _statusEntry; private set
            {
                _statusEntry = value;

                // raise event for status change
                OnEntryStatusChange.CheckedInvoke(value, !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
            }
        }
        #endregion

        #region Methods
        public void SelectEntry(string entry)
        {
            selectedEntry = selected?.List.FirstOrDefault(e => e == entry);
        }

        // list modification actions
        public void AddEntry()
        {
            // if not ready then
            // throw new Exception("The entry is invalid.")

            _inputList.Add(_inputEntry);

            // raise event: set (add) => use the ObservableCollection event
        }

        public void EditEntry()
        {
            // if not ready then
            // throw new Exception("The entry is invalid or unchanged.");

            int i = _inputList.IndexOf(editEntry);
            _inputList[i] = _inputEntry;

            // clear old entry value
            editEntry = null;

            // raise event: set (Edit) => use the ObservableCollection event
        }

        public void RemoveEntry()
        {
            _inputList.Remove(selectedEntry);
        }

        public void MoveEntry(string entry, ShiftDirection direction)
        {

        }

        // private methods

        // private functions (getter method)
        private bool IsInputEntryDuplicate(string value)
        {
            //throw new NotImplementedException();
            return selectedEntries.Contains(value);
        }

        private bool IsNotInputEntryAsEdit(string value)
        {
            return value != editEntry;
        }
        #endregion

        #region Fields
        // inputs
        private string _inputEntry;

        // inputs status
        private InputStatus _statusEntry;

        // flags

        // objects
        private List<string> selectedEntries;
        private string selectedEntry;
        private string editEntry;
        #endregion
    }
}