using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface.Shared
{
    public static class DataGridViewExtention
    {
        private static void DoubleBuffered(this DataGridView source, bool setting)
        {
            Type dgvType = source.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(source, setting, null);
        }
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

        public static int SelectedRowIndex(this DataGridView source)
        {
            return
                source.SelectedRows[0].Index;
        }

        public static void SelectRow(this DataGridView dgv, string value, string field = "")
        {
            int i;
            int c = 0;
            int rowIndex = -1;

            DataGridViewRow row;

            if (string.IsNullOrEmpty(field))
            {
                row = dgv.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[0].Value.ToString() == value)
                .FirstOrDefault();
            }
            else
            {
                row = dgv.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[field].Value.ToString() == value)
                .FirstOrDefault();

                c = dgv.Columns[field].Index;
            }

            i = row?.Index ?? rowIndex;

            if (i != -1)
                dgv.Rows[i].Cells[c].Selected = true;
        }

        public static void SelectRow(this DataGridView source, int index, bool exception = false)
        {
            // get DGV rows count
            int itemsCount = source.RowCount;

            if (index > -1 && itemsCount > 0)
            {
                // check if index within range of row count
                if (index >= itemsCount)
                    index = itemsCount - 1;

                source.Rows[index].Selected = true;
                source.FirstDisplayedScrollingRowIndex = index;
            }
            else
            {
                if (exception)
                {
                    if (index <= -1)
                        throw new IndexOutOfRangeException();

                    if (itemsCount <= 0)
                        throw new Exception("Rows must be greater than 0");
                }
            }
        }

        public static void SaveAndRestoreSelection(
            this DataGridView dataGridView, Action action)
        {
            int restoreIndex = dataGridView.SelectedRows[0].Index;

            action?.Invoke();

            // Get DGV number of rows
            int itemsCount = dataGridView.RowCount;

            if (restoreIndex > -1 && itemsCount > 0)
            {
                // Check if selection index exists in the list
                if (restoreIndex >= itemsCount)
                    restoreIndex = itemsCount - 1;

                dataGridView.Rows[restoreIndex].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = restoreIndex;
            }
        }

        public static void SaveAndRestoreSelection(
                this DataGridView dataGridView, Action action,
                EventHandler handler)
        {
            if (handler is null)
                throw new ArgumentNullException(nameof(handler));

            bool alreadySelected;
            int restoreIndex = dataGridView.SelectedRows[0].Index;

            dataGridView.SelectionChanged -= handler;

            action?.Invoke();

            // Get DGV number of rows
            int itemsCount = dataGridView.RowCount;

            if (restoreIndex > -1 && itemsCount > 0)
            {
                // Check if selection index exists in the list
                if (restoreIndex >= itemsCount)
                    restoreIndex = itemsCount - 1;

                alreadySelected = dataGridView.Rows[restoreIndex].Selected;

                dataGridView.SelectionChanged += handler;

                dataGridView.Rows[restoreIndex].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = restoreIndex;

                if (alreadySelected)
                    handler.Invoke(dataGridView, EventArgs.Empty);
            }
        }
    }
}