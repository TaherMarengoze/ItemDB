﻿
namespace Interfaces.Operations
{
    public interface ICache<TEntity>
    {
        void Create(TEntity content);

        TEntity Read(string entityId);

        void Update(string refId, TEntity content);

        void Delete(string entityId);
    }
}