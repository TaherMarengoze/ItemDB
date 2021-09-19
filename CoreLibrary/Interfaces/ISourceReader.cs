
using System.Collections.Generic;
using CoreLibrary.Models;


namespace CoreLibrary.Interfaces
{
    public interface ISourceReader
    {
        IEnumerable<IItem> GetItems();

        IEnumerable<ItemCategory> GetCategories();

        //IEnumerable<ISpecs> GetSpecs();

        IEnumerable<ISizeGroup> GetSizeGroups();

        IEnumerable<IBasicList> GetSizes();

        IEnumerable<IBasicList> GetBrands();

        IEnumerable<IBasicList> GetEnds();

        IEnumerable<string> GetImageNames();

        IEnumerable<string> GetCustomSpecs();

        IEnumerable<string> GetCustomSizes();

    }
}