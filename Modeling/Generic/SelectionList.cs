using Interfaces.Models;
using System.Collections.ObjectModel;

namespace Modeling.Generic
{
    public class SelectionList : IFieldList
    {
        public bool Include { get; set; } = false;

        public string ID { get; set; }

        public string Name { get; set; }

        public ObservableCollection<string> List { get; set; }
    }
}