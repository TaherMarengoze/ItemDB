
using Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace Modeling.DraftModels
{
    internal static class CloneExtension
    {
        public static List<ISpecsItem> Clone(this IEnumerable<ISpecsItem> specsItems)
        {
            IEnumerable<ISpecsItem> clonedItems =
                from si in specsItems
                select new DataModels.SpecsItem()
                {
                    Index = si.Index,
                    Name = si.Name,
                    ValuePattern = si.ValuePattern,
                    ListEntries = si.ListEntries.Clone(),
                    CustomInputID = si.CustomInputID
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
    }
}