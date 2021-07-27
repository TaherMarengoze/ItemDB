namespace UserInterface.Interfaces
{
    public interface ISpecsModifier
    {
        /// <summary>
        /// Adds a new specs to a data source
        /// </summary>
        /// <param name="specs"></param>
        void AddSpecs(ISpecs specs);

        /// <summary>
        /// Modifies an existing specs in the data source
        /// </summary>
        /// <param name="refId"></param>
        /// <param name="specs"></param>
        void ModifySpecs(string refId, ISpecs specs);

        /// <summary>
        /// Deletes a specs from its data source
        /// </summary>
        /// <param name="specsId">The delete specs ID</param>
        void DeleteSpecs(string specsId);
    }
}
