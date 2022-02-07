using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class RemoveEventArgs
    {
        public RemoveEventArgs(string removeRef, IList newList)
        {
            RemoveObject = removeRef;
            NewObjects = newList;
            Count = newList.Count;
        }

        public string RemoveObject { get; }

        public object NewObjects { get; }

        public int Count { get; }
    }
}