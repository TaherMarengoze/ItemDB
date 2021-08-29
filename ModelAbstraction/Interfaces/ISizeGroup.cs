
using System.Collections.Generic;


namespace ModelAbstraction.Interfaces
{
    public interface ISizeGroup : IIdentity
    {
        string DefaultListID { get; set; }

        List<string> AltIdList { get; set; }

        string CustomSize { get; set; }
    }
}