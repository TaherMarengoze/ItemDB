using Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace UT_Controllers
{
    [TestClass]
    public class Controller_ItemCommonNames
    {
        bool SKIP_LOG = false;

        ItemController ui;

        private void Log(Action loggingActions)
        {
            if (SKIP_LOG)
                return;

            //Console.WriteLine(LINE_START);
            loggingActions.Invoke();
            //Console.WriteLine(SEPARATOR_LINE);
            Console.WriteLine();
        }

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();
            ui = new ItemController();

            EventSubscriber();
        }

        private void EventSubscriber()
        {
            ui.CommonNames.OnLoad += CommonNames_OnLoad;
            ui.CommonNames.OnCommonNameStatusChange += CommonNames_OnCommonNameStatusChange;
            ui.CommonNames.OnSet += CommonNames_OnSet;
            ui.CommonNames.OnSelect += CommonNames_OnSelect;
            ui.CommonNames.OnPreDrafting += CommonNames_OnPreDrafting;
        }

        private void CommonNames_OnPreDrafting(object sender, PreModifyEventArgs e)
        {
            Log(delegate
            {
                if (e.Draft != null)
                {
                    Console.WriteLine("Edit Common Name = {0}", (string)e.Draft);
                }
                e.List.WriteList(" • ");
            });
        }

        private void CommonNames_OnSelect(object sender, SelectEventArgs<string> e)
        {
            Console.WriteLine("Common Name '{0}' selected", e.Selected);
        }

        private void CommonNames_OnSet(object sender, SetEventArgs e)
        {
            Console.WriteLine("Item set, list contains {0} item(s)",
                e.NewList.Count);
        }

        private void CommonNames_OnCommonNameStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input Common Name = {0}, Status = {1}",
                    e.Value.ToString(),
                    e.Status.ToString());
            });
        }

        private void CommonNames_OnLoad(object sender, LoadEventArgs e)
        {
            Console.WriteLine(e.Count);
        }

        [TestMethod]
        public void Test_CommonNamesController()
        {
            ui.ModifyCommonNames();
            ui.CommonNames.Load();
            // add a common name entry
            ui.CommonNames.InputCommonName = "New Common Name 1";
            ui.CommonNames.CommitChanges();

            // add another common name entry (different name)
            ui.CommonNames.InputCommonName = "New Common Name 2";
            ui.CommonNames.CommitChanges();

            // add another common name entry (duplicate name)
            ui.CommonNames.InputCommonName = "New Common Name 2";
            ui.CommonNames.CommitChanges(); // <== execption source

            ui.CommonNames.Save();
        }

        [TestMethod]
        public void Test_ComonNameSelect()
        {
            ui.Select("CPRP1");
            ui.Edit();
            ui.ModifyCommonNames();
            ui.CommonNames.Select("Copper Tube");
        }

        [TestMethod]
        public void Test_ComonNameNew()
        {
            ui.Select("CPRP1");
            ui.Edit();
            ui.ModifyCommonNames();
            ui.CommonNames.New();
        }

        [TestMethod]
        public void Test_ComonNameEdit()
        {
            ui.Select("CPRP1");
            ui.Edit();
            ui.ModifyCommonNames();
            ui.CommonNames.Select("Copper Tube");
            ui.CommonNames.Edit();
        }
    }
}
