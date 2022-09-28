using System;
using System.Linq;
using System.Xml.Linq;
using AppCore;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;
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
            dataSource = ((XmlContext)Globals.context).DataDocs.Items;
        }

        public void Create(IItem entity)
        {
            XElement content = Serialize.ItemEntity(entity);

            // get the item's new category or create it if not found
            XElement category = MustGetItemCategoryXElement(entity);

            // add the item to the category
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
            XElement oldContent = GetXElement(refId);
            XElement category = MustGetItemCategoryXElement(entity);
            XElement oldCategory = GetItemCategoryXElement(refId);

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
            GetXElement(entityId).Remove();

            OnChange?.Invoke(this, EventArgs.Empty);
        }

        private XElement GetXElement(string entityId)
        {
            return
                dataSource.Descendants("item").FirstOrDefault(node =>
                node.Attribute("itemID").Value == entityId);
        }

        /// <summary>
        /// Gets an item's category and create it if not found.
        /// </summary>
        /// <param name="item">The item object that its category to be found.</param>
        /// <returns>The <see cref="XElement"/> of the category.</returns>
        private XElement MustGetItemCategoryXElement(IItem item)
        {
            return GetCategoryXElement(item.CatID) ?? CreateCategory(item);
        }

        private XElement CreateCategory(IItem item)
        {
            Globals.categoryRepo
                .Create(new ItemCategory(item.CatID, item.CatName));

            return GetCategoryXElement(item.CatID);
        }

        private XElement GetCategoryXElement(string catID)
        {
            return
                dataSource.Descendants("category").FirstOrDefault(cat =>
                cat.Attribute("catID").Value == catID);
        }

        private XElement GetItemCategoryXElement(string itemID)
        {
            return
                dataSource.Descendants("item").FirstOrDefault(item =>
                item.Attribute("itemID").Value == itemID).Parent;
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