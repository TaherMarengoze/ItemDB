using System;
using System.IO;
using AppCore;
using XmlDataSource;

namespace UT_Controllers
{
    public static class Initialization
    {
        private static readonly string fixedPath1 = @"C:\Users\taher.marengoze\source\repos\TaherMarengoze\ItemDB\";
        private static readonly string fixedPath2 = @"D:\Developer\source\repos\ItemDB\";

        public static void Simulate()
        {
            // simulate Program.cs -- Program.Main() : main entry point
            string dynTestPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\";
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(dynTestPath);
            Globals.context = new XmlContext();
            XmlContext context = (XmlContext)Globals.context;
            context.TestLoadXmlContext(true ? dynTestPath : fixedPath2);
            ClientService.CacheIO.InitLists();

            
        }
    }
}