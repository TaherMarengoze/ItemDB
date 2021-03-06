using System.Collections.Generic;

namespace ClientService.Contracts
{
    public interface IProvider<T> : ITransformable<T>
    {
        List<T> GetList();

        List<string> GetIDs();

        int Count { get; }
    }
}