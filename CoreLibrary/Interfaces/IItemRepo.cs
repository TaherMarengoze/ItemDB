
namespace CoreLibrary.Interfaces
{
    public interface IItemRepo
    {
        void CreateItem(IItem item);

        void UpdateItem(string refId, IItem data);

        /// <summary>
        /// Deletes an item from its data source
        /// </summary>
        /// <param name="itemId">The delete item ID</param>
        void DeleteItem(string itemId);
    }
}
