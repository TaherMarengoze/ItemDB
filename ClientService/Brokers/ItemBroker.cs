using AppCore;
using Interfaces.Models;
using Interfaces.Operations;

namespace ClientService.Brokers
{
    public class ItemBroker : IBroker<IItem>
    {
        private readonly IRepo<IItem> repos = Globals.itemsRepo;
        private readonly ModelListsCache cache = Globals.ModelCache;

        public void Create(IItem content)
            => repos.Create(content);

        //public IItem Read(string entityId) => repos.Read(entityId);
        public IItem Read(string entityId)
            => cache.Items.Find(entity => entity.ItemID == entityId);

        public void Update(string refId, IItem content)
            => repos.Update(refId, content);

        public void Delete(string entityId)
            => repos.Delete(entityId);
    }
}