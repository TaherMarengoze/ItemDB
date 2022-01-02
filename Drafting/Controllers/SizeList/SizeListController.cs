using ClientService;
using ClientService.Brokers;
using ClientService.Data;
using CoreLibrary.Enums;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class SizeListController : IController
    {
        public SizeListController()
        {
            ClearInputs();
        }

        #region Events
        public event EventHandler<InputStatus> OnIdStatusChange;
        public event EventHandler<InputStatus> OnNameStatusChange;
        public event EventHandler<InputStatus> OnListStatusChange;
        #endregion

        #region Properties
        public string InputID
        {
            get => _inputID;
            set
            {
                _inputID = value;
                if (string.IsNullOrWhiteSpace(value))
                    StatusID = InputStatus.Blank;
                else
                {
                    // check for duplicate
                    bool isDuplicate = sizeDP.GetIDs().Contains(value) && value != draftObject?.ID;

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
            get => _inputName;
            set
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
            get => _inputList;
            set
            {
                _inputList = value;

                bool notNullOrEmpty = value?.Count > 0;
                StatusList = notNullOrEmpty ? InputStatus.Valid : InputStatus.Invalid;
            }
        }

        public InputStatus StatusID
        {
            get => _statusID; set
            {
                _statusID = value;

                // raise event for status change
                //CheckInvoke(OnIdStatusChange, value);
                OnIdStatusChange.CheckedInvoke(value, !DISABLE_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusName
        {
            get => _statusName; set
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
            get => _statusList; set
            {
                _statusList = value;

                // raise event for status change
                OnListStatusChange.CheckedInvoke(value, !DISABLE_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }


        #endregion

        #region Methods
        // public methods
        public void Save()
        {
            ContextProvider.Save(ContextEntity.Sizes);
        }

        public void Select() => throw new NotImplementedException();

        public void New() => throw new NotImplementedException();

        public void Edit(string refId) => throw new NotImplementedException();

        public void Remove(string refId) => throw new NotImplementedException();

        public void CommitChanges() => throw new NotImplementedException();

        public void CancelChanges() => throw new NotImplementedException();

        /// private methods

        private void CheckReadyStatus()
        {
            //throw new NotImplementedException();
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            isReady = isValid && isChanged;
        }

        private void ClearInputs()
        {
            DISABLE_RAISE_EVENT = true;

            InputID = string.Empty;
            InputName = string.Empty;

            DISABLE_RAISE_EVENT = false;
        }

        //private void CheckInvoke<T>(EventHandler<T> handler, T val)
        //{
        //    if (!DISABLE_RAISE_EVENT)
        //        handler?.Invoke(this, val);
        //}

        /// private getter methods

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
            bool[] inputs =
            {
                _inputID != draftObject.ID,
                _inputName != draftObject.Name
            };

            return inputs.Any(input => input);
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

        private SizeList draftObject;
        #endregion
    }
}