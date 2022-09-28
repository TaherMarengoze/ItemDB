using AppCore;
using ClientService.Contracts;
using Interfaces.Models;
using Interfaces.Operations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Data
{
    public class CategoryProvider : IProvider<IItemCategory>
    {
        public int Count => throw new NotImplementedException();

        public List<string> GetIDs()
        {
            return Globals.ModelCache.CategoriesIDs;
        }

        public List<IItemCategory> GetList()
        {
            return CacheIO.GetItemCategories();
        }

        public List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, IItemCategory>, new()
        {
            throw new System.NotImplementedException();
            //TViewModel adapter = new TViewModel();
            //return adapter.Transform(GetList());
        }
    }
}
