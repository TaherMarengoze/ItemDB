using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class ListMetadata
    {
        public ListMetadata(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public string ID { get; internal set; }
        public string Name { get; internal set; }
    }
}
