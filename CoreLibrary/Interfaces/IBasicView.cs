namespace CoreLibrary.Interfaces
{
    public interface IBasicView
    {
        string ID { get; }
        string Name { get; }

        string ToString();
    }
}