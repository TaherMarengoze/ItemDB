
namespace Interfaces.Operations
{
    public interface ICache<TEntity>
    {
        void Create(TEntity content);

        TEntity Read(string entityId);

        void Update(string refId, TEntity content);

        void Delete(string entityId);
    }

    public interface IRepo<TEntity> : IEntityNotify
    {
        void Create(TEntity content);

        TEntity Read(string entityId);

        void Update(string refId, TEntity content);

        void Delete(string entityId);
    }

    public interface IEntityNotify
    {
        event System.EventHandler OnChange;
    }
}