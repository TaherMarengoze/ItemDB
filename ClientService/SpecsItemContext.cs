
using Interfaces.Models;
using System.Linq;

namespace ClientService
{
    public static class SpecsItemContext
    {
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
    }
}