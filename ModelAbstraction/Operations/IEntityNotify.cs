
namespace Interfaces.Operations
{
    public interface IEntityNotify
    {
        /// <summary>
        /// Occurs when this entity repository is changed by any add, update or delete operation.
        /// </summary>
        event System.EventHandler OnChange;
    }
}