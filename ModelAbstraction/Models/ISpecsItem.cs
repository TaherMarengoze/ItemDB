
using System.Collections.Generic;


namespace Interfaces.Models
{
    public interface ISpecsItem
    {
        int Index { get; set; }

         string Name { get; set; }

         string ValuePattern { get; set; }

         IEnumerable<ISpecListEntry> ListEntries { get; set; }

         string CustomInputID { get; set; }
    }
}