namespace CoreLibrary.Interfaces
{
    public interface ISpecListEntry
    {
        int ValueID { get; set; }
        string Display { get; set; }
        string Value { get; set; }

        ISpecListEntry CopyEntry();
    }
}