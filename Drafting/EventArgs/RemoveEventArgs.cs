using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class RemoveEventArgs
    {
        public string RemoveID { get; set; }

        public object NewList { get; set; }

        public int Count { get; set; }
    }
}