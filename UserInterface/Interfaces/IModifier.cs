
namespace UserInterface.Interfaces
{
    public interface IModifier
    {
        void AddItem(IItem item);

        void ModifyItem(string refId, IItem data);

        /// <summary>
        /// Deletes an item from its data source
        /// </summary>
        /// <param name="itemId">The delete item ID</param>
        void DeleteItem(string itemId);
    }
}
