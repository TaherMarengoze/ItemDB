using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Models;

namespace Modeling.ViewModels.Item
{
    public class GenericView
    {
        public GenericView(IItem item)
        {
            CatID = item.CatID;
            CatName = item.CatName;
            ID = item.ItemID;
            BaseName = item.BaseName;
            DisplayName = item.DisplayName;
        }

        public string CatID { get; }
        public string CatName { get; }

        public string ID { get; }
        public string BaseName { get; }
        public string DisplayName { get; }

        public override string ToString()
        {
            return string.Format(
                "ID: {0}, Name: {1} (CatID: {2})"
                , ID.PadRight(5), Truncate(BaseName, 24).PadRight(25), CatID);
        }

        private string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "~";
        }
    }
}