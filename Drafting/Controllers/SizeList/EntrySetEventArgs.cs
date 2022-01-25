using System;
using System.Collections;

namespace Controllers
{
    public class EntrySetEventArgs : EventArgs
    {
        public string OldItem { get; set; }

        public string NewItem { get; set; }
        
        public IList SetList { get; set; }
    }
}