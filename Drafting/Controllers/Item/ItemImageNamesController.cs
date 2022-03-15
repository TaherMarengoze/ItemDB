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
    public class ItemImageNamesController : IController
    {
        public ItemImageNamesController()
        {
        }

        #region Events
        // common events
        public event EventHandler<LoadEventArgs> OnLoad;
        public event EventHandler OnSave;
        public event EventHandler<SelectEventArgs<string>> OnSelect;
        public event EventHandler<ReadyEventArgs> OnReadyStateChange;
        public event EventHandler<PreModifyEventArgs> OnPreDrafting;
        public event EventHandler<SetEventArgs> OnSet;
        public event EventHandler<CancelEventArgs> OnCancel;
        public event EventHandler<RemoveEventArgs> OnRemove;

        // specific events
        public event EventHandler<StatusEventArgs> OnImageNameStatusChange;
        #endregion

        #region Inputs
        public string InputImageName
        {
            get => inputImageName;
            set
            {
                inputImageName = value;
                SetStatusImageName(Operations.GetInputStatus(value,
                    GetEditObjectId(), provider.GetList()));
            }
        }

        #endregion

        #region Input Status
        private void SetStatusImageName(InputStatus value)
        {
            statusImageName = value;
            StatusEventArgs args = new StatusEventArgs(value, inputImageName);

            // raise event
            if (!DISABLE_STATUS_RAISE_EVENT)
                OnImageNameStatusChange?.Invoke(this, args);

            // check all inputs status
            CheckReadyStatus();
        }
        #endregion

        #region Public Methods
        public void Save()
        {
            ModifyParent();
            sourceList = null;
            broker = null;
            provider = null;

            // raise event
            OnSave?.Invoke(this, EventArgs.Empty);
        }

        public void Load()
        {
            LoadEventArgs args = new LoadEventArgs(GetGenericViewList());

            // raise event
            OnLoad?.Invoke(this, args);
        }

        public void Revert()
        {
            sourceList = null;
            broker = null;
            provider = null;

            // raise event
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

            // raise event
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

            var args = new PreModifyEventArgs(editObject, provider.GetIDs());

            // raise #event
            OnPreDrafting?.Invoke(this, args);
        }

        public void Remove()
        {
            if (selectedObject == null)
                throw new InvalidOperationException(
                    "Unable to perform operation before selection");

            broker.Delete(selectedObject);

            var args = new RemoveEventArgs(selectedObject,
                GetGenericViewList());

            selectedObject = null;

            // raise event
            OnRemove?.Invoke(this, args);
        }

        public void CommitChanges()
        {
            if (!STATE_DRAFT_READY)
                throw new Exception("Value is invalid or unchanged");

            CreateOrUpdate();

            var args = new SetEventArgs(inputImageName,
                editObject, GetGenericViewList());

            // clear objects
            selectedObject = null;
            editObject = null;
            ClearInputs();

            // raise #event
            OnSet?.Invoke(this, args);
        }

        public void CancelChanges()
        {
            CancelEventArgs args = new CancelEventArgs(selectedObject,
                GetGenericViewList());

            // clear objects
            editObject = null;
            ClearInputs();

            // raise #event
            OnCancel?.Invoke(this, args);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the edit object.
        /// </summary>
        private void SetEditObject()
        {
            editObject = selectedObject;
        }

        /// <summary>
        /// Copy the data of the edited object to the inputs.
        /// </summary>
        private void FillInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            InputImageName = editObject;

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
            // disable status change events triggering
            DISABLE_STATUS_RAISE_EVENT = true;

            InputImageName = string.Empty;

            DISABLE_STATUS_RAISE_EVENT = false;
        }
        #endregion

        #region Private Getters
        private List<string> GetGenericViewList()
        {
            return provider.GetList().ToList();
        }

        private string GetEditObjectId()
        {
            return editObject;
        }

        private bool IsValidInputs()
        {
            InputStatus[] inputStatus = {
                statusImageName,
            };

            return inputStatus.All(status => status == InputStatus.Valid);
        }

        private bool IsDraftChanged()
        {
            bool[] draftChange = {
                Operations.IsChanged(inputImageName, GetEditObjectId()),
            };

            return draftChange.Any(change => change);
        }

        private string CreateDraftObject()
        {
            return inputImageName;
        }
        #endregion

        #region Parent and Source
        internal void SetSource(List<string> source, Action<List<string>> set)
        {
            sourceList = source.AsReadOnly();
            broker = new ItemImageNamesBroker(source);
            provider = new ItemImageNamesProvider(source);
            setAction = set;
        }

        private void ModifyParent()
        {
            setAction(provider.GetList());
        }
        #endregion

        #region Fields
        private IBroker<string> broker;
        private IProvider<string> provider;
        private ReadOnlyCollection<string> sourceList; // will be needed in revert actions
        private Action<List<string>> setAction;

        // backing fields
        private string inputImageName;
        private InputStatus statusImageName;

        // flags
        private bool STATE_DRAFT_READY;
        private bool DISABLE_STATUS_RAISE_EVENT;

        // objects
        private string selectedObject;
        private string editObject;
        #endregion
    }
}
