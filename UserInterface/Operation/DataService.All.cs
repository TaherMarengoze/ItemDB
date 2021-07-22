namespace UserInterface.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UserInterface.Interfaces;
    using UserInterface.Models;

    public static partial class DataService
    {
        private static DataRepos repos;

        public static DataRepos TestRepos { get => repos; }

        public static void InitializeRepos()
        {
            repos = new DataRepos()
            {
                Items = Program.reader.GetItems(),
                Categories = Program.reader.GetCategories(),
                SpecsList = Program.reader.GetSpecs(),
                SizeGroups = Program.reader.GetSizeGroups(),
                SizesList = Program.reader.GetSizes(),
                BrandsList = Program.reader.GetBrands(),
                EndsList = Program.reader.GetEnds()
            };
        }

        private static void UpdateItems()
        {
            repos.Items = Program.reader.GetItems();
        }

        private static void UpdateCategories(XDocument document)
        {
            repos.Categories = Program.reader.GetCategories() /*_GetItemCategories(document)*/;
        }

        public static void UpdateSpecs(XDocument specsXDoc)
        {
            repos.SpecsList = _GetSpecs(specsXDoc);
        }

        public static void UpdateSizeGroups(XDocument sizeGroupsXDoc)
        {
            repos.SizeGroups = _GetSizeGroups(sizeGroupsXDoc);
        }

        public static void ValidateItemRawData(ItemRawData data)
        {
            // Category ID
            if (data.CatID.Length > 0 && data.CatID.Length <= 5)
            {

            }
        }

        public static Item ProcessItemRawData(IItemRawData data)
        {
            Item draftItem = new Item()
            {
                ItemID = data.ItemID,
                CatID = data.CatID,
                CatName = data.CatName,
                BaseName = data.BaseName,
                DisplayName = data.DisplayName,
                CommonNames = data.CommonNames,
                Description = data.Description,
                UoM = data.UoM,
                ImagesFileName = data.ImagesNames,
                Details = new ItemDetails()
                {
                    SpecsID = data.SpecsID,
                    SizeGroupID = data.SizeGroupID,
                    BrandListID = data.BrandListID,
                    EndsListID = data.EndsListID,
                    SpecsRequired = data.SpecsRequired,
                    SizeRequired = data.SizeRequired,
                    BrandRequired = data.BrandRequired,
                    EndsRequired = data.EndsRequired
                }
            };

            return draftItem;
        }

        public static void ModifyExistingItem(string itemId, ItemRawData data)
        {
            IItem editItem = GetItem(itemId);

            editItem.ItemID = data.ItemID;
            editItem.CatID = data.CatID;
            editItem.CatName = data.CatName;
            editItem.BaseName = data.BaseName;
            editItem.DisplayName = data.DisplayName;
            editItem.CommonNames = data.CommonNames;
            editItem.Description = data.Description;
            editItem.ImagesFileName = data.ImagesNames;
            editItem.UoM = data.UoM;

            // Item details
            editItem.Details.SpecsID = data.SpecsID;
            editItem.Details.SpecsRequired = data.SpecsRequired;
            editItem.Details.SizeGroupID = data.SizeGroupID;
            editItem.Details.SizeRequired = data.SizeRequired;
            editItem.Details.BrandListID = data.BrandListID;
            editItem.Details.BrandRequired = data.BrandRequired;
            editItem.Details.EndsListID = data.EndsListID;
            editItem.Details.EndsRequired = data.EndsRequired;
        }

        public static XElement SerializeItem(Item item)
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

        /// <summary>
        /// Adds the draft Item to the Items <see cref="XDocument"/>.
        /// </summary>
        /// <remarks>Draft Items are either a new Item or item being edited.</remarks>
        /// <param name="itemsXDoc">The <see cref="XDocument"/> to add the draft Item to.</param>
        /// <param name="data">The draft <see cref="ItemRawData"/> to be processed then added to the Items <see cref="XDocument"/>.</param>
        public static void AddItemToXDocument(XDocument itemsXDoc, IItemRawData data)
        {
            XElement item = SerializeItem(ProcessItemRawData(data));

            // Process Item Category
            CategorizeItem(itemsXDoc, data, item);

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories(itemsXDoc);
        }

        /// <summary>
        /// Modifies an item in the Items <see cref="XDocument"/>.
        /// </summary>
        /// <param name="itemsXDoc">The <see cref="XDocument"/> containing the item being modified.</param>
        /// <param name="existingId">The ID of the item being modified.</param>
        /// <param name="data">The input data to replace the modified item data.</param>
        public static void ModifyItemXDocument(XDocument itemsXDoc, string existingId, IItemRawData data)
        {
            //ModifyExistingItem(existingId, data);
            #region MyRegion
            XElement item = itemsXDoc.Descendants("item")
                    .Where(elem => elem.Attribute("itemID").Value == existingId)
                    .First();

            item.SetAttributeValue("itemID", data.ItemID);
            item.Element("names").SetElementValue("base", data.BaseName);
            item.Element("names").SetElementValue("display", data.DisplayName);

            // Common Names
            // Remove all existing common names
            item.Element("names").Element("common").RemoveNodes();

            // Add the given common names if any
            if (data.CommonNames != null)
            {
                foreach (string commonName in data.CommonNames)
                {
                    item.Element("names").Element("common").Add(new XElement("cname", commonName));
                }
            }

            item.SetElementValue("description", data.Description);

            // Unit of Measuring (UoM)
            item.SetElementValue("uom", data.UoM);

            // Images
            item.Element("images").RemoveNodes();
            if (data.ImagesNames != null)
            {
                foreach (string imageName in data.ImagesNames)
                {
                    item.Element("images").Add(new XElement("image", imageName));
                }
            }

            // Go from bottom to top to preserve details order
            XDataService.ModifyFieldXElement(item, "endsList", data.EndsListID, data.EndsRequired);
            XDataService.ModifyFieldXElement(item, "brandList", data.BrandListID, data.BrandRequired);
            XDataService.ModifyFieldXElement(item, "sizeGroup", data.SizeGroupID, data.SizeRequired);
            XDataService.ModifyFieldXElement(item, "specs", data.SpecsID, data.SpecsRequired);
            #endregion

            // Process Item Category
            ProcessItemCategory(itemsXDoc, existingId, data, item);

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories(itemsXDoc);
        }

        private static void ProcessItemCategory(XDocument itemsXDoc, string existingId, IItemRawData data, XElement item)
        {
            // Get old CatID of edited item
            string oldCatID = XDataService.GetItemCategoryIdAttribute(itemsXDoc, existingId).Value;

            // Compare categories IDs
            if (data.CatID != oldCatID)
            {
                // Remove the item from the old category
                item.Remove();

                CategorizeItem(itemsXDoc, data, item);
            }
        }

        private static void CategorizeItem(XDocument itemsXDoc, IItemRawData data, XElement item)
        {
            // Get the item's new category or create it if not found
            XElement category = XDataService.GetCategoryAdd(itemsXDoc, data.CatID, data.CatName);

            // Add the item to the category
            category.Add(item);
        }

        #region Item Object
        private static List<Item> _GetAllItems(XDocument itemXDoc)
        {
            return
             (from item in itemXDoc.Descendants("item")
              let names = item.Element("names")
              let commonNames = names.Element("common")
              let images = item.Element("images")
              let details = item.Element("details")
              let detailsSpecs = details.Element("specs")
              let detailsSize = details.Element("sizeGroup")
              let detailsBrand = details.Element("brandList")
              let detailsEnds = details.Element("endsList")
              select new Item()
              {
                  CatID = item.Parent.Attribute("catID").Value,
                  CatName = item.Parent.Attribute("name").Value,
                  ItemID = item.Attribute("itemID").Value,
                  BaseName = names.Element("base").Value,
                  DisplayName = names.Element("display").Value,
                  CommonNames =
                    commonNames.HasElements ? commonNames.Elements("cname").Select(cnm => cnm.Value).ToList() : null,

                  Description = item.Element("description").Value,
                  ImagesFileName =
                    images.HasElements ? images.Elements("image").Select(img => img.Value).ToList() : null,

                  Details = new ItemDetails()
                  {
                      SpecsID = detailsSpecs?.Attribute("ID").Value,
                      SpecsRequired =
                        detailsSpecs != null ? (bool)detailsSpecs.Attribute("required") : false,

                      SizeGroupID = detailsSize?.Attribute("ID").Value,
                      SizeRequired =
                        detailsSize != null ? (bool)detailsSize.Attribute("required") : false,

                      BrandListID = detailsBrand?.Attribute("ID").Value,
                      BrandRequired =
                        detailsBrand != null ? (bool)detailsBrand.Attribute("required") : false,

                      EndsListID = detailsEnds?.Attribute("ID").Value,
                      EndsRequired =
                        detailsEnds != null ? (bool)detailsEnds.Attribute("required") : false
                  },
                  UoM = item.Element("uom")?.Value
              }).ToList();
        }

        public static List<string> GetAllItemsId()
        {
            return repos.ItemsID;
        }

        public static List<ItemIdView> GetAllItemsBrief()
        {
            return repos.ItemIdViews;
        }

        public static List<ItemIdView> GetAllItemsBrief(string filterId)
        {
            return repos.ItemIdViews.Where(id => id.ID.Contains(filterId)).ToList();
        }

        /// <summary>
        /// Returns a list of item view object.
        /// </summary>
        /// <returns></returns>
        public static List<ItemVO> GetAllItemsVO()
        {
            return repos.ItemsView;
        }

        public static List<ItemVO> GetItemViewObjects(List<Item> items)
        {
            return
                items.Select(item => new ItemVO(item)).ToList();
        }

        public static List<ItemVO> GetFilteredItemsView(string itemId, string itemName, bool? image, string catId)
        {
            return
                (from item in repos.ItemsView
                 let filterId = itemId != string.Empty ? item.ID.Contains(itemId) : true
                 let filterName = itemName != string.Empty ? item.Name.ToUpper().Contains(itemName.ToUpper()) : true
                 let filterImage = FilterItemImage(image, item)
                 let filterCategory = catId == "*" ? true : item.CatID == catId
                 where filterId && filterName && filterImage && filterCategory
                 select item).ToList();
        }

        public static int GetItemsCount()
        {
            return repos.Items.Count();
        }

        public static IItem GetItem(string itemId)
        {
            return
                repos.Items.Where(id => id.ItemID == itemId).FirstOrDefault();
        }

        public static List<ItemVO> DeleteItem(string itemId, XDocument itemsXDoc)
        {
            repos.Items = repos.Items.Where(id => id.ItemID != itemId).ToList();

            // Delete Item Element from the XML Document
            DeleteItemFromXDocument(itemsXDoc, itemId);

            return repos.ItemsView;
        }

        private static void DeleteItemFromXDocument(XDocument itemsXDoc, string itemId)
        {
            XElement deleteItem =
                itemsXDoc.Descendants("item")
                .Where(item => item.Attribute("itemID").Value == itemId).First();

            deleteItem.Remove();
        }

        private static bool FilterItemImage(bool? image, IItemView item)
        {
            //return image == null ? true : (bool)image ? item.Images != null : item.Images == null;
            if (image != null)
            {
                if ((bool)image)
                {
                    return item.Images > 0;
                }
                else
                {
                    return item.Images <= 0;
                }
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Category Object
        public static List<ItemCategory> GetAllCategories()
        {
            List<ItemCategory> categories = new List<ItemCategory>
            {
                new ItemCategory() { CatID = "*", CatName = "<All categories>" }
            };
            categories.AddRange(repos.Categories);
            return categories;
        }

        public static List<ItemCategory> GetCategories()
        {
            return repos.Categories.ToList();
        }

        public static List<ItemCategory> FilterCategoriesById(string filterCatId)
        {
            return repos.Categories.Where(cat => cat.CatID.Contains(filterCatId)).ToList();
        }

        public static List<ItemCategory> FilterCategoriesByName(string filterCatName)
        {
            return
                repos.Categories.Where(cat =>
                cat.CatName.IndexOf(filterCatName, StringComparison.OrdinalIgnoreCase) != -1)
                .ToList();
        }

        public static string GetCategoryName(string catId)
        {
            return
                (from cat in repos.Categories where cat.CatID == catId select cat.CatName)
                .FirstOrDefault();
        }

        private static List<ItemCategory> _GetItemCategories(XDocument itemXDoc)
        {
            return
                (from cat in itemXDoc.Descendants("category")
                 select new ItemCategory()
                 {
                     CatID = cat.Attribute("catID").Value,
                     CatName = cat.Attribute("name").Value
                 }).ToList();
        }
        #endregion

        #region Specs Object
        internal static List<Specs> GetSpecs()
        {
            return repos.SpecsList.ToList();
        }

        public static List<Spec> GetSpecsItems(string specsId)
        {
            return
                (from specs in repos.SpecsList
                 where specs.ID == specsId
                 select specs.SpecItems).FirstOrDefault();
        }

        public static List<SpecListEntry> GetSpecListEntries(string specsId, int specIndex)
        {
            return
                (from specItem in (from specs in repos.SpecsList
                                   where specs.ID == specsId
                                   select specs.SpecItems).FirstOrDefault()
                 where specItem.Index == specIndex
                 select specItem.ListEntries).FirstOrDefault();
        }

        internal static List<BasicView> GetSpecsBrief()
        {
            return repos.SpecsList.Select(sp => sp.GetBasicView()).ToList();
        }

        public static List<string> GetAllSpecsId()
        {
            return repos.SpecsIdList;
        }

        private static List<Specs> _GetSpecs(XDocument specsXDoc)
        {
            return
                (from specs in specsXDoc.Descendants("specs")
                 select new Specs()
                 {
                     ID = specs.Attribute("specsID").Value,
                     Name = specs.Attribute("name").Value,
                     TextPattern = specs.Attribute("textPattern").Value,
                     SpecItems = specs.Descendants("specsItem").Select(spec =>
                     new Spec((XElement)spec.FirstNode)
                     {
                         Index = (int)spec.Attribute("index"),
                         Name = spec.Attribute("name").Value,
                         ValuePattern = spec.Attribute("valuePattern").Value
                     }).ToList()
                 }).ToList();
        }
        #endregion

        #region Size Groups Object
        //public static List<SizeGroup> GetSizeGroups()
        //{
        //    return repos.SizeGroups.ToList();
        //}

        public static List<string> GetSizeGroupsId()
        {
            return repos.SizeGroupIdList;
        }

        /// <summary>
        /// Gets view object list of size groups.
        /// </summary>
        /// <returns></returns>
        public static List<SizeGroupView> GetSizeGroupsVO()
        {
            return repos.SizeGroups
                .Select(grp => new SizeGroupView(grp)).ToList();
        }

        private static List<SizeGroup> _GetSizeGroups(XDocument sizeGroupXDoc)
        {
            return
                (from sg in sizeGroupXDoc.Descendants("group")
                 let list = sg.Element("altLists").HasElements ? sg.Element("altLists").Elements("listID").Select(l => l.Value).ToList() : null
                 let customId = sg.Element("customSizeDataID").Value
                 select new SizeGroup()
                 {
                     ID = sg.Attribute("groupID").Value,
                     Name = sg.Attribute("groupName").Value,
                     DefaultListID = sg.Element("defaultListID").Value,
                     AltIdList = list,
                     CustomSize = customId != string.Empty ? customId : null
                 }).ToList();
        }
        #endregion

        #region Size Lists Object
        public static List<BasicListView> GetSizes()
        {
            return repos.SizesList.ToList();
        }

        /// <summary>
        /// Get a list of entries for the given list ID.
        /// </summary>
        /// <param name="listId">The ID of the list to retrive its entries.</param>
        /// <returns></returns>
        public static List<string> GetSizeListEntries(string listId)
        {
            return
                (from list in repos.SizesList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }

        private static List<BasicListView> _GetSizes(XDocument sizesXDoc)
        {
            return
                (from list in sizesXDoc.Descendants("sizeList")
                 select new BasicListView()
                 {
                     ID = list.Attribute("listID").Value,
                     Name = list.Attribute("name").Value,
                     List = list.Descendants("size").Select(entry => entry.Value).ToList()
                 }).ToList();
        }

        private static List<BasicListView> DeleteSizeList(string listId, XDocument sizesListXDoc)
        {
            repos.SizesList = repos.SizesList.Where(list => list.ID != listId).ToList();

            // Delete Size List Element from the XML Document
            DeleteFieldListFromXDocument(sizesListXDoc, listId, "sizeList");

            return repos.SizesList.ToList();
        }
        #endregion

        #region Brand List Object
        public static List<BasicListView> GetBrands()
        {
            return repos.BrandsList.ToList();
        }

        public static List<string> GetBrandListsId() => repos.BrandsIdList;

        private static List<BasicListView> _GetBrands(XDocument brandsXDoc)
        {
            return
                (from brands in brandsXDoc.Descendants("brandList")
                 select new BasicListView()
                 {
                     ID = brands.Attribute("listID").Value,
                     Name = brands.Attribute("name").Value,
                     List = brands.Descendants("brand").Select(brand => brand.Value).ToList()
                 }).ToList();
        }

        private static List<BasicListView> DeleteBrandList(string listId, XDocument brandsListXDoc)
        {
            repos.BrandsList = repos.BrandsList.Where(list => list.ID != listId).ToList();

            // Delete Brand List Element from the XML Document
            DeleteFieldListFromXDocument(brandsListXDoc, listId, "brandList");

            return repos.BrandsList.ToList();
        }
        #endregion

        #region Ends List Object
        public static List<BasicListView> GetEnds()
        {
            return repos.EndsList.ToList();
        }

        public static List<string> GetEndsListsId() => repos.EndsIdList;

        private static List<BasicListView> _GetEnds(XDocument endsXDoc)
        {
            return
                (from ends in endsXDoc.Descendants("endsList")
                 select new BasicListView()
                 {
                     ID = ends.Attribute("listID").Value,
                     Name = ends.Attribute("name").Value,
                     List = ends.Descendants("end").Select(end => end.Value).ToList()
                 }).ToList();
        }

        private static List<BasicListView> DeleteEndsList(string listId, XDocument endsXDoc)
        {
            repos.EndsList = repos.EndsList.Where(list => list.ID != listId).ToList();

            // Delete Ends List Element from the XML Document
            DeleteFieldListFromXDocument(endsXDoc, listId, "endsList");

            return repos.EndsList.ToList();
        }
        #endregion

        public static bool IsDuplicateItemId(string itemId)
        {
            return repos.ItemsID.Contains(itemId);
        }
    }
}