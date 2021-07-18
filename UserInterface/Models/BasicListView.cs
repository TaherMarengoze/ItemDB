using System.Collections.Generic;
using UserInterface.Interfaces;

namespace UserInterface.Models
{
    public class BasicListView
    {
        public BasicListView() { }

        public BasicListView(IFieldList fieldList)
        {
            ID = fieldList.ID;
            Name = fieldList.Name;
            List = new List<string>(fieldList.List);
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public int Entries => List.Count;

        public List<string> List { get; set; }
    }
}