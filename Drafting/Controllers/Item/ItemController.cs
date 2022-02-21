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
        #region Public Methods
        public void Save() { }
        public void Load()
        {
            var args = new LoadEventArgs(provider.GetList().ToGenericView());
        }
        public void Select(string refId) { }
        public void New() { }
        public void Edit() { }
        public void Remove() { }
        public void CommitChanges() { }
        public void CancelChanges() { }
        #endregion

        #region Fields

        private readonly IBroker<IItem> broker = new ItemBroker();
        private readonly IProvider<IItem> provider = new ItemProvider();
        #endregion
    }
}