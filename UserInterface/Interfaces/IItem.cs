using System.Collections.Generic;
using UserInterface.Models;

namespace UserInterface.Interfaces
{
    public interface IItem
    {
        string BaseName { get; set; }
        string CatID { get; set; }
        string CatName { get; set; }
        List<string> CommonNames { get; set; }
        string Description { get; set; }
        ItemDetails Details { get; set; }
        string DisplayName { get; set; }
        List<string> ImagesFileName { get; set; }
        string ItemID { get; set; }
        string UoM { get; set; }
    }
}