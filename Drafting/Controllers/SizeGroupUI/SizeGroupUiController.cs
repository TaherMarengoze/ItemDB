
using ClientService;
using ClientService.Data;
using CoreLibrary.Enums;
using Modeling.DataModels;
using Modeling.ViewModels;
using Modeling.ViewModels.SizeGroup;
using System;
using System.Collections.Generic;


namespace Controllers.SizeGroupUI
{
    public class SizeGroupUiController
    {
        #region Events
        public event EventHandler<SizeGroupSelectionEventArgs> OnSelectionChange;
        public event EventHandler<InputStatus> OnIdStatusChange;
        public event EventHandler<InputStatus> OnNameStatusChange;
        public event EventHandler<InputStatus> OnDefaultIdStatusChange;
        public event EventHandler<InputStatus> OnCustomIdStatusChange;
        public event EventHandler<bool> OnAltListStatusChange;
        public event EventHandler<bool> OnReadyStateChange;
        public event EventHandler<string> OnNewEntityAdd;
        #endregion

        #region Properties

        public List<SizeGroupsGenericView> SizeGroups =>
            sgProvider.GetList().ToGenericView();

        public int Count => SizeGroups?.Count ?? 0;
        public List<string> SizeIDs => sProvider.GetIDs();

        public List<string> CustomSizeIDs => csProvider.GetIDs();

        public SizeGroup SelectedSizeGroup { get; private set; }

        #region UI Inputs
        public string InputID
        {
            get => _inputID;
            set
            {
                _inputID = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    // set blank id status
                    StatusID = InputStatus.Blank;
                }
                else
                {
                    // check for duplicate
                    bool isDuplicate = sgProvider.GetIDs().Contains(value);

                    if (isDuplicate)
                    {
                        // set duplicate id status
                        StatusID = InputStatus.Duplicate;
                    }
                    else
                    {
                        // check for valid characters
                        bool isValidChar = true;

                        if (isValidChar)
                        {
                            // set valid id status
                            StatusID = InputStatus.Valid;
                        }
                        else
                        {
                            // set invalid id status
                            StatusID = InputStatus.Invalid;
                        }
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
                {
                    StatusName = InputStatus.Blank;
                }
                else
                {
                    // check for valid characters
                    bool isValidChar = true;

                    if (isValidChar)
                    {
                        // set valid id status
                        StatusName = InputStatus.Valid;
                    }
                    else
                    {
                        // set invalid id status
                        StatusName = InputStatus.Invalid;
                    }
                }
            }
        }

        public string InputDefaultID
        {
            get => _inputDefaultID;
            set
            {
                _inputDefaultID = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    StatusDefaultID = InputStatus.Blank;
                }
                else
                {
                    // check if valid, this check is useless in this case
                    // since the UI provides predefined selection from a list
                    // but should the UI changes to a text input (i.e. Console)
                    // a check must be made to make sure that the id exists
                    bool isValid = true;
                    if (isValid)
                    {
                        StatusDefaultID = InputStatus.Valid;
                    }
                    else
                    {
                        StatusDefaultID = InputStatus.Invalid;
                    }
                }
            }
        }

        public bool InputAltListRequired
        {
            get => _inputAltListRequired;
            set
            {
                _inputAltListRequired = value;
                if (value == true)
                {
                    // check if list is valid
                    if (_validAltList == true)
                    {
                        StatusAltList = InputStatus.Valid;
                    }
                    else
                    {
                        StatusAltList = InputStatus.Invalid;
                    }
                }
                else
                {
                    StatusAltList = InputStatus.Ignore;
                }
            }
        }

        public List<string> InputAltList
        {
            get => _inputAltList;
            set
            {
                _inputAltList = value;
                int? notNullOrEmpty = value?.Count;
                ValidAltList = notNullOrEmpty > 0 ? true : false;
            }
        }

        public string InputCustomID
        {
            get => _inputCustomID;
            set
            {
                _inputCustomID = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    StatusCustomID = InputStatus.Blank;
                }
                else
                {
                    // check if valid, this check is useless in this case
                    // since the UI provides predefined selection from a list
                    // but should the UI changes to a text input (i.e. Console)
                    // a check must be made to make sure that the id exists
                    bool isValid = true;
                    if (isValid)
                    {
                        StatusCustomID = InputStatus.Valid;
                    }
                    else
                    {
                        StatusCustomID = InputStatus.Invalid;
                    }
                }
            }
        }
        #endregion

        #region Inputs Status
        public InputStatus StatusID
        {
            get => _statusID;
            private set
            {
                _statusID = value;

                // raise event for status change
                OnIdStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckInputsStatus();
            }
        }

        public InputStatus StatusName
        {
            get => _statusName;
            private set
            {
                _statusName = value;

                // raise event for status change
                OnNameStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckInputsStatus();
            }
        }

        public InputStatus StatusDefaultID
        {
            get => _statusDefaultID;
            private set
            {
                _statusDefaultID = value;

                // raise event for status change
                OnDefaultIdStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckInputsStatus();
            }
        }

        public InputStatus StatusAltList
        {
            get => _statusAltList;
            private set
            {
                _statusAltList = value;

                // raise event for status change

                // check all inputs status
                CheckInputsStatus();
            }
        }

        public InputStatus StatusCustomID
        {
            get => _statusCustomID;
            private set
            {
                _statusCustomID = value;

                // raise event for status change
                OnCustomIdStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckInputsStatus();
            }
        }

        public bool ValidAltList
        {
            get => _validAltList;
            private set
            {
                _validAltList = value;

                if (value == true)
                {
                    if (_inputAltListRequired == false)
                    {
                        _inputAltListRequired = true;
                    }
                    StatusAltList = InputStatus.Valid;
                    
                }
                else
                {
                    if (_inputAltListRequired == true)
                    {
                        StatusAltList = InputStatus.Invalid;
                    }
                    else
                    {
                        StatusAltList = InputStatus.Ignore;
                    }
                    
                }
                
                // raise event for status change
                //OnAltListStatusChange?.Invoke(this, value);

                // check all inputs status
                //CheckInputsStatus();
            }
        }
        #endregion

        #endregion

        #region Methods

        public void SetSelection(string id)
        {
            SizeGroup sizeGroup = (SizeGroup)cache.Read(id);

            // raise an event for selection change
            OnSelectionChange?.Invoke(this,
                new SizeGroupSelectionEventArgs(sizeGroup));
        }

        public List<string> GetListEntries(string listID)
        {
            return sProvider.GetEntries(listID);
        }

        public void New()
        {

        }

        public void AddNew()
        {
            //throw new NotImplementedException();
            SizeGroup draft = new SizeGroup
            {
                ID = InputID,
                Name = InputName,
                DefaultListID = InputDefaultID,
                AltIdList = InputAltList,
                CustomSize = InputCustomID
            };

            cache.Create(draft);

            //ClearInputs();

            OnNewEntityAdd?.Invoke(this, InputID);
        }

        // private methods
        private void CheckInputsStatus()
        {
            bool validID = StatusID == InputStatus.Valid;
            bool validName = StatusName == InputStatus.Valid;
            bool validDefaultID = StatusDefaultID == InputStatus.Valid;
            bool validAltList = StatusAltList == InputStatus.Valid || StatusAltList == InputStatus.Ignore;
            bool validCustomID = StatusCustomID == InputStatus.Valid;

            bool isReady =
                validID &&
                validName &&
                validDefaultID &&
                validAltList &&
                validCustomID;

            // raise event for the ready or unready state
            OnReadyStateChange?.Invoke(this, isReady);
        }

        private void ClearInputs()
        {
            //throw new NotImplementedException();
            _inputID = null;
            InputName = null;
            InputDefaultID = null;
            InputAltList = null;
            InputCustomID = null;
        }
        #endregion

        #region Fields
        private readonly SizeGroupCache cache = new SizeGroupCache();
        private SizeGroupProvider sgProvider = new SizeGroupProvider();
        private SizeProvider sProvider = new SizeProvider();
        private CustomSizeProvider csProvider = new CustomSizeProvider();
        private string _inputID;
        private string _inputName;
        private string _inputDefaultID;
        private bool _inputAltListRequired;
        private List<string> _inputAltList;
        private string _inputCustomID;
        private InputStatus _statusID;
        private InputStatus _statusName;
        private InputStatus _statusDefaultID;
        private InputStatus _statusAltList;
        private InputStatus _statusCustomID;
        private bool _validAltList;

        #endregion

        #region Simulation
        public void Simulate_New()
        {
            throw new NotImplementedException();
            //SizeGroup content = new SizeGroup
            //{
            //    ID = "GSIM0",
            //    Name = "Size Group Simulation",
            //    DefaultListID = "SHLM",
            //    AltIdList = null,
            //    CustomSize = null
            //};
            //repos.Create(content);
        }
        #endregion
    }
}