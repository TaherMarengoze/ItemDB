using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Interfaces
{
    public interface ISpecsModifier
    {
        void AddSpecs(Models.Specs specs);

        void ModifySpecs(string refId, Models.Specs specs);
    }
}
