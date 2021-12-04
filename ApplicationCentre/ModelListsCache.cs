
using Interfaces.Models;
using System;
using System.Collections.Generic;

namespace AppCore
{
    public class ModelListsCache
    {
        public event EventHandler OnSpecsChanged;
        public event EventHandler OnSizeGroupsChanged;
        
        public List<IItem> Items { get; set; }

        public List<ISpecs> Specs
        {
            get => _specs;
            set
            {
                _specs = value;
                OnSpecsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<ISizeGroup> SizeGroups
        {
            get => _sizeGroups;
            set
            {
                _sizeGroups = value;
                OnSizeGroupsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<IFieldList> SizeLists { get; set; }

        public List<IFieldList> BrandLists { get; set; }

        public List<IFieldList> EndLists { get; set; }

        public List<string> CustomSizes { get; set; }

        public List<string> CustomSpecs { get; set; }

        // fields
        private List<ISpecs> _specs;
        private List<ISizeGroup> _sizeGroups;
    }
}