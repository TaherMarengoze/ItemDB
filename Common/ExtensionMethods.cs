using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;

namespace Shared.UI
{
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }


        /// <summary>
        /// Sets a <see cref="DataGridView"/>'s <see cref="DataGridView.DataSource"/> and then auto resize its columns and rows.
        /// </summary>
        /// <param name="dgv">The <see cref="DataGridView"/> instance.</param>
        /// <param name="dataSource">The <see cref="object"/> of the datasource.</param>
        /// <param name="refresh">Indicates whether to refresh the <see cref="DataGridView"/> before binding or not.</param>
        public static void DataSourceResize(this DataGridView dgv, object dataSource, bool refresh = false)
        {
            if (refresh)
            {
                dgv.DataSource = null;
            }
            dgv.DataSource = dataSource;
            dgv.AutoResizeColumns();
            dgv.AutoResizeRows();
        }

        public static object SelectedObjectID(this DataGridView dgv, string fieldName = "")
        {
            if (dgv.Rows.Count <= 0)
                return null;

            DataGridViewRow firstSelectedRow = dgv.SelectedRows[0];
            if (firstSelectedRow == null)
                return null;

            if (fieldName == "")
            {
                // assumes that the ID column is the first one
                return firstSelectedRow.Cells[0].Value;
            }

            return
                firstSelectedRow.Cells[fieldName].Value;
        }

        /// <summary>
        /// Select all text contents in a <see cref="TextBox"/> and set the focus on it.
        /// </summary>
        /// <param name="textBox"></param>
        public static void FocusSelectAll(this TextBox textBox)
        {
            textBox.SelectAll();
            textBox.Focus();
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            foreach (T item in range)
            {
                collection.Add(item);
            }
        }
    }
}