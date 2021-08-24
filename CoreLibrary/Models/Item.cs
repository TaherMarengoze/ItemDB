
using CoreLibrary.Interfaces;
using System.Collections.Generic;


namespace CoreLibrary.Models
{
    public class Item : IItem
    {
        public Item() { }

        public string CatID { get; set; }
        public string CatName { get; set; }
        public string ItemID { get; set; }
        public string BaseName { get; set; }
        public string DisplayName { get; set; }
        public List<string> CommonNames { get; set; }
        public string Description { get; set; }
        public List<string> ImagesFileName { get; set; }
        public ItemDetails Details { get; set; }// = new ItemDetails();
        public string UoM { get; set; }
    }
}
