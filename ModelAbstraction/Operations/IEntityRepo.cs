
namespace Interfaces.Operations
{
    public interface ICache<TEntity>
    {
        void Create(TEntity content);

        TEntity Read(string entityId);

        void Update(string refId, TEntity content);

        void Delete(string entityId);
    }

    /// <summary>
    /// Represents a repository that allows CRUD operation.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for the repository.</typeparam>
    public interface IRepo<TEntity> : IEntityNotify
    {
        void Create(TEntity content);

        TEntity Read(string entityId);

        void Update(string refId, TEntity content);

        void Delete(string entityId);
    }

    public interface IEntityNotify
    {
        /// <summary>
        /// Occurs when this entity repository is changed by any add, update or delete operation.
        /// </summary>
        event System.EventHandler OnChange;
    }
}