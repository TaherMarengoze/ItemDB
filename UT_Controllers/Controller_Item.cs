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
                Console.WriteLine("ID           : {0}", e.RequestInfo);
                Console.WriteLine("Name         : {0}", e.Selected?.BaseName);
                Console.WriteLine("Display      : {0}", e.Selected?.DisplayName);
                Console.WriteLine("Category ID  : {0}", e.Selected?.CatID);
                Console.WriteLine("Category Name: {0}", e.Selected?.CatName);
                Console.WriteLine();
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
    }

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
}
