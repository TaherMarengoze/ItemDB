﻿using AppCore;
using ClientService.Contracts;
using Interfaces.Models;
using Interfaces.Operations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Data
{
    public class SizeGroupProvider : IProvider<ISizeGroup>
    {
        public List<ISizeGroup> GetList()
        {
            return CacheIO.GetSizeGroupList();
        }

        /// <summary>
        /// Gets a list of ID for SizeGroup objects.
        /// </summary>
        /// <returns></returns>
        public List<string> GetIDs()
        {
            return Globals.ModelCache.SizeGroupsIDs;
        }

        public List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, ISizeGroup>, new()
        {
            throw new NotImplementedException();
        }

        public int Count => throw new NotImplementedException();
    }
}