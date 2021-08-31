
using Interfaces.Models;
using System.Collections.Generic;


namespace Modeling.DataModels
{
    public class Item : IItem
    {
        public string CatID { get; set; }

        public string CatName { get; set; }

        public string ItemID { get; set; }

        public string BaseName { get; set; }

        public string DisplayName { get; set; }

        public List<string> CommonNames { get; set; }

        public string Description { get; set; }

        public List<string> ImagesFileName { get; set; }

        public IItemDetails Details { get; set; }

        public string UoM { get; set; }
    }
}