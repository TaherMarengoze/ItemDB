﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;

namespace Client.Controls
{
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this Control source, bool setting)
        {
            Type dgvType = source.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(source, setting, null);
        }


        /// <summary>
        /// Sets a <see cref="DataGridView"/>'s <see cref="DataGridView.DataSource"/> and then auto resize its columns and rows.
        /// </summary>
        /// <param name="source">The <see cref="DataGridView"/> instance.</param>
        /// <param name="dataSource">The <see cref="object"/> of the datasource.</param>
        /// <param name="refresh">Indicates whether to refresh the <see cref="DataGridView"/> before binding or not.</param>
        public static void DataSourceResize(this DataGridView source, object dataSource, bool refresh = false)
        {
            if (refresh)
            {
                source.DataSource = null;
            }
            source.DataSource = dataSource;
            source.AutoResizeColumns();
            source.AutoResizeRows();
        }

        /// <summary>
        /// Gets the selected object ID by the given column name. If no column name is given the first column is used.
        /// </summary>
        /// <param name="source">The <see cref="DataGridView"/> object to get the selected object ID from.</param>
        /// <param name="fieldName">The column name that contains the ID of the selected object. If omitted then the first column is used.</param>
        /// <returns>Object representing the ID of the selected row.</returns>
        public static object SelectedObjectID(this DataGridView source, string fieldName = "")
        {
            if (source.Rows.Count <= 0 || source.SelectedRows.Count <= 0)
                return null;

            DataGridViewRow firstSelectedRow = source.SelectedRows[0];
            if (firstSelectedRow == null)
                return null;

            if (fieldName == "")
            {
                foreach (string colName in new string[] { "ID", "Id", "id" })
                {
                    if (source.Columns[colName] != null)
                        return firstSelectedRow.Cells[colName].Value;
                }
                
                // assumes that the ID column is the first one
                return firstSelectedRow.Cells[0].Value;
            }

            return
                firstSelectedRow.Cells[fieldName].Value;
        }
        
        public static void RestoreSelection(this DataGridView source, object dataSource)
        {
            int _selectionIndex = source.SelectedRows[0].Index;

            //dgv.DataSourceResize(bindingSource, true);
            source.DataSource = dataSource;
            source.AutoResizeColumns();
            source.AutoResizeRows();

            // Get DGV number of rows
            int itemsCount = source.RowCount;

            if (_selectionIndex > -1 && itemsCount > 0)
            {
                // Check if selection index exists in the list
                if (_selectionIndex >= itemsCount)
                    _selectionIndex = itemsCount - 1;

                source.Rows[_selectionIndex].Selected = true;
                source.FirstDisplayedScrollingRowIndex = _selectionIndex;
            }
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

        public static void NotifyCheck(this CheckBox source, bool status, EventHandler<EventArgs> handler)
        {
            if (source.Checked == status)
            {
                handler(source, EventArgs.Empty);
            }
            else
            {
                source.Checked = status;
            }
        }
    }
}