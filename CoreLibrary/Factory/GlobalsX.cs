
using CoreLibrary.Interfaces;


namespace CoreLibrary
{
    public class GlobalsX
    {
        public static Models.FilePathProcessor fpp;
        public static Models.XDataDocuments xDataDocs;

        public static ISourceContext context;

        public static ISourceReader reader;

        public static IItemRepo itemsRepo;

        public static ISpecsRepo specsRepo;

        public static ISizeGroupRepos sizeGroupRepo;

        public static IFieldRepos sizesRepo;
        public static IFieldManipulator sizeManipulator;

        public static IFieldRepos brandsRepo;
        public static IFieldManipulator brandManipulator;

        public static IFieldRepos endsRepo;
        public static IFieldManipulator endsManipulator;
    }
}