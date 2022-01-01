using AppCore;
using Interfaces.Models;
using Interfaces.Operations;

namespace ClientService.Brokers
{
    public class SizeGroupBroker : IBroker<ISizeGroup>
    {
        private readonly IRepo<ISizeGroup> repos = Globals.sizeGroupRepo;

        public void Create(ISizeGroup content)
            => repos.Create(content);

        public ISizeGroup Read(string entityId)
            => Globals.ModelCache.SizeGroups
            .Find(entity => entity.ID == entityId);

        public void Update(string refId, ISizeGroup entity)
            => repos.Update(refId, entity);

        public void Delete(string entityId)
            => repos.Delete(entityId);
    }
}