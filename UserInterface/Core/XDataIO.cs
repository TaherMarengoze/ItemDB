using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UserInterface.Factory;
using UserInterface.Interfaces;
using UserInterface.Models;
using UserInterface.Operation;
using UserInterface.Enums;

namespace UserInterface
{
    public class XDataIO : ISource
    {

        public XDataIO()
        {

        }

        public XDataIO(XDataDocuments documents)
        {

        }

        public void AddNewFieldList(FieldType field, IFieldList fieldListItem)
        {
            IListStructure ls = GetFieldListStructure(field);
            XElement listNode = SerializeFieldList(ls, fieldListItem);
            XDocument fieldXDoc = XDataService.AddFieldItemToXDocument(field, listNode);
            XDataDocuments.Save(fieldXDoc, FilePathReader.FieldFilePath(field));
            //DataService.UpdateField(field);
        }

        private IListStructure GetFieldListStructure(FieldType field)
        {
            return (IListStructure)
                Delegators.FieldFunctionCallback(field,
                    delegate { return new ListStructure("size"); },
                    delegate { return new ListStructure("brand"); },
                    delegate { return new ListStructure("listID", "name", "endsList", "end", "ends"); });
        }

        private XElement SerializeFieldList(IListStructure ls, IFieldList fieldListItem)
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

        //public void AddNewBrandList(IFieldList fieldListItem)
        //{
        //    XElement listNode = CreateNewBrandList(fieldListItem);
        //    XDocument fieldXDoc = XDataService.AddFieldItemToXDocument(FieldType.BRAND, listNode);
        //    XDataDocuments.Save(fieldXDoc, FileProcessor.FieldFilePath(FieldType.BRAND));
        //    DataService.UpdateField(FieldType.BRAND);
        //}

        //private XElement CreateNewBrandList(IFieldList fieldListItem)
        //{
        //    IListStructure ls = new ListStructure("listID", "name", "brandList", "brand", "brands");
        //    IEnumerable<XElement> xEntries = fieldListItem.List.Select(entry => new XElement(ls.ListChild, entry));

        //    return
        //        new XElement(ls.ListParent,
        //            new XAttribute(ls.ListId, fieldListItem.ID),
        //            new XAttribute(ls.ListName, fieldListItem.Name),
        //            new XElement(ls.ChildGroup, xEntries));
        //}
    }
}