using Interfaces.Models;
using Modeling.ViewModels.Common;
using Modeling.ViewModels.SizeGroup;
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

        public static List<Item.GenericView> ToGenericView(
            this IEnumerable<IItem> source)
        {
            return
                source.Select(item => new Item.GenericView(item)).ToList();
        }

        public static List<Specs.GenericView> ToGenericView(
            this IEnumerable<ISpecsItem> source)
        {
            return
                (from si in source
                 select new Specs.GenericView
                 {
                     Index = si.Index,
                     Name = si.Name,
                     ValuePattern = si.ValuePattern,
                     Entries = si.ListEntries?.Count() ?? 0,
                     CustomInputID = si.CustomInputID
                 }).ToList();
        }
        
        public static List<SizeGroupsGenericView> ToGenericView(
            this IEnumerable<ISizeGroup> source)
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

        public static List<FieldListGenericView> ToGenericView(
            this IEnumerable<IFieldList> source)
        {
            return
                source.Select(list => new FieldListGenericView
                {
                    ID = list.ID,
                    Name = list.Name,
                    EntriesCount = list.List.Count
                }).ToList();
        }

        //TODO: to be relocated to the proper project
        public static List<T> As<T>(this IEnumerable<IFieldList> source)
            where T : IFieldList
        {
            return source.Cast<T>().ToList();
        }
    }
}