
using ModelAbstraction.Interfaces;
using System.Collections.Generic;


namespace Modeling.DataModels
{
    public class SizeGroup : ISizeGroup
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string DefaultListID { get; set; }

        public List<string> AltIdList { get; set; }

        public string CustomSize { get; set; }
    }
}