
using System.Collections.Generic;


namespace ModelAbstraction.Interfaces
{
    public interface ISpecsItem
    {
        int Index { get; set; }

         string Name { get; set; }

         string ValuePattern { get; set; }

         List<ISpecListEntry> ListEntries { get; set; }

         string CustomInputID { get; set; }
    }
}