
namespace Interfaces.Operations
{
    public interface IEntityRepo<TEntity>
    {
        void Create(TEntity entity);

        TEntity Read();

        void Update(string refId, TEntity entity);

        void Delete(string entityId);
    }
}