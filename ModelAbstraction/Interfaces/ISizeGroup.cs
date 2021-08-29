
using System.Collections.Generic;


namespace ModelAbstraction.Interfaces
{
    public interface ISizeGroup
    {
        string ID { get; set; }

        string Name { get; set; }

        string DefaultListID { get; set; }

        List<string> AltIdList { get; set; }

        string CustomSize { get; set; }
    }
}
