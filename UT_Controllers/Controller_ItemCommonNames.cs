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
        ItemCommonNamesController ui_ComnName;

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
            ui_ComnName = ui.CommonNames;

            EventSubscriber();
        }

        private void EventSubscriber()
        {
            // parent
            ui.OnCommonNamesStatusChange += Ui_OnCommonNamesStatusChange;

            ui.CommonNames.OnLoad += CommonNames_OnLoad;
            ui.CommonNames.OnSet += CommonNames_OnSet;
            ui.CommonNames.OnSelect += CommonNames_OnSelect;
            ui.CommonNames.OnPreDrafting += CommonNames_OnPreDrafting;
            ui.CommonNames.OnCommonNameStatusChange += CommonNames_OnCommonNameStatusChange;
            ui.CommonNames.OnReadyStateChange += CommonNames_OnReadyStateChange;
            ui.CommonNames.OnRemove += CommonNames_OnRemove;
            ui.CommonNames.OnCancel += CommonNames_OnCancel;
            ui.CommonNames.OnSave += CommonNames_OnSave;
        }

        private void CommonNames_OnSave(object sender, EventArgs e)
        {
            Log(delegate { Console.WriteLine("Common Names Saved"); });
        }

        private void Ui_OnCommonNamesStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {

                ((IList)e.Value).WriteStringList('•');
            });
        }

        private void CommonNames_OnCancel(object sender, CancelEventArgs e)
        {
            Log(delegate
            {
                if (e.Restore == null)
                {
                    Console.WriteLine("No object were selected to restore");
                }
                else
                {
                    Console.WriteLine("Restore Object = {0}", e.Restore);
                }
                if (e.EmptyList)
                {
                    Console.WriteLine("No list to restore");
                }
                else
                {
                    ((IList)e.List).WriteStringList('•');
                }
            });
        }

        private void SetParent()
        {
            ui.Select("CPRP1");
            ui.Edit();
            ui.ModifyCommonNames();
        }

        private void CommonNames_OnRemove(object sender, RemoveEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Name Removed: {0}", e.RemoveObject);
                ((IList)e.NewObjects).WriteStringList('•');
            });
        }

        private void CommonNames_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Ready = {0}, Info = {1}", e.Ready, e.Info);
            });
        }

        private void CommonNames_OnCommonNameStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Input Common Name = {0}, Status = {1}",
                    string.IsNullOrWhiteSpace((string)e.Value) ?
                    (e.Value == null ? "<null>" : "<blank>") :
                    e.Value,
                    e.Status.ToString());
            });
        }

        private void CommonNames_OnPreDrafting(object sender, PreModifyEventArgs e)
        {
            Log(delegate
            {
                if (e.Draft != null)
                {
                    Console.WriteLine("Edit Common Name = {0}", (string)e.Draft);
                }
                else
                {
                    Console.WriteLine("Adding New Common Name");
                }
                e.List.WriteStringList('•');
            });
        }

        private void CommonNames_OnSelect(object sender, SelectEventArgs<string> e)
        {
            Log(delegate
            {
                Console.WriteLine("Common Name '{0}' selected", e.Selected);
            });
        }

        private void CommonNames_OnSet(object sender, SetEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("New Value = {0}", e.NewID);
                Console.WriteLine("Old Value = {0}", e.OldID);
                e.NewList.WriteStringList('•');
            });
        }

        private void CommonNames_OnLoad(object sender, LoadEventArgs e)
        {
            Console.WriteLine(e.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_CommonNamesController()
        {
            SetParent();

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
            SetParent();

            ui.CommonNames.Select("Copper Tube");
        }

        [TestMethod]
        public void Test_ComonNameNew()
        {
            SetParent();

            ui.CommonNames.New();
        }

        [TestMethod]
        public void Test_ComonNameEdit()
        {
            SetParent();

            SKIP_LOG = true;
            ui.CommonNames.Select("Copper Tube");
            SKIP_LOG = false;
            ui.CommonNames.Edit();
            ui.CommonNames.InputCommonName = null;
            ui.CommonNames.InputCommonName = "";
            ui.CommonNames.InputCommonName = " ";
            ui.CommonNames.InputCommonName = "Copper Tube";
            ui.CommonNames.InputCommonName = "Copper Tubing";
        }

        [TestMethod]
        public void Test_ComonNameNew_Commit()
        {
            SetParent();

            ui.CommonNames.New();
            ui.CommonNames.InputCommonName = "Copper Tubing";
            ui.CommonNames.CommitChanges();
        }

        [TestMethod]
        public void Test_ComonNameEdit_Commit()
        {
            SetParent();

            SKIP_LOG = true;
            ui.CommonNames.Select("Copper Tube");
            SKIP_LOG = false;
            ui.CommonNames.Edit();
            ui.CommonNames.InputCommonName = "Copper Tubing";

            ui.CommonNames.CommitChanges();
        }

        [TestMethod]
        public void Test_CommonNameRemove()
        {
            SetParent();

            SKIP_LOG = true;
            ui.CommonNames.Select("ماسورة نحاس");
            SKIP_LOG = false;

            ui.CommonNames.Remove();
        }

        [TestMethod]
        public void Test_NoSelection_New_CancelChanges()
        {
            SetParent();

            SKIP_LOG = true;
            ui_ComnName.New();
            ui_ComnName.InputCommonName = "Whatever name doesn't matter";
            SKIP_LOG = false;
            ui_ComnName.CancelChanges();
        }

        [TestMethod]
        public void Test_Selection_New_CancelChanges()
        {
            SetParent();

            SKIP_LOG = true;
            ui_ComnName.Select("Copper Tube");
            ui_ComnName.New();
            ui_ComnName.InputCommonName = "Whatever name doesn't matter";
            SKIP_LOG = false;
            ui_ComnName.CancelChanges();
        }

        [TestMethod]
        public void Test_Edit_CancelChanges()
        {
            SetParent();

            SKIP_LOG = true;
            ui_ComnName.Select("ماسورة نحاس");
            ui_ComnName.Edit();
            ui_ComnName.InputCommonName = "Whatever name doesn't matter";
            SKIP_LOG = false;
            ui_ComnName.CancelChanges();
        }

        [TestMethod]
        public void Test_Save()
        {
            SetParent();

            ui.CommonNames.New();
            ui.CommonNames.InputCommonName = "Copper Tubing";
            ui.CommonNames.CommitChanges();

            ui_ComnName.Save();
        }
    }
}
