
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.Serialization;

namespace XmlDataSource.Repository
{
    public class Item : IEntityRepo<IItem>
    {
        private readonly XDocument dataSource;

        public Item(XDocument source)
        {
            dataSource = source;
        }

        public Item()
        {
            dataSource =
                ((XmlContext)AppCore.Globals.context).DataDocs.Items;
        }

        public void Create(IItem entity)
        {
            XElement content = Entity.Serialize(entity);
            //CategorizeItem(content, entity.CatID, entity.CatName);

            // Get the item's new category or create it if not found
            XElement category = GetCategoryAdd(entity);

            // Add the item to the category
            category.Add(content);
        }
        
        public IItem Read(string entityId) => throw new NotImplementedException();

        public void Update(string refId, IItem entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string entityId)
        {
            throw new NotImplementedException();
        }

        private XElement GetCategoryAdd(IItem item)
        {
            XElement category = GetCategory(item.CatID);

            // If category does not exist then create and add it
            if (category == null)
            {
                category = Entity.SerializeCategory(item);
                dataSource.Root.Add(category);
            }

            return category;
        }

        private XElement GetCategory(string catID)
        {
            return
                (from cat in dataSource.Descendants("category")
                 where cat.Attribute("catID").Value == catID
                 select cat).FirstOrDefault();
        }
    }
}