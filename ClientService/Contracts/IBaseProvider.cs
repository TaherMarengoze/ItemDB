using System.Collections.Generic;

namespace ClientService.Contracts
{
    public interface IBaseProvider<T>
    {
        /// <summary>
        /// Gets the number of elements of type <typeparamref name="T"/> contained in the collection.
        /// </summary>
        int Count { get; }

        List<string> GetIDs();

        List<T> GetList();
    }
}