using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Interfaces
{
    public interface IModifier
    {
        void AddItem(IItem item);

        void ModifyItem(string existingId, IItemRawData data);
    }
}
