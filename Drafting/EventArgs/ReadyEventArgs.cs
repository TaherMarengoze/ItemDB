using System;

namespace Controllers
{
    public class ReadyEventArgs : EventArgs
    {
        public ReadyEventArgs(bool valid, bool changed)
        {
            Ready = valid && changed;
            Info = valid ? (changed ? "Ready" : "Unchanged") : "Not Ready";
        }

        public ReadyEventArgs(bool ready, string info = "")
        {
            Ready = ready;
            Info = info;
        }

        public bool Ready { get; }

        public string Info { get; }
    }
}