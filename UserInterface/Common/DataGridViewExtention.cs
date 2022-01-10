using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface.Shared
{
    public static class DataGridViewExtention
    {
        //public static int GetRowIndex(DataGridView dgv, string value, string field = "")
        //{
        //    int rowIndex = -1;

        //    DataGridViewRow row;

        //    if (field == string.Empty)
        //    {
        //        row = dgv.Rows
        //        .Cast<DataGridViewRow>()
        //        .Where(r => r.Cells[0].Value.ToString().Equals(value))
        //        .FirstOrDefault();
        //    }
        //    else
        //    {
        //        row = dgv.Rows
        //        .Cast<DataGridViewRow>()
        //        .Where(r => r.Cells[field].Value.ToString().Equals(value))
        //        .FirstOrDefault();
        //    }

        //    return row?.Index ?? rowIndex;
        //}

        public static void SelectValueRow(this DataGridView dgv, string value, string field = "")
        {
            int i;
            int c = 0;
            int rowIndex = -1;

            DataGridViewRow row;

            if (string.IsNullOrEmpty(field))
            {
                row = dgv.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[0].Value.ToString().Equals(value))
                .FirstOrDefault();
            }
            else
            {
                row = dgv.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[field].Value.ToString().Equals(value))
                .FirstOrDefault();

                c = dgv.Columns[field].Index;
            }

            i = row?.Index ?? rowIndex;

            if (i != -1)
                dgv.Rows[i].Cells[c].Selected = true;
            
        }
    }
}