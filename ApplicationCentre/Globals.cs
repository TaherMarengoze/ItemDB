using Interfaces.Models;
using Interfaces.Operations;

namespace AppCore
{
    public static class Globals
    {
        public static ISourceContext context;

        public static IDataReader reader;

        public static IRepo<IItem> itemsRepo;
        public static IRepo<ISpecs> specsRepo;
        public static IRepo<ISizeGroup> sizeGroupRepo;
        public static IRepo<IFieldList> sizesRepo;
        public static IRepo<IFieldList> brandsRepo;
        public static IRepo<IFieldList> endsRepo;
        
        public static bool disableEditors = true;

        /// <summary>
        /// The global instance of the <see cref="ModelListsCache"/> class.
        /// </summary>
        public static ModelListsCache ModelCache { get; } = new ModelListsCache();

        public static DataListsCache DataLists { get; } = new DataListsCache();
    }
}