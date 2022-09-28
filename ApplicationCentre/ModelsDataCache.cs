
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Models;

namespace AppCore
{
    /// <summary>
    /// Contains cached collections of objects of the application's domain
    /// model and events of change for these collections.
    /// </summary>
    public class ModelsDataCache
    {
        #region Events
        public event EventHandler OnItemsChanged;
        public event EventHandler OnCategoriesChanged;
        public event EventHandler OnSpecsChanged;
        public event EventHandler OnSizeGroupsChanged;
        public event EventHandler OnSizeListChanged;
        public event EventHandler OnBrandListChanged;
        public event EventHandler OnEndListChanged;
        public event EventHandler OnCustomSizeListChanged;
        #endregion

        #region Item Model
        public List<IItem> Items
        {
            get => _items;
            set
            {
                _items = value;

                // set ID list
                _itemsIDs = value
                    .Select(entity => entity.ItemID)
                    .ToList();

                // raise change event
                OnItemsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> ItemsIDs { get => _itemsIDs; }
        #endregion

        #region Category Model
        public List<IItemCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;

                // set ID list
                _categoriesIDs = value
                    .Select(entity => entity.CatID)
                    .ToList();

                // raise change event
                OnCategoriesChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> CategoriesIDs => _categoriesIDs;
        #endregion

        #region Specs Model
        public List<ISpecs> Specs
        {
            get => _specs;
            set
            {
                _specs = value;

                // set ID list
                _specsIDs = value
                    .Select(entity => entity.ID)
                    .ToList();

                // raise change event
                OnSpecsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> SpecsIDs => _specsIDs;
        #endregion

        #region SizeGroup Model
        public List<ISizeGroup> SizeGroups
        {
            get => _sizeGroups;
            set
            {
                _sizeGroups = value;

                // set ID list
                _sizeGroupsIDs = value
                    .Select(entity => entity.ID)
                    .ToList();

                // raise change event
                OnSizeGroupsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> SizeGroupsIDs => _sizeGroupsIDs;
        #endregion

        #region SizeList Model
        public List<IFieldList> SizeLists
        {
            get => _sizeLists;
            set
            {
                _sizeLists = value;

                // set ID list
                _sizeListsIDs = value
                    .Select(entity => entity.ID)
                    .ToList();

                // raise change event
                OnSizeListChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> SizeListsIDs => _sizeListsIDs;
        #endregion

        #region BrandList Model
        public List<IFieldList> BrandLists
        {
            get => _brandLists; set
            {
                _brandLists = value;

                // set ID list
                _brandListsIDs = value
                    .Select(entity => entity.ID)
                    .ToList();

                // raise change event
                OnBrandListChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> BrandListsIDs => _brandListsIDs;
        #endregion

        #region EndList Model
        public List<IFieldList> EndLists
        {
            get => _endLists; set
            {
                _endLists = value;

                // set ID list
                _endListsIDs = value
                    .Select(entity => entity.ID)
                    .ToList();

                // raise change event
                OnEndListChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> EndListsIDs => _endListsIDs;
        #endregion

        #region CustomSize Model
        public List<string> CustomSizes
        {
            get => _customSizes;
            set
            {
                _customSizes = value;

                // set ID list
                _customSizesIDs = value
                    .ToList();

                // raise change event
                OnCustomSizeListChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> CustomSizesIDs { get => _customSizesIDs; }
        #endregion

        #region CustomSpecs Model
        public List<string> CustomSpecs { get => _customSpecs; set => _customSpecs = value; }
        #endregion

        #region Fields
        private List<IItem> _items;
        private List<IItemCategory> _categories;
        private List<ISpecs> _specs;
        private List<ISizeGroup> _sizeGroups;
        private List<IFieldList> _sizeLists;
        private List<IFieldList> _brandLists;
        private List<IFieldList> _endLists;
        private List<string> _customSizes;
        private List<string> _customSpecs;

        private List<string> _itemsIDs;
        private List<string> _categoriesIDs;
        private List<string> _specsIDs;
        private List<string> _sizeGroupsIDs;
        private List<string> _sizeListsIDs;
        private List<string> _brandListsIDs;
        private List<string> _endListsIDs;
        private List<string> _customSizesIDs;
        #endregion
    }
}