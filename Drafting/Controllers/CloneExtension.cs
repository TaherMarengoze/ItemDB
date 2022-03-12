
using Interfaces.Models;
using Modeling.DataModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Controllers
{
    internal static class CloneExtension
    {
        public static IItem Clone(this IItem item)
        {
            return
                new Item
                {
                    ItemID         = item.ItemID,
                    BaseName       = item.BaseName,
                    DisplayName    = item.DisplayName,
                    Description    = item.Description,
                    UoM            = item.UoM,
                    CatID          = item.CatID,
                    CatName        = item.CatName,
                    CommonNames    = item.CommonNames.ToList(),
                    Details        = item.Details,
                    ImagesFileName = item.ImagesFileName.ToList(),
                };
        }

        public static ISpecs Clone(this ISpecs specs)
        {
            return
                new Specs()
                {
                    ID = specs.ID,
                    Name = specs.Name,
                    TextPattern = specs.TextPattern,
                    SpecItems = specs.SpecItems.Clone()
                };
        }

        public static ISpecsItem Clone(this ISpecsItem specsItem)
        {
            return
                new SpecsItem()
                {
                    Index = specsItem.Index,
                    Name = specsItem.Name,
                    ValuePattern = specsItem.ValuePattern,
                    ListEntries = specsItem.ListEntries.Clone(),
                    CustomInputID = specsItem.CustomInputID
                };
        }

        public static T Clone<T>(this T source) where T: IFieldList, new()
        {
            return
                new T
                {
                    ID = source.ID,
                    Name = source.Name,
                    List = new ObservableCollection<string>(source.List)
                };
        }

        public static List<ISpecsItem> Clone(this IEnumerable<ISpecsItem> specItems)
        {
            IEnumerable<ISpecsItem> clonedItems =
                from spec in specItems
                select new SpecsItem()
                {
                    Index = spec.Index,
                    Name = spec.Name,
                    ValuePattern = spec.ValuePattern,
                    ListEntries = spec.ListEntries.Clone(),
                    CustomInputID = spec.CustomInputID
                };

            return clonedItems.ToList();
        }

        private static List<ISpecListEntry> Clone(this IEnumerable<ISpecListEntry> listEntries)
        {
            if (listEntries == null)
            {
                return null;
            }

            IEnumerable<ISpecListEntry> clonedEntries =
                from entry in listEntries
                select new SpecListEntry()
                {
                    ValueID = entry.ValueID,
                    Value = entry.Value,
                    Display = entry.Display
                };

            return clonedEntries.ToList();
        }

        //private static ObservableCollection<string> Clone(this IEnumerable<string> source)
        //{
        //    if (source == null)
        //        return null;

        //    return
        //        new ObservableCollection<string>(source);
        //}
    }
}