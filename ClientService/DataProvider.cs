﻿
using AppCore;
using Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for getting lists of entities from the reader and the global cache, and allows updating of this cache.
    /// </summary>
    public static class DataProvider
    {
        public static void InitLists()
        {
            Globals.Lists = new ModelListHolder();
            UpdateAllLists();
        }

        public static List<IItem> GetItemList()
        {
            return Globals.Lists.Items;
        }

        public static List<ISpecs> GetSpecsList()
        {
            return Globals.Lists.Specs;
        }

        public static List<ISizeGroup> GetSizeGroupList()
        {
            return Globals.Lists.SizeGroups;
        }

        public static List<IFieldList> GetSizeList()
        {
            return Globals.Lists.SizeLists;
        }

        public static List<IFieldList> GetBrandList()
        {
            return Globals.Lists.BrandLists;
        }

        public static List<IFieldList> GetEndList()
        {
            return Globals.Lists.EndLists;
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
            Globals.Lists.Items = Globals.reader.GetItems().ToList();
        }

        internal static void UpdateSpecsList()
        {
            // This will run a query against the data source
            Globals.Lists.Specs =
                Globals.reader.GetSpecs().ToList();
        }

        private static void UpdateSizeGroupList()
        {
            Globals.Lists.SizeGroups =
                Globals.reader.GetSizeGroups().ToList();
        }

        private static void UpdateSizesList()
        {
            Globals.Lists.SizeLists =
                Globals.reader.GetSizes().ToList();
        }

        private static void UpdateBrandsList()
        {
            Globals.Lists.BrandLists = Globals.reader.GetBrands().ToList();
        }

        private static void UpdateEndsList()
        {
            Globals.Lists.EndLists = Globals.reader.GetEnds().ToList();
        }

        private static void UpdateCustomSpecsList()
        {
            Globals.Lists.CustomSpecs = Globals.reader.GetCustomSpecs().ToList();
        }

        private static void UpdateCustomSizesList()
        {
            Globals.Lists.CustomSizes = Globals.reader.GetCustomSizes().ToList();
        }
    }
}