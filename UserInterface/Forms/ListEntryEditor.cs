using Interfaces.General;
using Shared.UI;
using System;
using System.Windows.Forms;

namespace UserInterface.Forms
{
    public partial class ListEntryEditor<T>: Form
        where T: IListEntry, new()
    {
        public IListEntry ListEntry { get; private set; }

        private bool displayAsValue = true;
        private bool skipEvents = false;

        public ListEntryEditor()
        {
            InitializeComponent();
        }

        public ListEntryEditor(IListEntry listEntry)
        {
            InitializeComponent();

            ListEntry = listEntry;

            skipEvents = true;

            if (listEntry.Value != listEntry.Display)
            {
                displayAsValue = false;
                chkSameValue.Checked = false;
                txtDisplay.ReadOnly = false;
                txtValue.FocusSelectAll();
                txtValue.Focus();
            }
            txtValue.Text = listEntry.Value;
            txtDisplay.Text = listEntry.Display;

            skipEvents = false;
        }
        
#pragma warning disable IDE1006 // Naming Styles

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            if (skipEvents) return;

            if (displayAsValue)
            {
                SetDisplayTextAsValue();
            }
        }

        private void chkSameValue_CheckedChanged(object sender, EventArgs e)
        {
            if (skipEvents) return;

            if (chkSameValue.Checked == true)
            {
                displayAsValue = true;
                txtDisplay.ReadOnly = true;
                SetDisplayTextAsValue();
                txtValue.SelectAll();
                txtValue.Focus();
            }
            else
            {
                displayAsValue = false;
                txtDisplay.ReadOnly = false;
                txtDisplay.SelectAll();
                txtDisplay.Focus();
            }
        }

        private void SetDisplayTextAsValue()
        {
            txtDisplay.Text = txtValue.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ListEntry == null)
                ListEntry = new T();

            ListEntry.Value = txtValue.Text;
            ListEntry.Display = txtDisplay.Text;
        }
    }
}
