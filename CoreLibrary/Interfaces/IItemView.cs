namespace CoreLibrary.Interfaces
{
    public interface IItemView
    {
        string Category { get; set; }

        string CatID { get; set; }

        string ID { get; set; }

        int Images { get; set; }

        string Name { get; set; }

        string UoM { get; set; }
    }
}