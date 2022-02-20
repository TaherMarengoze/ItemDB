using System;
using CoreLibrary.Enums;

namespace Controllers
{
    public class StatusEventArgs : EventArgs
    {
        public StatusEventArgs(InputStatus status, object value)
        {
            Status = status;
            Value = value;
        }

        public InputStatus Status { get; }

        public object Value { get; }
    }
}
