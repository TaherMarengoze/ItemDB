
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
                SpecsList = GlobalsX.reader.GetSpecs().ToList(),
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
        public static void UpdateSpecsRepos() => appCache.SpecsList = GlobalsX.reader.GetSpecs().ToList();
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
        public static void Save()
        {
            GlobalsX.context.Save();
        }

        public static void Save(ContextEntity context)
        {
            GlobalsX.context.Save(context);
        }
        #endregion

        public static void ValidateItemRawData(IItemRawData data)
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
                 let filterId = itemId != string.Empty ? item.ID.Contains(itemId) : true
                 let filterName = itemName != string.Empty ? item.Name.ToUpper().Contains(itemName.ToUpper()) : true
                 let filterImage = FilterItemImage(image, item)
                 let filterCategory = catId == "*" ? true : item.CatID == catId
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

        #region Specs Object
        // Context

        /// <summary>
        /// Gets a list of <see cref="Specs"/> object.
        /// </summary>
        /// <returns></returns>
        public static List<ISpecs> GetSpecsList() => appCache.SpecsList;

        /// <summary>
        /// Gets a list of ID of the <see cref="Specs"/> object.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetSpecsIdList() => appCache.SpecsIdList;

        // Entity
        public static void AddSpecs(ISpecs specs)
        {
            // Add to data source
            GlobalsX.specsRepo.AddSpecs(specs);

            // Updating will automatically refresh the local cache
            UpdateSpecsRepos();
        }
        public static void UpdateSpecs(string refId, ISpecs content)
        {
            // Modify local cache (no need because changes are made on local cache)
            // Modify data source
            GlobalsX.specsRepo.UpdateSpecs(refId, content);
        }
        public static void DeleteSpecs(string specsId)
        {
            // Delete from local cache
            appCache.SpecsList = appCache.SpecsList.Where(specs => specs.ID != specsId).ToList();

            // Delete from data source
            GlobalsX.specsRepo.DeleteSpecs(specsId);
        }
        public static ISpecs GetSpecs(string specsId) => appCache.SpecsList.Find(specs => specs.ID == specsId);

        // Entity Operations
        public static ISpec GetSpecsItem(string specsId, int specIndex)
        {
            ISpecs specs =
                appCache.SpecsList.Find(sp => sp.ID == specsId);

            return
                specs.SpecItems.Find(spec => spec.Index == specIndex);
        }

        public static ISpec GetSpecsItem(ISpecs specs, int specIndex)
        {
            return
                specs.SpecItems
                .FirstOrDefault(si => si.Index == specIndex);
        }

        // Entity Manipulation



        public static IEnumerable<ISpec> GetSpecsItems(string specsId)
        {
            return
                (from specs in appCache.SpecsList
                 where specs.ID == specsId
                 select specs.SpecItems).FirstOrDefault();
        }

        public static List<ISpecListEntry> GetSpecListEntries(string specsId, int specIndex)
        {
            List<ISpec> specsItems =
                (from specs in appCache.SpecsList
                 where specs.ID == specsId
                 select specs.SpecItems).FirstOrDefault();

            return
                (from spec in specsItems
                 where spec.Index == specIndex
                 select spec.ListEntries).FirstOrDefault();
        }

        #endregion

        #region Size Groups Object
        /// <summary>
        /// Retrieves the list of <see cref="SizeGroup"/> from the cache.
        /// </summary>
        /// <returns></returns>
        public static List<SizeGroup> GetSizeGroups()
        {
            return
                appCache.SizeGroups
                .Cast<SizeGroup>()
                .ToList();
        }

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

        public static void UpdateSizeGroup(string refId, ISizeGroup group)
        {
            // Update data source
            GlobalsX.sizeGroupRepo.Update(refId, group);
        }

        /// <summary>
        /// Deletes a specific <see cref="SizeGroup"/>.
        /// </summary>
        /// <param name="groupId">The ID of the <see cref="SizeGroup"/> object to delete.</param>
        public static void DeleteSizeGroup(string groupId)
        {
            // Delete from local cache
            appCache.SizeGroups = appCache.SizeGroups.Where(group => group.ID != groupId).ToList();

            // Delete from data source
            GlobalsX.sizeGroupRepo.Delete(groupId);

        }

        public static ISizeGroup GetSizeGroup(string groupId)
        {
            return
                appCache.SizeGroups
                .Find(group => group.ID == groupId);

            appCache.SizeGroups.Cast<SizeGroup>().ToList()
                .Find(group => group.ID == groupId);
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
        public static void EditFieldList(FieldType field, string refId, IBasicList content)
        {
            Delegators.FieldActionCallback(field,
                delegate { EditSizeList(refId, content); },
                delegate { EditBrandList(refId, content); },
                delegate { EditEndsList(refId, content); });
        }
        public static void DeleteFieldList(FieldType field, string listId)
        {
            Delegators.FieldActionCallback(field,
            DeleteSizeList, DeleteBrandList, DeleteEndsList,
            listId);
        }

        public static IBasicList GetFieldList(FieldType field, string listId)
        {
            return (IBasicList)
                Delegators.FieldFunctionCallback(field,
                delegate { return GetSizeList(listId); },
                delegate { return GetBrandList(listId); },
                delegate { return GetEndsList(listId); });
        }

        public static ObservableCollection<string> FieldListGetEntries(FieldType field, string listId)
        {
            return (ObservableCollection<string>)
                Delegators.FieldFunctionCallback(field,
                SizeListGetEntries, BrandListGetEntries, EndsListGetEntries,
                listId);
        }
        public static void FieldListAddEntry(FieldType field, string listId, string entry)
        {
            Delegators.FieldActionCallback(field,
                SizeListAddEntry, BrandListAddEntry, EndsListAddEntry,
                listId, entry);
        }

        public static void FieldListEditEntry(FieldType field, string listId, string oldValue, string newValue)
        {
            Delegators.FieldActionCallback(field,
                SizeListEditEntry, BrandListEditEntry, EndsListEditEntry,
                listId, oldValue, newValue);
        }

        public static void FieldListDeleteEntry(FieldType field, string listId, string entry)
        {
            Delegators.FieldActionCallback(field,
                SizeListDeleteEntry, BrandListDeleteEntry, EndsListDeleteEntry,
                listId, entry);
        }

        public static void FieldListMoveEntry(FieldType field, string listId, string entryValue, ShiftDirection direction)
        {
            Delegators.FieldActionCallback(field,
                delegate { SizeListMoveEntry(listId, entryValue, direction); },
                delegate { BrandListMoveEntry(listId, entryValue, direction); },
                delegate { EndsListMoveEntry(listId, entryValue, direction); });
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

        public static List<BasicListView> GetSizesExclude(string excludeId)
        {
            return
                appCache.SizesList
                .Where(list => list.ID != excludeId)
                .Cast<BasicListView>()
                .ToList();
        }

        private static IEnumerable<string> GetSizesId() => appCache.SizesIdList;
        public static List<string> GetSizesIdExclude(List<string> excludeIdList) => (from list in appCache.SizesList where !excludeIdList.Contains(list.ID) select list.ID).ToList();

        // Entity
        private static void AddSizeList(IBasicList content)
        {
            // Add to data source
            GlobalsX.sizesRepo.AddList(content);

            // Updating will automatically refresh the local cache
            UpdateSizesRepos();
        }
        private static void EditSizeList(string refId, IBasicList content)
        {
            // Modify local cache (no need because changes are made on local cache)
            // Modify data source
            GlobalsX.sizesRepo.UpdateList(refId, content);
        }
        private static void DeleteSizeList(string listId)
        {
            // Delete from local cache
            appCache.SizesList = appCache.SizesList.Where(list => list.ID != listId).ToList();

            // Delete from data source
            GlobalsX.sizesRepo.DeleteList(listId);
        }
        private static IBasicList GetSizeList(string listId) => appCache.SizesList.Find(list => list.ID == listId);

        // Entity Manipulation
        private static ObservableCollection<string> SizeListGetEntries(string listId)
        {
            IEnumerable<ObservableCollection<string>> qry =
                from list in appCache.SizesList where list.ID == listId select list.List;

            return qry.FirstOrDefault();
        }
        private static void SizeListAddEntry(string listId, string entry)
        {
            // Add to local cache
            appCache.SizesList.Where(list => list.ID == listId).First().List.Add(entry);

            // Add to data source
            GlobalsX.sizeManipulator.AddEntry(listId, entry);
        }
        private static void SizeListEditEntry(string listId, string oldValue, string newValue)
        {
            // Edit local cache
            int index = SizeListGetEntries(listId).IndexOf(oldValue);
            SizeListGetEntries(listId)[index] = newValue;

            // Edit data source
            GlobalsX.sizeManipulator.EditEntry(listId, oldValue, newValue);
        }
        private static void SizeListDeleteEntry(string listId, string entry)
        {
            // Remove from local cache
            GetSizeList(listId).List.Remove(entry);

            // Remove from data source
            GlobalsX.sizeManipulator.DeleteEntry(listId, entry);
        }
        private static void SizeListMoveEntry(string listId, string entryValue, ShiftDirection direction)
        {
            // Move entry in local cache
            ObservableCollection<string> listEntries =
                appCache.SizesList.Where(list => list.ID == listId)
                .FirstOrDefault().List;

            int n = listEntries.IndexOf(entryValue);
            listEntries.Move(n, n + (int)direction);

            // Move entry in data source
            GlobalsX.sizeManipulator.MoveEntry(listId, entryValue, direction);
        }

        // Others
        public static List<string> GetTs()
        {
            return null;
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
        private static void EditBrandList(string refId, IBasicList content)
        {
            // Modify local cache (no need because changes are made on local cache)
            // Modify data source
            GlobalsX.brandsRepo.UpdateList(refId, content);
        }
        private static void DeleteBrandList(string listId)
        {
            // Delete from local cache
            appCache.BrandsList = appCache.BrandsList.Where(list => list.ID != listId).ToList();

            // Delete from data source
            GlobalsX.brandsRepo.DeleteList(listId);
        }
        private static IBasicList GetBrandList(string listId) => appCache.BrandsList.Find(list => list.ID == listId);
        private static ObservableCollection<string> BrandListGetEntries(string listId)
        {
            return
                (from list in appCache.BrandsList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }
        private static void BrandListAddEntry(string listId, string entry)
        {
            // Add to local cache
            appCache.BrandsList.Where(list => list.ID == listId).First().List.Add(entry);

            // Add to data source
            GlobalsX.brandManipulator.AddEntry(listId, entry);
        }
        private static void BrandListEditEntry(string listId, string oldValue, string newValue)
        {
            // Edit local cache
            int index = BrandListGetEntries(listId).IndexOf(oldValue);
            BrandListGetEntries(listId)[index] = newValue;

            // Edit data source
            GlobalsX.brandManipulator.EditEntry(listId, oldValue, newValue);
        }
        private static void BrandListDeleteEntry(string listId, string entry)
        {
            // Remove from local cache
            GetBrandList(listId).List.Remove(entry);

            // Remove from data source
            GlobalsX.brandManipulator.DeleteEntry(listId, entry);
        }
        private static void BrandListMoveEntry(string listId, string entryValue, ShiftDirection direction)
        {
            // Move entry in local cache
            ObservableCollection<string> listEntries =
                appCache.BrandsList.Where(list => list.ID == listId)
                .FirstOrDefault().List;

            int n = listEntries.IndexOf(entryValue);
            listEntries.Move(n, n + (int)direction);

            // Move entry in data source
            GlobalsX.brandManipulator.MoveEntry(listId, entryValue, direction);
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
        private static void EditEndsList(string refId, IBasicList content)
        {
            // Modify local cache (no need because changes are made on local cache)
            // Modify data source
            GlobalsX.endsRepo.UpdateList(refId, content);
        }
        private static void DeleteEndsList(string listId)
        {
            // Delete from local cache
            appCache.EndsList = appCache.EndsList.Where(list => list.ID != listId).ToList();

            // Delete from data source
            GlobalsX.endsRepo.DeleteList(listId);
        }
        private static IBasicList GetEndsList(string listId) => appCache.EndsList.Find(list => list.ID == listId);
        private static ObservableCollection<string> EndsListGetEntries(string listId)
        {
            return
                (from list in appCache.EndsList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }
        private static void EndsListAddEntry(string listId, string entry)
        {
            // Add to local cache
            appCache.EndsList.Where(list => list.ID == listId).First().List.Add(entry);

            // Add to data source
            GlobalsX.endsManipulator.AddEntry(listId, entry);
        }
        private static void EndsListEditEntry(string listId, string oldValue, string newValue)
        {
            // Edit local cache
            int index = EndsListGetEntries(listId).IndexOf(oldValue);
            EndsListGetEntries(listId)[index] = newValue;

            // Edit data source
            GlobalsX.endsManipulator.EditEntry(listId, oldValue, newValue);
        }
        private static void EndsListDeleteEntry(string listId, string entry)
        {
            // Remove from local cache
            GetEndsList(listId).List.Remove(entry);

            // Remove from data source
            GlobalsX.endsManipulator.DeleteEntry(listId, entry);
        }
        private static void EndsListMoveEntry(string listId, string entryValue, ShiftDirection direction)
        {
            // Move entry in local cache
            ObservableCollection<string> listEntries =
                appCache.EndsList.Where(list => list.ID == listId)
                .FirstOrDefault().List;

            int n = listEntries.IndexOf(entryValue);
            listEntries.Move(n, n + (int)direction);

            // Move entry in data source
            GlobalsX.endsManipulator.MoveEntry(listId, entryValue, direction);
        }
        #endregion

        #region Custom Sizes
        public static List<string> GetCustomSizes() => appCache.CustomSizes;
        #endregion

        #region Custom Specs
        public static List<string> GetCustomSpecs() => appCache.CustomSpecs;
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