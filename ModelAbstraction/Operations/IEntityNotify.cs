
namespace Interfaces.Operations
{
    public interface IEntityNotify
    {
        /// <summary>
        /// Occurs when this entity's repository is changed by any create, update or delete operation.
        /// </summary>
        event System.EventHandler OnChange;
    }
}