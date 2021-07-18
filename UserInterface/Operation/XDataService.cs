using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UserInterface.Factory;
using UserInterface.Interfaces;
using UserInterface.Models;
using UserInterface.Types;

namespace UserInterface.Operation
{
    public static class XDataService
    {
        public static XElement GetItemCategory(XDocument itemsXDoc, string itemId)
        {
            XElement category =
            (from item in itemsXDoc.Descendants("item")
             where item.Attribute("itemID").Value == itemId
             select item.Parent).FirstOrDefault();
            return category;
        }

        public static XAttribute GetItemCategoryIdAttribute(XDocument itemsXDoc, string itemId)
        {
            return GetItemCategory(itemsXDoc,itemId).Attribute("catID");
        }

        public static XAttribute GetItemCategoryNameAttribute(XDocument itemsXDoc, string itemId)
        {
            return GetItemCategory(itemsXDoc, itemId).Attribute("name");
        }

        public static XElement GetCategoryAdd(XDocument itemsXDoc, string catId, string catName)
        {
            XElement category = GetCategory(itemsXDoc, catId);

            // If category do not exists then add it
            if (category == null)
            {
                category = CreateCategory(catId, catName);
                itemsXDoc.Root.Add(category);
            }

            return category;
        }

        public static XElement GetCategory(XDocument itemsXDoc, string catId)
        {
            return
                (from cat in itemsXDoc.Descendants("category")
                 where cat.Attribute("catID").Value == catId
                 select cat).FirstOrDefault();
        }

        public static XElement CreateCategory(string catId, string catName)
        {
            return
                new XElement("category",
                    new XAttribute("catID", catId),
                    new XAttribute("name", catName));
        }

        public static XDocument AddFieldItemToXDocument(FieldType field, XElement fieldItem)
        {
            XDocument fieldXdoc = GetFieldXDoc(field);
            fieldXdoc.Root.Add(fieldItem);
            DataService.ReadFieldXDocument(field);

            return fieldXdoc;
        }

        public static void ModifyFieldXDocument(FieldType field, string existingId, ListMetadata data, IListStructure xn)
        {
            XDocument fieldXdoc = GetFieldXDoc(field);

            XElement list =
                (from fieldList in fieldXdoc.Descendants(xn.ListParent)
                 where fieldList.Attribute(xn.ListId).Value == existingId
                 select fieldList).First();

            list.Attribute(xn.ListId).Value = data.ID;
            list.Attribute(xn.ListName).Value = data.Name;

            DataService.ReadFieldXDocument(field);
        }

        public static void ModifyFieldXElement(XElement itemXElement, XName fieldXName,
            string fieldId, bool fieldRequired)
        {
            // Get Field XElement (Node)
            XElement fieldXElement = itemXElement.Element("details").Element(fieldXName);

            // Check if the field is given or not
            if (string.IsNullOrEmpty(fieldId))
            {
                // Remove Field node if exists
                if (fieldXElement != null)
                {
                    fieldXElement.Remove();
                }
            }
            else
            {
                // If Field node does not exist then add it
                if (fieldXElement == null)
                {
                    itemXElement.Element("details").AddFirst(
                        new XElement(fieldXName,
                        new XAttribute("ID", fieldId),
                        new XAttribute("required", fieldRequired)));
                }
                else // modify attribute values
                {
                    fieldXElement.SetAttributeValue("ID", fieldId);
                    fieldXElement.SetAttributeValue("required", fieldRequired);
                }
            }
        }

        public static XDocument GetFieldXDoc(FieldType field)
        {
            return (XDocument) Delegators.FieldFunctionCallback(field,
                sizeCallback: delegate { return Program.xDataDocs.Sizes; },
                brandCallback: delegate { return Program.xDataDocs.Brands; },
                endsCallback: delegate { return Program.xDataDocs.Ends; });
        }

        private static void DeleteFieldListFromXDocument(XDocument fieldXDoc, string listId, XName nodeName)
        {
            XElement deleteFieldList =
                fieldXDoc.Descendants(nodeName)
                .Where(list => list.Attribute("listID").Value == listId).First();

            deleteFieldList.Remove();
        }
    }
}