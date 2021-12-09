
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
        public event EventHandler<ValidityStatus> OnIdStatusChange;
        public event EventHandler<ValidityStatus> OnNameStatusChange;
        public event EventHandler<ValidityStatus> OnDefaultIdStatusChange;
        public event EventHandler<bool> OnReadyStateChange;
        #endregion

        #region Properties

        public List<SizeGroupsGenericView> SizeGroups =>
            sgProvider.GetList().ToGenericView();

        public int Count => SizeGroups?.Count ?? 0;
        public List<string> SizeIDs => sProvider.GetIDs();

        public List<string> CustomSizeIDs => csProvider.GetIDs();

        public SizeGroup SelectedSizeGroup { get; private set; }

        // UI Inputs
        public string InputID
        {
            get => _inputID;
            set
            {
                _inputID = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    // set blank id status
                    StatusID = ValidityStatus.Blank;
                }
                else
                {
                    // check for duplicate
                    bool isDuplicate = sgProvider.GetIDs().Contains(value);

                    if (isDuplicate)
                    {
                        // set duplicate id status
                        StatusID = ValidityStatus.Duplicate;
                    }
                    else
                    {
                        // check for valid characters
                        bool isValidChar = true;

                        if (isValidChar)
                        {
                            // set valid id status
                            StatusID = ValidityStatus.Valid;
                        }
                        else
                        {
                            // set invalid id status
                            StatusID = ValidityStatus.Invalid;
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
                    StatusName = ValidityStatus.Blank;
                }
                else
                {
                    // check for valid characters
                    bool isValidChar = true;

                    if (isValidChar)
                    {
                        // set valid id status
                        StatusName = ValidityStatus.Valid;
                    }
                    else
                    {
                        // set invalid id status
                        StatusName = ValidityStatus.Invalid;
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
                    StatusDefaultID = ValidityStatus.Blank;
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
                        StatusDefaultID = ValidityStatus.Valid;
                    }
                    else
                    {
                        StatusDefaultID = ValidityStatus.Invalid;
                    }
                }
            }
        }
        
        // Inputs Status
        public ValidityStatus StatusID
        {
            get => _statusID;
            private set
            {
                _statusID = value;

                // raise event for status change
                OnIdStatusChange?.Invoke(this, value);

                // draft object ready check
                CheckReady();
            }
        }

        public ValidityStatus StatusName
        {
            get => _statusName;
            set
            {
                _statusName = value;

                // raise event for status change
                OnNameStatusChange?.Invoke(this, value);

                // draft object ready check
                CheckReady();
            }
        }

        public ValidityStatus StatusDefaultID
        {
            get => _statusDefaultID;
            set
            {
                _statusDefaultID = value;

                // raise event for status change
                OnDefaultIdStatusChange?.Invoke(this, value);

                // draft object ready check
                CheckReady();
            }
        }
        #endregion

        #region Methods

        public void SetSelection(string id)
        {
            SizeGroup sizeGroup = (SizeGroup)cache.Read(id);

            // raise an event for selection change
            OnSelectionChange?.Invoke(this,
                new SizeGroupSelectionEventArgs(sizeGroup));
        }

        public void New()
        {
            
        }

        public void CheckReady()
        {
            bool validID = StatusID == ValidityStatus.Valid;
            bool validName = StatusName == ValidityStatus.Valid;
            bool validDefaultID = StatusDefaultID == ValidityStatus.Valid;

            bool isReady = validID && validName & validDefaultID;

            // raise event for the ready or unready state
            OnReadyStateChange(this, isReady);
        }

        public List<string> GetListEntries(string listID)
        {
            return sProvider.GetEntries(listID);
        }
        // private methods
        #endregion

        #region Fields
        private readonly SizeGroupCache cache = new SizeGroupCache();
        private SizeGroupProvider sgProvider = new SizeGroupProvider();
        private SizeProvider sProvider = new SizeProvider();
        private CustomSizeProvider csProvider = new CustomSizeProvider();
        private string _inputID;
        private string _inputName;
        private string _inputDefaultID;
        private ValidityStatus _statusID;
        private ValidityStatus _statusName;
        private ValidityStatus _statusDefaultID;

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