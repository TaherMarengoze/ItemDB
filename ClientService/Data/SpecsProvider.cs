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
    public class SpecsProvider : IProvider<ISpecs>
    {
        public List<ISpecs> GetList() => throw new NotImplementedException();

        public List<string> GetIDs() => Globals.ModelCache.SpecsIDs;

        public int Count => GetIDs().Count;

        public List<string> FilterSpecsIds(string exactId) => Globals.ModelCache.SpecsIDs.Where(id => id.Contains(exactId)).ToList();

        public List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, ISpecs>, new()
        {
            throw new NotImplementedException();
        }
    }
}