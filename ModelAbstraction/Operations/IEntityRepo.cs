using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Operations
{
    public interface IEntityRepo<TEntity>
    {
        void Create(TEntity group);

        TEntity Read();

        void Update(string refId, TEntity group);

        void Delete(string groupId);
    }
}