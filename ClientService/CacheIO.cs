
using AppCore;
using Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for getting lists of entities from the reader and the global cache, and allows updating of this cache.
    /// </summary>
    public static class CacheIO
    {
        public static void InitLists()
        {
            //Globals.ModelLists = new ModelListsCache();

            // Register events for DataProvider class
            DataProvider.RegisterModelListsEvents();

            // Update list values
            UpdateAllLists();
        }

        public static List<IItem> GetItemList()
        {
            return Globals.ModelLists.Items;
        }

        internal static List<ISpecs> GetSpecsList()
        {
            return Globals.ModelLists.Specs;
        }

        public static List<ISizeGroup> GetSizeGroupList()
        {
            return Globals.ModelLists.SizeGroups;
        }

        public static List<IFieldList> GetSizeList()
        {
            return Globals.ModelLists.SizeLists;
        }

        public static List<IFieldList> GetBrandList()
        {
            return Globals.ModelLists.BrandLists;
        }

        public static List<IFieldList> GetEndList()
        {
            return Globals.ModelLists.EndLists;
        }

        public static List<string> GetCustomSpecsList()
        {
            return Globals.ModelLists.CustomSpecs;
        }

        public static List<string> GetCustomSizeList()
        {
            return Globals.ModelLists.CustomSizes;
        }

        private static void UpdateAllLists()
        {
            UpdateItemList();
            UpdateSpecsList();
            UpdateSizeGroupList();
            UpdateSizesList();
            UpdateBrandsList();
            UpdateEndsList();
            UpdateCustomSpecsList();
            UpdateCustomSizesList();
        }

        private static void UpdateItemList()
        {
            Globals.ModelLists.Items =
                Globals.reader.GetItems().ToList();
        }

        internal static void UpdateSpecsList()
        {
            // this will run a query against the data source
            Globals.ModelLists.Specs =
                Globals.reader.GetSpecs().ToList();
        }

        private static void UpdateSizeGroupList()
        {
            Globals.ModelLists.SizeGroups =
                Globals.reader.GetSizeGroups().ToList();
        }

        private static void UpdateSizesList()
        {
            Globals.ModelLists.SizeLists =
                Globals.reader.GetSizes().ToList();
        }

        private static void UpdateBrandsList()
        {
            Globals.ModelLists.BrandLists = Globals.reader.GetBrands().ToList();
        }

        private static void UpdateEndsList()
        {
            Globals.ModelLists.EndLists = Globals.reader.GetEnds().ToList();
        }

        private static void UpdateCustomSpecsList()
        {
            Globals.ModelLists.CustomSpecs = Globals.reader.GetCustomSpecs().ToList();
        }

        private static void UpdateCustomSizesList()
        {
            Globals.ModelLists.CustomSizes = Globals.reader.GetCustomSizes().ToList();
        }
    }
}