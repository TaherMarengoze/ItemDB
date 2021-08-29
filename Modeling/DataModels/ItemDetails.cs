
using ModelAbstraction.Interfaces;


namespace Modeling.DataModels
{
    public class ItemDetails : IItemDetails
    {
        public string SpecsID { get; set; }

        public string SizeGroupID { get; set; }

        public string BrandListID { get; set; }

        public string EndsListID { get; set; }

        public bool SpecsRequired { get; set; } = false;

        public bool SizeRequired { get; set; } = false;

        public bool BrandRequired { get; set; } = false;

        public bool EndsRequired { get; set; } = false;
    }
}