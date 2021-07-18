namespace UserInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Specs : IView
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
