using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.General
{
    public interface IDraftable
    {
        void Save();
        void CommitChanges();
    }
}
