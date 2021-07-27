using System.Collections.Generic;

namespace UserInterface.Models
{
    using Interfaces;

    public class Specs : IView, ISpecs
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TextPattern { get; set; } = "{base}";
        public IEnumerable<ISpec> SpecItems { get; set; } = new List<ISpec>();

        public BasicView GetBasicView()
        {
            return new BasicView(ID, TextPattern);
        }
    }
}
