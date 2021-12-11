using AppCore;
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    public class SizeGroupCache : ICache<ISizeGroup>
    {
        public void Create(ISizeGroup content)
        {
            Globals.sizeGroupRepo.Create(content);
            //CacheIO.UpdateSizeGroupList();
        }

        public ISizeGroup Read(string entityId)
        {
            return
                Globals.ModelCache.SizeGroups
                .Find(entity => entity.ID == entityId);
        }

        public void Update(string refId, ISizeGroup entity) => throw new NotImplementedException();

        public void Delete(string entityId) => throw new NotImplementedException();
    }
}
