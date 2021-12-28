
using Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace Modeling.DataModels
{
    public class SpecsItem : ISpecsItem
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public string ValuePattern { get; set; } = "{val}";

        public IEnumerable<ISpecListEntry> ListEntries { get;  set; }

        public string CustomInputID { get;  set; }
    }
}