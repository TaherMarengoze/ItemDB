using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Brokers;
using ClientService.Contracts;
using ClientService.Data;
using Controllers.Common;
using CoreLibrary.Enums;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.ViewModels;

namespace Controllers
{
    public class ItemController : IController
    {
        #region Events
        // common events
        public event EventHandler<LoadEventArgs> OnLoad;
        public event EventHandler<SelectEventArgs<IItem>> OnSelect;
        public event EventHandler<ReadyEventArgs> OnReadyStateChange;
        public event EventHandler<PreModifyEventArgs> OnPreDrafting;
        public event EventHandler<SetEventArgs> OnSet;
        public event EventHandler<CancelEventArgs> OnCancel;
        public event EventHandler<RemoveEventArgs> OnRemove;

        // specific events
        public event EventHandler<StatusEventArgs> OnIdStatusChange;
        #endregion

        #region Inputs
        public string InputID
        {
            get => inputID;
            set
            {
                inputID = value;

                StatusID = Operations.GetInputStatus(value,
                    provider.GetIDs()/*, editObject?.ID*/);
            }
        }
        #endregion

        #region Input Status
        public InputStatus StatusID
        {
            get => statusID;
            private set
            {
                statusID = value;

                StatusEventArgs args = new StatusEventArgs(value, inputID);

                // raise #event
                OnIdStatusChange.CheckedInvoke(args,
                    !DISABLE_STATUS_RAISE_EVENT);

                // check all inputs status
                CheckReadyStatus();
            }
        }
        #endregion

        #region Public Methods
        public void Save() { }

        public void Load()
        {
            var args = new LoadEventArgs(provider.GetList().ToGenericView());

            // raise event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string refId)
        {
            if (refId == null)
                throw new ArgumentNullException(nameof(refId));

            selectedObject = broker.Read(refId);

            var args = new SelectEventArgs<IItem>
            {
                Selected = selectedObject,
                RequestInfo = refId
            };

            // raise #event
            OnSelect?.Invoke(this, args);
        }
        public void New()
        {
            var args = new PreModifyEventArgs(provider.GetIDs());

            // raise event
            OnPreDrafting?.Invoke(this, args);
        }
        public void Edit() { }
        public void Remove()
        {
            if (selectedObject == null)
                throw new InvalidOperationException(
                    "Unable to perform operation before selection");

            broker.Delete(selectedObject.ItemID);

            var args = new RemoveEventArgs(selectedObject.ItemID,
                provider.GetList().ToGenericView());

            selectedObject = null;

            // raise event
            OnRemove?.Invoke(this, args);
        }
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

        /// <summary>
        /// Sets the edit object.
        /// </summary>
        private void SetEditObject()
        {
            editObject = broker.Read(selectedObject.ItemID);
        }

        /// <summary>
        /// Copy the data of the edited object to the inputs.
        /// </summary>
        private void FillInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            //InputID = editObject.ID;
            //InputName = editObject.Name;
            // etc ...

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        /// <summary>
        /// Clears the selected object field.
        /// </summary>
        private void ClearSelection()
        {
            selectedObject = null;
        }

        /// <summary>
        /// Clear all inputs without raising the change event of the associated
        /// input status.
        /// </summary>
        private void ClearInputs()
        {
            DISABLE_STATUS_RAISE_EVENT = true;

            //InputID = string.Empty;
            //InputName = string.Empty;
            // etc ...

            DISABLE_STATUS_RAISE_EVENT = false;
        }

        private void SetStatusInitialValues()
        {
            //statusID = InputStatus.Blank;
            //statusName = InputStatus.Blank;
            // etc ...
        }
        #endregion

        #region Private Getters
        private bool IsValidInputs()
        {
            InputStatus[] inputStatus = {
                //StatusID,
                //StatusName,
                // etc ...
            };

            return inputStatus.All(status => status == InputStatus.Valid);
        }

        private bool IsDraftChanged()
        {
            bool[] draftChange = {
                //inputID != null && inputID != editObject?.ID,
                //inputName != null && inputName != editObject?.Name,
                // etc ...
            };

            return draftChange.Any(change => change);
        }
        #endregion

        #region Fields

        private readonly IBroker<IItem> broker = new ItemBroker();
        private readonly IProvider<IItem> provider = new ItemProvider();
        
        // backing fields
        private string inputID;
        private InputStatus statusID;

        // flags
        private bool STATE_DRAFT_READY;
        private bool DISABLE_STATUS_RAISE_EVENT;

        // objects
        private IItem selectedObject;
        private IItem editObject;
        #endregion
    }
}