using System.Xml.Linq;

namespace UserInterface.Interfaces
{
    public interface IListStructure
    {
        XName ChildGroup { get; }
        XName ListChild { get; }
        XName ListId { get; }
        XName ListName { get; }
        XName ListParent { get; }
    }
}