using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlDataSource.Repository
{
    public class Entity : IEntityRepo<ISpecs>
    {
        public void Create(ISpecs group)
        {
            throw new NotImplementedException();
        }

        public void Delete(string groupId)
        {
            throw new NotImplementedException();
        }

        public ISpecs Read()
        {
            throw new NotImplementedException();
        }

        public void Update(string refId, ISpecs group)
        {
            throw new NotImplementedException();
        }
    }

    class MyClass
    {
        IEntityRepo<ISpecs> entity = new Entity();
    }
}
