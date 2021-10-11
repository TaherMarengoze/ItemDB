
using Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace Modeling.DraftModels
{
    internal static class CloneExtension
    {
        public static List<ISpecsItem> Clone(this IEnumerable<ISpecsItem> specItems)
        {
            IEnumerable<ISpecsItem> clonedItems =
                from spec in specItems
                select new DataModels.SpecsItem()
                {
                    Index = spec.Index,
                    Name = spec.Name,
                    ValuePattern = spec.ValuePattern,
                    ListEntries = spec.ListEntries.Clone(),
                    CustomInputID = spec.CustomInputID
                };

            return clonedItems.ToList();
        }

        private static IEnumerable<ISpecListEntry> Clone(this IEnumerable<ISpecListEntry> listEntries)
        {
            if (listEntries == null)
            {
                return null;
            }

            IEnumerable<ISpecListEntry> clonedEntries =
                from entry in listEntries
                select new DataModels.SpecListEntry()
                {
                    ValueID = entry.ValueID,
                    Value = entry.Value,
                    Display = entry.Display
                };

            return clonedEntries;
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (T cur in e)
            {
                yield return cur;
            }
            yield return value;
        }
    }
}