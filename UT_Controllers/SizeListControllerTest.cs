using AppCore;
using Controllers;
using CoreLibrary.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modeling.DataModels;
using Modeling.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace UT_Controllers
{
    [TestClass]
    public class SizeListControllerTest
    {
        const string UQID = "UTSZ0";
        const string DPID = "STEST";
        const string NAME = "Unit Test Size List";
        const string BLNK = "";
        private const string SEPARATOR_LINE = "-----";
        private static readonly List<string> LIST_ = new List<string> { "Entry 1", "Entry 2", "Entry 3" };
        private static readonly List<string> LIST1 = new List<string> { "Entry 1", "Entry 2", "Entry 3*" };

        SizeListController ui;
        #region EventArgs Fields
        private List<FieldListGenericView> onLoadArgs;
        private SizeListSelectionEventArgs onSelectionArgs;
        private InputStatus onIdStatusChangeArgs;
        private InputStatus onNameStatusChangeArgs;
        private InputStatus onListStatusChangeArgs;
        private ReadyEventArgs onReadyStateChangeArgs;
        private string onSetArgs;
        private CancelEventArgs onCancelEventArgs;
        private RemoveEventArgs onRemoveArgs;

        //private CancelEventArgs onCancelEntryEventArgs;
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();

            ui = new SizeListController();
            EventSubscriber();
        }

        #region Controller Events
        private void EventSubscriber()
        {
            ui.OnLoad += Ui_OnLoad;
            ui.OnSelect += Ui_OnSelect;
            //ui.OnSelection += Ui_OnSelection;
            ui.OnIdStatusChange += Ui_OnIdStatusChange;
            ui.OnNameStatusChange += Ui_OnNameStatusChange;
            ui.OnListStatusChange += Ui_OnListStatusChange;
            ui.OnReadyStateChange += Ui_OnReadyStateChange;
            ui.OnSet += Ui_OnSet;
            ui.OnCancel += Ui_OnCancel;
            ui.OnRemove += Ui_OnRemove;

            ui.OnLoadEntries += Ui_OnLoadEntries;
            ui.OnEntrySelect += Ui_OnEntrySelect;
            ui.OnEntryStatusChange += Ui_OnEntryStatusChange;
            ui.OnEntrySet += Ui_OnEntrySet;
            ui.OnEntryCancel += Ui_OnEntryCancel;
            ui.OnEntryRemove += Ui_OnEntryRemove;
        }

        private void Ui_OnEntryStatusChange(object sender, InputStatus e)
        {
            Console.WriteLine("> Entry Status: {0}", e.ToString());
            Console.WriteLine(SEPARATOR_LINE);
        }

        private void Ui_OnLoad(object sender, LoadEventArgs e)
        {
            onLoadArgs = (List<FieldListGenericView>)e.GenericViewList;
        }
        private void Ui_OnSelect(object sender, SelectEventArgs<SizeList> e)
        {
            //Console.WriteLine("> Object selected [{0}]\n" +
            //                  "  Details\n" +
            //                  "  # ID: {1}\n" +
            //                  "  # Name: {2}\n" +
            //                  "  # List [{3} entries]:\n   - {4}"
            //                  , e.Selected.ToString()
            //                  , e.Selected.ID, e.Selected.Name, e.Selected.List.Count
            //                  , String.Join("\n   - ", e.Selected.List));
            Console.WriteLine("> Object selected [{0}]", e.Selected.ToString());
            Console.WriteLine("  #ID = {0}", e.Selected.ID);
            Console.WriteLine("  #Name = {0}", e.Selected.Name);
            Console.WriteLine("  #List [{0} item(s)]", e.Selected.List.Count);
            Console.WriteLine("  - {0}", String.Join("\n  - ", e.Selected.List));
            Console.WriteLine(SEPARATOR_LINE);
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
            //onListStatusChangeArgs = e;
            Console.WriteLine("> List Status: {0}", e.ToString());
            Console.WriteLine(SEPARATOR_LINE);
        }
        private void Ui_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            //onReadyStateChangeArgs = e;
            Console.WriteLine("> Draft object state: {0}",
                e.Info);
            Console.WriteLine(SEPARATOR_LINE);
        }
        private void Ui_OnSet(object sender, SetEventArgs e)
        {
            Console.WriteLine("> Object set ({1}) [ID = {0}]",
                e.NewID, e.OldID == null ? "Add" : "Edit");
            //var lst = e.NewList.Cast<FieldListGenericView>().Select(el => el.ID);
            //Console.WriteLine("  New List [{0} item(s)]", e.NewList.Count);
            //Console.WriteLine("  - {0}", string.Join("\n  - ", lst.ToList()));
            Console.WriteLine(SEPARATOR_LINE);

            ui.Select(e.NewID);
        }
        private void Ui_OnCancel(object sender, CancelEventArgs e)
        {
            onCancelEventArgs = e;
        }
        private void Ui_OnRemove(object sender, RemoveEventArgs e)
        {
            onRemoveArgs = e;
        }
        private void Ui_OnLoadEntries(object sender, LoadEventArgs e)
        {
            Console.WriteLine("> Entries loaded [{1}]:\n - {0}",
                string.Join("\n - ", (ObservableCollection<String>)e.GenericViewList),
                e.Count);
            Console.WriteLine(SEPARATOR_LINE);
        }
        private void Ui_OnEntrySelect(object sender, SelectEventArgs<string> e)
        {
            if (e.Selected == null)
            {
                Console.WriteLine("> Entry not found [{0}]",
                    e.RequestInfo);
            }
            else
            {
                Console.WriteLine("> Entry selected [{0}]",
                    e.Selected);
            }
            Console.WriteLine(SEPARATOR_LINE);
        }
        private void Ui_OnEntrySet(object sender, EntrySetEventArgs e)
        {
            Console.WriteLine("> Entry Set [{0}]",
                e.NewItem);

            Console.WriteLine("  New List Entries [{0}]:\n - {1}",
                e.SetList.Count, string.Join("\n - ", (List<string>)e.SetList));
            Console.WriteLine(SEPARATOR_LINE);
        }
        private void Ui_OnEntryCancel(object sender, CancelEventArgs e)
        {
            Console.WriteLine("> Restore object: {0}",
                e.RestoreID);

            Console.WriteLine("> {0} List",
                e.EmptyList ? "Empty" : "Non-empty");
        }
        private void Ui_OnEntryRemove(object sender, RemoveEventArgs e)
        {
            Console.WriteLine("> Entry deleted [{0}]",
                e.RemoveID);

            Console.WriteLine("> New list entries:\n - {0}",
                string.Join("\n - ", (List<string>)e.NewList));
        }
        #endregion

        #region Deactivated Tests
        //[TestMethod]
        public void Should_Initialize()
        {
            Assert.AreEqual(null, ui.InputID);
            Assert.AreEqual(null, ui.InputName);
            //Assert.AreEqual(null, ui.InputList);
            Assert.AreEqual(InputStatus.Blank, ui.StatusID);
            Assert.AreEqual(InputStatus.Blank, ui.StatusName);
            Assert.AreEqual(InputStatus.Invalid, ui.StatusList);
            Assert.AreEqual(null, ui._Selected);
            Assert.AreEqual(null, ui._EditObject);

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

        //[TestMethod]
        [DataRow(new object[] { InputStatus.Valid, new string[] { "entry_01", "entry_02" } })]
        public void Should_ValidateInputList(object[] inputObjects)
        {
            // Arrange
            object expValue = inputObjects[0];

            // Act
            //ui.InputList = new ObservableCollection<string>((string[])inputObjects[1]);

            // Assert
            Assert.AreEqual(expValue, onListStatusChangeArgs);
        }

        //[TestMethod]
        [DynamicData(nameof(TestInputs_ValidateInputList), DynamicDataSourceType.Method)]
        public void Should_ValidateInputList_dyn(InputStatus expValue, ObservableCollection<string> inputs)
        {
            // Arrange

            // Act
            //ui.InputList = inputs;

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

        //[TestMethod]
        [DynamicData(nameof(TestInputs_BeReady), DynamicDataSourceType.Method,
            DynamicDataDisplayName = nameof(GetTestDisplayName))]
        public void Should_BeReady(object expValue, object[] inputs, string _)
        {
            // Arrange

            // Act
            ui.InputID = (string)inputs[0];
            ui.InputName = (string)inputs[1];
            //ui.InputList = (ObservableCollection<string>)inputs[2];

            // Assert
            Assert.AreEqual(expValue, onReadyStateChangeArgs.Ready);

            Console.WriteLine(onReadyStateChangeArgs.Info);
        }
        public static IEnumerable<object[]> TestInputs_BeReady()
        {
            //const string uqID = "UTSZ0";
            //const string dpID = "STEST";
            //const string name = "Unit Test Size List";
            //const string blnk = "";
            List<string> ITEMS_LIST = new List<string> { "entry_01", "entry_02" };
            List<string> BLANK_LIST = new List<string>();

            return new[]
            {
                new object[] {  true, new object[] { UQID, NAME, ITEMS_LIST }, "All Valid" },
                new object[] { false, new object[] { DPID, NAME, ITEMS_LIST }, "Duplicate ID" },
                new object[] { false, new object[] { BLNK, NAME, ITEMS_LIST }, "Missing ID" },
                new object[] { false, new object[] { UQID, BLNK, ITEMS_LIST }, "Missing Name" },
                new object[] { false, new object[] { UQID, NAME, BLANK_LIST }, "Empty List" },
                new object[] { false, new object[] { UQID, NAME, null }, "Null List" }
            };
        }

        //[TestMethod, DataRow(UQID, NAME, UQID)]
        public void Should_CommitChanges(string id, string name, string expValue)
        {
            // Arrange
            ui.InputID = id;
            ui.InputName = name;
            //ui.InputList = new ObservableCollection<string> { "entry_01", "entry_02" };
            int countBefore = ui.SizeLists.Count;

            // Act
            ui.CommitChanges();

            // Assert
            Assert.AreEqual(expValue, onSetArgs);
            Assert.AreEqual(null, ui._Selected);
            Assert.AreEqual("", ui.InputID);
            Assert.AreEqual("", ui.InputName);
            //Assert.AreEqual(null, ui.InputList);
            Assert.AreEqual(InputStatus.Blank, ui.StatusID);
            Assert.AreEqual(InputStatus.Blank, ui.StatusName);
            Assert.AreEqual(InputStatus.Invalid, ui.StatusList);
            Assert.AreEqual(1, ui.SizeLists.Count - countBefore);
        }

        //[TestMethod]
        public void Should_EditGetRightObject()
        {
            // Arrange
            string[] expectedList = new string[] { "Entry 1", "Entry 2", "Entry 3" };

            // Act
            ui.Edit("STEST");

            // Assert
            Assert.AreEqual("STEST", ui._EditObject.ID);
            Assert.AreEqual("Test Size List", ui._EditObject.Name);
            CollectionAssert.AreEqual(expectedList, ui._EditObject.List);
            Assert.AreEqual(ui.InputID, ui._EditObject.ID);
            Assert.AreEqual(ui.InputName, ui._EditObject.Name);
            //CollectionAssert.AreEqual(ui.InputList, ui._EditObject.List);
        }

        //[TestMethod, DynamicData(nameof(DynInputs_EditCommitChanges))]
        public void Should_EditCommitChanges(string editId, string inputId, string inputName, ObservableCollection<string> inputList)
        {
            // Arrange
            ui.Edit(editId);
            ui.InputID = inputId;
            ui.InputName = inputName;
            //ui.InputList = inputList;

            // Act
            ui.CommitChanges();
            SizeList newObject = ui.SizeLists.Find(item => item.ID == inputId);

            // Assert
            Assert.AreEqual(inputId, newObject.ID);
            Assert.AreEqual(inputName, newObject.Name);
            CollectionAssert.AreEqual(inputList, newObject.List);
        }
        public static IEnumerable<object[]> DynInputs_EditCommitChanges => new[]
        {
            new object[] { "STEST",
                "STEST", "Test Sizes List", new string[] { "Entry 1", "Entry 2", "Entry 3" }.ToList() }
        };

        //[TestMethod]
        [DynamicData(nameof(DynInputs_DraftChanged))]
        public void Should_DraftChanged(object expValue, string editId,
            string inputId, string inputName, ObservableCollection<string> inputList)
        {
            // Arrange
            ui.Edit(editId);

            // Act
            ui.InputID = inputId;
            ui.InputName = inputName;
            //ui.InputList = inputList;

            // Assert
            Assert.AreEqual(expValue, ui._IsChanged);
        }
        public static IEnumerable<object[]> DynInputs_DraftChanged => new[]
        {
            new object[] { false, "STEST", "STEST", "Test Size List", LIST_ },
            new object[] { false, "STEST", null, null, null },
            new object[] { false, "STEST", "STEST", null, null },
            new object[] { false, "STEST", null, "Test Size List", null },
            new object[] { false, "STEST", null, null, LIST_ },

            new object[] { true, "STEST", "XTEST", "Test Size List", LIST_ },
            new object[] { true, "STEST", "STEST", "Test Sizes List", LIST_ },
            new object[] { true, "STEST", "STEST", "Test Size List", LIST1 },
            new object[] { true, "STEST", "XTEST", null, null },
            new object[] { true, "STEST", null, "Test Sizes List", null },
            new object[] { true, "STEST", null, null, LIST1 },
            new object[] { true, "STEST", "XTEST", "Test Sizes List", LIST1 },
        };

        //[TestMethod]
        [DynamicData(nameof(DynInputs_CancelChanges))]
        public void Should_CancelChanges(object[] expValues, string input)
        {
            // Arrange
            ui.Select(input);

            // Act
            ui.CancelChanges();

            // Assert
            Assert.AreEqual(expValues[0], onCancelEventArgs.RestoreID);
            Assert.AreEqual(expValues[1], onCancelEventArgs.EmptyList);
        }
        public static IEnumerable<object[]> DynInputs_CancelChanges => new[]
        {
            new object[] { new object[] { null, false }, null },
            new object[] { new object[] { "STEST", false }, "STEST" },
        };

        //[TestMethod, DynamicData("DynInputs_Remove")]
        public void Should_Remove(int expValue, string input)
        {
            // Arrange
            // Act
            ui.Remove(input);
            ui.Select(input);

            // Assert
            Assert.AreEqual(expValue, onRemoveArgs);
            Assert.AreEqual(null, onSelectionArgs.Selected);
        }
        public static IEnumerable<object[]> DynInputs_Remove => new[]
        {
            new object[] { 25, "STEST" },
        };

        //[TestMethod]
        public void MyTestMethod()
        {
            // Arrange

            // Act
            ui.Edit("STEST");
            //ui.AddEntry("Test Entry");
            //ui.EditEntry("Test Entry", "Test Entry 1");
            //ui.RemoveEntry("Test Entry 1");
            //ui.AddEntry("Test Entry 2");

            // Assert
            Assert.AreEqual(InputStatus.Valid, ui.StatusList);
        }

        //[TestMethod]
        public void Should_PartialModify()
        {
            // Arrange
            ui.Select("STEST");
            ui.PartialModify_Entries();
            ui.New_Entry();
            ui.InputEntry = "Entry 5";

            // Act
            ui.CommitChanges_Entry();
            ui.PartialCommit_Entries();


            // Assert
        }

        //[TestMethod]
        public void Should_Revert()
        {
            // Arrange
            ui.Select("STEST");
            ui.PartialModify_Entries();
            ui.New_Entry();
            ui.InputEntry = "Entry 4 (New)";

            // Act
            ui.CommitChanges_Entry();
            ui.Revert_Entries();

            // Assert

        }
        #endregion

        [TestMethod]
        public void Should_EditAndAddNewEntry()
        {
            ui.Select("STEST");
            ui.Edit();
            ui.Load_Entries();
            ui.New_Entry();
            ui.InputEntry = "Entry 4";
            ui.CommitChanges_Entry();
            ui.Save_Entries();
            ui.CommitChanges();
        }

        [TestMethod]
        public void Should_EditAndEditExistingEntry()
        {
            ui.Select("STEST");
            ui.Edit();
            ui.Load_Entries();
            ui.SelectEntry("Entry 3");
            ui.Edit_Entry();
            ui.InputEntry = "Entry 3 (edit)";
            ui.CommitChanges_Entry();
            ui.Save_Entries();
            ui.CommitChanges();
        }
        //[TestMethod]
        public void Should_SaveEntries()
        {
            ui.Select("STEST");
            ui.Edit();

            ui.Load_Entries();

            ui.InputEntry = "Entry 4 (New)";
        }

        // Generic Method
        public static string GetTestDisplayName(MethodInfo mInfo, object[] data)
        {
            return $"[{ data[2].ToString() }] - Expected: { data[0] }";
        }
    }
}

//[TestMethod, DynamicData(nameof(DynInputs_))]
//public void Should_(int expValue, string input)
//{
//    // Arrange

//    // Act

//    // Assert
//    Assert.AreEqual(expValue, null);
//}
//public static IEnumerable<object[]> DynInputs_ => new[]
//{
//            new object[] { },
//};

/*
// Arrange

// Act

// Assert


//object obj = new object[] { 1, "S" };
//int oFirst = (int)((object[])obj)[0];
//string oSecond = (string)((object[])obj)[1];
*/
