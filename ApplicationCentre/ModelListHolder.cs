
using Interfaces.Models;
using System.Collections.Generic;

namespace AppCore
{
    public class ModelListHolder
    {
        public List<IItem> Items { get; set; }

        public List<ISpecs> Specs { get; set; }

        public List<ISizeGroup> SizeGroups { get; set; }

        public List<IFieldList> SizeLists { get; set; }

        public List<IFieldList> BrandLists { get; set; }

        public List<IFieldList> EndLists { get; set; }

        public List<string> CustomSizes { get; set; }

        public List<string> CustomSpecs { get; set; }
    }
}