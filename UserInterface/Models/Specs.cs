using System.Collections.Generic;

namespace UserInterface.Models
{
    using Interfaces;

    public class Specs : IView, ISpecs
    {
        //public Specs()
        //{
        //}

        public string ID { get; set; }
        public string Name { get; set; }
        public string TextPattern { get; set; } = "{base}";
        public List<Spec> SpecItems { get; set; } = new List<Spec>();

        public BasicView GetBasicView()
        {
            return new BasicView(ID, TextPattern);
        }
    }
}
