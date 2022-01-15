using System;

namespace Controllers
{
    public class EntrySetEventArgs : EventArgs
    {
        public string OldItem { get; set; }

        public string NewItem { get; set; }
    }
}