using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface.Forms
{
    public partial class ValueEdit : Form
    {
        public string NewValue { get; set; }

        public ValueEdit(string oldValue)
        {
            InitializeComponent();
            txtItemValue.Text = oldValue;
        }

        private void ValueEdit_Load(object sender, EventArgs e)
        {
            txtItemValue.SelectAll();
            txtItemValue.Focus();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            NewValue = txtItemValue.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
