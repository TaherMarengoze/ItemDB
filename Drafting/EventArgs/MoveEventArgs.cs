using CoreLibrary.Enums;
using System.Collections;

namespace Controllers
{
    public class MoveEventArgs
    {
        public MoveEventArgs(string moveRef, IList newList,
            ShiftDirection direction)
        {
            MoveObject = moveRef;
            NewObjects = newList;
            Count = newList.Count;
            Direction = direction;
        }

        public string MoveObject { get; }

        public object NewObjects { get; }

        public int Count { get; }

        public ShiftDirection Direction { get; }
    }
}