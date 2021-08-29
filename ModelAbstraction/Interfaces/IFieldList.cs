
using System.Collections.ObjectModel;


namespace ModelAbstraction.Interfaces
{
    public interface IFieldList
    {
        string ID { get; set; }

        string Name { get; set; }

        ObservableCollection<string> List { get; set; }
    }
}
