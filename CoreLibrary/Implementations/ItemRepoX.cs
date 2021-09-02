
using System.Linq;
using System.Xml.Linq;


namespace CoreLibrary
{
    using Interfaces;


    public class ItemRepoX : IModifier
    {
        public void CreateItem(IItem item)
        {
            XElement xItem = SerializeItem(item);
            CategorizeItem(xItem, item.CatID, item.CatName);
        }

        public void UpdateItem(string refId, IItem data)
        {
            XElement xItem = AppFactory.xDataDocs.Items.Descendants("item")
                    .Where(elem => elem.Attribute("itemID").Value == refId)
                    .SingleOrDefault();

            //xItem.SetAttributeValue("itemID", data.ItemID);
            xItem.Element("names").SetElementValue("base", data.BaseName);
            xItem.Element("names").SetElementValue("display", data.DisplayName);

            // Common Names
            // Remove all existing common names
            xItem.Element("names").Element("common").RemoveNodes();

            // Add the given common names if any
            if (data.CommonNames != null)
            {
                foreach (string commonName in data.CommonNames)
                {
                    xItem.Element("names").Element("common").Add(new XElement("cname", commonName));
                }
            }

            xItem.SetElementValue("description", data.Description);

            // Unit of Measuring (UoM)
            xItem.SetElementValue("uom", data.UoM);

            // Images
            xItem.Element("images").RemoveNodes();
            if (data.ImagesFileName != null)
            {
                foreach (string imageName in data.ImagesFileName)
                {
                    xItem.Element("images").Add(new XElement("image", imageName));
                }
            }

            // Go from bottom to top to preserve details order
            ModifyField(xItem, "endsList", data.Details.EndsListID, data.Details.EndsRequired);
            ModifyField(xItem, "brandList", data.Details.BrandListID, data.Details.BrandRequired);
            ModifyField(xItem, "sizeGroup", data.Details.SizeGroupID, data.Details.SizeRequired);
            ModifyField(xItem, "specs", data.Details.SpecsID, data.Details.SpecsRequired);


            ProcessItemCategory(refId, xItem, data.CatID, data.CatName);

            // Change the Item ID after any modification
            xItem.SetAttributeValue("itemID", data.ItemID);
        }

        public void DeleteItem(string itemId)
        {
            XElement deleteItem =
                AppFactory.xDataDocs.Items.Descendants("item")
                .Where(item => item.Attribute("itemID").Value == itemId).First();

            deleteItem.Remove();
        }

        private XElement SerializeItem(IItem item)
        {
            XElement commonNames = new XElement("common");
            foreach (string name in item.CommonNames)
            {
                commonNames.Add(new XElement("cname", name));
            }

            XElement images = new XElement("images");
            foreach (string image in item.ImagesFileName)
            {
                images.Add(new XElement("image", image));
            }

            XElement details = new XElement("details");
            if (string.IsNullOrEmpty(item.Details.SpecsID) == false)
            {
                details.Add(
                new XElement("specs",
                new XAttribute("ID", item.Details.SpecsID),
                new XAttribute("required", item.Details.SpecsRequired)));
            }

            if (string.IsNullOrEmpty(item.Details.SizeGroupID) == false)
            {
                details.Add(
                new XElement("sizeGroup",
                new XAttribute("ID", item.Details.SizeGroupID),
                new XAttribute("required", item.Details.SizeRequired)));
            }

            if (string.IsNullOrEmpty(item.Details.BrandListID) == false)
            {
                details.Add(
                new XElement("brandList",
                new XAttribute("ID", item.Details.BrandListID),
                new XAttribute("required", item.Details.BrandRequired)));
            }

            if (string.IsNullOrEmpty(item.Details.EndsListID) == false)
            {
                details.Add(
                new XElement("endsList",
                new XAttribute("ID", item.Details.EndsListID),
                new XAttribute("required", item.Details.EndsRequired)));
            }

            XElement draftItem =
                new XElement("item",
                new XAttribute("itemID", item.ItemID),
                new XAttribute("order", "0"),
                    new XElement("names",
                        new XElement("base", item.BaseName),
                        new XElement("display", item.DisplayName),
                        commonNames),
                    new XElement("description", item.Description),
                    images,
                    details,
                    new XElement("uom", item.UoM));

            return draftItem;
        }

        private void ProcessItemCategory(string existingId, XElement xItem, string catId, string catName)
        {
            // Get old CatID of edited item
            string oldCatID =
                GetItemCategoryIdAttribute(AppFactory.xDataDocs.Items, existingId).Value;

            // Compare categories IDs
            if (catId != oldCatID)
            {
                // Remove the item from the old category
                xItem.Remove();

                CategorizeItem(xItem, catId, catName);
            }
        }

        private void CategorizeItem(XElement serializedItem, string catId, string catName)
        {
            // Get the item's new category or create it if not found
            XElement category = GetCategoryAdd(AppFactory.xDataDocs.Items, catId, catName);

            // Add the item to the category
            category.Add(serializedItem);
        }

        private void ModifyField(XElement itemXElement, XName fieldXName, string fieldId, bool fieldRequired)
        {
            // Get Field XElement (Node)
            XElement fieldXElement = itemXElement.Element("details").Element(fieldXName);

            // Check if the field is given or not
            if (string.IsNullOrEmpty(fieldId))
            {
                // Remove Field node if exists
                if (fieldXElement != null)
                {
                    fieldXElement.Remove();
                }
            }
            else
            {
                // If Field node does not exist then add it
                if (fieldXElement == null)
                {
                    itemXElement.Element("details").AddFirst(
                        new XElement(fieldXName,
                        new XAttribute("ID", fieldId),
                        new XAttribute("required", fieldRequired)));
                }
                else // modify attribute values
                {
                    fieldXElement.SetAttributeValue("ID", fieldId);
                    fieldXElement.SetAttributeValue("required", fieldRequired);
                }
            }
        }

        private XElement GetCategoryAdd(XDocument itemsXDoc, string catId, string catName)
        {
            XElement category = GetCategory(itemsXDoc, catId);

            // If category do not exists then add it
            if (category == null)
            {
                category = CreateCategory(catId, catName);
                itemsXDoc.Root.Add(category);
            }

            return category;
        }

        private XElement GetCategory(XDocument itemsXDoc, string catId)
        {
            return
                (from cat in itemsXDoc.Descendants("category")
                 where cat.Attribute("catID").Value == catId
                 select cat).FirstOrDefault();
        }

        private XElement CreateCategory(string catId, string catName)
        {
            return
                new XElement("category",
                    new XAttribute("catID", catId),
                    new XAttribute("name", catName));
        }

        private XElement GetItemCategory(XDocument itemsXDoc, string itemId)
        {
            XElement category =
            (from item in itemsXDoc.Descendants("item")
             where item.Attribute("itemID").Value == itemId
             select item.Parent).FirstOrDefault();
            return category;
        }

        private  XAttribute GetItemCategoryIdAttribute(XDocument itemsXDoc, string itemId)
        {
            return GetItemCategory(itemsXDoc, itemId).Attribute("catID");
        }
    }
}