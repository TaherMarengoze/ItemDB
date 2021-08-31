
using System.Collections.ObjectModel;


namespace Interfaces.Models
{
    public interface IFieldList : IIdentity
    {
        ObservableCollection<string> List { get; set; }
    }
}