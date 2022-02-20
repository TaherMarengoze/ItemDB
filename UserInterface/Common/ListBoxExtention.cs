using System;
using System.Windows.Forms;

namespace UserInterface.Shared
{
    public static class ListBoxExtention
    {
        //private static void DoubleBuffered(this DataGridView source, bool setting)
        //{
        //    Type dgvType = source.GetType();

        //    PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
        //        BindingFlags.Instance | BindingFlags.NonPublic);

        //    pi.SetValue(source, setting, null);
        //}
        
        //public static int SelectedRowIndex(this DataGridView source)
        //{
        //    return
        //        source.SelectedRows[0].Index;
        //}

        //public static void SelectRow(this DataGridView dgv, string value, string field = "")
        //{
        //    int i;
        //    int c = 0;
        //    int rowIndex = -1;

        //    DataGridViewRow row;

        //    if (string.IsNullOrEmpty(field))
        //    {
        //        row = dgv.Rows
        //        .Cast<DataGridViewRow>()
        //        .Where(r => r.Cells[0].Value.ToString() == value)
        //        .FirstOrDefault();
        //    }
        //    else
        //    {
        //        row = dgv.Rows
        //        .Cast<DataGridViewRow>()
        //        .Where(r => r.Cells[field].Value.ToString() == value)
        //        .FirstOrDefault();

        //        c = dgv.Columns[field].Index;
        //    }

        //    i = row?.Index ?? rowIndex;

        //    if (i != -1)
        //        dgv.Rows[i].Cells[c].Selected = true;
        //}

        //public static void SelectRow(this DataGridView source, int index, bool exception = false)
        //{
        //    // get DGV rows count
        //    int itemsCount = source.RowCount;

        //    if (index > -1 && itemsCount > 0)
        //    {
        //        // check if index within range of row count
        //        if (index >= itemsCount)
        //            index = itemsCount - 1;

        //        source.Rows[index].Selected = true;
        //        source.FirstDisplayedScrollingRowIndex = index;
        //    }
        //    else
        //    {
        //        if (exception)
        //        {
        //            if (index <= -1)
        //                throw new IndexOutOfRangeException();

        //            if (itemsCount <= 0)
        //                throw new Exception("Rows must be greater than 0");
        //        }
        //    }
        //}

        public static void SaveAndRestoreSelection(this ListBox listBox,
            Action action)
        {
            int restoreIndex = listBox.SelectedIndex;

            action?.Invoke();

            // get ListBox items count
            int itemsCount = listBox.Items.Count;

            if (restoreIndex > -1 && itemsCount > 0)
            {
                // check if selection index exists in the list
                if (restoreIndex >= itemsCount)
                    restoreIndex = itemsCount - 1;

                listBox.SelectedIndex = restoreIndex;
            }
        }
    }
}