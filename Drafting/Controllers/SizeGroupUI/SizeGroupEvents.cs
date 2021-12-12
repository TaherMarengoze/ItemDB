using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.SizeGroupUI
{
    public class SizeGroupSelectionEventArgs : EventArgs
    {
        public SizeGroupSelectionEventArgs(SizeGroup sg)
        {
            ID = sg.ID;
            Name = sg.Name;
            Default = sg.DefaultListID;
            AltList = sg.AltIdList;
            AltListCount = AltList?.Count ?? 0;
            Custom = sg.CustomSize;
        }

        public SizeGroup Selected { get; internal set; }

        public string ID { get; private set; }

        public string Name { get; private set; }

        public string Default { get; private set; }

        public List<string> AltList { get; private set; }

        public int AltListCount { get; private set; }

        public string Custom { get; private set; }
    }

    public class SizeGroupSetEventArgs : EventArgs
    {
        public string ID { get; set; }

        public List<SizeGroup> SizeGroups { get; set; }
    }
}
