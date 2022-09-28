using Interfaces.Models;
using Interfaces.Operations;

namespace AppCore
{
    public static class Globals
    {
        public static ISourceContext context;

        public static IDataReader reader;

        public static IRepo<IItem> itemsRepo;
        public static IRepo<IItemCategory> categoryRepo;
        public static IRepo<ISpecs> specsRepo;
        public static IRepo<ISizeGroup> sizeGroupRepo;
        public static IRepo<IFieldList> sizesRepo;
        public static IRepo<IFieldList> brandsRepo;
        public static IRepo<IFieldList> endsRepo;
        
        public static bool disableEditors = true;

        /// <summary>
        /// Returns a global instance of <see cref="ModelsDataCache"/>; which
        /// contains cached collections of the application's domain models.
        /// </summary>
        public static ModelsDataCache ModelCache { get; } = new ModelsDataCache();
    }
}