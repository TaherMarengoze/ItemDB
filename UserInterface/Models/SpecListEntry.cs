namespace UserInterface.Models
{
    using Interfaces;

    public class SpecListEntry : ISpecListEntry
    {
        public int ValueID { get; set; }
        public string Value { get; set; }
        public string Display { get; set; }

        public ISpecListEntry CopyEntry()
        {
            return
                new SpecListEntry()
                {
                    ValueID = ValueID,
                    Value = Value,
                    Display = Display
                };
        }
    }

    
}
