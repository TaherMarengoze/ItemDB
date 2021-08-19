using System.Collections.Generic;

namespace CoreLibrary.Models
{
    using Interfaces;

    public class ItemRawData : IItemRawData
    {
        public string ItemID { get; set; }
        public string CatID { get; set; }
        public string CatName { get; set; }
        public string BaseName { get; set; }
        public string DisplayName { get; set; }
        public List<string> CommonNames { get; set; }
        
        /// <summary>
        /// Gets or sets the list of file names of the item images without the file extension.
        /// </summary>
        public List<string> ImagesNames { get; set; }
        public string Description { get; set; }
        public string SpecsID { get; set; }
        public bool SpecsRequired { get; set; }
        public string SizeGroupID { get; set; }
        public bool SizeRequired { get; set; }
        public string BrandListID { get; set; }
        public bool BrandRequired { get; set; }
        public string EndsListID { get; set; }
        public bool EndsRequired { get; set; }
        public string UoM { get; set; }

    }
}