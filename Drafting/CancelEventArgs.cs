using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class CancelEventArgs : EventArgs
    {
        public string RestoreID { get; set; }

        public bool EmptyList { get; set; }
    }
}
