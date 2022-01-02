using AppCore;
using Interfaces.Operations;
using Interfaces.Models;
using System;

namespace ClientService.Brokers
{
    public class SizeListBroker : IBroker<IFieldList>
    {
        private readonly IRepo<IFieldList> repos = Globals.sizesRepo;

        public void Create(IFieldList content)
            => repos.Create(content);

        public IFieldList Read(string entityId)
            => repos.Read(entityId);

        public void Update(string refId, IFieldList content) =>
            repos.Update(refId, content);

        public void Delete(string entityId) =>
            repos.Delete(entityId);
    }
}