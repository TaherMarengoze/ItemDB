
namespace UserInterface.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface ISourceReader
    {
        IEnumerable<IItem> GetItems();

        IEnumerable<ItemCategory> GetCategories();

        IEnumerable<Specs> GetSpecs();

        IEnumerable<SizeGroup> GetSizeGroups();

        IEnumerable<BasicListView> GetSizes();

        IEnumerable<BasicListView> GetBrands();

        IEnumerable<BasicListView> GetEnds();

        IEnumerable<string> GetImageNames();
    }
}