using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UserInterface
{
    public static class Common
    {
        public delegate void FileLoadCallback(string path);

        public static bool BrowseXmlFile(FileLoadCallback loadCallback)
        {
            OpenFileDialog fileSelector = SetupBrowser();
            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                loadCallback(fileSelector.FileName);
                return true;
            }
            else
            {
                MessageBox.Show("File loading canceled");
                return false;
            }
        }

        public static OpenFileDialog SetupBrowser()
        {
            return
                new OpenFileDialog()
                {
                    Title = "Browse XML File",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = "xml",
                    Filter = "XML files(*.xml) | *.xml"
                };
        }

        public static DialogResult ShowEntryRemoveConfirmation(bool showConfirmation)
        {
            if (showConfirmation)
            {
                return
                    MessageBox.Show(
                    caption: "Confirm Delete",
                    text: "Are you sure you want to remove the selected entry ?",
                    buttons: MessageBoxButtons.OKCancel,
                    icon: MessageBoxIcon.Exclamation,
                    defaultButton: MessageBoxDefaultButton.Button1);
            }

            return DialogResult.OK;
        }

        public static void SetDataGridViewDataSource(DataGridView dgv, object dataSource)
        {
            dgv.DataSource = dataSource;
            dgv.AutoResizeColumns();
            dgv.AutoResizeRows();
        }

        /// <summary>
        /// Gets the index of the first row in a <see cref="DataGridView"/> selection.
        /// </summary>
        /// <param name="dgv">The <see cref="DataGridView"/> containing the selected rows.</param>
        /// <returns></returns>
        public static int GetDataGridViewSelectionIndex(DataGridView dgv)
        {
            if (dgv.DataSource == null)
                return -1;

            int itemsCount = dgv.Rows.Count;
            int selectedIndex = dgv.SelectedRows[0].Index;

            if (selectedIndex == itemsCount - 1)
            {
                return itemsCount - 2;
            }
            else
            {
                return selectedIndex;
            }
        }

        public static void RestoreDataGridViewSelection(DataGridView dgv, int selectionIndex)
        {
            if (selectionIndex > -1)
            {
                dgv.CurrentCell = dgv.Rows[selectionIndex].Cells[0];
                dgv.Rows[selectionIndex].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = selectionIndex;
            }
        }

        public static void SelectDataGridViewFirstRow(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows[0].Selected = true;
                dgv.FirstDisplayedCell = dgv.Rows[0].Cells[0];
            }
        }

        public static int SaveListboxSelection(ListBox lbx)
        {
            if (lbx.DataSource == null)
                return -1;

            int itemsCount = lbx.Items.Count;
            int selectedIndex = lbx.SelectedIndex;

            if (selectedIndex == itemsCount - 1)
            {
                return itemsCount - 2;
            }
            else
            {
                return selectedIndex;
            }
        }

        public static void RestoreListboxSelection(ListBox lbx, int selectionIndex)
        {
            if (selectionIndex > -1)
            {
                lbx.SelectedIndex = selectionIndex;
            }
        }

        public static string TextApplyPattern(string text, string pattern, string replace = "")
        {
            return Regex.Replace(text, pattern, replace, RegexOptions.Compiled);
        }

        public static void DuplicateIdLabel(Label label, bool isDuplicate)
        {
            if (isDuplicate)
            {
                label.Text = "• Duplicte ID";
            }
            else
            {
                label.Text = string.Empty;
            }
        }

        public static void AlphaNumericId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !(e.KeyChar == (char)8))
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }
    }
}