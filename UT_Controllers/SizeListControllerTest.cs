using System;
using System.Collections.Generic;
using System.Linq;
using AppCore;
using Controllers;
using CoreLibrary.Enums;
using Interfaces.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modeling.DataModels;
using Modeling.ViewModels;

namespace UT_Controllers
{
    [TestClass]
    public class SizeListControllerTest
    {
        SizeListController ui;
        private List<SizeList> onLoadArgs;
        private SizeListSelectionEventArgs onSelectionArgs;
        private InputStatus onIdStatusChangeArgs;
        private InputStatus onNameStatusChangeArgs;
        private InputStatus onListStatusChangeArgs;

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();

            ui = new SizeListController();
            EventSubscriber();
        }

        private void EventSubscriber()
        {
            ui.OnLoad += Ui_OnLoad;
            ui.OnSelection += Ui_OnSelection;
            ui.OnIdStatusChange += Ui_OnIdStatusChange;
            ui.OnNameStatusChange += Ui_OnNameStatusChange;
            ui.OnListStatusChange += Ui_OnListStatusChange;
        }

        private void Ui_OnLoad(object sender, List<SizeList> e)
        {
            onLoadArgs = e;
        }

        private void Ui_OnSelection(object sender, SizeListSelectionEventArgs e)
        {
            onSelectionArgs = e;
        }

        private void Ui_OnIdStatusChange(object sender, InputStatus e)
        {
            onIdStatusChangeArgs = e;
        }

        private void Ui_OnNameStatusChange(object sender, InputStatus e)
        {
            onNameStatusChangeArgs = e;
        }

        private void Ui_OnListStatusChange(object sender, InputStatus e)
        {
            onListStatusChangeArgs = e;
        }

        //[TestMethod]
        public void Should_Initialize()
        {
            Assert.AreEqual(null, ui.InputID);
            Assert.AreEqual(null, ui.InputName);
            Assert.AreEqual(null, ui.InputList);
            Assert.AreEqual(InputStatus.Blank, ui.StatusID);
            Assert.AreEqual(InputStatus.Blank, ui.StatusName);
            Assert.AreEqual(InputStatus.Invalid, ui.StatusList);
            Assert.AreEqual(null, ui._Selected);
            Assert.AreEqual(null, ui._DraftObject);

        }

        //[TestMethod]
        public void Should_Load()
        {
            ui.Load();

            List<SizeList> expectedList =
                Globals.reader.GetSizes().Cast<SizeList>().ToList();

            CollectionAssert.AreEqual(
                expectedList.Select(list => list.ID).ToList(),
                onLoadArgs.Select(list => list.ID).ToList());

            CollectionAssert.AreEqual(
                expectedList.Select(list => list.Name).ToList(),
                onLoadArgs.Select(list => list.Name).ToList());

        }

        //[TestMethod]
        public void Should_Select()
        {
            string exp_selectedID = "STEST";
            List<string> exp_selectedList =
                new List<string> { "Entry 1", "Entry 2", "Entry 3" };

            ui.Select(exp_selectedID);

            Assert.AreEqual(exp_selectedID, onSelectionArgs.Selected.ID);
            CollectionAssert.AreEqual(exp_selectedList, onSelectionArgs.Selected.List);
        }

        //[DataTestMethod]
        [DataRow(InputStatus.Blank, "", DisplayName = "1: Blank ID")]
        [DataRow(InputStatus.Duplicate, "STEST", DisplayName = "2: Duplicate ID")]
        [DataRow(InputStatus.Valid, "TSZL0", DisplayName = "3: Unique ID")]
        public void Should_ValidateInputID(InputStatus expectedResult, string input)
        {
            ui.InputID = input;

            Assert.AreEqual(expectedResult, onIdStatusChangeArgs);
        }

        [TestMethod]
        //[DynamicData(nameof(TestInputs_ValidateInputList), DynamicDataSourceType.Method)]
        [DataRow(new object[] { InputStatus.Valid, new string[] { "entry_01", "entry_02" } })]
        public void Should_ValidateInputList(object[] inputObjects)
        {
            // Arrange
            object expValue = inputObjects[0];

            // Act
            ui.InputList = ((string[])inputObjects[1]).ToList();

            // Assert
            Assert.AreEqual(expValue, onListStatusChangeArgs);
        }

        public static IEnumerable<object[]> TestInputs_ValidateInputList()
        {
            return new[]
            {
                new object[] { InputStatus.Valid, new List<string> { "entry_01", "entry_02" } },
                new object[] { InputStatus.Invalid, new List< string>() },
                new object[] { InputStatus.Invalid, null }
            };
        }
    }
}

// Arrange
// Act
// Assert