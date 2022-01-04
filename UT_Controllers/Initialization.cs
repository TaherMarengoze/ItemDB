using System;
using System.IO;
using System.Linq;
using AppCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlDataSource;

namespace UT_Controllers
{
    public static class Initialization
    {
        const string PATH_LOC_1 = @"C:\Users\taher.marengoze\source\repos\TaherMarengoze\ItemDB\";
        const string PATH_LOC_2 = @"D:\Developer\source\repos\ItemDB\";

        public static void Simulate()
        {
            // simulate Program.cs -- Program.Main() : main entry point
            string dynamicTestPath = VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName + @"\";
            Globals.context = new XmlContext();
            XmlContext context = (XmlContext)Globals.context;
            context.TestLoadXmlContext(dynamicTestPath);
            ClientService.CacheIO.InitLists();
        }
    }

    public static class VisualStudioProvider
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }
}