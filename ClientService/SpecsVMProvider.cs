
using AppCore;
using Modeling.ViewModels.Specs;
using System.Collections.Generic;
using System.Linq;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for Specs view models (VM).
    /// </summary>
    public static class SpecsVMProvider
    {
        public static List<BasicView> Brief()
        {
            return
                (from spec in Globals.ModelLists.Specs
                 select new BasicView()
                 {
                     ID = spec.ID,
                     Name = spec.Name,
                     TextPattern = spec.TextPattern
                 }).ToList();
        }
    }
}