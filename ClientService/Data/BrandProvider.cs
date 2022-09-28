using System.Collections.Generic;
using System.Linq;

using AppCore;

using ClientService.Contracts;

using Interfaces.Models;
using Interfaces.Operations;

namespace ClientService.Data
{
    public class BrandProvider : IProvider<IFieldList>
    {
        public List<IFieldList> GetList()
        {
            return CacheIO.GetBrandList();
        }

        public List<string> GetIDs() => Globals.ModelCache.BrandListsIDs;

        public int Count => GetIDs()?.Count ?? 0;

        public List<string> GetEntries(string listID)
        {
            System.Collections.ObjectModel.ObservableCollection<string> entries =
                (from list in CacheIO.GetBrandList()
                 where list.ID == listID
                 select list.List).FirstOrDefault();

            return entries?.ToList();
        }

        public List<IFieldList> GetListExcluded(IFieldList exclude)
        {
            List<IFieldList> exList = CacheIO.GetBrandList();
            exList.Remove(exclude);
            return exList;
        }

        public List<IFieldList> GetListExcluded(string excludeId)
        {
            List<IFieldList> exList = CacheIO.GetBrandList();
            exList.RemoveAll(item => item.ID == excludeId);
            return exList;
        }

        public List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, IFieldList>, new()
        {
            throw new System.NotImplementedException();
        }
    }
}