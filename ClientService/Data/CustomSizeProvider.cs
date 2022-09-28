using AppCore;
using ClientService.Contracts;

using Interfaces.Operations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Data
{
    public class CustomSizeProvider : IProvider<string>
    {
        public List<string> GetList()
        {
            throw new NotImplementedException();
        }

        public List<string> GetIDs()
        {
            return Globals.ModelCache.CustomSizesIDs;
        }

        public List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, string>, new()
        {
            throw new NotImplementedException();
        }

        public int Count => throw new NotImplementedException();
    }
}