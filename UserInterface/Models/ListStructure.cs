using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserInterface.Interfaces;

namespace UserInterface.Models
{
    public class ListStructure : ISchema
    {
        public ListStructure(string listId, string listName, string listParentName, string listChildName, string childGroupName)
        {
            ListId = listId;
            ListName = listName;
            ListParent = listParentName;
            ListChild = listChildName;
            ChildGroup = childGroupName;
        }

        public ListStructure(string name)
        {
            ListId = "listID";
            ListName = "name";
            ListParent = $"{ name }List";
            ListChild = name;
            ChildGroup = $"{ name }s";
        }

        //public ListStructure() : this("field") { }

        public XName ListId { get; private set; }
        public XName ListName { get; private set; }
        public XName ListParent { get; private set; }
        public XName ListChild { get; private set; }
        public XName ChildGroup { get; private set; }
    }
}
