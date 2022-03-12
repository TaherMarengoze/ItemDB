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
                SetStatusCommonName(Operations.GetInputStatus(value,
                    GetEditObjectId(), provider.GetList()));
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

        public void Save()
        {
            setAction(provider.GetList());
            sourceList = null;
            broker = null;
            provider = null;

            // raise event
        }

        public void Load()
        {
            LoadEventArgs args = new LoadEventArgs(GetGenericViewList());

            // raise event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string refId)
        {
            if (refId == null)
                throw new ArgumentNullException(nameof(refId));

            selectedObject = broker.Read(refId);

            var args = new SelectEventArgs<string>
            {
                Selected = selectedObject,
                RequestInfo = refId
            };

            // raise #event
            OnSelect?.Invoke(this, args);
        }

        public void New()
        {
            var args = new PreModifyEventArgs(provider.GetList());

            // raise event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Edit()
        {
            SetEditObject();
            FillInputs();

            var args = new PreModifyEventArgs(editObject.Clone(),
                provider.GetIDs());

            // raise #event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Remove() { }

        public void CommitChanges()
        {
            if (!STATE_DRAFT_READY)
                throw new Exception("Entry is invalid or unchanged");

            CreateOrUpdate();

            var args = new SetEventArgs
            {
                NewID = inputCommonName,
                OldID = editObject,
                NewList = provider.GetList().ToList()
            };

            // clear objects
            selectedObject = null;
            editObject = null;
            ClearInputs();

            // raise #event
            OnSet?.Invoke(this, args);
        }

        public void CancelChanges() { }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the edit object.
        /// </summary>
        private void SetEditObject()
        {
            editObject = /*broker.Read(selectedObject)*/selectedObject;
        }

        /// <summary>
        /// Copy the data of the edited object to the inputs.
        /// </summary>
        private void FillInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputCommonName = editObject;

            DISABLE_STATUS_RAISE_EVENT = false;
        }
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

        private void CreateOrUpdate()
        {
            string draftObject = CreateDraftObject();

            if (editObject == null)
            {
                broker.Create(draftObject);
            }
            else
            {
                broker.Update(editObject, draftObject);
            }
        }

        private void ClearInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputCommonName = string.Empty;

            DISABLE_STATUS_RAISE_EVENT = false;
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
            };

            return inputStatus.All(status => status == InputStatus.Valid);
        }

        private bool IsDraftChanged()
        {
            bool[] draftChange = {
                Operations.IsChanged(inputCommonName, GetEditObjectId()),
            };

            return draftChange.Any(change => change);
        }

        private string CreateDraftObject()
        {
            return inputCommonName;
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
