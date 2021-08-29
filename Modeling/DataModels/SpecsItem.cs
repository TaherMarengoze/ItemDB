
using ModelAbstraction.Interfaces;
using System.Collections.Generic;


namespace Modeling.DataModels
{
    public class SpecsItem : ISpecsItem
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public string ValuePattern { get; set; } = "{val}";

        public List<ISpecListEntry> ListEntries { get;  set; }

        public string CustomInputID { get;  set; }
        

    }
}