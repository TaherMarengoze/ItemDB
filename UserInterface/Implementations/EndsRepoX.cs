using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface
{
    using Interfaces;

    public class EndsRepoX : IFieldRepos
    {
        private readonly XDocument dataSource;
        private readonly ISchema schema;

        public EndsRepoX(XDocument source)
        {
            dataSource = source;
            schema = new Models.FieldSchema(Enums.FieldType.ENDS);
        }

        public void AddFieldList(IBasicList listData)
        {
            XElement content = SerializeSizeList(listData);
            dataSource.Root.Add(content);
        }

        public IBasicList ReadFieldList(string listId) => throw new NotImplementedException();

        public void UpdateFieldList(string refId, IBasicList list) => throw new NotImplementedException();

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