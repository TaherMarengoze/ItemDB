namespace CoreLibrary.Interfaces
{
    public interface ISpecListEntry
    {
        string Display { get; set; }
        string Value { get; set; }
        int ValueID { get; set; }

        ISpecListEntry CopyEntry();
    }
}