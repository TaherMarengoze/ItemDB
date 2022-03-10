using System.Collections.Generic;
using ClientService.Contracts;

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
            throw new System.NotImplementedException();
        }

        public List<string> GetList()
        {
            return source;
        }
    }
}