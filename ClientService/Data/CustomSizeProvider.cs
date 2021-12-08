using AppCore;
using ClientService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Data
{
    public class CustomSizeProvider : IProvider<string>
    {
        public List<string> GetList()
        {
            throw new NotImplementedException();
        }

        public List<string> GetIDs()
        {
            return Globals.DataLists.CustomSizeIDs;
        }
    }
}
