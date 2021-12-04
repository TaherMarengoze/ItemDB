using AppCore;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    public static class DataProvider
    {
        internal static void RegisterModelListsEvents()
        {
            Globals.ModelLists.OnSpecsChanged += Lists_OnSpecsChanged;
            Globals.ModelLists.OnSizeGroupsChanged += ModelLists_OnSizeGroupsChanged;
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
                AppCore.Globals.DataLists.SpecsIdList
                .Where(id => id.Contains(exactId))
                .ToList();
        }

        public static int SpecsCount => GetSpecsIds().Count;

        public static class SizeGroup
        {
            public static List<ISizeGroup> GetSizeGroups()
            {
                return CacheIO.GetSizeGroupList().ToList();
            }
        }
    }
}
