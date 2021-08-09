using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface
{
    using Enums;
    using Interfaces;

    public class FieldXmlRepository : IFieldRepos
    {
        private readonly XDocument dataSource;
        private readonly ISchema schema;

        public FieldXmlRepository(XDocument source, FieldType field)
        {
            dataSource = source;
            schema = new Models.FieldSchema(field);
        }

        public void AddFieldList(IBasicList listData)
        {
            XElement content = SerializeSizeList(listData);
            dataSource.Root.Add(content);
        }

        public IBasicList ReadFieldList(string listId) => throw new NotImplementedException();

        public void UpdateFieldList(string refId, IBasicList list)
        {
            XElement content = GetFieldList(refId);
            content.Attribute(schema.ListId).Value = list.ID;
            content.SetAttributeValue(schema.ListName, list.Name);
        }

        public void DeleteFieldList(string listId)
        {
            GetFieldList(listId).Remove();
        }

        private XElement GetFieldList(string listId)
        {
            return
                dataSource.Descendants(schema.ListParent)
                .Where(list => list.Attribute(schema.ListId).Value == listId)
                .FirstOrDefault();
        }

        private XElement SerializeSizeList(IBasicList field)
        {
            IEnumerable<XElement> entries =
                field.List.Select(entry => new XElement(schema.ListChild, entry));

            XElement draftSizeList =
                new XElement(schema.ListParent,
                    new XAttribute(schema.ListId, field.ID),
                    new XAttribute(schema.ListName, field.Name),
                    new XElement(schema.ChildGroup, entries));

            return draftSizeList;
        }

    }
}