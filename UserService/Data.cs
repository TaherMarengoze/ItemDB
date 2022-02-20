
using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Factory;
using CoreLibrary.Interfaces;
using CoreLibrary.Models;
using DataCache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UserService
{
    public static class Data
    {
        /// <summary>
        /// Application local cache.
        /// </summary>
        public static Cache appCache;

        public static void InitializeRepos()
        {
            appCache = new Cache()
            {
                Items = GlobalsX.reader.GetItems(),
                Categories = GlobalsX.reader.GetCategories(),
                SizeGroups = GlobalsX.reader.GetSizeGroups().ToList(),
                SizesList = GlobalsX.reader.GetSizes().ToList(),
                BrandsList = GlobalsX.reader.GetBrands().ToList(),
                EndsList = GlobalsX.reader.GetEnds().ToList(),
                CustomSpecs = GlobalsX.reader.GetCustomSpecs().ToList(),
                CustomSizes = GlobalsX.reader.GetCustomSizes().ToList()
            };
        }

        #region Updater code
        private static void UpdateItemsRepos() => appCache.Items = GlobalsX.reader.GetItems();
        private static void UpdateCategoriesRepos() => appCache.Categories = GlobalsX.reader.GetCategories();
        public static void UpdateSizeGroupsRepos()
        {
            appCache.SizeGroups = GlobalsX.reader
                .GetSizeGroups().ToList();
        }

        private static void UpdateSizesRepos()
        {
            appCache.SizesList = GlobalsX.reader
                .GetSizes().ToList();
        }

        private static void UpdateBrandsRepos()
        {
            appCache.BrandsList = GlobalsX.reader
                .GetBrands().ToList();
        }

        private static void UpdateEndsRepos()
        {
            appCache.EndsList = GlobalsX.reader
                .GetEnds().ToList();
        }
        #endregion

        #region Context Saving
        public static void Save(ContextEntity context)
        {
            GlobalsX.context.Save(context);
        }
        #endregion
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
            GlobalsX.itemsRepo.CreateItem(ProcessItemRawData(data));

            // Update Items List and Categories
            UpdateItemsRepos();
            UpdateCategoriesRepos();
        }

        public static void ModifyItem(string refId, IItemRawData data)
        {
            GlobalsX.itemsRepo.UpdateItem(refId, ProcessItemRawData(data));

            // Update Items List and Categories
            UpdateItemsRepos();
            UpdateCategoriesRepos();
        }

        public static List<ItemVO> DeleteItem(string itemId)
        {
            GlobalsX.itemsRepo.DeleteItem(itemId);

            // Set the Items property to itself but excluding the item to be
            // deleted; so that ItemsView property is updated automatically
            appCache.Items = appCache.Items.Where(id => id.ItemID != itemId).ToList();

            return appCache.ItemsView;
        }

        #endregion

        #region Item Object

        public static List<ItemIdView> GetAllItemsBrief()
        {
            return appCache.ItemIdViews;
        }

        public static List<ItemIdView> GetAllItemsBrief(string filterId)
        {
            return appCache.ItemIdViews.Where(id => id.ID.Contains(filterId)).ToList();
        }

        /// <summary>
        /// Returns a list of item view object.
        /// </summary>
        /// <returns></returns>
        public static List<ItemVO> GetAllItemsVO()
        {
            return appCache.ItemsView;
        }

        public static List<ItemVO> GetFilteredItemsView(string itemId, string itemName, bool? image, string catId)
        {
            return
                (from item in appCache.ItemsView
                 //let filterId = itemId != string.Empty ? item.ID.Contains(itemId) : true
                 //let filterName = itemName != string.Empty ? item.Name.ToUpper().Contains(itemName.ToUpper()) : true
                 //let filterCategory = catId == "*" ? true : item.CatID == catId
                 let filterId = itemId == string.Empty || item.ID.Contains(itemId)
                 let filterName = itemName == string.Empty || item.Name.ToUpper().Contains(itemName.ToUpper())
                 let filterImage = FilterItemImage(image, item)
                 let filterCategory = catId == "*" || item.CatID == catId
                 where filterId && filterName && filterImage && filterCategory
                 select item).ToList();
        }

        public static int GetItemsCount()
        {
            return appCache.Items.Count();
        }

        public static IItem GetItem(string itemId)
        {
            return
                appCache.Items.Where(id => id.ItemID == itemId).FirstOrDefault();
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
                new ItemCategory() { ID = "*", Name = "<All categories>" }
            };
            categories.AddRange(appCache.Categories.Cast<ItemCategory>());
            return categories;
        }

        public static List<ItemCategory> GetCategories()
        {
            return
                appCache.Categories
                .Cast<ItemCategory>()
                .ToList();
        }

        public static List<ItemCategory> FilterCategoriesById(string filterCatId)
        {
            return
                appCache.Categories
                .Where(cat => cat.ID.Contains(filterCatId))
                .Cast<ItemCategory>()
                .ToList();
        }

        public static List<ItemCategory> FilterCategoriesByName(string filterCatName)
        {
            return
                appCache.Categories
                .Where(cat =>cat.Name.IndexOf(filterCatName, StringComparison.OrdinalIgnoreCase) != -1)
                .Cast<ItemCategory>()
                .ToList();
        }

        public static string GetCategoryName(string catId)
        {
            return
                (from cat in appCache.Categories where cat.ID == catId select cat.Name)
                .FirstOrDefault();
        }
        #endregion

        #region Size Groups Object
        public static List<string> GetSizeGroupsId()
        {
            return appCache.SizeGroupIdList;
        }

        public static IEnumerable<BasicView> GetSizeGroupsBasic()
        {
            return
                appCache.SizeGroups.Select(grp => new BasicView(grp.ID, grp.Name));
        }

        /// <summary>
        /// Gets view object list of size groups.
        /// </summary>
        /// <returns></returns>
        public static List<SizeGroupView> GetSizeGroupsVO()
        {
            return appCache.SizeGroups
                .Select(grp => new SizeGroupView(grp)).ToList();
        }

        public static void AddSizeGroup(ISizeGroup group)
        {
            // Add to local cache


            // Add to data source
            GlobalsX.sizeGroupRepo.Create(group);

            // Update to refresh local cache
            UpdateSizeGroupsRepos();
        }

        #endregion

        #region Fields (Sizes, Brands or Ends)
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

        public static IEnumerable<string> GetFieldIds(FieldType field)
        {
            return (IEnumerable<string>)
                Delegators.FieldFunctionCallback(field,
                GetSizesId, GetBrandsId, GetEndsId);
        }

        public static void AddFieldList(FieldType field, IBasicList fieldList)
        {
            Delegators.FieldActionCallback(field,
            AddSizeList, AddBrandList, AddEndsList,
            fieldList);
        }
        
        public static ObservableCollection<string> FieldListGetEntries(FieldType field, string listId)
        {
            return (ObservableCollection<string>)
                Delegators.FieldFunctionCallback(field,
                SizeListGetEntries, BrandListGetEntries, EndsListGetEntries,
                listId);
        }

        #endregion

        #region Size Lists
        // Context
        private static List<BasicListView> GetSizes()
        {
            return
                appCache.SizesList
                .Cast<BasicListView>()
                .ToList();
        }

        private static IEnumerable<string> GetSizesId() => appCache.SizesIdList;
        
        // Entity
        private static void AddSizeList(IBasicList content)
        {
            // Add to data source
            GlobalsX.sizesRepo.AddList(content);

            // Updating will automatically refresh the local cache
            UpdateSizesRepos();
        }
        // Entity Manipulation
        private static ObservableCollection<string> SizeListGetEntries(string listId)
        {
            IEnumerable<ObservableCollection<string>> qry =
                from list in appCache.SizesList where list.ID == listId select list.List;

            return qry.FirstOrDefault();
        }
        #endregion

        #region Brand Lists
        private static List<BasicListView> GetBrands()
        {
            return
                appCache.BrandsList
                .Cast<BasicListView>()
                .ToList();
        }

        private static IEnumerable<string> GetBrandsId() => appCache.BrandsIdList;

        private static void AddBrandList(IBasicList content)
        {
            // Add to data source
            GlobalsX.brandsRepo.AddList(content);

            // Updating will automatically refresh the local cache
            UpdateBrandsRepos();
        }
        private static ObservableCollection<string> BrandListGetEntries(string listId)
        {
            return
                (from list in appCache.BrandsList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }
        #endregion

        #region Ends Lists
        private static List<BasicListView> GetEnds()
        {
            return
                appCache.EndsList
                .Cast<BasicListView>()
                .ToList();
        }

        private static IEnumerable<string> GetEndsId() => appCache.EndsIdList;
        private static void AddEndsList(IBasicList content)
        {
            GlobalsX.endsRepo.AddList(content);
            UpdateEndsRepos();
        }

        private static ObservableCollection<string> EndsListGetEntries(string listId)
        {
            return
                (from list in appCache.EndsList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }
        #endregion

        #region Custom Sizes
        //public static List<string> GetCustomSizes() => appCache.CustomSizes;
        #endregion

        #region Custom Specs
        //public static List<string> GetCustomSpecs() => appCache.CustomSpecs;
        #endregion

        public static bool IsDuplicateItemId(string itemId)
        {
            return appCache.ItemsID.Contains(itemId);
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
                appCache.SizesList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }

        public static List<BasicView> BrandsBasicView()
        {
            return
                appCache.BrandsList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }

        public static List<BasicView> EndsBasicView()
        {
            return
                appCache.EndsList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }
        #endregion

    }
}