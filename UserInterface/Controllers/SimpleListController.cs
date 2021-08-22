using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UserInterface.Forms;

namespace UserInterface.Controllers
{
    public class SimpleListController
    {
        public event EventHandler OnEntryAdd;
        public event EventHandler OnEntryDelete;

        // Controls
        private readonly Button addButton;
        private readonly Button editButton;
        private readonly Button removeButton;
        private readonly TextBox newEntryTextbox;
        private readonly ListBox entriesListbox;

        private List<string> listEntries;

        bool isDeleteKeyDown = false;

        public SimpleListController(Button add, Button edit, Button remove,
            TextBox entry, ListBox listbox, List<string> list)
        {
            addButton = add;
            editButton = edit;
            removeButton = remove;
            newEntryTextbox = entry;
            entriesListbox = listbox;

            listEntries = list;

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            //Button
            addButton.Click += AddButton_Click;
            editButton.Click += EditButton_Click;
            removeButton.Click += RemoveButton_Click;

            //TextBox
            newEntryTextbox.KeyDown += NewEntryTextbox_KeyDown;
            newEntryTextbox.TextChanged += NewEntryTextbox_TextChanged;

            //Listbox
            entriesListbox.MouseDoubleClick += EntriesListbox_MouseDoubleClick;
            entriesListbox.KeyDown += EntriesListbox_KeyDown;
            entriesListbox.KeyUp += EntriesListbox_KeyUp;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string entry = newEntryTextbox.Text;

            if (!listEntries.Contains(entry))
            {
                listEntries.Add(entry);
                UpdateListEntriesUI();
                ClearEntryTextbox();

                // Invoke OnEntryAdd event
                OnEntryAdd?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Duplicate Entry");
                newEntryTextbox.FocusSelectAll();
            }

            CheckListEntriesValidity();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"{ ((Button)sender).Text } button was clicked");
            if (entriesListbox.SelectedIndex == -1)
            {
                MessageBox.Show("No item selected");
            }
            else
            {
                string dataItem = entriesListbox.Text;
                int entryIndex = listEntries.IndexOf(dataItem);

                ValueEdit valueEditBox = new ValueEdit(dataItem);
                if (valueEditBox.ShowDialog() == DialogResult.OK)
                {
                    dataItem = valueEditBox.NewValue;
                    listEntries[entryIndex] = dataItem;
                    UpdateListEntriesUI();
                    entriesListbox.Text = dataItem;
                }
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (entriesListbox.SelectedIndex == -1)
            {
                MessageBox.Show("No item selected");
            }
            else
            {
                string selectedItem = entriesListbox.Text;
                int selectedIdx = Common.SaveListboxSelection(entriesListbox);
                listEntries.Remove(selectedItem);
                UpdateListEntriesUI();
                CheckListEntriesValidity();
                Common.RestoreListboxSelection(entriesListbox, selectedIdx);

                // Invoke OnEntryDelete event
                OnEntryDelete?.Invoke(this, EventArgs.Empty);
            }
        }

        private void NewEntryTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && addButton.Enabled)
            {
                AddButton_Click(sender, e);
            }
        }

        private void NewEntryTextbox_TextChanged(object sender, EventArgs e)
        {
            string input = newEntryTextbox.Text;

            if (input != string.Empty)
            {
                addButton.Enabled = true;
            }
            else
            {
                addButton.Enabled = false;
            }
        }

        private void EntriesListbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = entriesListbox.IndexFromPoint(e.Location);
            
            if (index != ListBox.NoMatches)
            {
                EditButton_Click(sender, e);
            }
        }

        private void EntriesListbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && isDeleteKeyDown == false)
            {
                isDeleteKeyDown = true;
                RemoveButton_Click(sender, e);
            }
        }

        private void EntriesListbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && isDeleteKeyDown == true)
            {
                isDeleteKeyDown = false;
            }
        }

        private void CheckListEntriesValidity()
        {
            if (listEntries.Count > 0)
            {
                //inputValidator.ValidEntries = true;
                SetModifyButtonsAbility(true);
            }
            else
            {
                //inputValidator.ValidEntries = false;
                SetModifyButtonsAbility(false);
            }
        }

        private void UpdateListEntriesUI()
        {
            entriesListbox.DataSource = null;
            entriesListbox.DataSource = listEntries;
        }

        private void ClearEntryTextbox()
        {
            newEntryTextbox.Clear();
            newEntryTextbox.Focus();
        }

        private void SetModifyButtonsAbility(bool enable)
        {
            editButton.Enabled = enable;
            removeButton.Enabled = enable;
        }
    }
}
