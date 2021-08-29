
using System.Collections.Generic;


namespace ModelAbstraction.Interfaces
{
    public interface ISpecs : IIdentity
    {
        string TextPattern { get; set; }

        List<ISpecsItem> SpecItems { get; set; }
    }
}