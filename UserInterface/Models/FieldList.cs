using System.Collections.Generic;
using UserInterface.Interfaces;

namespace UserInterface.Models
{
    public class FieldList : IFieldList
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public List<string> List { get; set; } = new List<string>();
    }
}
