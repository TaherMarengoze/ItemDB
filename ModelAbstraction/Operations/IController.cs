namespace Interfaces.Operations
{
    public interface IController
    {
        void Save();

        void Select();

        void New();

        void Edit(string refId);

        void Remove(string refId);

        void CommitChanges();

        void CancelChanges();
    }
}