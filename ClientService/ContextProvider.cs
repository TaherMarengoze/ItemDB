using AppCore;
using CoreLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for saving and loading the context of all the entities, also provides partial saving for context's specific entity.
    /// </summary>
    public class ContextProvider
    {
        public static void Load()
        {
            Globals.context.Load();
        }

        public static void Save()
        {
            Globals.context.Save();
        }

        public static void Save(ContextEntity context)
        {
            Globals.context.Save(context);
        }
    }
}
