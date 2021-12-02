using Interfaces.Models;
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

        public static List<Specs.SpecsItemGenericView> ToGenericView(this IEnumerable<ISpecsItem> source)
        {
            return
                (from si in source
                 select new Specs.SpecsItemGenericView
                 {
                     Index = si.Index,
                     Name = si.Name,
                     ValuePattern = si.ValuePattern,
                     Entries = si.ListEntries?.Count() ?? 0,
                     CustomInputID = si.CustomInputID
                 }).ToList();
        }
    }
}
