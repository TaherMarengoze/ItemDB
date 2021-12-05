
using ClientService;
using Modeling.DataModels;
using Modeling.ViewModels;
using Modeling.ViewModels.SizeGroup;
using System.Collections.Generic;
using System.Linq;


namespace Drafting
{
    public class SizeGroupUiController
    {
        private readonly SizeGroupRepository repos = new SizeGroupRepository();

        public List<SizeGroupsGenericView> SizeGroups => DataProvider.SizeGroup.GetList().ToGenericView();
            //null;

        public int Count => SizeGroups?.Count ?? 0;
        public List<string> SizeIDs => DataProvider.Size.GetIDs();

        public List<string> CustomSizeIDs => DataProvider.CustomSize.GetIDs();

        public SizeGroup SelectedSizeGroup { get; set; }

        public void SetSelection(string id)
        {
            SelectedSizeGroup = (SizeGroup)repos.Read(id);
            
            // raise an event for selection change
        }
    }
}