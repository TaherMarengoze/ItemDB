using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Models
{

    public class SizeGroupView : IBasicField
    {
        public SizeGroupView(SizeGroup group)
        {
            ID = group.ID;
            Name = group.Name;
            DefaultListID = group.DefaultListID;
            AltLists = group.AltIdList == null ? "" : string.Join(", ", group.AltIdList);
            CustomDataID = group.CustomSize;
            AltIdList = group.AltIdList;
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string DefaultListID { get; set; }
        public string AltLists { get; set; }
        public string CustomDataID { get; set; }
        public List<string> AltIdList { get; set; }
    }
}