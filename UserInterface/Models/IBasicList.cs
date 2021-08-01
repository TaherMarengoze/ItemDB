using System.Collections.Generic;

namespace UserInterface.Interfaces
{
    public interface IBasicList
    {
        string ID { get; set; }
        List<string> List { get; set; }
        string Name { get; set; }
    }
}