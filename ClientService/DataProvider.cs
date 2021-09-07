
using AppCore;
using Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientService
{
    public static class DataProvider
    {
        public static void InitLists()
        {
            Globals.Lists = new ModelListHolder()
            {
                Items = Globals.reader.GetItems().ToList(),
                Specs = Globals.reader.GetSpecs().ToList(),
                SizeGroups = Globals.reader.GetSizeGroups().ToList(),
                SizeLists = Globals.reader.GetSizes().ToList(),
                BrandLists = Globals.reader.GetBrands().ToList(),
                EndLists = Globals.reader.GetEnds().ToList(),
                CustomSpecs = Globals.reader.GetCustomSpecs().ToList(),
                CustomSizes = Globals.reader.GetCustomSizes().ToList()
            };
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
    }
}