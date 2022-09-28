using System.Collections.Generic;
using AppCore;
using ClientService.Contracts;
using Interfaces.Models;
using Interfaces.Operations;

namespace ClientService.Data
{
    public class ItemProvider : IProvider<IItem>
    {
        public int Count => GetIDs()?.Count ?? 0;

        public List<string> GetIDs()
        {
            return Globals.ModelCache.ItemsIDs;
        }

        public List<IItem> GetList()
        {
            return CacheIO.GetItemList();
        }

        public List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, IItem>, new()
        {
            //throw new System.NotImplementedException();
            TViewModel model = new TViewModel();
            return model.Transform(GetList());
        }
    }
}