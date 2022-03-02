using System.Collections.Generic;
using AppCore;
using ClientService.Contracts;
using Interfaces.Models;

namespace ClientService.Data
{
    public class ItemProvider : IProvider<IItem>
    {
        public int Count => throw new System.NotImplementedException();

        public List<string> GetIDs()
        {
            return Globals.DataLists.ItemIDs;
        }

        public List<IItem> GetList()
        {
            return CacheIO.GetItemList();
        }
    }
}