namespace Interfaces.Operations
{
    public interface IController
    {
        void Save();

        void Load();

        void Select(string refId);

        void New();

        void Edit();

        void Remove();

        void CommitChanges();

        void CancelChanges();
    }
}