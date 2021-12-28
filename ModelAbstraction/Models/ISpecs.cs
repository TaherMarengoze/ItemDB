
using System.Collections.Generic;


namespace Interfaces.Models
{
    public interface ISpecs : IIdentity
    {
        string TextPattern { get; set; }

        IEnumerable<ISpecsItem> SpecItems { get; set; }
    }
}