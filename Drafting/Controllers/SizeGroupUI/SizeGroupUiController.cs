
using ClientService;
using ClientService.Data;
using CoreLibrary.Enums;
using Interfaces.Models;
using Modeling.DataModels;
using Modeling.ViewModels;
using Modeling.ViewModels.SizeGroup;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controllers.SizeGroupUI
{
    public class SizeGroupUiController : Interfaces.General.IDraftable
    {
        public SizeGroupUiController()
        {
            ClearInputs();
        }

        #region Events
        public event EventHandler<SizeGroupSelectionEventArgs> OnSelectionChange;
        public event EventHandler<SizeGroupAltListSetEventArgs> OnInputAltListSet;
        public event EventHandler<InputStatus> OnIdStatusChange;
        public event EventHandler<InputStatus> OnNameStatusChange;
        public event EventHandler<InputStatus> OnDefaultIdStatusChange;
        public event EventHandler<InputStatus> OnCustomIdStatusChange;
        public event EventHandler<InputStatus> OnAltListStatusChange;
        public event EventHandler<SizeGroupReadyEventArgs> OnReadyStateChange;
        public event EventHandler<string> OnEntitySet;
        public event EventHandler<string> OnDraftCancel;
        #endregion

        #region Properties

        public List<SizeGroupsGenericView> SizeGroups =>
            sizeGroupDP.GetList().ToGenericView();

        public int Count => SizeGroups?.Count ?? 0;

        public List<string> SizeIDs => sizeDP.GetIDs();

        public List<string> CustomSizeIDs => csProvider.GetIDs();

        public SizeGroup SelectedSizeGroup { get; private set; }

        public List<SizeList> SizeLists => sizeDP.GetList().As<SizeList>();

        /// <summary>
        /// Get the size lists excluding the default size list.
        /// </summary>
        public List<IFieldList> SizeListsDefaultEx => sizeDP.GetListExcluded(InputDefaultID);

        /// <summary>
        /// Get the size ID lists excluding the alt size list IDs.
        /// </summary>
        public List<string> SizeIdListsAltEx =>
            (from sizeList in sizeDP.GetList()
             where !_inputAltList.Contains(sizeList.ID)
             select sizeList.ID)
            .ToList();

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
                    bool isDuplicate = sizeGroupDP.GetIDs().Contains(value) && value != draftSizeGroup?.ID;

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

                // set input validity status
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

                // set input validity status
                if (string.IsNullOrWhiteSpace(value))
                {
                    StatusDefaultID = InputStatus.Blank;
                }
                else
                {
                    /// check if valid, this check is useless in this case
                    /// since the UI provides predefined selection from a list
                    /// but should the UI changes to a text input(i.e.Console)
                    /// a check must be made to make sure that the id exists
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
                CheckReadyStatus();
            }
        }

        public List<string> InputAltList
        {
            get => _inputAltList;
            set
            {
                _inputAltList = value;

                // raise an event for change
                if (!DISABLE_RAISE_EVENT)
                    OnInputAltListSet?.Invoke(this, new SizeGroupAltListSetEventArgs
                    {
                        SelectedSizeLists = _inputAltList,
                        AvailableSizeLists = GetAvailableSizesID().ToList()
                    });

                // set input validity status
                bool notNullOrEmpty = value?.Count > 0;
                StatusAltList = notNullOrEmpty ? InputStatus.Valid : InputStatus.Invalid;
            }
        }

        public bool InputCustomIdRequired
        {
            get => _inputCustomIdRequired;
            set
            {
                _inputCustomIdRequired = value;
                CheckReadyStatus();
            }
        }

        public string InputCustomID
        {
            get => _inputCustomID;
            set
            {
                _inputCustomID = value;

                // set input validity status
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
                if (!DISABLE_RAISE_EVENT)
                    OnIdStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusName
        {
            get => _statusName;
            private set
            {
                _statusName = value;

                // raise event for status change
                if (!DISABLE_RAISE_EVENT)
                    OnNameStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusDefaultID
        {
            get => _statusDefaultID;
            private set
            {
                _statusDefaultID = value;

                // raise event for status change
                if (!DISABLE_RAISE_EVENT)
                    OnDefaultIdStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusAltList
        {
            get => _statusAltList;
            private set
            {
                _statusAltList = value;

                // raise event for status change
                if (!DISABLE_RAISE_EVENT)
                    OnAltListStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckReadyStatus();
            }
        }

        public InputStatus StatusCustomID
        {
            get => _statusCustomID;
            private set
            {
                _statusCustomID = value;

                // raise event for status change
                if (!DISABLE_RAISE_EVENT)
                    OnCustomIdStatusChange?.Invoke(this, value);

                // check all inputs status
                CheckReadyStatus();
            }
        }
        #endregion

        #endregion

        #region Methods
        /* public getter methods */
        public List<SizeList> GetSizeListsExcluded(string excludId)
        {
            return sizeDP.GetListExcluded(excludId).As<SizeList>();
        }
        public List<string> GetListEntries(string listID)
        {
            return sizeDP.GetEntries(listID);
        }

        public void SetSelection(string id)
        {
            SizeGroup sizeGroup = (SizeGroup)cache.Read(id);

            // raise an event for selection change
            OnSelectionChange?.Invoke(this,
                new SizeGroupSelectionEventArgs(sizeGroup));
        }

        public void New()
        {
            //DISABLE_RAISE_EVENT = true;

            //InputID = string.Empty;
            //InputName = string.Empty;
            //InputDefaultID = string.Empty;
            //InputAltList = null;
            //InputCustomID = string.Empty;
            //InputAltListRequired = false;
            //InputCustomIdRequired = false;

            //DISABLE_RAISE_EVENT = false;
        }

        public void Edit(string groupId)
        {
            // get and store the original object
            draftSizeGroup = (SizeGroup)cache.Read(groupId);

            DISABLE_RAISE_EVENT = true;

            InputID = draftSizeGroup.ID;
            InputName = draftSizeGroup.Name;
            InputDefaultID = draftSizeGroup.DefaultListID;
            InputAltList = draftSizeGroup.AltIdList;
            InputCustomID = draftSizeGroup.CustomSize;
            InputAltListRequired = _inputAltList != null ? true : false;
            InputCustomIdRequired = _inputCustomID != null ? true : false;

            DISABLE_RAISE_EVENT = false;
        }

        public void CommitChanges()
        {
            if (isReady == false)
                return;

            SizeGroup draft = new SizeGroup
            {
                ID = InputID,
                Name = InputName,
                DefaultListID = InputDefaultID,
                AltIdList = InputAltList,
                CustomSize = InputCustomID
            };

            if (draftSizeGroup == null)
            {
                cache.Create(draft);
            }
            else
            {
                cache.Update(draftSizeGroup.ID, draft);
                draftSizeGroup = null;
            }

            OnEntitySet?.Invoke(this, InputID);

            ClearInputs();
        }

        public void CancelChanges()
        {
            if (draftSizeGroup == null)
                draftSizeGroup = null;

            // raise cancel event
            OnDraftCancel?.Invoke(this, string.Empty);

            ClearInputs();
        }

        // private getter methods
        private IEnumerable<string> GetAvailableSizesID()
        {
            return _inputAltList == null ?
                SizeIDs :
                SizeIDs.Where(id => _inputAltList.Contains(id) == false);
        }

        private bool IsValidInputs()
        {
            bool validID = StatusID == InputStatus.Valid;
            bool validName = StatusName == InputStatus.Valid;
            bool validDefaultID = StatusDefaultID == InputStatus.Valid;
            bool validAltList = _inputAltListRequired ? _statusAltList == InputStatus.Valid : true;
            bool validCustomID = _inputCustomIdRequired ? StatusCustomID == InputStatus.Valid : true;

            bool isValid =
                validID && validName && validDefaultID && validAltList && validCustomID;

            return isValid;
        }

        private bool IsDraftChanged()
        {
            // in case of adding new
            if (draftSizeGroup == null)
                return true;

            bool idChanged = _inputID != draftSizeGroup.ID;
            bool nameChanged = _inputName != draftSizeGroup.Name;
            bool defIdChanged = _inputDefaultID != draftSizeGroup.DefaultListID;

            bool altChanged = IsAltListChanged();
            bool custIdChanged = IsCustomIdChanged();

            bool isChanged =
                idChanged || nameChanged || defIdChanged || custIdChanged || altChanged;

            return isChanged;
        }

        private bool IsAltListChanged()
        {
            //bool draftAltRequired = false;

            // draft has no Alt ID list
            if (draftSizeGroup.AltIdList == null)
            {
                if (_inputAltList == null || !_inputAltListRequired)
                    return false;

                return true;
            }
            else
            {
                //draftAltRequired = true;

                if (_inputAltList == null || !_inputAltListRequired)
                    return true;

                return !_inputAltList.SequenceEqual(draftSizeGroup.AltIdList);
            }
        }

        private bool IsCustomIdChanged()
        {
            // draft has no Custom ID
            if (string.IsNullOrWhiteSpace(draftSizeGroup.CustomSize))
            {
                if (string.IsNullOrWhiteSpace(_inputCustomID) || !_inputCustomIdRequired)
                    return false;

                return true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_inputCustomID) || !_inputCustomIdRequired)
                    return true;

                return !_inputCustomID.Equals(draftSizeGroup.CustomSize);
            }
        }


        // private methods
        private void CheckReadyStatus()
        {
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            isReady = isValid && isChanged;

            // raise event for the ready or unready state
            OnReadyStateChange?.Invoke(this, new SizeGroupReadyEventArgs
            {
                Ready = isReady,
                Info = isValid ? isChanged ? "Ready" : "Unchanged" : "Not Ready"
            });
        }

        private void ClearInputs()
        {
            DISABLE_RAISE_EVENT = true;

            InputID = string.Empty;
            InputName = string.Empty;
            InputDefaultID = string.Empty;
            InputAltList = null;
            InputCustomID = string.Empty;
            InputAltListRequired = false;
            InputCustomIdRequired = false;

            DISABLE_RAISE_EVENT = false;
        }
        #endregion

        #region Fields[FLDS]
        private readonly SizeGroupCache cache = new SizeGroupCache();
        private SizeGroupProvider sizeGroupDP = new SizeGroupProvider();
        private SizeProvider sizeDP = new SizeProvider();
        private CustomSizeProvider csProvider = new CustomSizeProvider();
        private string _inputID;
        private string _inputName;
        private string _inputDefaultID;
        private bool _inputAltListRequired;
        private List<string> _inputAltList;
        private bool _inputCustomIdRequired;
        private string _inputCustomID;
        private InputStatus _statusID;
        private InputStatus _statusName;
        private InputStatus _statusDefaultID;
        private InputStatus _statusAltList;
        private InputStatus _statusCustomID;
        private bool isReady;
        private SizeGroup draftSizeGroup;

        // flags
        private bool DISABLE_RAISE_EVENT;
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