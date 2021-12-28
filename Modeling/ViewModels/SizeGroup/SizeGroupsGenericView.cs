using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.ViewModels.SizeGroup
{
    public class SizeGroupsGenericView
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string DefaultListID { get; set; }

        public int AltListsCount { get; set; }

        public string CustomSize { get; set; }
    }
}
