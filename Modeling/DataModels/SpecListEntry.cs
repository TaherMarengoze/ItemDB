
using ModelAbstraction.Interfaces;


namespace Modeling.DataModels
{
    public class SpecListEntry : ISpecListEntry
    {
        public int ValueID { get; set; }
        public string Value { get; set; }
        public string Display { get; set; }
    }
}