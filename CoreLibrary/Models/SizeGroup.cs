using System.Collections.Generic;

namespace CoreLibrary.Models
{
    using Interfaces;

    public class SizeGroup : IView
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DefaultListID { get; set; }
        public List<string> AltIdList { get; set; }
        public int AltListsCount
        {
            get
            {
                if (AltIdList != null)
                {
                    return AltIdList.Count;
                }
                return 0;
            }
        }
        public string CustomSize { get; set; }

        public BasicView GetBasicView()
        {
            return new BasicView(ID, Name);
        }
    }
}