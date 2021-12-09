using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore
{
    public class DataListsCache
    {
        public DataListsCache()
        {
            Globals.ModelCache.OnSpecsChanged += ModelLists_OnSpecsChanged;
            Globals.ModelCache.OnSizeGroupsChanged += ModelLists_OnSizeGroupsChanged;
            Globals.ModelCache.OnSizeListChanged += ModelLists_OnSizeListChanged;
            Globals.ModelCache.OnCustomSizeListChanged += ModelLists_OnCustomSizeListChanged;
        }

        private void ModelLists_OnSpecsChanged(object sender, EventArgs e)
        {
            SpecsIDs = Globals.ModelCache.Specs
                .Select(entity => entity.ID)
                .ToList();
        }

        private void ModelLists_OnSizeGroupsChanged(object sender, EventArgs e)
        {
            SizeGroupIDs = Globals.ModelCache.SizeGroups
                .Select(entity => entity.ID)
                .ToList();
        }

        private void ModelLists_OnSizeListChanged(object sender, EventArgs e)
        {
            SizeIDs = Globals.ModelCache.SizeLists
                .Select(size => size.ID)
                .ToList();
        }

        private void ModelLists_OnCustomSizeListChanged(object sender, EventArgs e)
        {
            CustomSizeIDs = Globals.ModelCache.CustomSizes;
        }

        public List<string> SpecsIDs { get; set; }

        public List<string> SizeGroupIDs { get; set; }

        public List<string> SizeIDs { get; set; }

        public List<string> CustomSizeIDs { get; set; }
    }
}