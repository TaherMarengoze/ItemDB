
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
            // Update list values
            UpdateAllLists();
        }

        public static List<IItem> GetItemList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.Items.ToList();
        }

        internal static List<ISpecs> GetSpecsList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.Specs.ToList();
        }

        public static List<ISizeGroup> GetSizeGroupList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.SizeGroups.ToList();
        }

        public static List<IFieldList> GetSizeList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.SizeLists.ToList();
        }

        public static List<IFieldList> GetBrandList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.BrandLists.ToList();
        }

        public static List<IFieldList> GetEndList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.EndLists.ToList();
        }

        public static List<string> GetCustomSpecsList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.CustomSpecs.ToList();
        }

        public static List<string> GetCustomSizeList()
        {
            // .ToList will return a copy preserving the one in the cache
            return Globals.ModelLists.CustomSizes.ToList();
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

        internal static void UpdateSizeGroupList()
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
            Globals.ModelLists.BrandLists =
                Globals.reader.GetBrands().ToList();
        }

        private static void UpdateEndsList()
        {
            Globals.ModelLists.EndLists =
                Globals.reader.GetEnds().ToList();
        }

        private static void UpdateCustomSpecsList()
        {
            Globals.ModelLists.CustomSpecs =
                Globals.reader.GetCustomSpecs().ToList();
        }

        private static void UpdateCustomSizesList()
        {
            Globals.ModelLists.CustomSizes =
                Globals.reader.GetCustomSizes().ToList();
        }
    }
}