
using AppCore;
using Interfaces.Models;

namespace ClientService
{
    public static class SpecsRepository
    {
        public static void Create(ISpecs content)
        {
            // Add to data source
            // whatever it is, thanks to interfaces
            Globals.specsRepo.Create(content);

            // Update the list in the Globals class
            DataManager.UpdateSpecsList();

            /*
             • The above code will run a query against the
             data source every time a new specs is added.

             • Will adding the specs to the global list
             be faster than querying, like in the code below ?
            
            Globals.Lists.Specs.Add(specs);

            */
        }

        public static ISpecs Read(string entityId)
        {
            return
                Globals.ModelLists.Specs.Find(entity => entity.ID == entityId);
        }

        public static void Update(string refId, ISpecs content)
        {
            // Update data source
            Globals.specsRepo.Update(refId, content);

            // Update the list in the Globals class
            //>
            DataManager.UpdateSpecsList();
        }

        public static void Delete(string entityId)
        {
            // Delete from data source
            Globals.specsRepo.Delete(entityId);

            // Delete from local cache

            // Option 1:
            //    Update from data source
            DataManager.UpdateSpecsList();

            // Option 2:
            //    Find the item by ID and delete it from the global list
            ISpecs entityToRemove =
                Globals.ModelLists.Specs.Find(entity => entity.ID == entityId);

            Globals.ModelLists.Specs.Remove(entityToRemove);
        }
    }
}