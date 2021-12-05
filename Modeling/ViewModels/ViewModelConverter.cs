﻿using Interfaces.Models;
using Modeling.ViewModels.SizeGroup;
using Modeling.ViewModels.Specs;
using System.Collections.Generic;
using System.Linq;

namespace Modeling.ViewModels
{
    public static class ViewModelConverter
    {
        //public static SpecsInfoObject ToInfoObject(this ISpecs specs)
        //{
        //    return
        //        new SpecsInfoObject
        //        {
        //            ID = specs.ID,
        //            Name = specs.Name,
        //            TextPattern = specs.TextPattern,
        //            SpecItems = specs.SpecItems.ToList(),
        //            SpecItemsInfo = specs.SpecItems.ToGenericView()
        //        };
        //}

        public static List<SpecsItemGenericView> ToGenericView(this IEnumerable<ISpecsItem> source)
        {
            return
                (from si in source
                 select new SpecsItemGenericView
                 {
                     Index = si.Index,
                     Name = si.Name,
                     ValuePattern = si.ValuePattern,
                     Entries = si.ListEntries?.Count() ?? 0,
                     CustomInputID = si.CustomInputID
                 }).ToList();
        }

        public static List<SizeGroupsGenericView> ToGenericView(this IEnumerable<ISizeGroup> source)
        {
            return
                source.Select(sg => new SizeGroupsGenericView
                {
                    ID = sg.ID,
                    Name = sg.Name,
                    DefaultListID = sg.DefaultListID,
                    AltListsCount = sg.AltIdList?.Count ?? 0,
                    CustomSize = sg.CustomSize
                }).ToList();
        }
    }
}