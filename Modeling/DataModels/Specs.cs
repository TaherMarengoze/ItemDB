
using Interfaces.Models;
using System.Collections.Generic;


namespace Modeling.DataModels
{
    public class Specs : ISpecs
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string TextPattern { get; set; } = "{base}";

        public IEnumerable<ISpecsItem> SpecItems { get; set; }
            = new List<ISpecsItem>();
    }
}