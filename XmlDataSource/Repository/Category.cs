using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XmlDataSource.IO;

namespace XmlDataSource.Repository
{
    public class Category : IRepo<IItemCategory>
    {
        private readonly XDocument dataSource;

        public event EventHandler OnChange;

        public Category()
        {
            dataSource = ((XmlContext)AppCore.Globals.context).DataDocs
                .Items;
        }

        public void Create(IItemCategory entity)
        {
            XElement content = Serialize.CategoryEntity(entity);
            dataSource.Root.Add(content);

            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public IItemCategory Read(string entityId)
        {
            return
                Deserialize.CategoryXElement(GetCategoryXElement(entityId));
        }

        public void Update(string refId, IItemCategory content)
        {
            var cat = GetCategoryXElement(refId);
            cat.SetAttributeValue("catID", content.CatID);
            cat.SetAttributeValue("name", content.CatName);

            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public void Delete(string entityId)
        {
            GetCategoryXElement(entityId).Remove();

            OnChange?.Invoke(this, EventArgs.Empty);
        }

        private XElement GetCategoryXElement(string catID)
        {
            return
                dataSource.Descendants("category").FirstOrDefault(cat =>
                cat.Attribute("catID").Value == catID);
        }

#if DEBUG
        internal XElement UnitTestGetCategoryXElement(string catID)
        {
            return GetCategoryXElement(catID);
        }
#endif
    }
}