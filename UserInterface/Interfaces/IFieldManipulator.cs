using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Interfaces
{
    using Enums;

    public interface IFieldManipulator
    {
        void AddEntry(string listId, string entry);

        void EditEntry(string listId, string oldValue, string newValue);

        void DeleteEntry(string listId, string entry);

        void MoveEntry(string listId, string entry, ShiftDirection direction);
    }
}