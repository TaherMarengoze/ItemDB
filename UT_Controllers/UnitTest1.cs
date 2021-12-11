using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_Controllers
{
    [TestClass]
    public class UnitTest1
    {
        Controllers.SizeGroupUI.SizeGroupUiController sgc =
                new Controllers.SizeGroupUI.SizeGroupUiController();

        bool actualOutput;
        private ClientService.SizeGroupCache cache = new ClientService.SizeGroupCache();

        private void SimulateInit()
        {
            AppCore.Globals.context = new XmlDataSource.XmlContext();

            XmlDataSource.XmlContext context = (XmlDataSource.XmlContext)AppCore.Globals.context;
            UserInterface.Runtime.Test.LoadCallback testLoadXmlContext = context.TestLoadXmlContext;
            UserInterface.Runtime.Test.AutoLoad(testLoadXmlContext);

            ClientService.CacheIO.InitLists();

            sgc.OnReadyStateChange += Sgc_OnReadyStateChange;
        }

        [TestMethod]
        public void Test_UiController_New()
        {
            SimulateInit();

            sgc.InputID = "GTEST";
            sgc.InputName = "Test Size Group";
            sgc.InputDefaultID = "DTEST";
            sgc.InputAltList =
                new System.Collections.Generic.List<string>() { "ATEST" };

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
            sgc.InputAltList =
                new System.Collections.Generic.List<string>() { "ATEST" };

            sgc.InputCustomID = "CTEST";

            sgc.AddNew();

            Assert.AreEqual("GTEST", cache.Read("GTEST").ID);
        }

        private void Sgc_OnReadyStateChange(object sender, bool e)
        {
            actualOutput = e;
        }
    }
}
