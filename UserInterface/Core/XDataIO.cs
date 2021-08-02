using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UserInterface.Factory;
using UserInterface.Interfaces;
using UserInterface.Models;
using UserInterface.Operation;
using UserInterface.Enums;

namespace UserInterface
{
    public class XDataIO
    {
        public void AddNewFieldList(FieldType field, IFieldList fieldListItem)
        {
            ISchema schema = GetFieldSchema(field);
            XElement listNode = SerializeFieldList(schema, fieldListItem);
            //XDocument fieldXDoc = XDataService.AddFieldItemToXDocument(field, listNode);
            //XDataDocuments.Save(fieldXDoc, FilePathProcessor.FieldFilePath(field));
        }

        private ISchema GetFieldSchema(FieldType field)
        {
            return (ISchema)
                Delegators.FieldFunctionCallback(field,
                    delegate { return new ListStructure("size"); },
                    delegate { return new ListStructure("brand"); },
                    delegate { return new ListStructure("listID", "name", "endsList", "end", "ends"); });
        }

        private XElement SerializeFieldList(ISchema ls, IFieldList fieldListItem)
        {
            IEnumerable<XElement> xEntries = fieldListItem.List.Select(entry => new XElement(ls.ListChild, entry));

            return
                new XElement(ls.ListParent,
                    new XAttribute(ls.ListId, fieldListItem.ID),
                    new XAttribute(ls.ListName, fieldListItem.Name),
                    new XElement(ls.ChildGroup, xEntries));
        }

        private static List<SizeGroup> _GetSizeGroups(XDocument sizeGroupXDoc)
        {
            return
                (from sg in sizeGroupXDoc.Descendants("group")
                 let list = sg.Element("altLists").HasElements ? sg.Element("altLists").Elements("listID").Select(l => l.Value).ToList() : null
                 let customId = sg.Element("customSizeDataID").Value
                 select new SizeGroup()
                 {
                     ID = sg.Attribute("groupID").Value,
                     Name = sg.Attribute("groupName").Value,
                     DefaultListID = sg.Element("defaultListID").Value,
                     AltIdList = list,
                     CustomSize = customId != string.Empty ? customId : null
                 }).ToList();
        }
    }
}