using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Brokers;
using ClientService.Contracts;
using ClientService.Data;
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

        #region Public Methods
        public void Save() { }
        public void Load()
        {
            var args = new LoadEventArgs(provider.GetList().ToGenericView());

            // raise event
            OnLoad?.Invoke(this, args);
        }

        public void Select(string refId){
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
        public void New() {
            var args = new PreModifyEventArgs(provider.GetIDs());

            // raise event
            OnPreDrafting?.Invoke(this, args);
        }
        public void Edit() { }
        public void Remove(){
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

        #region Fields

        private readonly IBroker<IItem> broker = new ItemBroker();
        private readonly IProvider<IItem> provider = new ItemProvider();
        private IItem selectedObject;
        #endregion
    }
}