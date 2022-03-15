using System.Collections.Generic;
using System.Linq;
using ClientService.Contracts;

namespace ClientService.Data
{
    public class ItemImageNamesProvider : IProvider<string>
    {
        private readonly List<string> source;

        public ItemImageNamesProvider()
        {

        }

        public ItemImageNamesProvider(List<string> source)
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
    }
}