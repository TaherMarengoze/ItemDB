using AppCore;
using Interfaces.Operations;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    public class SizeListCache : ICache<IFieldList>
    {
        private readonly IRepo<IFieldList> repos = Globals.sizesRepo;

        public void Create(IFieldList content)
            => repos.Create(content);

        public IFieldList Read(string entityId) => throw new NotImplementedException();

        public void Update(string refId, IFieldList content) => throw new NotImplementedException();

        public void Delete(string entityId) => throw new NotImplementedException();
    }
}