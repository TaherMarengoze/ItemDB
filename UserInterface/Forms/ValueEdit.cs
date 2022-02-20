
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace UserInterface.Forms
{
    public partial class ValueEdit : Form
    {
        public string NewValue { get; private set; }

        public string OldValue { get; private set; }

        private string inputValue;
        private const string VALUE_BLANK = "Blank";
        private const string VALUE_READY = "Ready";
        private const string VALUE_DUPLICATE = "Duplicate";
        private const string VALUE_UNCHANGED = "Unchanged";

        private readonly List<string> existingList;

        public ValueEdit(string oldValue)
        {
            InitializeComponent();
            OldValue = oldValue;
            txtItemValue.Text = oldValue;
        }

        public ValueEdit(string oldValue, List<string> existingList)
        {
            InitializeComponent();
            OldValue = oldValue;
            txtItemValue.Text = oldValue;

            this.existingList = existingList;
        }

        private void ValueEdit_Load(object sender, EventArgs e)
        {
            txtItemValue.SelectAll();
            txtItemValue.Focus();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            NewValue = inputValue.Trim();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtItemValue_TextChanged(object sender, EventArgs e)
        {
            inputValue = ((TextBox)sender).Text;

            bool changed = IsChanged(inputValue);
            bool unique = IsUnique(inputValue);
            bool blank = string.IsNullOrWhiteSpace(inputValue);

            if (changed && unique && !blank)
            {
                btnChange.Enabled = true;
            }
            else
            {
                btnChange.Enabled = false;
            }

            DisplayValidity(changed, unique, blank);
        }

        private bool IsChanged(string value)
        {
            return value.Trim() != OldValue;
        }

        private bool IsUnique(string value)
        {
            return (!existingList?.Contains(value)) ?? true;
        }

        private void DisplayValidity(bool changed, bool unique, bool blank)
        {
            if (blank)
            {
                tslblStatus.Text = VALUE_BLANK;
            }
            else
            {
                if (changed)
                {
                    if (unique)
                    {
                        tslblStatus.Text = VALUE_READY;
                    }
                    else
                    {
                        tslblStatus.Text = VALUE_DUPLICATE;
                    }
                }
                else
                {
                    tslblStatus.Text = VALUE_UNCHANGED;
                }
            }
        }

        private void tslblStatus_TextChanged(object sender, EventArgs e)
        {
            switch (tslblStatus.Text)
            {
                case VALUE_READY:
                    tslblStatus.ForeColor = Color.Green;
                    break;
                case VALUE_DUPLICATE:
                    tslblStatus.ForeColor = Color.Red;
                    break;
                case VALUE_UNCHANGED:
                    tslblStatus.ForeColor = Color.OrangeRed;
                    break;
                case VALUE_BLANK:
                    tslblStatus.ForeColor = Color.DarkRed;
                    break;
                default:
                    tslblStatus.ForeColor = SystemColors.ControlText;
                    break;
            }
        }
    }
}
