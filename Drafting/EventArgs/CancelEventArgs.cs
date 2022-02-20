using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class CancelEventArgs : EventArgs
    {
        public CancelEventArgs(string restore, IList restoreList)
        {
            Restore = restore;
            List = restoreList;
            Count = restoreList?.Count ?? 0;
            EmptyList = (restoreList?.Count ?? 0) < 1;
        }

        public string Restore { get; }

        public object List { get; }

        public int Count { get; }

        public bool EmptyList { get; set; }
    }
}