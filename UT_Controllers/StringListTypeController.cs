using Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace UT_Controllers
{
    [TestClass]
    public class StringListTypeController
    {
        bool SKIP_LOG = false;

        ItemController ui;
        ItemCommonNamesController ui_Child;

        private void Log(Action loggingActions)
        {
            if (SKIP_LOG)
                return;

            loggingActions.Invoke();
            Console.WriteLine();
        }

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();
            ui = new ItemController();
            ui_Child = ui.CommonNames;

            EventSubscriber();
        }

        private void EventSubscriber()
        {
            // parent
            ui.OnCommonNamesStatusChange += Ui_OnCommonNamesStatusChange;

            ui_Child.OnLoad                   += Ui_Child_OnLoad;
            ui_Child.OnSet                    += Ui_Child_OnSet;
            ui_Child.OnSelect                 += Ui_Child_OnSelect;
            ui_Child.OnPreDrafting            += Ui_Child_OnPreDrafting;
            ui_Child.OnCommonNameStatusChange += Ui_Child_OnCommonNameStatusChange;
            ui_Child.OnReadyStateChange       += Ui_Child_OnReadyStateChange;
            ui_Child.OnRemove                 += Ui_Child_OnRemove;
            ui_Child.OnCancel                 += Ui_Child_OnCancel;
            ui_Child.OnSave                   += Ui_Child_OnSave;
        }

        private void Ui_Child_OnSave(object sender, EventArgs e)
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

        private void Ui_Child_OnCancel(object sender, CancelEventArgs e)
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

        private void Ui_Child_OnRemove(object sender, RemoveEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Name Removed: {0}", e.RemoveObject);
                ((IList)e.NewObjects).WriteStringList('•');
            });
        }

        private void Ui_Child_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Ready = {0}, Info = {1}", e.Ready, e.Info);
            });
        }

        private void Ui_Child_OnCommonNameStatusChange(object sender, StatusEventArgs e)
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

        private void Ui_Child_OnPreDrafting(object sender, PreModifyEventArgs e)
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

        private void Ui_Child_OnSelect(object sender, SelectEventArgs<string> e)
        {
            Log(delegate
            {
                Console.WriteLine("Common Name '{0}' selected", e.Selected);
            });
        }

        private void Ui_Child_OnSet(object sender, SetEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("New Value = {0}", e.NewID);
                Console.WriteLine("Old Value = {0}", e.OldID);
                e.NewList.WriteStringList('•');
            });
        }

        private void Ui_Child_OnLoad(object sender, LoadEventArgs e)
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
            ui_Child.New();
            ui_Child.InputCommonName = "Whatever name doesn't matter";
            SKIP_LOG = false;
            ui_Child.CancelChanges();
        }

        [TestMethod]
        public void Test_Selection_New_CancelChanges()
        {
            SetParent();

            SKIP_LOG = true;
            ui_Child.Select("Copper Tube");
            ui_Child.New();
            ui_Child.InputCommonName = "Whatever name doesn't matter";
            SKIP_LOG = false;
            ui_Child.CancelChanges();
        }

        [TestMethod]
        public void Test_Edit_CancelChanges()
        {
            SetParent();

            SKIP_LOG = true;
            ui_Child.Select("ماسورة نحاس");
            ui_Child.Edit();
            ui_Child.InputCommonName = "Whatever name doesn't matter";
            SKIP_LOG = false;
            ui_Child.CancelChanges();
        }

        [TestMethod]
        public void Test_Save()
        {
            SetParent();

            ui.CommonNames.New();
            ui.CommonNames.InputCommonName = "Copper Tubing";
            ui.CommonNames.CommitChanges();

            ui_Child.Save();
        }
    }
}
