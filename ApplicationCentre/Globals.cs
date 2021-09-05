using Interfaces.Models;
using Interfaces.Operations;
using XmlDataSorce;

namespace AppCore
{
    public static class Globals
    {
        public static FilePathProcessor fpp;
        public static DataDocuments xDataDocs;

        public static IDataReader reader;

        public static ISourceContext context;

        public static IItem items;

        public static ISpecs specsRepo;

        public static ISizeGroup sizeGroupRepo;

        public static IFieldList sizes;
        //public static IFieldManipulator sizeManipulator;

        public static IFieldList brands;
        //public static IFieldManipulator brandManipulator;

        public static IFieldList ends;
        //public static IFieldManipulator endsManipulator;
    }
}