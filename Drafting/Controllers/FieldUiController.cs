using ClientService.Brokers;
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
            
        }

        private readonly SizeListBroker broker = new SizeListBroker();
    }
}