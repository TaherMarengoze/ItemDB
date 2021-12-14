using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_Controllers
{
    [TestClass]
    public class UnitTest_SizeGroupUiController
    {
        Controllers.SizeGroupUI.SizeGroupUiController sgc;

        bool actualOutput;
        object carryOverValue;

        private ClientService.SizeGroupCache cache;
        private IDataReader reader;

        private void SimulateInit()
        {
            // simulate Program.cs -- Program.Main() : main entry point
            string dynTestPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string fixedPath1 = @"C:\Users\taher.marengoze\source\repos\TaherMarengoze\ItemDB\";
            string fixedPath2 = @"D:\Developer\source\repos\ItemDB\";

            AppCore.Globals.context = new XmlDataSource.XmlContext();

            XmlDataSource.XmlContext context = (XmlDataSource.XmlContext)AppCore.Globals.context;
            //UserInterface.Runtime.Test.LoadCallback testLoadXmlContext = context.TestLoadXmlContext;
            //UserInterface.Runtime.Test.AutoLoad(testLoadXmlContext);
            
            context.TestLoadXmlContext(fixedPath1);

            reader = AppCore.Globals.reader;
            cache = new ClientService.SizeGroupCache();

            ClientService.CacheIO.InitLists();

            // simulate new SizeGroupUiController
            sgc = new Controllers.SizeGroupUI.SizeGroupUiController();
            sgc.OnReadyStateChange += Sgc_OnReadyStateChange;
            //sgc.OnNewEntityAdd += Sgc_OnNewEntityAdd;
        }

        private void SimulateInputs()
        {
            sgc.InputID = "GTEST";
            sgc.InputName = "Test Size Group";
            sgc.InputDefaultID = "DTEST";
            //sgc.InputAltListRequired = true;
            sgc.InputAltListRequired = false;
            sgc.InputAltList = false ? new List<string>() { "ATEST" } : null;

            //sgc.InputCustomID = "CTEST";
        }

        [TestMethod]
        public void Should_InputValid()
        {
            SimulateInit();
            SimulateInputs();
            
            Assert.AreEqual(true, actualOutput);
        }

        [TestMethod]
        public void Should_InputAltListRequired()
        {
            SimulateInit();

            //sgc.InputAltListRequired = true;
            sgc.InputAltList = true ? new List<string>() { "ATEST" } : null;
            //sgc.InputAltListRequired = false;

            Assert.AreEqual(true, sgc.InputAltListRequired);
        }

        public int MyProperty { get; set; }

        //[TestMethod]
        public void Test_UiController_AddNew()
        {
            SimulateInit();

            sgc.InputID = "GTEST";
            sgc.InputName = "Test Size Group";
            sgc.InputDefaultID = "DTEST";
            sgc.InputAltList = new List<string>() { "ATEST" };
            sgc.InputCustomID = "CTEST";
            sgc.AddNew();

            //Assert.AreEqual(23, reader.GetSizeGroups().Count());
            //Assert.AreEqual("GTEST", cache.Read("GTEST").ID);
            Assert.AreEqual("GTEST", (string)carryOverValue);
            Assert.AreEqual(23, sgc.Count);
        }

        private void Sgc_OnReadyStateChange(object sender, bool e)
        {
            actualOutput = e;
        }

        public void Sgc_OnNewEntityAdd(object sender, string e)
        {
            carryOverValue = e;
        }
    }
}
