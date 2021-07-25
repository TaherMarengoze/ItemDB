using System.Xml.Linq;

namespace UserInterface
{
    using Interfaces;
    using Models;
    using Operation;
    using System.Linq;

    public class ModifyXml : IModifier
    {
        public void AddItem(IItem item)
        {
            XElement xItem = SerializeItem(item);
            CategorizeItem(xItem, item.CatID, item.CatName);
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

        private void CategorizeItem(XElement serializedItem, string catId, string catName)
        {
            // Get the item's new category or create it if not found
            XElement category = XDataService.GetCategoryAdd(Program.xDataDocs.Items, catId, catName);

            // Add the item to the category
            category.Add(serializedItem);
        }

        public void ModifyItem(string existingId, IItemRawData data)
        {
            XElement xItem = Program.xDataDocs.Items.Descendants("item")
                    .Where(elem => elem.Attribute("itemID").Value == existingId)
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
            if (data.ImagesNames != null)
            {
                foreach (string imageName in data.ImagesNames)
                {
                    xItem.Element("images").Add(new XElement("image", imageName));
                }
            }

            // Go from bottom to top to preserve details order
            XDataService.ModifyFieldXElement(xItem, "endsList", data.EndsListID, data.EndsRequired);
            XDataService.ModifyFieldXElement(xItem, "brandList", data.BrandListID, data.BrandRequired);
            XDataService.ModifyFieldXElement(xItem, "sizeGroup", data.SizeGroupID, data.SizeRequired);
            XDataService.ModifyFieldXElement(xItem, "specs", data.SpecsID, data.SpecsRequired);
            

            ProcessItemCategory(existingId, xItem, data.CatID, data.CatName);

            // Change the Item ID after any modification
            xItem.SetAttributeValue("itemID", data.ItemID);
        }

        private void ProcessItemCategory(string existingId, XElement xItem, string catId, string catName)
        {
            // Get old CatID of edited item
            string oldCatID =
                XDataService.GetItemCategoryIdAttribute(Program.xDataDocs.Items, existingId).Value;

            // Compare categories IDs
            if (catId != oldCatID)
            {
                // Remove the item from the old category
                xItem.Remove();

                CategorizeItem(xItem, catId, catName);
            }
        }
    }
}