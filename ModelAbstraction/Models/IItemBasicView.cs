using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces.Operations;

namespace Interfaces.Models
{
    public interface IItemBasicView
    {
        string ID { get; set; }

        string Name { get; set; }
    }
}
