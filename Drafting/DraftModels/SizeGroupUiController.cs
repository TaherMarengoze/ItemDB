
using ClientService;
using Modeling.ViewModels;
using Modeling.ViewModels.SizeGroup;
using System.Collections.Generic;
using System.Linq;


namespace Drafting
{
    public class SizeGroupUiController
    {
        public List<SizeGroupsGenericView> SizeGroups => null; //DataProvider.SizeGroup.GetList().ToGenericView();

        public int Count => SizeGroups?.Count ?? 0;
        public List<string> SizeIDs => DataProvider.Size.GetIDs();

        public List<string> CustomSizeIDs => DataProvider.CustomSize.GetIDs();

        public object SelectedSizeGroup { get; set; }

        public void SetSelection(string id)
        {
            SelectedSizeGroup = null;
        }
    }
}