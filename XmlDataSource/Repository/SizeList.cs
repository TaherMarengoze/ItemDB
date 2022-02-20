
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.IO;

namespace XmlDataSource.Repository
{
    public class SizeList : IRepo<IFieldList>
    {
        private readonly XDocument dataSource;
        private readonly XmlContext context;

        /// <summary>
        /// Occurs when <c>this</c> <see cref="SizeList"/> repository is changed by any create, update or delete operation.
        /// </summary>
        public event EventHandler OnChange;

        public SizeList(XDocument source)
        {
            dataSource = source;
        }

        public SizeList()
        {
            context = (XmlContext)AppCore.Globals.context;
            dataSource = context.DataDocs.Sizes;
        }

        public void Create(IFieldList entity)
        {
            XElement content = Serialize.SizeListEntity(entity);
            dataSource.Root.Add(content);

            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public IFieldList Read(string entityId)
        {
            return
                Deserialize.SizeXElement(dataSource.Descendants("sizeList")
                .FirstOrDefault(node => node.Attribute("listID").Value == entityId));
        }

        public void Update(string refId, IFieldList entity)
        {
            XElement newContent = Serialize.SizeListEntity(entity);
            XElement oldContent = GetElement(refId);
            oldContent.ReplaceWith(newContent);

            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public void Delete(string entityId)
        {
            GetElement(entityId).Remove();

            OnChange?.Invoke(this, EventArgs.Empty);
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