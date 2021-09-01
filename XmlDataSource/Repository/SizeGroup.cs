
using Interfaces.Models;
using Interfaces.Operations;
using System;

namespace XmlDataSource.Repository
{
    public class SizeGroup : IEntityRepo<ISizeGroup>
    {
        public void Create(ISizeGroup entity) => throw new NotImplementedException();

        public ISizeGroup Read() => throw new NotImplementedException();

        public void Update(string refId, ISizeGroup entity) => throw new NotImplementedException();

        public void Delete(string entityId) => throw new NotImplementedException();
    }
}