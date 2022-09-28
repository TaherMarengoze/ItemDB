using System.Collections.Generic;
using System.Linq;
using ClientService.Contracts;

using Interfaces.Operations;

namespace ClientService.Data
{
    public class ItemCommonNamesProvider : IProvider<string>
    {
        private readonly List<string> source;

        public ItemCommonNamesProvider()
        {

        }

        public ItemCommonNamesProvider(List<string> source)
        {
            this.source = source;
        }

        public int Count => throw new System.NotImplementedException();

        public List<string> GetIDs()
        {
            return source.ToList();
        }

        public List<string> GetList()
        {
            return source;
        }

        public List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, string>, new()
        {
            throw new System.NotImplementedException();
        }
    }
}