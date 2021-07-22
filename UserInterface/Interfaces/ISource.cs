using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Enums;

namespace UserInterface.Interfaces
{
    public interface ISource
    {
        void AddNewFieldList(FieldType field, IFieldList fieldListItem);

        //void AddNewBrandList(IFieldList fieldListItem);
    }
}
