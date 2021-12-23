using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoreLibrary.Enums;
using Interfaces.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserInterface.Forms;

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

        private void SimulateInitialization()
        {
            // simulate Program.cs -- Program.Main() : main entry point
            string dynTestPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string fixedPath1 = @"C:\Users\taher.marengoze\source\repos\TaherMarengoze\ItemDB\";
            string fixedPath2 = @"D:\Developer\source\repos\ItemDB\";

            AppCore.Globals.context = new XmlDataSource.XmlContext();

            XmlDataSource.XmlContext context = (XmlDataSource.XmlContext)AppCore.Globals.context;
            //UserInterface.Runtime.Test.LoadCallback testLoadXmlContext = context.TestLoadXmlContext;
            //UserInterface.Runtime.Test.AutoLoad(testLoadXmlContext);

            context.TestLoadXmlContext(true ? dynTestPath : fixedPath2);

            reader = AppCore.Globals.reader;
            cache = new ClientService.SizeGroupCache();

            ClientService.CacheIO.InitLists();

            // simulate new SizeGroupUiController
            sgc = new Controllers.SizeGroupUI.SizeGroupUiController();
            //sgc.OnReadyStateChange += Sgc_OnReadyStateChange;
            //sgc.OnNewEntityAdd += Sgc_OnNewEntityAdd;
        }

        private bool SimulateInputsAndGetExpected(int @case = 0)
        {
            sgc.InputID = "GTEST";
            sgc.InputName = "Test Size Group";
            sgc.InputDefaultID = "DTEST";

            switch (@case)
            {
                
                case 1:
                    // set the Alt list first
                    sgc.InputAltList = true ? new List<string>() { "ATEST" } : null;

                    // then set required to false
                    sgc.InputAltListRequired = false;
                    return true;
                //case 2:
                //    break;
                //case 3:
                //    break;
                //case 4:
                //    break;
                //case 5:
                //    break;
                //case 6:
                //    break;
                default:
                    break;
            }

            return true;
        }

        //[TestMethod]
        public void Should_InputValid()
        {
            SimulateInitialization();
            
            Assert.AreEqual(SimulateInputsAndGetExpected(1), actualOutput);
        }

        //[TestMethod]
        public void Should_RequiredStatusAltList()
        {
            SimulateInitialization();

            //sgc.InputAltListRequired = true;
            sgc.InputAltList = false ? new List<string>() { "ATEST" } : null;
            //sgc.InputAltListRequired = false;
            sgc.InputAltListRequired = true;
            sgc.InputAltListRequired = false;

            Assert.AreEqual(InputStatus.Blank, sgc.StatusAltList);
        }

        //[TestMethod]
        public void Test_RemainingSizeLists()
        {
            SimulateInitialization();

            //sgc.InputDefaultID = "PIP2";

            Assert.AreEqual(sgc.SizeLists.Count - 1*0, sgc.SizeListsDefaultEx.Count);
        }

        //[DataTestMethod]
        //[DataRow("SSHO")]
        public void Should_GetInputAltList(string p1)
        {
            SimulateInitialization();

            sgc.InputDefaultID = p1;
            sgc.InputAltList = new List<string> { "PIP1", "PIP2" };

            ListSelector selector = new ListSelector(sgc.SizeListsDefaultEx, sgc.InputAltList);
            selector.ShowDialog();
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