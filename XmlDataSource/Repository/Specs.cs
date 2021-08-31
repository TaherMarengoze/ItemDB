
using Interfaces.Models;
using Interfaces.Operations;
using System;


namespace Modeling
{
    public class Specs<ISpecs> : IEntityRepo<ISpecs>
    {
        public void Create(ISpecs specs) => throw new NotImplementedException();

        public ISpecs Read() => throw new NotImplementedException();

        public void Update(string refId, ISpecs specs) => throw new NotImplementedException();

        public void Delete(string specsId) => throw new NotImplementedException();
    }
}