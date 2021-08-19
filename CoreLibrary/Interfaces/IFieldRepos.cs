namespace CoreLibrary.Interfaces
{
    public interface IFieldRepos
    {
        void AddList(IBasicList list);

        IBasicList GetList(string listId);

        void UpdateList(string refId, IBasicList list);

        /// <summary>
        /// Deletes a field list from its data source.
        /// </summary>
        /// <param name="listId">The ID of the field list to be deleted.</param>
        void DeleteList(string listId);
    }
}
