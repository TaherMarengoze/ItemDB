using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserInterface.Interfaces
{
    public interface IBasicList
    {
        string ID { get; set; }
        ObservableCollection/*List*/<string> List { get; set; }
        string Name { get; set; }
    }
}