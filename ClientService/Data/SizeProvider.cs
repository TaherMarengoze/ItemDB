using AppCore;
using ClientService.Contracts;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Data
{
    public class SizeProvider : IProvider<IFieldList>
    {
        public List<IFieldList> GetList() => throw new NotImplementedException();

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
    }
}