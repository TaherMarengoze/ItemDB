using System.Collections.Generic;

namespace Interfaces.Operations.Info
{
    public interface IProvider<T>
    {
        List<T> GetList();

        List<string> GetIDs();

        int Count { get; }
    }
}