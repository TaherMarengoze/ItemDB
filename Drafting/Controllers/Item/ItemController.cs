using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Operations;

namespace Controllers
{
    public class ItemController : IController
    {
        #region Public Methods
        public void Save() { }
        public void Load()
        {
            var args = new LoadEventArgs(null);
        }
        public void Select(string refId) { }
        public void New() { }
        public void Edit() { }
        public void Remove() { }
        public void CommitChanges() { }
        public void CancelChanges() { }
        #endregion

        #region Fields

        private readonly object broker;
        private readonly object provider;
        #endregion
    }
}