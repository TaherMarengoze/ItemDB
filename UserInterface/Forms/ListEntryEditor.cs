using System;
using System.Windows.Forms;
using UserInterface.Models;

namespace UserInterface.Forms
{
    public partial class ListEntryEditor : Form
    {
        public SpecListEntry ListEntry { get; set; }

        private bool displayAsValue = true;
        private bool skipEvents = false;

        public ListEntryEditor()
        {
            InitializeComponent();
        }

        public ListEntryEditor(SpecListEntry listEntry)
        {
            InitializeComponent();

            skipEvents = true;

            ListEntry = listEntry;

            if (listEntry.Value != listEntry.Display)
            {
                displayAsValue = false;
                chkSameValue.Checked = false;
                txtDisplay.ReadOnly = false;
                txtValue.SelectAll();
                txtValue.Focus();
            }

            txtValue.Text = listEntry.Value;
            txtDisplay.Text = listEntry.Display;

            skipEvents = false;
        }

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
                ListEntry = new SpecListEntry();
            
            ListEntry.Value = txtValue.Text;
            ListEntry.Display = txtDisplay.Text;
        }
    }
}
