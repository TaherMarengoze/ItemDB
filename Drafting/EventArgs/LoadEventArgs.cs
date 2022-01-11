using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class LoadEventArgs : EventArgs
    {
        public object GenericViewList { get; set; }

        public int Count { get; set; }
    }

    public class LoadEventArgs<TView> : EventArgs
    {
        public IEnumerable<TView> GenericViewList { get; set; }

        public int ItemCount => GenericViewList.Count();
    }
}