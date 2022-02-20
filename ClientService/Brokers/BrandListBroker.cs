using AppCore;
using Interfaces.Models;
using Interfaces.Operations;
using System;

namespace ClientService.Brokers
{
    public class BrandListBroker : IBroker<IFieldList>
    {
        private readonly IRepo<IFieldList> repos = Globals.brandsRepo;
        private readonly ModelListsCache cache = Globals.ModelCache;

        public void Create(IFieldList content)
            => repos.Create(content);

        public IFieldList Read(string entityId) =>
            cache.BrandLists.Find(entity => entity.ID == entityId);

        private IFieldList _Read(string entityId) => repos.Read(entityId);

        public void Update(string refId, IFieldList content) =>
            repos.Update(refId, content);

        public void Delete(string entityId) =>
            repos.Delete(entityId);
    }
}