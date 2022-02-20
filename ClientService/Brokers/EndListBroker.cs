using System;

using AppCore;

using Interfaces.Models;
using Interfaces.Operations;

namespace ClientService.Brokers
{
    public class EndListBroker : IBroker<IFieldList>
    {
        private readonly IRepo<IFieldList> repos = Globals.endsRepo;
        private readonly ModelListsCache cache = Globals.ModelCache;

        public void Create(IFieldList content)
            => repos.Create(content);

        public IFieldList Read(string entityId) =>
            cache.EndLists.Find(entity => entity.ID == entityId);

        private IFieldList _Read(string entityId) => repos.Read(entityId);

        public void Update(string refId, IFieldList content) =>
            repos.Update(refId, content);

        public void Delete(string entityId) =>
            repos.Delete(entityId);
    }
}