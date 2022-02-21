using System;
using XmlDataSource.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ClientService.Brokers;

namespace UT_Controllers
{
    [TestClass]
    public class XmlDatasource_ItemTest
    {
        readonly ItemBroker itemBroker = new ItemBroker();

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();

            _ = new Item();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string i0 = "BSP01";
            string i1 = "ACV01";
            string i2 = "CLMP1";
            string i3 = "PVC";

            var item = itemBroker.Read(i3);

            Console.WriteLine("Category: ID = {0}, Name  = {1}",
                item.CatID, item.CatName);

            Console.WriteLine("ItemID = {0}", item.ItemID);
            Console.WriteLine("Name (Base) = {0}", item.BaseName);
            Console.WriteLine("Name (Display) = {0}", item.DisplayName);
            string list = string.Join(", \n\t", item.CommonNames);
            Console.WriteLine("Name (Common) = \n\t{0}\n\t[{1} items(s)]",
                list, item.CommonNames.Count);
            Console.WriteLine();
            Console.WriteLine("Description = {0}", item.Description);
            Console.WriteLine("UoM = {0}", item.UoM);
            Console.WriteLine();
            Console.WriteLine("Details");
            Console.WriteLine("\tSpecs\t\t: ID = {0}, Required = {1}",
                item.Details.SpecsID,
                item.Details.SpecsRequired ? "Yes" : "No");

            Console.WriteLine("\tSize Group\t: ID = {0}, Required = {1}",
                item.Details.SizeGroupID,
                item.Details.SizeRequired ? "Yes" : "No");

            Console.WriteLine("\tBrand\t\t: ID = {0}, Required = {1}",
                item.Details.BrandListID,
                item.Details.BrandRequired ? "Yes" : "No");

            Console.WriteLine("\tEnds\t\t: ID = {0}, Required = {1}",
                item.Details.EndsListID,
                item.Details.EndsRequired ? "Yes" : "No");
            Console.WriteLine();
            string images = string.Join(", \n\t", item.ImagesFileName);
            Console.WriteLine("Images Name = \n\t{0}\n\t[{1} items(s)]",
                images, item.ImagesFileName.Count);

            Console.WriteLine("property = {0}", item);
        }

        private string DisplayList(string separator ,IEnumerable<string> list)
        {

            string l = "";

            for (int i = 0; i < list.Count(); i++)
            {
                l += list.ElementAt(i) + (i < list.Count()-1 ? separator : "");
            }

            return l;
        }
    }
}
