﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Models
{
    public interface IItemCategory //: IIdentity
    {
        string CatID { get; set; }

        string CatName { get; set; }
    }
}