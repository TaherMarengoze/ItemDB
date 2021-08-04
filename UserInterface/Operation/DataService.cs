
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface.Operation
{
    using Enums;
    using Factory;
    using Interfaces;
    using Models;

    public static partial class DataService
    {
        /// <summary>
        /// The <see cref="DataService"/> local cache for <see cref="DataRepos"/> data.
        /// </summary>
        public static DataRepos repos;

        /// <summary>
        /// Caches a new instance of <see cref="DataRepos"/> and set its members.
        /// </summary>
        public static void InitializeRepos()
        {
            repos = new DataRepos()
            {
                Items = Program.reader.GetItems(),
                Categories = Program.reader.GetCategories(),
                SpecsList = Program.reader.GetSpecs(),
                SizeGroups = Program.reader.GetSizeGroups(),
                SizesList = Program.reader.GetSizes().ToList(),
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
            repos.SizesList = Program.reader.GetSizes().ToList();
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
            Program.itemModifier.AddItem(ProcessItemRawData(data));

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories();
        }

        public static void ModifyItem(string refId, IItemRawData data)
        {
            Program.itemModifier.ModifyItem(refId, ProcessItemRawData(data));

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories();
        }

        public static List<ItemVO> DeleteItem(string itemId)
        {
            Program.itemModifier.DeleteItem(itemId);

            // Set the Items property to itself but excluding the item to be
            // deleted; so that ItemsView property is updated automatically
            repos.Items = repos.Items.Where(id => id.ItemID != itemId).ToList();

            return repos.ItemsView;
        }

        public static void AddFieldList(FieldType fieldType, IBasicList fieldList)
        {
            Delegators.FieldActionCallback(fieldType,
                delegate { AddSizesList(fieldList); },
                null,
                null);
        }

        public static void FieldListAddEntry(FieldType field, string listId, string entry)
        {

        }

        public static void SizeListAddEntry(string listId, string entry)
        {
            repos.SizesList.Where(list => list.ID == listId).First().List.Add(entry);
        }

        // Size
        public static void AddSizesList(IBasicList content)
        {
            Program.sizesRepo.AddFieldList(content);
            UpdateSizes();
        }

        /// <summary>
        /// Returns a specific size list object by its ID
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static IBasicList GetSizeList(string listId)
        {
            return repos.SizesList.Find(list => list.ID == listId);
        }
        #endregion

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

        public static IEnumerable<ISpec> GetSpecsItems(string specsId)
        {
            return
                (from specs in repos.SpecsList
                 where specs.ID == specsId
                 select specs.SpecItems).FirstOrDefault();
        }

        public static List<ISpecListEntry> GetSpecListEntries(string specsId, int specIndex)
        {
            List<ISpec> specsItems =
                (from specs in repos.SpecsList
                 where specs.ID == specsId
                 select specs.SpecItems).FirstOrDefault();

            return
                (from spec in specsItems
                 where spec.Index == specIndex
                 select spec.ListEntries).FirstOrDefault();
        }

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
        public static List<BasicListView> GetSizes() => repos.SizesList;

        /// <summary>
        /// Get a list of entries for the given list ID.
        /// </summary>
        /// <param name="listId">The ID of the list to retrieve its entries.</param>
        /// <returns></returns>
        public static List<string> GetSizeListEntries(string listId)
        {
            return
                (from list in repos.SizesList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }

        private static List<BasicListView> DeleteSizeList(string listId)
        {
            Program.sizesRepo.DeleteFieldList(listId);
            repos.SizesList = repos.SizesList.Where(list => list.ID != listId).ToList();

            return repos.SizesList.ToList();
        }
        #endregion
        #region Brand List Object
        public static List<BasicListView> GetBrands()
        {
            return repos.BrandsList.ToList();
        }

        public static List<string> GetBrandListsId()
        {
            return repos.BrandsIdList;
        }

        private static List<BasicListView> DeleteBrandList(string listId)
        {
            repos.BrandsList = repos.BrandsList.Where(list => list.ID != listId).ToList();

            // Delete Brand List Element from the XML Document
            DeleteFieldListFromXDocument(Program.xDataDocs.Brands, listId, "brandList");

            return repos.BrandsList.ToList();
        }
        #endregion
        #region Ends List Object
        public static List<BasicListView> GetEnds()
        {
            return repos.EndsList.ToList();
        }

        public static List<string> GetEndsListsId() => repos.EndsIdList;

        private static List<BasicListView> DeleteEndsList(string listId)
        {
            repos.EndsList = repos.EndsList.Where(list => list.ID != listId).ToList();

            // Delete Ends List Element from the XML Document
            DeleteFieldListFromXDocument(Program.xDataDocs.Ends, listId, "endsList");

            return repos.EndsList.ToList();
        }
        #endregion

        private static void DeleteFieldListFromXDocument(XDocument fieldXDoc, string listId, XName nodeName)
        {
            XElement deleteFieldList =
                fieldXDoc.Descendants(nodeName)
                .Where(list => list.Attribute("listID").Value == listId).First();

            deleteFieldList.Remove();
        }

        public static bool IsDuplicateItemId(string itemId)
        {
            return repos.ItemsID.Contains(itemId);
        }

        public static void UpdateFieldList(FieldType field)
        {
            Delegators.FieldActionCallback(field,
                UpdateSizes, UpdateBrands, UpdateEnds);
        }

        public static void DeleteFieldList(FieldType field, string listId)
        {
            Delegators.FieldActionCallback(field,
                delegate { DeleteSizeList(listId); },
                delegate { DeleteBrandList(listId); },
                delegate { DeleteEndsList(listId); });
        }

        #region Basic View
        public static object GetFieldBasicView(FieldType field)
        {
            return Delegators.FieldFunctionCallback(field,
                 SizesBasicView, BrandsBasicView, EndsBasicView);
        }

        public static List<BasicView> SizesBasicView()
        {
            return
                repos.SizesList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }

        public static List<BasicView> BrandsBasicView()
        {
            return
                repos.BrandsList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }

        public static List<BasicView> EndsBasicView()
        {
            return
                repos.EndsList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }
        #endregion

        /// <summary>
        /// Adds a new field List to data source.
        /// </summary>
        /// <param name="fieldType">The field type is either a SIZE, BRAND or ENDS.</param>
        /// <param name="fieldList">The <see cref="IFieldList"/> object that contains the field list data.</param>
        internal static void AddNewFieldList(FieldType fieldType, IFieldList fieldList)
        {
            //DataCache.AddNewFieldList(fieldType, fieldList);
            //UpdateFieldList(fieldType);
        }

        /// <summary>
        /// Gets a list containing the ID, Name and Entries for the given field type.
        /// </summary>
        /// <param name="field">The field type is either a Size type, Brand type or Ends type.</param>
        /// <returns></returns>
        public static List<BasicListView> GetFieldLists(FieldType field)
        {
            return (List<BasicListView>)
                Delegators.FieldFunctionCallback(field,
                GetSizes, GetBrands, GetEnds);
        }

        public static List<string> GetFieldListsId(FieldType fieldType)
        {
            return (List<string>)
                Delegators.FieldFunctionCallback(fieldType,
                null, GetBrandListsId, GetEndsListsId);
        }
    }
}