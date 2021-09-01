
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.Serialization;

namespace XmlDataSorce.Repository
{
    public class Specs : IEntityRepo<ISpecs>
    {
        private readonly XDocument dataSource;

        public Specs(XDocument source)
        {
            dataSource = source;
        }

        public void Create(ISpecs specs)
        {
            XElement content = EntitySerializer.Serialize(specs);
            dataSource.Root.Add(content);
        }

        public ISpecs Read() => throw new NotImplementedException();

        public void Update(string refId, ISpecs specs)
        {
            XElement newContent = EntitySerializer.Serialize(specs);
            XElement oldContent = GetSpecs(refId);
            oldContent.ReplaceWith(newContent);
        }

        public void Delete(string specsId)
        {
            GetSpecs(specsId).Remove();
        }

        private XElement GetSpecs(string specsId)
        {
            return
                dataSource.Descendants("specs")
                .Where(sp => sp.Attribute("specsID").Value == specsId)
                .FirstOrDefault();
        }
    }
}