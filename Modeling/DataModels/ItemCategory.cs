
using Interfaces.Models;


namespace Modeling.DataModels
{
    public class ItemCategory : IItemCategory
    {
        public ItemCategory()
        {

        }

        public ItemCategory(string id, string name)
        {
            CatID = id;
            CatName = name;
        }

        public string CatID { get; set; }

        public string CatName { get; set; }
    }
}