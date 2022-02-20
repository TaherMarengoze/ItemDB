using System.Collections.Generic;
using System.Linq;

using AppCore;

using ClientService.Contracts;

using Interfaces.Models;

namespace ClientService.Data
{
    public class EndProvider : IProvider<IFieldList>
    {
        public List<IFieldList> GetList()
        {
            return CacheIO.GetEndList();
        }

        public List<string> GetIDs() => Globals.DataLists.EndIDs;

        public int Count => GetIDs()?.Count ?? 0;

        public List<string> GetEntries(string listID)
        {
            System.Collections.ObjectModel.ObservableCollection<string> entries =
                (from list in CacheIO.GetEndList()
                 where list.ID == listID
                 select list.List).FirstOrDefault();

            return entries?.ToList();
        }

        public List<IFieldList> GetListExcluded(IFieldList exclude)
        {
            List<IFieldList> exList = CacheIO.GetEndList();
            exList.Remove(exclude);
            return exList;
        }

        public List<IFieldList> GetListExcluded(string excludeId)
        {
            List<IFieldList> exList = CacheIO.GetEndList();
            exList.RemoveAll(item => item.ID == excludeId);
            return exList;
        }
    }
}