namespace CoreLibrary.Interfaces
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