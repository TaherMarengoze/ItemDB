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

            OnChange?.Invoke(this, EventArgs.Empty);
        }

        public IItem Read(string entityId)
        {
            bool idMatch(XElement node) =>
                node.Attribute("itemID").Value == entityId;

            XElement element =
                dataSource.Descendants("item").FirstOrDefault(idMatch);

            return Deserialize.ItemXElement(element);
        }

        public void Update(string refId, IItem entity)
        {
            XElement newContent = Serialize.ItemEntity(entity);
            XElement oldContent = GetElement(refId);
            XElement category = GetCategoryAdd(entity);
            XElement oldCategory = GetItemCategory(refId);

            if (IsSameCategory(category, oldCategory))
            {
                oldContent.ReplaceWith(newContent);
            }
            else
            {
                category.Add(newContent);
                oldContent.Remove();
            }

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
                dataSource.Descendants("item")
                .FirstOrDefault(
                    node => node.Attribute("itemID").Value == entityId);
        }

        /// <summary>
        /// Gets the item's category and create it if not found.
        /// </summary>
        /// <param name="item">The item object that its category to be quired.</param>
        /// <returns></returns>
        private XElement GetCategoryAdd(IItem item)
        {
            XElement category = GetCategory(item.CatID);

            // If category does not exist then create and add it
            if (category == null)
                category = CreateCategory(item);

            return category;
        }

        private XElement GetCategory(string catID)
        {
            return
                (from cat in dataSource.Descendants("category")
                 where cat.Attribute("catID").Value == catID
                 select cat).FirstOrDefault();
        }

        private XElement CreateCategory(IItem item)
        {
            XElement category = Serialize.CategoryEntity(item);
            dataSource.Root.Add(category);
            return category;
        }

        private XElement GetItemCategory(string itemID)
        {
            return
                (from item in dataSource.Descendants("item")
                 where item.Attribute("itemID").Value == itemID
                 select item.Parent).FirstOrDefault();
        }

        /// <summary>
        /// Determines whether two categories are the same by comparing the ID.
        /// </summary>
        /// <param name="category1">The first category element.</param>
        /// <param name="category2">The second category element.</param>
        /// <returns></returns>
        private static bool IsSameCategory(XElement category1,
            XElement category2)
        {
            return
                category1.Attribute("catID") ==
                category2.Attribute("catID");
        }
    }
}