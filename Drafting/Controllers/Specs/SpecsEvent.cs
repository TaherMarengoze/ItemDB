using Interfaces.Models;
using Modeling.DataModels;
using Modeling.ViewModels.Specs;
using System;
using System.Collections.Generic;

namespace Controllers.SpecsUi
{
    public class SpecsSetEventArgs : EventArgs
    {
        public string SetID { get; set; }

        public bool Existing { get; set; }
    }

    public class SpecsItemSetEventArgs : EventArgs
    {
        public int Index { get; set; }

        public List<GenericView> SpecsItems { get; set; }
    }

    public class SpecsItemRemoveEventArgs : EventArgs
    {
        public List<GenericView> SpecsItems { get; set; }
        public int Count => SpecsItems?.Count ?? 0;
    }

    public class SpecsItemCancelEventArgs : EventArgs
    {
        public bool NoItem { get; set; }

        public int Index { get; set; }

        public List<ISpecsItem> SpecsItems { get; set; }
    }

    public class ListEntryEventArgs : EventArgs
    {
        //public int EntryID { get; set; }
        public List<SpecListEntry> Entries { get; set; }
        public int Count => Entries?.Count ?? 0;
    }

}