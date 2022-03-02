using Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Controllers
{
    public class PreDraftingEventArgs : EventArgs
    {
        public List<string> PreList { get; set; }

        public IFieldList DraftObject { get; set; }
    }
}