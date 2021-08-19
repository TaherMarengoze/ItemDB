using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoreLibrary.Interfaces
{
    public interface IBasicList
    {
        string ID { get; set; }
        ObservableCollection<string> List { get; set; }
        string Name { get; set; }
    }
}