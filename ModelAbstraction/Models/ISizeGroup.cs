
using System.Collections.Generic;


namespace Interfaces.Models
{
    public interface ISizeGroup : IIdentity
    {
        string DefaultListID { get; set; }

        List<string> AltIdList { get; set; }

        string CustomSize { get; set; }
    }
}