using System;
using CoreLibrary.Enums;

namespace Controllers
{
    public class StatusEventArgs : EventArgs
    {
        public StatusEventArgs(InputStatus status, string value)
        {
            Status = status;
            Value = value;
        }

        public InputStatus Status { get; }

        public string Value { get; }
    }
}
