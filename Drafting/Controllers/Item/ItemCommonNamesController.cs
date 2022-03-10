using System;
using System.Collections;
using System.Collections.Custom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Brokers;
using ClientService.Contracts;
using ClientService.Data;
using Controllers.Common;
using CoreLibrary.Enums;
using Interfaces.Operations;

namespace Controllers
{
    public class ItemCommonNamesController : IController
    {
        public ItemCommonNamesController()
        {
        }

        #region Events
        // common events
        public event EventHandler<LoadEventArgs> OnLoad;
        public event EventHandler<SelectEventArgs<string>> OnSelect;
        public event EventHandler<ReadyEventArgs> OnReadyStateChange;
        public event EventHandler<PreModifyEventArgs> OnPreDrafting;
        public event EventHandler<SetEventArgs> OnSet;
        public event EventHandler<CancelEventArgs> OnCancel;
        public event EventHandler<RemoveEventArgs> OnRemove;

        // specific events
        public event EventHandler<StatusEventArgs> OnCommonNameStatusChange;
        #endregion

        #region Inputs
        public string InputCommonName
        {
            get => inputCommonName;
            set
            {
                inputCommonName = value;
                SetStatusCommonName(Operations.GetInputStatus(value));
            }
        }

        #endregion

        #region Input Status
        private void SetStatusCommonName(InputStatus value)
        {
            statusCommonName = value;
            StatusEventArgs args = new StatusEventArgs(value, inputCommonName);

            // raise event
            OnCommonNameStatusChange.CheckedInvoke(args,
                !DISABLE_STATUS_RAISE_EVENT);

            // check all inputs status
            CheckReadyStatus();
        }
        #endregion

        #region Public Methods
        internal void SetSource(List<string> source, Action<List<string>> set)
        {
            sourceList = source.AsReadOnly();
            broker = new ItemCommonNamesBroker(source);
            provider = new ItemCommonNamesProvider(source);
            setAction = set;
        }

        public void Save() {
            setAction(provider.GetList());
            sourceList = null;
            broker = null;
            provider = null;

            // raise event
        }

        public void Load() {
            LoadEventArgs args = new LoadEventArgs(GetGenericViewList());

            // raise event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string refId) { }
        public void New() { }
        public void Edit() { }
        public void Remove() { }
        public void CommitChanges() { }
        public void CancelChanges() { }
        #endregion

        #region Private Methods
        private void CheckReadyStatus()
        {
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            // set flags
            STATE_DRAFT_READY = isValid && isChanged;

            ReadyEventArgs args = new ReadyEventArgs(isValid, isChanged);

            // raise #event
            OnReadyStateChange?.CheckedInvoke(args,
                !DISABLE_STATUS_RAISE_EVENT);
        }
        #endregion

        #region Private Getters
        private List<string> GetGenericViewList()
        {
            return provider.GetList();
        }

        private string GetEditObjectId()
        {
            return editObject;
        }

        private bool IsValidInputs()
        {
            InputStatus[] inputStatus = {
                statusCommonName,
                // etc ...
            };

            return inputStatus.All(status => status == InputStatus.Valid);
        }

        private bool IsDraftChanged()
        {
            bool[] draftChange = {
                Operations.IsChanged(inputCommonName, GetEditObjectId()),
                //inputName != null && inputName != editObject?.Name,
                // etc ...
            };

            return draftChange.Any(change => change);
        }
        #endregion

        #region Fields
        private IBroker<string> broker;
        private IProvider<string> provider;
        private ReadOnlyCollection<string> sourceList;
        private Action<List<string>> setAction;

        //private readonly ItemController parent;
        //private ObservableCollection<string> inputCommonNamesDraft;

        // backing fields
        private string inputCommonName;
        private InputStatus statusCommonName;

        // flags
        private bool STATE_DRAFT_READY;
        private bool DISABLE_STATUS_RAISE_EVENT;

        // objects
        private string selectedObject;
        private string editObject;
        #endregion
    }
}
