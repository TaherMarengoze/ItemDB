using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using CoreLibrary.Enums;
using Interfaces.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modeling.ViewModels.Item;
using XmlDataSource.Repository;

namespace UT_Controllers
{
    public static class Extension
    {
        public static void WriteList(this IList list, string bullet = " * ")
        {
            foreach (var listItem in list)
            {
                Console.WriteLine("{1}{0}", listItem, bullet);
            }
            Console.WriteLine();
        }

        public static void DrawTable(this IEnumerable<GenericView> list)
        {
            var length = list.Max(s => s.BaseName.Length);

            Console.WriteLine("╔════════╦═{0}═╗", "═".PadRight(length, '═'));
            Console.WriteLine("║ ItemID ║ {0} ║", "Base Name".PadBoth(length));
            Console.WriteLine("╠════════╬═{0}═╣", "═".PadRight(length, '═'));
            foreach (var item in list)
            {
                Console.WriteLine("║ {0} ║ {1} ║",
                    item.ID.PadRight(6),
                    item.BaseName.PadRight(length));
            }
            Console.WriteLine("╚════════╩═{0}═╝", "═".PadRight(length, '═'));
            Console.WriteLine("{0} item(s)", list.Count());
        }


        public static string PadBoth(this string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }

    }

    [TestClass]
    public class Controller_Item
    {
        bool SKIP_LOG = false;

        ItemController ui;

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
                    e.Value.ToString().PadRight(5,'_'),
                    e.Status.ToString());
            });
        }

        private void Ui_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Ready = {0}, Info = {1}", e.Ready, e.Info);
            });
        }

        private void Ui_OnPreDrafting(object sender, PreModifyEventArgs e)
        {
            Log(delegate
            {
                e.List.WriteList();
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
            SKIP_LOG = false;
            ui.InputID = "TEST0";
            ui.InputBaseName = "Base Name";
            ui.InputDisplayName = "Display Name";
            ui.InputDescription = "Description";
            ui.InputUom = "UoM";
            ui.InputCatId = "CATID";
            ui.InputCatName = "New Category";
            
        }
    }
}
