using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    public static class DataProvider
    {
        internal static void RegisterModelEvents()
        {
            AppCore.Globals.ModelLists.OnSpecsChanged += Lists_OnSpecsChanged;
        }
        
        private static void Lists_OnSpecsChanged(object sender, System.EventArgs e)
        {
            AppCore.Globals.DataLists.SpecsIdList =
                AppCore.Globals.ModelLists.Specs.Select(entity => entity.ID).ToList();
        }

        /// <summary>
        /// Gets a list of ID for all specs objects.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSpecsIds()
        {
            return AppCore.Globals.DataLists.SpecsIdList;
        }
        public static List<string> FilterSpecsIds(string exactId)
        {
            return
                AppCore.Globals.DataLists.SpecsIdList
                .Where(id => id.Contains(exactId))
                .ToList();
        }

        public static int SpecsCount => GetSpecsIds().Count;
    }
}
