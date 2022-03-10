using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public partial class ItemController
    {
        public ItemCommonNamesController CommonNames
            = new ItemCommonNamesController();

        public void ModifyCommonNames()
        {
            //CommonNames = new ItemCommonNamesController(this,
            //    inputCommonNames ?? new List<string>());

            CommonNames.SetSource(inputCommonNames ?? new List<string>(),
                SetInputCommonNames);
        }
    }
}
