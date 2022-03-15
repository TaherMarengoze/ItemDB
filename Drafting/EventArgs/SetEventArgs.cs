using System.Collections;

namespace Controllers
{
    public class SetEventArgs
    {
        public SetEventArgs(string newId, string oldId, IList newList)
        {
            OldID = oldId;
            NewID = newId;
            NewList = newList;
        }

        public string OldID { get; set; }

        public string NewID { get; set; }

        public IList NewList { get; set; }

        public int Count => NewList.Count;
    }
}