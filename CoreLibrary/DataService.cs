
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UserService
{
    using CoreLibrary;
    using CoreLibrary.Enums;
    using CoreLibrary.Factory;
    using CoreLibrary.Interfaces;
    using CoreLibrary.Models;

    public static class DataService
    {
        /// <summary>
        /// Local cache for <see cref="DataRepos"/> members.
        /// </summary>
        public static DataRepos repos;

        /// <summary>
        /// Caches a new instance of <see cref="DataRepos"/> and set its members.
        /// </summary>
        public static void InitializeRepos()
        {
            repos = new DataRepos()
            {
                Items = AppFactory.reader.GetItems(),
                Categories = AppFactory.reader.GetCategories(),
                SpecsList = AppFactory.reader.GetSpecs().ToList(),
                SizeGroups = AppFactory.reader.GetSizeGroups().ToList(),
                SizesList = AppFactory.reader.GetSizes().ToList(),
                BrandsList = AppFactory.reader.GetBrands().ToList(),
                EndsList = AppFactory.reader.GetEnds().ToList(),
                CustomSpecs = AppFactory.reader.GetCustomSpecs().ToList(),
                CustomSizes = AppFactory.reader.GetCustomSizes().ToList()
            };
        }

        #region Updater code
        private static void UpdateItems() => repos.Items = AppFactory.reader.GetItems();
        private static void UpdateCategories() => repos.Categories = AppFactory.reader.GetCategories();
        public static void UpdateSpecs() => repos.SpecsList = AppFactory.reader.GetSpecs().ToList();
        public static void UpdateSizeGroups() => repos.SizeGroups = AppFactory.reader.GetSizeGroups().ToList();
        private static void UpdateSizes() => repos.SizesList = AppFactory.reader.GetSizes().ToList();
        private static void UpdateBrands() => repos.BrandsList = AppFactory.reader.GetBrands().ToList();
        private static void UpdateEnds() => repos.EndsList = AppFactory.reader.GetEnds().ToList();
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
            AppFactory.itemModifier.CreateItem(ProcessItemRawData(data));

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories();
        }

        public static void ModifyItem(string refId, IItemRawData data)
        {
            AppFactory.itemModifier.UpdateItem(refId, ProcessItemRawData(data));

            // Update Items List and Categories
            UpdateItems();
            UpdateCategories();
        }

        public static List<ItemVO> DeleteItem(string itemId)
        {
            AppFactory.itemModifier.DeleteItem(itemId);

            // Set the Items property to itself but excluding the item to be
            // deleted; so that ItemsView property is updated automatically
            repos.Items = repos.Items.Where(id => id.ItemID != itemId).ToList();

            return repos.ItemsView;
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
        // Context

        /// <summary>
        /// Gets a list of <see cref="Specs"/> object.
        /// </summary>
        /// <returns></returns>
        public static List<Specs> GetSpecsList() => repos.SpecsList;

        /// <summary>
        /// Gets a list of ID of the <see cref="Specs"/> object.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetSpecsIdList() => repos.SpecsIdList;

        // Entity

        public static void DeleteSpecs(string specsId)
        {
            // Delete from local cache
            repos.SpecsList = repos.SpecsList.Where(specs => specs.ID != specsId).ToList();

            // Delete from data source
            AppFactory.specsRepo.DeleteSpecs(specsId);
        }

        // Entity Manipulation



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

        #endregion

        #region Size Groups Object
        public static List<SizeGroup> GetSizeGroups() => repos.SizeGroups;
        public static List<string> GetSizeGroupsId()
        {
            return repos.SizeGroupIdList;
        }

        public static IEnumerable<BasicView> GetSizeGroupsBasic()
        {
            return
                repos.SizeGroups.Select(grp => new BasicView(grp.ID, grp.Name));
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

        public static void AddSizeGroup(SizeGroup group)
        {
            // Add to local cache


            // Add to data source
            AppFactory.sizeGroupRepo.Create(group);

            // Update to refresh local cache
            UpdateSizeGroups();
        }
        public static void UpdateSizeGroup(string refId, SizeGroup group)
        {
            // Update data source
            AppFactory.sizeGroupRepo.Update(refId, group);
        }

        /// <summary>
        /// Deletes a specific <see cref="SizeGroup"/>.
        /// </summary>
        /// <param name="groupId">The ID of the <see cref="SizeGroup"/> object to delete.</param>
        public static void DeleteSizeGroup(string groupId)
        {
            // Delete from local cache
            repos.SizeGroups = repos.SizeGroups.Where(group => group.ID != groupId).ToList();

            // Delete from data source
            AppFactory.sizeGroupRepo.Delete(groupId);

        }
        public static SizeGroup GetSizeGroup(string groupId) => repos.SizeGroups.Find(group => group.ID == groupId);
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
        private static List<BasicListView> GetSizes() => repos.SizesList;
        public static List<BasicListView> GetSizesExclude(string excludeId) => repos.SizesList.Where(list => list.ID != excludeId).ToList();
        private static IEnumerable<string> GetSizesId() => repos.SizesIdList;
        public static List<string> GetSizesIdExclude(List<string> excludeIdList) => (from list in repos.SizesList where !excludeIdList.Contains(list.ID) select list.ID).ToList();

        // Entity
        private static void AddSizeList(IBasicList content)
        {
            // Add to data source
            AppFactory.sizesRepo.AddList(content);

            // Updating will automatically refresh the local cache
            UpdateSizes();
        }
        private static void EditSizeList(string refId, IBasicList content)
        {
            // Modify local cache (no need because changes are made on local cache)
            // Modify data source
            AppFactory.sizesRepo.UpdateList(refId, content);
        }
        private static void DeleteSizeList(string listId)
        {
            // Delete from local cache
            repos.SizesList = repos.SizesList.Where(list => list.ID != listId).ToList();

            // Delete from data source
            AppFactory.sizesRepo.DeleteList(listId);
        }
        private static IBasicList GetSizeList(string listId) => repos.SizesList.Find(list => list.ID == listId);

        // Entity Manipulation
        private static ObservableCollection<string> SizeListGetEntries(string listId)
        {
            IEnumerable<ObservableCollection<string>> qry =
                from list in repos.SizesList where list.ID == listId select list.List;

            return qry.FirstOrDefault();
        }
        private static void SizeListAddEntry(string listId, string entry)
        {
            // Add to local cache
            repos.SizesList.Where(list => list.ID == listId).First().List.Add(entry);

            // Add to data source
            AppFactory.sizeManipulator.AddEntry(listId, entry);
        }
        private static void SizeListEditEntry(string listId, string oldValue, string newValue)
        {
            // Edit local cache
            int index = SizeListGetEntries(listId).IndexOf(oldValue);
            SizeListGetEntries(listId)[index] = newValue;

            // Edit data source
            AppFactory.sizeManipulator.EditEntry(listId, oldValue, newValue);
        }
        private static void SizeListDeleteEntry(string listId, string entry)
        {
            // Remove from local cache
            GetSizeList(listId).List.Remove(entry);

            // Remove from data source
            AppFactory.sizeManipulator.DeleteEntry(listId, entry);
        }
        private static void SizeListMoveEntry(string listId, string entryValue, ShiftDirection direction)
        {
            // Move entry in local cache
            ObservableCollection<string> listEntries =
                repos.SizesList.Where(list => list.ID == listId)
                .FirstOrDefault().List;

            int n = listEntries.IndexOf(entryValue);
            listEntries.Move(n, n + (int)direction);

            // Move entry in data source
            AppFactory.sizeManipulator.MoveEntry(listId, entryValue, direction);
        }

        // Others
        public static List<string> GetTs()
        {
            return null;
        }
        #endregion

        #region Brand Lists
        private static List<BasicListView> GetBrands() => repos.BrandsList;
        private static IEnumerable<string> GetBrandsId() => repos.BrandsIdList;
        private static void AddBrandList(IBasicList content)
        {
            // Add to data source
            AppFactory.brandsRepo.AddList(content);

            // Updating will automatically refresh the local cache
            UpdateBrands();
        }
        private static void EditBrandList(string refId, IBasicList content)
        {
            // Modify local cache (no need because changes are made on local cache)
            // Modify data source
            AppFactory.brandsRepo.UpdateList(refId, content);
        }
        private static void DeleteBrandList(string listId)
        {
            // Delete from local cache
            repos.BrandsList = repos.BrandsList.Where(list => list.ID != listId).ToList();

            // Delete from data source
            AppFactory.brandsRepo.DeleteList(listId);
        }
        private static IBasicList GetBrandList(string listId) => repos.BrandsList.Find(list => list.ID == listId);
        private static ObservableCollection<string> BrandListGetEntries(string listId)
        {
            return
                (from list in repos.BrandsList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }
        private static void BrandListAddEntry(string listId, string entry)
        {
            // Add to local cache
            repos.BrandsList.Where(list => list.ID == listId).First().List.Add(entry);

            // Add to data source
            AppFactory.brandManipulator.AddEntry(listId, entry);
        }
        private static void BrandListEditEntry(string listId, string oldValue, string newValue)
        {
            // Edit local cache
            int index = BrandListGetEntries(listId).IndexOf(oldValue);
            BrandListGetEntries(listId)[index] = newValue;

            // Edit data source
            AppFactory.brandManipulator.EditEntry(listId, oldValue, newValue);
        }
        private static void BrandListDeleteEntry(string listId, string entry)
        {
            // Remove from local cache
            GetBrandList(listId).List.Remove(entry);

            // Remove from data source
            AppFactory.brandManipulator.DeleteEntry(listId, entry);
        }
        private static void BrandListMoveEntry(string listId, string entryValue, ShiftDirection direction)
        {
            // Move entry in local cache
            ObservableCollection<string> listEntries =
                repos.BrandsList.Where(list => list.ID == listId)
                .FirstOrDefault().List;

            int n = listEntries.IndexOf(entryValue);
            listEntries.Move(n, n + (int)direction);

            // Move entry in data source
            AppFactory.brandManipulator.MoveEntry(listId, entryValue, direction);
        }
        #endregion

        #region Ends Lists
        private static List<BasicListView> GetEnds() => repos.EndsList;
        private static IEnumerable<string> GetEndsId() => repos.EndsIdList;
        private static void AddEndsList(IBasicList content)
        {
            AppFactory.endsRepo.AddList(content);
            UpdateEnds();
        }
        private static void EditEndsList(string refId, IBasicList content)
        {
            // Modify local cache (no need because changes are made on local cache)
            // Modify data source
            AppFactory.endsRepo.UpdateList(refId, content);
        }
        private static void DeleteEndsList(string listId)
        {
            // Delete from local cache
            repos.EndsList = repos.EndsList.Where(list => list.ID != listId).ToList();

            // Delete from data source
            AppFactory.endsRepo.DeleteList(listId);
        }
        private static IBasicList GetEndsList(string listId) => repos.EndsList.Find(list => list.ID == listId);
        private static ObservableCollection<string> EndsListGetEntries(string listId)
        {
            return
                (from list in repos.EndsList
                 where list.ID == listId
                 select list.List).FirstOrDefault();
        }
        private static void EndsListAddEntry(string listId, string entry)
        {
            // Add to local cache
            repos.EndsList.Where(list => list.ID == listId).First().List.Add(entry);

            // Add to data source
            AppFactory.endsManipulator.AddEntry(listId, entry);
        }
        private static void EndsListEditEntry(string listId, string oldValue, string newValue)
        {
            // Edit local cache
            int index = EndsListGetEntries(listId).IndexOf(oldValue);
            EndsListGetEntries(listId)[index] = newValue;

            // Edit data source
            AppFactory.endsManipulator.EditEntry(listId, oldValue, newValue);
        }
        private static void EndsListDeleteEntry(string listId, string entry)
        {
            // Remove from local cache
            GetEndsList(listId).List.Remove(entry);

            // Remove from data source
            AppFactory.endsManipulator.DeleteEntry(listId, entry);
        }
        private static void EndsListMoveEntry(string listId, string entryValue, ShiftDirection direction)
        {
            // Move entry in local cache
            ObservableCollection<string> listEntries =
                repos.EndsList.Where(list => list.ID == listId)
                .FirstOrDefault().List;

            int n = listEntries.IndexOf(entryValue);
            listEntries.Move(n, n + (int)direction);

            // Move entry in data source
            AppFactory.endsManipulator.MoveEntry(listId, entryValue, direction);
        }
        #endregion

        #region Custom Sizes
        public static List<string> GetCustomSizes() => repos.CustomSizes;
        #endregion

        #region Custom Specs
        public static List<string> GetCustomSpecs() => repos.CustomSpecs;
        #endregion

        public static bool IsDuplicateItemId(string itemId)
        {
            return repos.ItemsID.Contains(itemId);
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

    }
}