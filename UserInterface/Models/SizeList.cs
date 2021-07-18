using System.Collections.Generic;

namespace UserInterface.Models
{
    public class SizeList
    {
        public SizeList()
        {

        }

        public string ID { get; set; }
        public string ListName { get; set; }
        public List<string> Sizes { get; set; }
    }

    public class SelectionSizeList : SizeList
    {
        public bool Include { get; set; } = false;
    }

    //public static class ExtensionMethods
    //{
    //    public static List<SelectionSizeList> ConvertToSelectionList(this List<SizeList> lists)
    //    {
    //        return null;
    //    }
    //}
}