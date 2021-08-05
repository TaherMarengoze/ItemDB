using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Interfaces
{
    public interface IFieldManipulator
    {
        void AddEntry(string listId, string entry);

        void DeleteEntry(string listId, string entry);
    }
}
