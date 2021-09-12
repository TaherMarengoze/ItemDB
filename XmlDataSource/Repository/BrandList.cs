
using AppCore;
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.Serialization;

namespace XmlDataSource.Repository
{
    public class BrandList : IEntityRepo<IFieldList>
    {
        private readonly XDocument dataSource;

        public BrandList(XDocument source)
        {
            dataSource = source;
        }

        public BrandList( )
        {
            dataSource =
                ((XmlContext)Globals.context).DataDocs.Brands;
        }

        public void Create(IFieldList entity)
        {
            XElement content = Entity.SerializeBrand(entity);
            dataSource.Root.Add(content);
        }

        public IFieldList Read() => throw new NotImplementedException();

        public void Update(string refId, IFieldList entity)
        {
            XElement newContent = Entity.SerializeBrand(entity);
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
                dataSource.Descendants("brandList")
                .Where(node => node.Attribute("listID").Value == entityId)
                .FirstOrDefault();
        }
    }
}