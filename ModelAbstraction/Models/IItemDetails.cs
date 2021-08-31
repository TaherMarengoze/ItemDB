
namespace Interfaces.Models
{
    public interface IItemDetails
    {
        string SpecsID { get; set; }

        string SizeGroupID { get; set; }

        string BrandListID { get; set; }

        string EndsListID { get; set; }

        bool SpecsRequired { get; set; }

        bool SizeRequired { get; set; }

        bool BrandRequired { get; set; }

        bool EndsRequired { get; set; }
    }
}