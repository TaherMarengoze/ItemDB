using System.Collections.Generic;

namespace CoreLibrary.Interfaces
{
    public interface ISpecs
    {
        string ID { get; set; }
        string Name { get; set; }
        List<ISpec> SpecItems { get; set; }
        string TextPattern { get; set; }
    }
}