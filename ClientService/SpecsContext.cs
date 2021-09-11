
using AppCore;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    public static class SpecsContext
    {
        public static void Create(ISpecs specs)
        {
            // Add to data source
            // whatever it is, thanks to interfaces
            Globals.specsRepo.Create(specs);

            // Update the list in the Globals class
            DataProvider.UpdateSpecsList();
        }

        //>
    }
}
