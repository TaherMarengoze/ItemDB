
using Interfaces.Models;
using System.Collections.ObjectModel;


namespace Modeling.DataModels
{
    public class BrandList : IFieldList
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public ObservableCollection<string> List { get; set; }
    }
}