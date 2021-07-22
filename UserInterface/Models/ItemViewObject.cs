
using UserInterface.Interfaces;


namespace UserInterface.Models
{
    /// <summary>
    /// View Object for <see cref="Item"/> class.
    /// </summary>
    public class ItemVO : IItemView
    {
        public ItemVO(IItem item)
        {
            ID = item.ItemID;
            Name = item.DisplayName;

            Images = item.ImagesFileName != null ? item.ImagesFileName.Count : 0;
            UoM = item.UoM;

            SpecsID = $"{ item.Details.SpecsID }{ Required(item.Details.SpecsRequired) }";
            SizeGroupID = $"{ item.Details.SizeGroupID }{ Required(item.Details.SizeRequired) }";
            BrandsID = $"{ item.Details.BrandListID }{ Required(item.Details.BrandRequired) }";
            EndsID = $"{ item.Details.EndsListID }{ Required(item.Details.EndsRequired) }";

            CatID = item.CatID;
            Category = item.CatName;
        }

        public string ID { get; set; }
        public string Name { get; set; }

        public int Images { get; set; }
        public string UoM { get; set; }

        public string SpecsID { get; private set; }
        public string SizeGroupID { get; private set; }
        public string BrandsID { get; private set; }
        public string EndsID { get; private set; }

        public string CatID { get; set; }
        public string Category { get; set; }

        private string Required(bool fieldRequired)
        {
            return fieldRequired ? "*" : "";
        }
    }
}
