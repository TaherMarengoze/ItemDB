using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class LoadEventArgs : EventArgs
    {
        public LoadEventArgs(IList listObject)
        {
            GenericViewList = listObject;
            Count = listObject.Count;
        }

        public object GenericViewList { get; }

        public int Count { get; }
    }

    public class LoadEventArgs<TView> : EventArgs
    {
        public List<TView> GenericViewList { get; set; }

        public int ItemCount => GenericViewList.Count();
    }
}