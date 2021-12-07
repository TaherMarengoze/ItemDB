using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore
{
    public class DataListsCache
    {
        public DataListsCache()
        {
            Globals.ModelLists.OnSpecsChanged += ModelLists_OnSpecsChanged;
            Globals.ModelLists.OnSizeGroupsChanged += ModelLists_OnSizeGroupsChanged;
            Globals.ModelLists.OnSizeListChanged += ModelLists_OnSizeListChanged;
            Globals.ModelLists.OnCustomSizeListChanged += ModelLists_OnCustomSizeListChanged;
        }

        private void ModelLists_OnSpecsChanged(object sender, EventArgs e)
        {
            SpecsIDs = Globals.ModelLists.Specs
                .Select(entity => entity.ID)
                .ToList();
        }

        private void ModelLists_OnSizeGroupsChanged(object sender, EventArgs e)
        {
            SizeGroupIDs = Globals.ModelLists.SizeGroups
                .Select(entity => entity.ID)
                .ToList();
        }

        private void ModelLists_OnSizeListChanged(object sender, EventArgs e)
        {
            SizeIDs = Globals.ModelLists.SizeLists
                .Select(size => size.ID)
                .ToList();
        }

        private void ModelLists_OnCustomSizeListChanged(object sender, EventArgs e)
        {
            CustomSizeIDs = Globals.ModelLists.CustomSizes;
        }

        public List<string> SpecsIDs { get; set; }

        public List<string> SizeGroupIDs { get; set; }

        public List<string> SizeIDs { get; set; }

        public List<string> CustomSizeIDs { get; set; }
    }
}