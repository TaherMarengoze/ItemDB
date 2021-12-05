using AppCore;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for getting field data from entities and also updates those data when models' data change.
    /// </summary>
    public static class DataProvider
    {
        internal static void RegisterModelListsEvents()
        {
            Globals.ModelLists.OnSpecsChanged += Lists_OnSpecsChanged;
            Globals.ModelLists.OnSizeGroupsChanged += ModelLists_OnSizeGroupsChanged;
            Globals.ModelLists.OnSizeListChanged += ModelLists_OnSizeListChanged;
            Globals.ModelLists.OnCustomSizeListChanged += ModelLists_OnCustomSizeListChanged;
        }
        
        private static void Lists_OnSpecsChanged(object sender, EventArgs e)
        {
            Globals.DataLists.SpecsIdList =
                Globals.ModelLists.Specs.Select(entity => entity.ID).ToList();
        }

        private static void ModelLists_OnSizeGroupsChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void ModelLists_OnSizeListChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.SizeIdList =
                Globals.ModelLists.SizeLists.Select(size => size.ID).ToList();
        }

        private static void ModelLists_OnCustomSizeListChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.CustomSizeIdList =
                Globals.ModelLists.CustomSizes;
        }

        /// <summary>
        /// Gets a list of ID for all specs objects.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSpecsIds()
        {
            return Globals.DataLists.SpecsIdList;
        }
        public static List<string> FilterSpecsIds(string exactId)
        {
            return
                Globals.DataLists.SpecsIdList
                .Where(id => id.Contains(exactId))
                .ToList();
        }

        public static int SpecsCount => GetSpecsIds().Count;

        public static class SizeGroup
        {
            public static List<ISizeGroup> GetList()
            {
                return CacheIO.GetSizeGroupList().ToList();
            }
        }

        public static class Size
        {
            public static List<string> GetIDs()
            {
                return Globals.DataLists.SizeIdList;
            }
        }

        public static class CustomSize
        {
            public static List<string> GetIDs()
            {
                return Globals.DataLists.CustomSizeIdList;
            }
        }
    }
}
