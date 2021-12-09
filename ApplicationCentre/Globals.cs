
using Interfaces.Models;
using Interfaces.Operations;

namespace AppCore
{
    public static class Globals
    {
        public static ISourceContext context;

        public static IDataReader reader;

        public static IEntityRepo<IItem> itemsRepo;
        public static IEntityRepo<ISpecs> specsRepo;
        public static IEntityRepo<ISizeGroup> sizeGroupRepo;
        public static IEntityRepo<IFieldList> sizesRepo;
        public static IEntityRepo<IFieldList> brandsRepo;
        public static IEntityRepo<IFieldList> endsRepo;
        
        public static bool disableEditors = true;

        public static ModelListsCache ModelCache { get; } = new ModelListsCache();

        public static DataListsCache DataLists { get; } = new DataListsCache();
    }
}