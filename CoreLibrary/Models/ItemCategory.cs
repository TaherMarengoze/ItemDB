using CoreLibrary.Interfaces;

namespace CoreLibrary.Models
{
    public class ItemCategory : IBasicField
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
