
using Interfaces.Models;
using System;
using System.Collections.Generic;

namespace AppCore
{
    public class ModelListsCache
    {
        private List<ISpecs> _specs;

        public event System.EventHandler OnSpecsChanged;

        public List<IItem> Items { get; set; }

        public List<ISpecs> Specs
        {
            get => _specs;
            set
            {
                _specs = value;
                OnSpecsChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public List<ISizeGroup> SizeGroups { get; set; }

        public List<IFieldList> SizeLists { get; set; }

        public List<IFieldList> BrandLists { get; set; }

        public List<IFieldList> EndLists { get; set; }

        public List<string> CustomSizes { get; set; }

        public List<string> CustomSpecs { get; set; }
    }
}