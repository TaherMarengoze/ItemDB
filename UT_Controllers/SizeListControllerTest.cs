using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Controllers;
using CoreLibrary.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modeling.DataModels;
using Modeling.ViewModels.Common;

namespace UT_Controllers
{
    [TestClass]
    public class SizeListControllerTest
    {
        private const string LINE_START = "----- START OF MESSAGE -----";
        private const string SEPARATOR_LINE = "----- END OF MESSAGE -----\n";

        private bool SKIP_LOG;

        SizeListController ui;

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();

            ui = new SizeListController();
            EventSubscriber();
            SKIP_LOG = false;
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

        #region Controller Events
        private void EventSubscriber()
        {
            ui.OnLoad += Ui_OnLoad;
            ui.OnSelect += Ui_OnSelect;
            ui.OnPreDrafting += Ui_OnPreDrafting;
            ui.OnIdStatusChange += Ui_OnIdStatusChange;
            ui.OnNameStatusChange += Ui_OnNameStatusChange;
            ui.OnListStatusChange += Ui_OnListStatusChange;
            ui.OnReadyStateChange += Ui_OnReadyStateChange;
            ui.OnSet += Ui_OnSet;
            ui.OnCancel += Ui_OnCancel;
            ui.OnRemove += Ui_OnRemove;

            ui.OnLoadEntries += Ui_OnLoadEntries;
            ui.OnSaveEntries += Ui_OnSaveEntries;
            ui.OnRevertEntries += Ui_OnRevertEntries;
            ui.OnEntrySelect += Ui_OnEntrySelect;
            ui.OnEntryStatusChange += Ui_OnEntryStatusChange;
            ui.OnEntrySet += Ui_OnEntrySet;
            ui.OnEntryCancel += Ui_OnEntryCancel;
            ui.OnEntryRemove += Ui_OnEntryRemove;
        }
        #endregion
        
        #region Controller Events Responses
        private void Ui_OnLoad(object sender, LoadEventArgs e)
        {
            var viewList = (List<FieldListGenericView>)e.GenericViewList;
            Log(delegate
            {
                Console.WriteLine("{0} object(s) loaded\n", e.Count);
                foreach (FieldListGenericView item in viewList)
                {
                    Console.WriteLine("{0}{3}[ {2:00} item(s) ]\t{1}"
                        , item.ID, item.Name, item.EntriesCount,
                        item.ID.Length <= 5 ? "\t\t" : "\t");
                }
            });
        }
        private void Ui_OnSelect(object sender, SelectEventArgs<SizeList> e)
        {
            Log(delegate
            {
                Console.WriteLine("Object selected [ID = {0}, Name = {1}]",
                e.Selected.ID, e.Selected.Name);
                Console.WriteLine("List =");
                Console.WriteLine(" • {0}", string.Join("\n • ",
                    e.Selected.List));

                Console.WriteLine("[{0} item(s)]", e.Selected.List.Count);
            });
        }
        private void Ui_OnPreDrafting(object sender, PreDraftingEventArgs e)
        {
            bool isNew = e.DraftObject == null;
            string mode = isNew ? "Adding New" : "Edit Existing";

            Log(delegate
            {
                Console.WriteLine("{0}", mode);
                if (!isNew)
                {
                    Console.WriteLine("Edit object [ID = {0}, Name = {1}]",
                        e.DraftObject.ID, e.DraftObject.Name);

                    Console.WriteLine("List =");
                    Console.WriteLine(" • {0}", string.Join("\n • ",
                        e.DraftObject.List));

                    Console.WriteLine($"[{e.DraftObject.List.Count} item(s)]");
                }
            });
        }
        private void Ui_OnSelection(object sender, SizeListSelectionEventArgs e)
        {
            
        }
        private void Ui_OnIdStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("ID Status = {0}, [{1}]",
                    e.Status.ToString(), e.Value);
            });
        }
        private void Ui_OnNameStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Name Status = {0}, [{1}]",
                    e.Status.ToString(), e.Value);
            });
        }
        private void Ui_OnListStatusChange(object sender, StatusEventArgs e)
        {
            int count = ((List<string>)e.Value).Count;
            Log(delegate
            {
                Console.WriteLine("List Status = {0}", e.Status.ToString());
                Console.WriteLine("List =");
                Console.WriteLine(" • {0}",
                    string.Join("\n • ", (List<string>)e.Value));

                Console.WriteLine($"[{count} item(s)]");
            });
        }
        private void Ui_OnReadyStateChange(object sender, ReadyEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Draft object state: {0}", e.Info);
            });
        }
        private void Ui_OnSet(object sender, SetEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine();
                Console.WriteLine("Object set ({1}) [ID = {0}]",
                e.NewID, e.OldID == null ? "Add" : "Edit");
            });

            //ui.Select(e.NewID);
        }
        private void Ui_OnCancel(object sender, CancelEventArgs e)
        {
            
        }
        private void Ui_OnRemove(object sender, RemoveEventArgs e)
        {
            
        }
        private void Ui_OnLoadEntries(object sender, LoadEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Entries loaded for modification");
                Console.WriteLine("Entries:");
                Console.WriteLine(" • {0}",
                    string.Join("\n • ",
                    (ObservableCollection<string>)e.GenericViewList));
                Console.WriteLine("[{0} item(s)]", e.Count);
            });
        }
        private void Ui_OnSaveEntries(object sender, EventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Entries modification saved");
            });
        }
        private void Ui_OnRevertEntries(object sender, RevertEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Entries modification reverted");
                Console.WriteLine("Entries:");
                if (e.Restored != null)
                {
                    Console.WriteLine(" • {0}",
                        string.Join("\n • ", (List<string>)e.Restored));
                }
                Console.WriteLine("[{0} item(s)]", e.Count);
            });
        }
        private void Ui_OnEntrySelect(object sender, SelectEventArgs<string> e)
        {
            Log(delegate
            {
                if (e.Selected == null)
                {
                    Console.WriteLine("Entry not found [{0}]", e.RequestInfo);
                }
                else
                {
                    Console.WriteLine("Entry selected [{0}]", e.Selected);
                }
            });
        }
        private void Ui_OnEntryStatusChange(object sender, StatusEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Entry Status = {0}, [{1}]",
                    e.Status.ToString(), e.Value);
            });
        }
        private void Ui_OnEntrySet(object sender, EntrySetEventArgs e)
        {
            Log(delegate
            {
                if (e.OldItem == null)
                    Console.WriteLine("Entry Added [{0}]", e.NewItem);
                else
                    Console.WriteLine("Entry Edited [{1} => {0}]",
                        e.NewItem, e.OldItem);

                Console.WriteLine("  New List Entries [{0}]:\n • {1}",
                    e.SetList.Count, string.Join("\n • ",
                    (List<string>)e.SetList));
            });
        }
        private void Ui_OnEntryCancel(object sender, CancelEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Restore object: {0}", e.RestoreID);
                Console.WriteLine("{0} List",
                    e.EmptyList ? "Empty" : "Non-empty");
            });
        }
        private void Ui_OnEntryRemove(object sender, RemoveEventArgs e)
        {
            Log(delegate
            {
                Console.WriteLine("Entry deleted [{0}]", e.RemoveObject);
                Console.WriteLine("New Entries:");
                Console.WriteLine(" • {0}",
                    string.Join("\n • ", (List<string>)e.NewObjects));

                Console.WriteLine("[{0} item(s)]", e.Count);
            });
        }
        #endregion

        [TestMethod]
        public void Should_SelectEntry()
        {
            ui.Select("STEST");
            ui.SelectEntry("Entry 1");
        }

        [TestMethod]
        public void Should_Edit_SelectEntry()
        {
            ui.Select("STEST");
            ui.Edit();
            ui.Load_Entries();
            ui.SelectEntry("Entry 1");
        }

        [TestMethod]
        public void Should_RevertEntries()
        {
            ui.New();

            ui.Load_Entries();

            ui.New_Entry();
            ui.InputEntry = "test entry 1";
            ui.CommitChanges_Entry();

            ui.New_Entry();
            ui.InputEntry = "test entry 2";
            ui.CommitChanges_Entry();

            ui.New_Entry();
            ui.InputEntry = "test entry 3";
            ui.CommitChanges_Entry();

            ui.Revert_Entries();
        }
    }
}