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

        public IBasicList ReadFieldList(string fieldItemId) => throw new NotImplementedException();

        public void UpdateFieldList(string refId, IBasicList field) => throw new NotImplementedException();

        public void DeleteFieldList(string fieldId)
        {
            GetField(fieldId).Remove();
        }

        private XElement GetField(string fieldId)
        {
            return
                Program.xDataDocs.Sizes.Descendants("sizeList")
                .Where(list => list.Attribute("listID").Value == fieldId)
                .FirstOrDefault();
        }
    }
}