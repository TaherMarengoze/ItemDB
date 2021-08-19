using System.Collections.Generic;

namespace CoreLibrary.Interfaces
{
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

        IEnumerable<string> GetCustomSpecs();

        IEnumerable<string> GetCustomSizes();

    }
}