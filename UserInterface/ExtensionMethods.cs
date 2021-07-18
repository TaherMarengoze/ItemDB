using System;
using System.Reflection;
using System.Windows.Forms;

namespace UserInterface
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
        public static void DataSourceResize(this DataGridView dgv, object dataSource)
        {
            dgv.DataSource = dataSource;
            dgv.AutoResizeColumns();
            dgv.AutoResizeRows();
        }

        public static void FocusSelectAll(this TextBox textBox)
        {
            textBox.SelectAll();
            textBox.Focus();
        }
    }
}