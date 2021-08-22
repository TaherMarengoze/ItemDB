using System.Collections.Generic;

namespace CoreLibrary.Interfaces
{
    public interface IItemRawData
    {
        string BaseName { get; set; }
        string BrandListID { get; set; }
        bool BrandRequired { get; set; }
        string CatID { get; set; }
        string CatName { get; set; }
        List<string> CommonNames { get; set; }
        string Description { get; set; }
        string DisplayName { get; set; }
        string EndsListID { get; set; }
        bool EndsRequired { get; set; }
        List<string> ImagesNames { get; set; }
        string ItemID { get; set; }
        string SizeGroupID { get; set; }
        bool SizeRequired { get; set; }
        string SpecsID { get; set; }
        bool SpecsRequired { get; set; }
        string UoM { get; set; }
    }
}