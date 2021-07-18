namespace UserInterface.Models
{
    public class SpecListEntry
    {
        public int ValueID { get; set; }
        public string Value { get; set; }
        public string Display { get; set; }

        public SpecListEntry CopyEntry()
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
