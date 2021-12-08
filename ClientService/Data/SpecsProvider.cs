using AppCore;
using ClientService.Contracts;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Data
{
    public class SpecsProvider : IProvider<ISpecs>
    {
        public List<ISpecs> GetList()
        {
            throw new NotImplementedException();
        }

        public List<string> GetIDs() =>
            Globals.DataLists.SpecsIDs;

        public List<string> FilterSpecsIds(string exactId) =>
            Globals.DataLists.SpecsIDs
                .Where(id => id.Contains(exactId))
                .ToList();
        
        public int Count => GetIDs().Count;
    }
}
