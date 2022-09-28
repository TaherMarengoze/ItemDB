
using System.Collections.ObjectModel;


namespace Interfaces.Models
{
    public interface IFieldList : IIdentifiable
    {
        ObservableCollection<string> List { get; set; }
    }
}