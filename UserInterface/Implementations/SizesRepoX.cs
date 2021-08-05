using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface
{
    using Interfaces;

    public class SizesRepoX : IFieldRepos
    {
        private readonly XDocument dataSource;

        public SizesRepoX(XDocument source)
        {
            dataSource = source;
        }

        public void AddFieldList(IBasicList sizeList)
        {
            XElement content = SerializeSizeList(sizeList);
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
                dataSource.Descendants("sizeList")
                .Where(list => list.Attribute("listID").Value == listId)
                .FirstOrDefault();
        }

        private XElement SerializeSizeList(IBasicList field)
        {
            ISchema schema = new Models.ListStructure("size");
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