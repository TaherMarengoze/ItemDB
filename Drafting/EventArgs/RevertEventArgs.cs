using System;
using System.Collections;

namespace Controllers
{
    public class RevertEventArgs : EventArgs
    {
        public RevertEventArgs(IList restore)
        {
            Restored = restore;
            Count = restore?.Count ?? 0;
        }

        public object Restored { get; }

        public int Count { get; }
    }
}