using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoreLibrary.Models
{
    using Interfaces;

    public class BasicListView : IBasicList
    {
        public BasicListView() { }

        public BasicListView(IFieldList fieldList)
        {
            ID = fieldList.ID;
            Name = fieldList.Name;
            List = new ObservableCollection<string>(fieldList.List);
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public int Entries => List.Count;

        //public List<string> List { get; set; }

        public ObservableCollection<string> List { get; set; }
    }
}