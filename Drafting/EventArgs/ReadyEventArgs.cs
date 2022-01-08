using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ReadyEventArgs : EventArgs
    {
        public bool Ready { get; set; }

        public string Info { get; set; }
    }
}
