using System.Collections.ObjectModel;

namespace CoreLibrary.Models
{
    using Interfaces;

    public class FieldList : IFieldList
    {
        public string ID { get; set; }

        public string Name { get; set; }

        //public List<string> List { get; set; } = new List<string>();
        public ObservableCollection<string> List { get; set; }
            = new ObservableCollection<string>();
    }
}
