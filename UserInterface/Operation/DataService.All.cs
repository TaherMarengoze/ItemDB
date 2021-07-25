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

        #region Updater code
        private static void UpdateItems()
        {
            repos.Items = Program.reader.GetItems();
        }

        private static void UpdateCategories()
        {
            repos.Categories = Program.reader.GetCategories();
        }

        public static void UpdateSpecs()
        {
            repos.SpecsList = Program.reader.GetSpecs();
        }

        public static void UpdateSizeGroups()
        {
            repos.SizeGroups = Program.reader.GetSizeGroups();
        }

        private static void UpdateSizes()
        {
            repos.SizesList = Program.reader.GetSizes();
        }

        private static void UpdateBrands()
        {
            repos.BrandsList = Program.reader.GetBrands();
        }

        private static void UpdateEnds()
        {
            repos.EndsList = Program.reader.GetEnds();
        }
        #endregion

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
        
        #region Interface Implementation
        public static void AddNewItem(IItemRawData data)
        {
            Program.modifier.AddItem(ProcessItemRawData(data));
            
            // Update Items List and Categories
            UpdateItems();
            UpdateCategories();
        }
        
        public static void ModifyItem(string refId, IItemRawData data)
        {
            Program.modifier.ModifyItem(refId, data);

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories();
        }
        #endregion

        /// <summary>
        /// Modifies an item in the Items <see cref="XDocument"/>.
        /// </summary>
        /// <param name="itemsXDoc">The <see cref="XDocument"/> containing the item being modified.</param>
        /// <param name="existingId">The ID of the item being modified.</param>
        /// <param name="data">The input data to replace the modified item data.</param>
        public static void ModifyItemXDocument(XDocument itemsXDoc, string existingId, IItemRawData data)
        {
            #region MyRegion
            XElement xItem = itemsXDoc.Descendants("item")
                    .Where(elem => elem.Attribute("itemID").Value == existingId)
                    .First();

            xItem.SetAttributeValue("itemID", data.ItemID);
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
            #endregion

            // Process Item Category
            ProcessItemCategory(itemsXDoc, existingId, data, xItem);

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories();
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

        //internal static List<BasicView> GetSpecsBrief()
        //{
        //    return repos.SpecsList.Select(sp => sp.GetBasicView()).ToList();
        //}

        public static List<string> GetAllSpecsId()
        {
            return repos.SpecsIdList;
        }
        #endregion

        #region Size Groups Object

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