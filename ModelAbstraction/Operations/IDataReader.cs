
using Interfaces.Models;
using System.Collections.Generic;


namespace Interfaces.Operations
{
    /// <summary>
    /// Contains definitions for entities lists from a data source after deserialization.
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