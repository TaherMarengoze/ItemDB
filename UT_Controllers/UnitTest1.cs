using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_Controllers
{
    [TestClass]
    public class UnitTest1
    {
        Controllers.SizeGroupUI.SizeGroupUiController sgc =
                new Controllers.SizeGroupUI.SizeGroupUiController();

        bool actualOutput;
        private ClientService.SizeGroupCache cache;
        private IDataReader reader;

        private void SimulateInit()
        {
            AppCore.Globals.context = new XmlDataSource.XmlContext();

            XmlDataSource.XmlContext context = (XmlDataSource.XmlContext)AppCore.Globals.context;
            //UserInterface.Runtime.Test.LoadCallback testLoadXmlContext = context.TestLoadXmlContext;
            //UserInterface.Runtime.Test.AutoLoad(testLoadXmlContext);
            context.TestLoadXmlContext(@"C:\Users\taher.marengoze\source\repos\TaherMarengoze\ItemDB\");

            reader = AppCore.Globals.reader;
            cache = new ClientService.SizeGroupCache();

            ClientService.CacheIO.InitLists();

            //sgc.OnReadyStateChange += Sgc_OnReadyStateChange;
        }

        //[TestMethod]
        public void Test_UiController_New()
        {
            SimulateInit();

            sgc.InputID = "GTEST";
            sgc.InputName = "Test Size Group";
            sgc.InputDefaultID = "DTEST";
            sgc.InputAltList = new List<string>() { "ATEST" };
            sgc.InputCustomID = "CTEST";

            Assert.AreEqual(true, actualOutput);
        }

        [TestMethod]
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
            Assert.AreEqual("GTEST", cache.Read("GTEST").ID);
        }

        private void Sgc_OnReadyStateChange(object sender, bool e)
        {
            actualOutput = e;
        }
    }
}
