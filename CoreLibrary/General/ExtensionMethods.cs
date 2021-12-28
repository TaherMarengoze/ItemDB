using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;

namespace CoreLibrary
{
    public static class ExtensionMethods
    {
        public static void DoubleBuffer(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }


        /// <summary>
        /// Sets a <see cref="DataGridView"/>'s DataSource property and then auto resizes its columns and rows.
        /// </summary>
        /// <param name="dgv">The <see cref="DataGridView"/> instance.</param>
        /// <param name="dataSource">The <see cref="object"/> of the datasource.</param>
        public static void DataSourceResize(this DataGridView dgv, object dataSource)
        {
            dgv.DataSource = dataSource;
            dgv.AutoResizeColumns();
            dgv.AutoResizeRows();
        }

        public static void UnbindNotify(this DataGridView sender, EventHandler handler)
        {
            if (sender.DataSource == null)
            {
                handler(sender, EventArgs.Empty);
                return;
            }
            sender.DataSource = null;
        }

        public static DataGridViewRow SelectedFirstRow(this DataGridView dgv)
        {
            return
                dgv.DataSource != null && dgv.SelectedRows.Count > 0 ?
                dgv.SelectedRows[0] : null;
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