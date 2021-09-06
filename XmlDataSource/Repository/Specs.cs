
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.Serialization;

namespace XmlDataSource.Repository
{
    public class Specs : IEntityRepo<ISpecs>
    {
        private readonly XDocument dataSource;

        public Specs(XDocument source)
        {
            dataSource = source;
        }

        public Specs()
        {
            dataSource = new XDocument(); // this should be Specs XDocument
        }

        public void Create(ISpecs entity)
        {
            XElement content = Entity.Serialize(entity);
            dataSource.Root.Add(content);
        }

        public ISpecs Read() => throw new NotImplementedException();

        public void Update(string refId, ISpecs entity)
        {
            XElement newContent = Entity.Serialize(entity);
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
                dataSource.Descendants("specs")
                .Where(node => node.Attribute("specsID").Value == entityId)
                .FirstOrDefault();
        }
    }
}