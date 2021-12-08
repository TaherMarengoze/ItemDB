
using Interfaces.Models;
using System.Collections.Generic;


namespace Interfaces.Operations
{
    /// <summary>
    /// Contain methods definition for getting data from a data source
    /// </summary>
    public interface IDataReader
    {
        IEnumerable<IItem> GetItems();

        IEnumerable<IItemCategory> GetCategories();

        IEnumerable<ISpecs> GetSpecs();

        IEnumerable<ISizeGroup> GetSizeGroups();

        IEnumerable<IFieldList> GetSizes();

        IEnumerable<IFieldList> GetBrands();

        IEnumerable<IFieldList> GetEnds();

        IEnumerable<string> GetImageNames();

        IEnumerable<string> GetCustomSpecs();

        IEnumerable<string> GetCustomSizes();
    }
}