using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UT_Controllers
{
    public partial class ListViewer : Form
    {
        public ListViewer(object list)
        {
            InitializeComponent();

            dgvMain.DataSource = list;
        }
    }
}
