using System;
using System.Collections;

namespace Controllers
{
    public class EntrySetEventArgs : EventArgs
    {
        public EntrySetEventArgs(string newVal, string oldVal, IList newList)
        {
            NewItem = newVal;
            OldItem = oldVal;
            SetList = newList;
        }

        public string OldItem { get; }

        public string NewItem { get; }
        
        public IList SetList { get; }
    }
}