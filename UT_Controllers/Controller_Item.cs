using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Interfaces.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modeling.ViewModels.Item;
using XmlDataSource.Repository;

namespace UT_Controllers
{

    [TestClass]
    public class Controller_Item
    {
        bool SKIP_LOG = false;

        ItemController ui;
        private bool commitReady;

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();
            ui = new ItemController();

            EventSubscriber();
        }

        private void Log(Action loggingActions)
        {
            if (SKIP_LOG)
                return;

            //Console.WriteLine(LINE_START);
            loggingActions.Invoke();
            //Console.WriteLine(SEPARATOR_LINE);
            Console.WriteLine();
        }

        private void EventSubscriber()
        {
            ui.OnLoad += Ui_OnLoad;
            ui.OnSelect += Ui_OnSelect;
            ui.OnRemove += Ui_OnRemove;
            ui.OnPreDrafting += Ui_OnPreDrafting;
            ui.OnReadyStateChange += Ui_OnReadyStateChange;
            ui.OnIdStatusChange += Ui_OnIdStatusChange;
            ui.OnBaseNameStatusChange += Ui_OnBaseNameStatusChange;
            ui.OnDisplayNameStatusChange += Ui_OnDisplayNameStatusChange;
            ui.OnDescriptionStatusChange += Ui_OnDescriptionStatusChange;
            ui.OnUomStatusChange += Ui_OnUomStatusChange;
            ui.OnCatIdStatusChange += Ui_OnCatIdStatusChange;
            ui.OnCatNameStatusChange += Ui_OnCatNameStatusChange;
            ui.OnCommonNamesStatusChange += Ui_OnCommonNamesStatusChange;
            ui.OnSet += Ui_OnSet;
        }

        SetEventArgs setEventArgs;
        private void Ui_OnSet(object sender, SetEventArgs e)
        {
            setEventArgs = e;
            Log(delegate
            {
                if (e.OldID == null)
                {
                    Console.WriteLine("New item added [ID = {0}]", e.NewID);
                }
                else
                {
                    Console.WriteLine("Item edited [ID = {0}{1}]",
                        e.NewID,
                        e.NewID != e.OldID ? ", Old ID = " + e.OldID : "");
                }
                e.NewList.WriteList();
            });
        }

        private void Ui_OnCommonNamesStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input Common Names = {0}, Status = {1}",
                    ((IList)e.Value).Count,
                    e.Status/*.ToString()*/);
            });
        }

        private void Ui_OnCatNameStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input CatName = {0}, Status = {1}",
                    e.Value.ToString(),
                    e.Status.ToString());
            });
        }

        private void Ui_OnCatIdStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input CatID = {0}, Status = {1}",
                    e.Value.ToString(),
                    e.Status.ToString());
            });
        }

        private void Ui_OnUomStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input UoM = {0}, Status = {1}",
                    e.Value.ToString(),
                    e.Status.ToString());
            });
        }

        private void Ui_OnDescriptionStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input Description = {0}, Status = {1}",
                    e.Value.ToString(),
                    e.Status.ToString());
            });
        }

        private void Ui_OnDisplayNameStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input DisplayName = {0}, Status = {1}",
                    e.Value.ToString(),
                    e.Status.ToString());
            });
        }

        private void Ui_OnBaseNameStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input BaseName = {0}, Status = {1}",
                    e.Value.ToString(),
                    e.Status.ToString());
            });
        }

        private void Ui_OnIdStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input ID = {0}, Status = {1}",
                    e.Value.ToString().PadRight(5, '_'),
                    e.Status.ToString());
            });
        }

        private void Ui_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Ready = {0}, Info = {1}", e.Ready, e.Info);
            });
            commitReady = e.Ready;
        }

        private void Ui_OnPreDrafting(object sender, PreModifyEventArgs e)
        {
            Log(delegate
            {
                if (e.Draft != null)
                {
                    var item = (IItem)e.Draft;
                    int l = new List<string> {
                        item.ItemID,
                        item.BaseName,
                        item.DisplayName,
                        item.CatID,
                        item.CatName,
                    }.Max(s => s.Length);

                    Console.WriteLine("╔═ EDIT ITEM ═════{0}═╗", "═".PadRight(l, '═'));
                    Console.WriteLine("║ ID            = {0} ║", item.ItemID.PadRight(l));
                    Console.WriteLine("║ Name          = {0} ║", item.BaseName.PadRight(l));
                    Console.WriteLine("║ Display       = {0} ║", item.DisplayName.PadRight(l));
                    Console.WriteLine("║ Category ID   = {0} ║", item.CatID.PadRight(l));
                    Console.WriteLine("║ Category Name = {0} ║", item.CatName.PadRight(l));
                    Console.WriteLine("║ Common Names  = {0} ║", item.CommonNames.Count.ToString().PadRight(l));
                    Console.WriteLine("╚═════════════════{0}═╝", "═".PadRight(l, '═'));
                }
                e.List.WriteList(" • ");
            });
        }

        private void Ui_OnRemove(object sender, RemoveEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Item removed: {0}", e.RemoveObject);
                Console.WriteLine();
                ((List<GenericView>)e.NewObjects).DrawTable();
            });
        }

        private void Ui_OnSelect(object sender,
            SelectEventArgs<IItem> e)
        {
            Log(delegate
            {
                int l = new List<string> {
                    e.RequestInfo,
                    e.Selected?.BaseName,
                    e.Selected?.DisplayName,
                    e.Selected?.CatID,
                    e.Selected?.CatName,
                }.Max(s => s.Length);

                Console.WriteLine("╔═════════════════{0}═╗", "═".PadRight(l, '═'));
                Console.WriteLine("║ ID            = {0} ║", e.RequestInfo.PadRight(l));
                Console.WriteLine("║ Name          = {0} ║", e.Selected?.BaseName.PadRight(l));
                Console.WriteLine("║ Display       = {0} ║", e.Selected?.DisplayName.PadRight(l));
                Console.WriteLine("║ Category ID   = {0} ║", e.Selected?.CatID.PadRight(l));
                Console.WriteLine("║ Category Name = {0} ║", e.Selected?.CatName.PadRight(l));
                Console.WriteLine("╚═════════════════{0}═╝", "═".PadRight(l, '═'));
            });
        }

        private void Ui_OnLoad(object sender, LoadEventArgs e)
        {
            //new ListViewer(e.GenericViewList).ShowDialog();

            Console.WriteLine("{0} item(s)", e.Count);
            foreach (var item in (IList)e.GenericViewList)
            {
                Console.WriteLine(" • {0}", item);
            }
        }

        [TestMethod]
        public void Test_Load()
        {
            ui.Load();
        }

        [TestMethod]
        public void Test_Select()
        {
            ui.Select("BSP01");
            ui.Select("SSP");
        }

        [TestMethod]
        public void Test_Delete()
        {
            SKIP_LOG = true;
            ui.Select("GWH01");
            ui.Remove();
            ui.Select("GWH02");
            ui.Remove();
            ui.Select("ANGR1");
            ui.Remove();
            ui.Select("DIGR1");
            SKIP_LOG = false;
            ui.Remove();
        }

        [TestMethod]
        public void Test_New()
        {
            ui.New();
        }

        [TestMethod]
        public void Test_New_ReadyStatus()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputID = ""; // blank
            ui.InputID = "BSP01"; // dupe
            ui.InputID = "TEST0"; // valid
        }

        [TestMethod]
        public void Test_New_InputId()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputID = ""; // blank
            ui.InputID = "BSP01"; // dupe
            ui.InputID = "TEST0"; // valid
        }

        [TestMethod]
        public void Test_New_InputBaseName()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputBaseName = "test base item";
        }

        [TestMethod]
        public void Test_New_InputDisplayName()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputDisplayName = "test display item";
        }

        [TestMethod]
        public void Test_New_InputDescription()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputDescription = "test item description";
        }

        [TestMethod]
        public void Test_New_InputUoM()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputUom = "test item uom";
        }

        [TestMethod]
        public void Test_New_InputCatID()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputCatId = "test item Cat ID";
        }

        [TestMethod]
        public void Test_New_InputCatName()
        {
            SKIP_LOG = true;
            ui.New();
            SKIP_LOG = false;
            ui.InputCatName = "test item Cat Name";
        }

        [TestMethod]
        public void Test_New_InputAll()
        {
            SKIP_LOG = true;
            ui.New();
            ui.InputID = "TEST0";
            ui.InputBaseName = "Base Name";
            ui.InputDisplayName = "Display Name";
            ui.InputDescription = "Description";
            ui.InputUom = "UoM";
            ui.InputCatId = "CATID";
            ui.InputCatName = "New Category";

            ui.ModifyCommonNames();
            ui.CommonNames.New();
            ui.CommonNames.InputCommonName = "Common Name 1";
            ui.CommonNames.CommitChanges();
            ui.CommonNames.New();
            ui.CommonNames.InputCommonName = "Common Name 2";
            ui.CommonNames.CommitChanges();
            ui.CommonNames.Save();
            SKIP_LOG = false;

            Assert.IsTrue(commitReady, "Invalid object");
        }

        [TestMethod]
        [DataRow(true, "TEST0", "Base Name", "Display Name", "UoM", "CATID", "New Category",
            "Description", true , DisplayName = "All given")]
        [DataRow(true, "TEST0", "Base Name", "Display Name", "UoM", "CATID", "New Category",
            ""           , true , DisplayName = "Blank description")]
        [DataRow(true, "TEST0", "Base Name", "Display Name", "UoM", "CATID", "New Category",
            ""           , false, DisplayName = "Blank common names")]
        [DataRow(true, "TEST0", "Base Name", "Display Name", "UoM", "CATID", "New Category",
            null         , false , DisplayName = "null description")]
        public void MyTestMethod(bool expected,
            string id,
            string baseName,
            string displayName,
            string uom,
            string catId,
            string catName,
            string desc,
            bool commonNames)
        {
            SKIP_LOG = true;
            ui.New();
            ui.InputID = id;
            ui.InputBaseName = baseName;
            ui.InputDisplayName = displayName;
            ui.InputDescription = desc;
            ui.InputUom = uom;
            ui.InputCatId = catId;
            ui.InputCatName = catName;

            if (commonNames)
            {
                ui.ModifyCommonNames();
                ui.CommonNames.New();
                ui.CommonNames.InputCommonName = "Common Name 1";
                ui.CommonNames.CommitChanges();
                ui.CommonNames.New();
                ui.CommonNames.InputCommonName = "Common Name 2";
                ui.CommonNames.CommitChanges();
                ui.CommonNames.Save();
            }

            SKIP_LOG = false;

            Assert.IsTrue(expected, "Invalid object");
        }

        [TestMethod]
        public void Test_ItemEdit()
        {
            SKIP_LOG = true;
            ui.Select("CPRP1");
            SKIP_LOG = false;
            ui.Edit();
        }

        /// <summary>
        /// Tests <see cref="ItemController.CommitChanges"/> method when adding
        /// a new item.
        /// </summary>
        [TestMethod]
        public void NewCommitChangesTest()
        {
            SKIP_LOG = true;

            ui.New();

            // mandatory inputs
            ui.InputID = "TEST0";
            ui.InputBaseName = "Base Name";
            ui.InputDisplayName = "Display Name";
            ui.InputUom = "UoM";
            ui.InputCatId = "CATID";
            ui.InputCatName = "New Category";

            // optional inputs
            ui.InputDescription = "Description";

            // optional list inputs
            ui.ModifyCommonNames();
            ui.CommonNames.New();
            ui.CommonNames.InputCommonName = "Common Name 1";
            ui.CommonNames.CommitChanges();
            ui.CommonNames.New();
            ui.CommonNames.InputCommonName = "Common Name 2";
            ui.CommonNames.CommitChanges();
            ui.CommonNames.Save();

            SKIP_LOG = false;

            ui.CommitChanges(); // event response: Ui_OnSet

            Assert.AreEqual("TEST0", setEventArgs.NewID);
            Assert.IsNull(setEventArgs.OldID);
            Assert.AreEqual(56, setEventArgs.Count);
        }

        /// <summary>
        /// Tests <see cref="ItemController.CommitChanges"/> method when
        /// editing an existing item.
        /// </summary>
        [TestMethod]
        public void EditCommitChanges()
        {
            string oldId = "BSP01";
            CategoryCase catCase = CategoryCase.NEW;
            InputCase itemCase = InputCase.UNCHANGED;
            string newId =
                ((itemCase & (InputCase.FIELDS | InputCase.UNCHANGED)) != 0)
                ? oldId : "TEST0";

            SKIP_LOG = true;

            ui.Select(oldId);
            ui.Edit();

            CategoryInputs(catCase);
            ItemInputs(itemCase);

            SKIP_LOG = false;

            ui.CommitChanges(); // event response: Ui_OnSet

            Assert.AreEqual(newId, setEventArgs.NewID);
            Assert.AreEqual(oldId, setEventArgs.OldID);
            Assert.AreEqual(3, setEventArgs.Count);
        }

        enum CategoryCase
        {
            UNCHANGED,
            NEW,
            EXISTING
        }

        [Flags]
        enum InputCase
        {
            UNCHANGED = 1,
            ALL = 2,
            FIELDS = 4,
            ID = 8
        }

        private void CategoryInputs(CategoryCase categoryCase)
        {
            switch (categoryCase)
            {
                case CategoryCase.NEW:
                    // new category
                    ui.InputCatId = "CATID";
                    ui.InputCatName = "New Category";
                    break;
                case CategoryCase.EXISTING:
                    // existing category
                    ui.InputCatId = "STPLT";
                    ui.InputCatName = "Steel Plates";
                    break;
                default:
                    break;
            }
        }

        private void ItemInputs(InputCase inputCase)
        {
            switch (inputCase)
            {
                case InputCase.ALL:
                    // mandatory inputs
                    ui.InputID = "TEST0";
                    ui.InputBaseName = "Test Base Name";
                    ui.InputDisplayName = "Display Name";
                    ui.InputUom = "UoM";

                    // optional inputs
                    ui.InputDescription = "Description";

                    // optional list inputs
                    ui.ModifyCommonNames();
                    ui.CommonNames.New();
                    ui.CommonNames.InputCommonName = "Carbon Steel Pipe";
                    ui.CommonNames.CommitChanges();
                    ui.CommonNames.Save();
                    break;
                case InputCase.FIELDS:
                    // mandatory inputs
                    ui.InputBaseName = "Base Name";
                    ui.InputDisplayName = "Display Name";
                    ui.InputUom = "UoM";

                    // optional inputs
                    ui.InputDescription = "Description";

                    // optional list inputs
                    ui.ModifyCommonNames();
                    ui.CommonNames.New();
                    ui.CommonNames.InputCommonName = "Carbon Steel Pipe";
                    ui.CommonNames.CommitChanges();
                    ui.CommonNames.Save();
                    break;
                case InputCase.ID:
                    ui.InputID = "TEST0";
                    break;
                default:
                    break;
            }

        }
    }
}
