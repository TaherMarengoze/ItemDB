
using ClientService;
using CoreLibrary.Enums;
using Modeling.DataModels;
using Modeling.ViewModels;
using Modeling.ViewModels.SizeGroup;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Controllers.SizeGroupUi
{
    public class SizeGroupSelectionEventArgs : EventArgs
    {
        public SizeGroupSelectionEventArgs(SizeGroup sg)
        {
            ID = sg.ID;
            Name = sg.Name;
            Default = sg.DefaultListID;
            AltList = sg.AltIdList;
            AltListCount = AltList?.Count ?? 0;
            Custom = sg.CustomSize;
        }

        public SizeGroup Selected { get; internal set; }

        public string ID { get; private set; }

        public string Name { get; private set; }

        public string Default { get; private set; }

        public List<string> AltList { get; private set; }

        public int AltListCount { get; private set; }

        public string Custom { get; private set; }
    }

    public class SizeGroupUiController
    {
        DataProvider.SizeGroup dpSizeGroup = new DataProvider.SizeGroup();
        DataProvider.Size dpSize = new DataProvider.Size();
        DataProvider.CustomSize dpCustSize = new DataProvider.CustomSize();

        #region Events
        public event EventHandler<SizeGroupSelectionEventArgs> OnSelectionChange;
        public event EventHandler<ValidityStatus> OnIdStatusChange;
        public event EventHandler<ValidityStatus> OnNameStatusChange;
        public event EventHandler<ValidityStatus> OnDefaultIdStatusChange;
        public event EventHandler<bool> OnReadyStateChange;
        #endregion

        #region Properties

        public List<SizeGroupsGenericView> SizeGroups =>
            /*DataProvider.SizeGroup*/dpSizeGroup.GetList().ToGenericView();

        public int Count => SizeGroups?.Count ?? 0;
        public List<string> SizeIDs => /*DataProvider.Size*/dpSize.GetIDs();

        public List<string> CustomSizeIDs => /*DataProvider.CustomSize*/dpCustSize.GetIDs();

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
                    bool isDuplicate = /*DataProvider.SizeGroup*/dpSizeGroup.GetIDs().Contains(value);

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
            SizeGroup sizeGroup = (SizeGroup)repos.Read(id);

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
            return dpSize.GetEntries(listID);
        }
        // private methods
        #endregion

        #region Fields
        private readonly SizeGroupRepository repos = new SizeGroupRepository();
        private string _inputID;
        private string _inputName;
        private string _inputDefaultID;
        private ValidityStatus _statusID;
        private ValidityStatus _statusName;
        private ValidityStatus _statusDefaultID;
        #endregion
    }
}