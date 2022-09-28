
using System.Collections.Generic;


namespace Interfaces.Models
{
    public interface ISpecs : IIdentifiable
    {
        string TextPattern { get; set; }

        IEnumerable<ISpecsItem> SpecItems { get; set; }
    }
}