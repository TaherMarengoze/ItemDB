
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.Serialization;

namespace XmlDataSource.Repository
{
    public class SizeList : IEntityRepo<IFieldList>
    {
        private readonly XDocument dataSource;

        public SizeList(XDocument source)
        {
            dataSource = source;
        }

        public SizeList()
        {
            dataSource =
                ((XmlContext)AppCore.Globals.context).DataDocs.Sizes;
        }

        public void Create(IFieldList entity)
        {
            XElement content = Entity.SerializeSize(entity);
            dataSource.Root.Add(content);
        }

        public IFieldList Read(string entityId) => throw new NotImplementedException();

        public void Update(string refId, IFieldList entity)
        {
            XElement newContent = Entity.SerializeSize(entity);
            XElement oldContent = GetElement(refId);
            oldContent.ReplaceWith(newContent);
        }

        public void Delete(string entityId)
        {
            GetElement(entityId).Remove();
        }

        private XElement GetElement(string entityId)
        {
            return
                dataSource.Descendants("sizeList")
                .Where(node => node.Attribute("listID").Value == entityId)
                .FirstOrDefault();
        }
    }
}