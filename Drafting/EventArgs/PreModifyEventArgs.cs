using System;
using System.Collections;

namespace Controllers
{
    public class PreModifyEventArgs : EventArgs
    {
        public PreModifyEventArgs(object draft, IList list)
        {
            Draft = draft;
            List = list;
        }

        public object Draft { get; }

        public IList List { get; }
    }
}