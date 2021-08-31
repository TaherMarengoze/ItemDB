
using System.Collections.Generic;


namespace Interfaces.Models
{
    public interface IItem
    {
        string CatID { get; set; }

        string CatName { get; set; }

        string ItemID { get; set; }

        string BaseName { get; set; }

        string DisplayName { get; set; }

        List<string> CommonNames { get; set; }

        string Description { get; set; }

        List<string> ImagesFileName { get; set; }

        IItemDetails Details { get; set; }

        string UoM { get; set; }
    }
}