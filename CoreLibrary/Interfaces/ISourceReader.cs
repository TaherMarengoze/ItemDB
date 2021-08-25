
using System.Collections.Generic;
using CoreLibrary.Models;


namespace CoreLibrary.Interfaces
{
    public interface ISourceReader
    {
        IEnumerable<IItem> GetItems();

        IEnumerable<ItemCategory> GetCategories();

        IEnumerable<ISpecs> GetSpecs();

        IEnumerable<SizeGroup> GetSizeGroups();

        IEnumerable<BasicListView> GetSizes();

        IEnumerable<BasicListView> GetBrands();

        IEnumerable<BasicListView> GetEnds();

        IEnumerable<string> GetImageNames();

        IEnumerable<string> GetCustomSpecs();

        IEnumerable<string> GetCustomSizes();

    }
}