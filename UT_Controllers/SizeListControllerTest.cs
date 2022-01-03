using System;
using Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_Controllers
{
    [TestClass]
    public class SizeListControllerTest
    {
        SizeListController ui;

        private void SimulateController()
        {
            ui = new SizeListController();
        }

        [TestMethod]
        public void Should_InitiateController()
        {
            Initialization.Simulate();
            SimulateController();

            Assert.AreEqual("", ui.InputID);
        }
    }
}
