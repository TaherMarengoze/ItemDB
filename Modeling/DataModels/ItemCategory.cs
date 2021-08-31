
using Interfaces.Models;


namespace Modeling.DataModels
{
    public class ItemCategory : IItemCategory
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }
}