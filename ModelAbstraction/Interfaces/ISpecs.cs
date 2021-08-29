
using System.Collections.Generic;


namespace ModelAbstraction.Interfaces
{
    public interface ISpecs
    {
        string ID { get; set; }

        string Name { get; set; }

        string TextPattern { get; set; }

        List<ISpecsItem> SpecItems { get; set; }
    }
}