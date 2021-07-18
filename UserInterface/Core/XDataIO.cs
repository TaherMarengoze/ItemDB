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
using UserInterface.Types;

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
            XDataDocuments.Save(fieldXDoc, FileProcessor.FieldFilePath(field));
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