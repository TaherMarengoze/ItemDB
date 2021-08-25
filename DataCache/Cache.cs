
using CoreLibrary.Interfaces;
using CoreLibrary.Models;
using System.Collections.Generic;
using System.Linq;


namespace DataCache
{
    public class Cache
    {
        private IEnumerable<IItem> _items;
        private List<ISpecs> _specsList;
        private List<ISizeGroup> _sizeGroups;

        private List<IBasicList> _sizesList;
        private List<IBasicList> _brandsList;
        private List<IBasicList> _endsList;

        public Cache()
        {

        }

        public IEnumerable<IItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                ItemsID = Items.Select(item => item.ItemID).ToList();

                ItemIdViews = value.Select(item => new ItemIdView()
                { ID = item.ItemID, Name = item.BaseName }).ToList();

                ItemsView = value.Select(item => new ItemVO(item)).ToList();
            }
        }

        public List<string> ItemsID { get; private set; }

        public List<ItemIdView> ItemIdViews { get; private set; }

        public List<ItemVO> ItemsView { get; private set; }

        public IEnumerable<IBasicField> Categories { get; set; }

        #region Specs
        public List<ISpecs> SpecsList
        {
            get => _specsList;
            set
            {
                _specsList = value;
                SpecsIdList = value.Select(specs => specs.ID).ToList();
            }
        }

        public IEnumerable<string> SpecsIdList { get; private set; }
        #endregion

        #region Size Groups
        public List<ISizeGroup> SizeGroups
        {
            get => _sizeGroups;
            set
            {
                _sizeGroups = value;
                SizeGroupIdList = value.Select(group => group.ID).ToList();
            }
        }

        public List<string> SizeGroupIdList { get; private set; }
        #endregion

        #region Sizes
        public List<IBasicList> SizesList
        {
            get => _sizesList;
            set
            {
                _sizesList = value;
                SizesIdList = value.Select(sizes => sizes.ID);
            }
        }

        public IEnumerable<string> SizesIdList { get; private set; }
        #endregion

        #region Brands
        public List<IBasicList> BrandsList
        {
            get => _brandsList;
            set
            {
                _brandsList = value;
                BrandsIdList = value.Select(brands => brands.ID);
            }
        }

        public IEnumerable<string> BrandsIdList { get; private set; }
        #endregion

        #region Ends
        public List<IBasicList> EndsList
        {
            get => _endsList;
            set
            {
                _endsList = value;
                EndsIdList = value.Select(ends => ends.ID);
            }
        }

        public IEnumerable<string> EndsIdList { get; private set; }
        #endregion

        #region Custom Sizes
        public List<string> CustomSizes { get; set; }
        #endregion

        #region Custom Specs
        public List<string> CustomSpecs { get; set; }
        #endregion
    }
}
