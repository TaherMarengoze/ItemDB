namespace UserInterface.Interfaces
{
    /// <summary>
    /// Provides CRUD operations for <see cref="Models.Specs"/> field.
    /// </summary>
    public interface ISpecsRepo
    {
        /// <summary>
        /// Adds a new specs to a data source.
        /// </summary>
        /// <param name="specs">The specs to be added object data.</param>
        void CreateSpecs(ISpecs specs);

        /// <summary>
        /// Gets a specs from a data source.
        /// </summary>
        /// <param name="specsId">The ID of the specs to get.</param>
        ISpecs ReadSpecs(string specsId);

        /// <summary>
        /// Modifies an existing specs in the data source.
        /// </summary>
        /// <param name="refId">The previous ID of the specs to be replaced.</param>
        /// <param name="specs">The specs to be updated object data.</param>
        void UpdateSpecs(string refId, ISpecs specs);

        /// <summary>
        /// Deletes a specs from its data source.
        /// </summary>
        /// <param name="specsId">The delete specs ID.</param>
        void DeleteSpecs(string specsId);
    }
}
