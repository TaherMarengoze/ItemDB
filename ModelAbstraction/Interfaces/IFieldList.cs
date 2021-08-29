
using System.Collections.ObjectModel;


namespace ModelAbstraction.Interfaces
{
    public interface IFieldList : IIdentity
    {
        ObservableCollection<string> List { get; set; }
    }
}