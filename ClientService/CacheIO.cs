
using AppCore;
using Interfaces.Models;
using Interfaces.Operations;
using System.Collections.Generic;
using System.Linq;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for getting lists of entities from the reader and the global cache, and allows updating of this cache.
    /// </summary>
    public static class CacheIO
    {
        static ModelListsCache cache = Globals.ModelCache;
        static IDataReader reader = Globals.reader;

        public static void InitLists()
        {
            Globals.sizeGroupRepo.OnChange += CacheIO_OnChange_SizeGroup;
            // Update list values
            UpdateAllLists();
        }

        private static void CacheIO_OnChange_SizeGroup(object sender, System.EventArgs e)
        {
            UpdateSizeGroupList();
            //cache.SizeGroups = reader.GetSizeGroups().ToList();
        }

        // using .ToList will return a copy of the list
        // preserving the one in the cache

        public static List<IItem> GetItemList() => cache.Items.ToList();

        internal static List<ISpecs> GetSpecsList() => cache.Specs.ToList();

        public static List<ISizeGroup> GetSizeGroupList() => cache.SizeGroups.ToList();

        public static List<IFieldList> GetSizeList() => cache.SizeLists.ToList();

        public static List<IFieldList> GetBrandList() => cache.BrandLists.ToList();

        public static List<IFieldList> GetEndList() => cache.EndLists.ToList();

        public static List<string> GetCustomSpecsList() => cache.CustomSpecs.ToList();

        public static List<string> GetCustomSizeList() => Globals.ModelCache.CustomSizes.ToList();

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

        private static void UpdateItemList() =>
            cache.Items = reader.GetItems().ToList();

        internal static void UpdateSpecsList() =>
            cache.Specs = reader.GetSpecs().ToList();

        internal static void UpdateSizeGroupList()
        {
            List<ISizeGroup> list = reader.GetSizeGroups().ToList();
            cache.SizeGroups = list;
        }

        private static void UpdateSizesList() =>
            cache.SizeLists = reader.GetSizes().ToList();

        private static void UpdateBrandsList() =>
            cache.BrandLists = reader.GetBrands().ToList();

        private static void UpdateEndsList() =>
            cache.EndLists = reader.GetEnds().ToList();

        private static void UpdateCustomSpecsList() =>
            cache.CustomSpecs = reader.GetCustomSpecs().ToList();

        private static void UpdateCustomSizesList() =>
            cache.CustomSizes = reader.GetCustomSizes().ToList();
    }
}