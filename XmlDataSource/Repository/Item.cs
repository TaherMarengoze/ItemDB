using System;
using System.Linq;
using System.Xml.Linq;
using Interfaces.Models;
using Interfaces.Operations;
using XmlDataSource.IO;


namespace XmlDataSource.Repository
{
    public class Item : IRepo<IItem>
    {
        private readonly XDocument dataSource;

        public event EventHandler OnChange;

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
            XElement content = Serialize.ItemEntity(entity);
            //CategorizeItem(content, entity.CatID, entity.CatName);

            // Get the item's new category or create it if not found
            XElement category = GetCategoryAdd(entity);

            // Add the item to the category
            category.Add(content);
        }

        public IItem Read(string entityId)
        {
            Func<XElement, bool> idMatch =
                node => node.Attribute("itemID").Value == entityId;

            XElement element =
                dataSource.Descendants("item").FirstOrDefault(idMatch);

            return Deserialize.ItemXElement(element);
        }

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
                category = Serialize.CategoryEntity(item);
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