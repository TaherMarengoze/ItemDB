﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Interfaces;

namespace UserInterface.Models
{
    public class DataRepos
    {
        private IEnumerable<IItem> _items;
        private IEnumerable<Specs> _specsList;
        private IEnumerable<SizeGroup> _sizeGroups;
        private List<BasicListView> _sizesList;
        private IEnumerable<BasicListView> _brandsList;
        private IEnumerable<BasicListView> _endsList;

        public DataRepos()
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

        public IEnumerable<ItemCategory> Categories { get; set; }

        #region Specs
        public IEnumerable<Specs> SpecsList
        {
            get => _specsList;
            set
            {
                _specsList = value;
                SpecsIdList = value.Select(specs => specs.ID).ToList();
            }
        }

        public List<string> SpecsIdList { get; private set; }
        #endregion

        #region Size Groups
        public IEnumerable<SizeGroup> SizeGroups
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
        public List<BasicListView> SizesList
        {
            get => _sizesList; set
            {
                _sizesList = value;
                SizesIdList = value.Select(sizes => sizes.ID);
            }
        }

        public IEnumerable<string> SizesIdList { get; private set; }
        #endregion

        #region Brands
        public IEnumerable<BasicListView> BrandsList
        {
            get => _brandsList;
            set
            {
                _brandsList = value;
                BrandsIdList = value.Select(brands => brands.ID).ToList();
            }
        }

        public List<string> BrandsIdList { get; private set; }
        #endregion

        #region Ends
        public IEnumerable<BasicListView> EndsList
        {
            get => _endsList;
            set
            {
                _endsList = value;
                EndsIdList = value.Select(ends => ends.ID).ToList();
            }
        }

        public List<string> EndsIdList { get; private set; }
        #endregion
    }
}