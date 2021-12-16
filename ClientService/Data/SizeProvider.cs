using AppCore;
using ClientService.Contracts;
using Interfaces.Models;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Data
{
    public class SizeProvider : IProvider<IFieldList>
    {
        public List<IFieldList> GetList()
        {
            return CacheIO.GetSizeList();
        }

        public List<string> GetIDs() => Globals.DataLists.SizeIDs;

        public int Count => throw new NotImplementedException();

        public List<string> GetEntries(string listID)
        {
            System.Collections.ObjectModel.ObservableCollection<string> entries =
                (from list in CacheIO.GetSizeList()
                 where list.ID == listID
                 select list.List).FirstOrDefault();

            return entries?.ToList();
        }

        public List<IFieldList> GetListExcluded(IFieldList exclude)
        {
            List<IFieldList> exList = CacheIO.GetSizeList();
            exList.Remove(exclude);
            return exList;
        }

        public List<IFieldList> GetListExcluded(string excludeId)
        {
            List<IFieldList> exList = CacheIO.GetSizeList();
            exList.RemoveAll(item => item.ID == excludeId);
            return exList;
        }
    }
}