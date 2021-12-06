
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
        #region Events
        public event EventHandler<SizeGroupSelectionEventArgs> OnSelectionChange;
        public event EventHandler<ValidityStatus> OnIdStatusChange;
        #endregion

        #region Properties

        public List<SizeGroupsGenericView> SizeGroups =>
            DataProvider.SizeGroup.GetList().ToGenericView();

        public int Count => SizeGroups?.Count ?? 0;
        public List<string> SizeIDs => DataProvider.Size.GetIDs();

        public List<string> CustomSizeIDs => DataProvider.CustomSize.GetIDs();

        public SizeGroup SelectedSizeGroup { get; private set; }

        // UI Inputs
        public string InputID
        {
            get => _inputID;
            set
            {
                _inputID = value;
                if (string.IsNullOrEmpty(value))
                {
                    // set blank id status
                    StatusID = ValidityStatus.Blank;
                }
                else
                {
                    // check for duplicate
                    bool isDuplicate = DataProvider.SizeGroup.GetIDs().Contains(value);

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

        #endregion

        #region Fields
        private readonly SizeGroupRepository repos = new SizeGroupRepository();
        private string _inputID;
        private ValidityStatus _statusID;
        #endregion
    }
}