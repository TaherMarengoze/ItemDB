namespace CoreLibrary
{
    using Interfaces;

    public class AppFactory
    {
        public static Models.FilePathProcessor fpp;
        public static Models.XDataDocuments xDataDocs;

        public static ISourceReader reader;

        public static ISourceContext context;

        public static IModifier itemModifier;

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