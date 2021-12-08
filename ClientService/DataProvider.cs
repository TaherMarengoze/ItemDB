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
    public class DataProvider
    {
        /// <summary>
        /// Gets a list of ID for all specs objects.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIDs()
        {
            return Globals.DataLists.SpecsIDs;
        }
        public static List<string> FilterSpecsIds(string exactId)
        {
            return
                Globals.DataLists.SpecsIDs
                .Where(id => id.Contains(exactId))
                .ToList();
        }
        public static int SpecsCount => GetIDs().Count;
    }
}
