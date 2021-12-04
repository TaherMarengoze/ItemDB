
using ClientService;
using System.Collections.Generic;
using System.Linq;


namespace Drafting
{
    public class SizeGroupDrafter
    {
        public List<string> SizeGroups => DataProvider.SizeGroup.GetSizeGroups().Select(sg => sg.ID).ToList();
    }
}