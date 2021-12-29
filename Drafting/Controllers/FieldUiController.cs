using AppCore;
using ClientService;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Field
{
    public class FieldUiController
    {
        public FieldUiController()
        {
            IFieldList draft = new SizeList
            {
                ID = "XYZ00",
                Name = "Test",
                List = new System.Collections.ObjectModel.ObservableCollection<string> { "entry 1", "entry 2", "entry 3" }
            };

            repos.Create(draft);
        }

        private readonly SizeListCache cache = new SizeListCache();
        private readonly IRepo<IFieldList> repos = Globals.sizesRepo;
    }
}