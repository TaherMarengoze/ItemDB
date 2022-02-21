using System.Collections.Generic;
using ClientService.Contracts;
using Interfaces.Models;

namespace ClientService.Data
{
    public class ItemProvider : IProvider<IItem>
    {
        public int Count => throw new System.NotImplementedException();

        public List<string> GetIDs()
        {
            throw new System.NotImplementedException();
        }

        public List<IItem> GetList()
        {
            return CacheIO.GetItemList();
        }
    }
}