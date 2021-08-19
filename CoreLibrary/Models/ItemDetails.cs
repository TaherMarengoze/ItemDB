namespace CoreLibrary.Models
{
    public class ItemDetails
    {
        public string SpecsID { get; set; }
        public string SizeGroupID { get; set; }
        public string BrandListID { get; set; }
        public string EndsListID { get; set; }

        public bool SpecsRequired { get; set; } = false;
        public bool SizeRequired { get; set; } = false;
        public bool BrandRequired { get; set; } = false;
        public bool EndsRequired { get; set; } = false;

        public override string ToString()
        {
            string joined = "";
            string separator = " | ";
            if (SpecsID != null)
            {
                joined = joined + $"{{Specs: {SpecsID},{SpecsRequired.ToString()}}}{separator}";
            }

            if (SizeGroupID != null)
            {
                joined = joined + $"{{Size: {SizeGroupID},{SizeRequired.ToString()}}}{separator}";
            }

            if (BrandListID != null)
            {
                joined = joined + $"{{Brand: {BrandListID},{BrandRequired.ToString()}}}{separator}";
            }

            if (EndsListID != null)
            {
                joined = joined + $"{{Ends: {EndsListID},{EndsRequired.ToString()}}}{separator}";
            }

            if (joined.Length > 3)
            {
                return joined.Remove(joined.Length - separator.Length);
            }
            return joined;
        }
    }
}
