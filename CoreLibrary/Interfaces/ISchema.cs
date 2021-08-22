using System.Xml.Linq;

namespace CoreLibrary.Interfaces
{
    public interface ISchema
    {
        XName ChildGroup { get; }
        XName ListChild { get; }
        XName ListId { get; }
        XName ListName { get; }
        XName ListParent { get; }
    }
}