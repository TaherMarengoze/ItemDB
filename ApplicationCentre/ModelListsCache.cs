
using Interfaces.Models;
using System;
using System.Collections.Generic;

namespace AppCore
{
    public class ModelListsCache
    {
        public event EventHandler OnSpecsChanged;
        public event EventHandler OnSizeGroupsChanged;
        public event EventHandler OnSizeListChanged;
        public event EventHandler OnCustomSizeListChanged;

        public List<IItem> Items { get => _items; set => _items = value; }

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

        public List<IFieldList> SizeLists
        {
            get => _sizeLists;
            set
            {
                _sizeLists = value;
                OnSizeListChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<IFieldList> BrandLists { get => _brandLists; set => _brandLists = value; }

        public List<IFieldList> EndLists { get => _endLists; set => _endLists = value; }

        public List<string> CustomSizes
        {
            get => _customSizes;
            set
            {
                _customSizes = value;
                OnCustomSizeListChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<string> CustomSpecs { get => _customSpecs; set => _customSpecs = value; }

        // fields
        private List<IItem> _items;
        private List<ISpecs> _specs;
        private List<ISizeGroup> _sizeGroups;
        private List<IFieldList> _sizeLists;
        private List<IFieldList> _brandLists;
        private List<IFieldList> _endLists;
        private List<string> _customSizes;
        private List<string> _customSpecs;
    }
}