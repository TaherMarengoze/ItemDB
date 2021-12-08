using System.Collections.Generic;

namespace ClientService.Contracts
{
    public interface IProvider<T>
    {
        List<T> GetList();

        List<string> GetIDs();
    }
}
