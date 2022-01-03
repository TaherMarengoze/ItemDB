using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class SizeListSelectionEventArgs : EventArgs
    {
        public SizeList Selected { get; set; }
    }
}