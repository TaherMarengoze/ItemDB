namespace Interfaces.Operations
{
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
}