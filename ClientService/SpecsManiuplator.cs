
using AppCore;
using Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientService
{
    public static class SpecsManiuplator
    {
        public static IEnumerable<ISpecsItem> GetSpecsItems(string specsId)
        {
            return
                (from specs in Globals.Lists.Specs
                 where specs.ID == specsId
                 select specs.SpecItems).FirstOrDefault();
        }

        public static ISpecsItem GetSpecsItem(string specsId, int specIndex)
        {
            ISpecs specs = SpecsContext.Read(specsId);

            return
                specs.SpecItems
                .FirstOrDefault(spec => spec.Index == specIndex);
        }

        public static ISpecsItem GetSpecsItem(ISpecs specs, int specIndex)
        {
            return
                specs.SpecItems
                .FirstOrDefault(spec => spec.Index == specIndex);
        }

        public static IEnumerable<ISpecListEntry> GetSpecsItemListEntries(string specsId, int specsItemIndex)
        {
            IEnumerable<ISpecsItem> specsItems = GetSpecsItems(specsId);

            return
                (from specsItem in specsItems
                 where specsItem.Index == specsItemIndex
                 select specsItem.ListEntries).FirstOrDefault();
        }

        public static IEnumerable<ISpecListEntry> GetSpecsItemListEntries(ISpecs specs, int specsItemIndex)
        {
            return
                (from specsItem in specs.SpecItems
                 where specsItem.Index == specsItemIndex
                 select specsItem.ListEntries).FirstOrDefault();
        }
    }
}