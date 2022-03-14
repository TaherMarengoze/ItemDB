using AppCore;
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for getting lists of entities from the reader and the global cache, and allows updating of this cache.
    /// </summary>
    public static class CacheIO
    {
        private static ModelListsCache cache = Globals.ModelCache;
        private static IDataReader reader = Globals.reader;

        static CacheIO()
        {
            Globals.itemsRepo.OnChange += ItemsRepo_OnChange;
            Globals.sizeGroupRepo.OnChange += SizeGroupRepo_OnChange;
            Globals.sizesRepo.OnChange += SizesRepo_OnChange;
        }

        public static void InitLists() => UpdateAllLists();

        private static void ItemsRepo_OnChange(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff}: Updating Items Repository.");
            UpdateItemList();
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff}: Updated Items Repository.");
        }

        private static void SizeGroupRepo_OnChange(object sender, EventArgs e)
        {
            UpdateSizeGroupList();
        }

        private static void SizesRepo_OnChange(object sender, EventArgs e)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff}: Updating Sizes Repository.");
            UpdateSizesList();
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff}: Updated Sizes Repository.");
        }
        #region Lists Getter
        // .ToList returns a copy of the list preserving the one in the cache

        public static List<IItem> GetItemList() => cache.Items.ToList();

        internal static List<ISpecs> GetSpecsList() => cache.Specs.ToList();

        public static List<ISizeGroup> GetSizeGroupList() => cache.SizeGroups.ToList();

        public static List<IFieldList> GetSizeList() => cache.SizeLists.ToList();

        public static List<IFieldList> GetBrandList() => cache.BrandLists.ToList();

        public static List<IFieldList> GetEndList() => cache.EndLists.ToList();

        public static List<string> GetCustomSpecsList() => cache.CustomSpecs.ToList();

        public static List<string> GetCustomSizeList() => Globals.ModelCache.CustomSizes.ToList();
        #endregion

        #region Lists Updater
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

        internal static void UpdateSizeGroupList() =>
            cache.SizeGroups = reader.GetSizeGroups().ToList();

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
        #endregion
    }
}