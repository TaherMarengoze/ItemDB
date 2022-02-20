
using Interfaces.Models;
using System.Collections.ObjectModel;


namespace Modeling.DataModels
{
    public class SizeList : IFieldList
    {
        public string ID { get; set; }

        public string Name { get; set; }

        //TODO: change 'ObservableCollection' to 'List'
        public ObservableCollection<string> List { get; set; }
    }
}