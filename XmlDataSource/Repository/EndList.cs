
using AppCore;
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.IO;

namespace XmlDataSource.Repository
{
    public class EndList : IRepo<IFieldList>
    {
        private readonly XDocument dataSource;

        public event EventHandler OnChange;

        public EndList(XDocument source)
        {
            dataSource = source;
        }

        public EndList()
        {
            dataSource =
                ((XmlContext)Globals.context).DataDocs.Ends;
        }

        public void Create(IFieldList entity)
        {
            XElement content = Serialize.EndsListEntity(entity);
            dataSource.Root.Add(content);
        }

        public IFieldList Read(string entityId) => throw new NotImplementedException();

        public void Update(string refId, IFieldList entity)
        {
            XElement newContent = Serialize.EndsListEntity(entity);
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
                dataSource.Descendants("endsList")
                .Where(node => node.Attribute("listID").Value == entityId)
                .FirstOrDefault();
        }
    }
}