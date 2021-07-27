using System.Collections.Generic;

namespace UserInterface.Interfaces
{
    public interface ISpecs
    {
        string ID { get; set; }
        string Name { get; set; }
        List<Models.Spec> SpecItems { get; set; }
        string TextPattern { get; set; }
    }
}