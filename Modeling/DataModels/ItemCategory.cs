
using ModelAbstraction.Interfaces;


namespace Modeling.DataModels
{
    public class ItemCategory : IIdentity
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }
}