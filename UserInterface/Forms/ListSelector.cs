
using Interfaces.Models;
using Modeling.Generic;
using Shared.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;


namespace UserInterface.Forms
{
    public partial class ListSelector : Form
    {
        public List<SelectionList> UT_InputList => inputList;

        public List<string> OutputList { get; private set; }

        private List<SelectionList> inputList;
        private List<SelectionList> inputListFilter;
        Func<IFieldList, SelectionList> selector;

        public ListSelector(IList<IFieldList> source, IList<string> selected)
        {
            InitializeComponent();
            SetSelectionList(source, selected);
        }
        
        private void SetSelectionList(IList<IFieldList> source, IList<string> selected)
        {
            if (selected == null)
            {
                selector = item =>
                new SelectionList()
                {
                    ID = item.ID,
                    Name = item.Name,
                    List = new ObservableCollection<string>(item.List)
                };
            }
            else
            {
                selector = item =>
                new SelectionList()
                {
                    Include = selected.Contains(item.ID),
                    ID = item.ID,
                    Name = item.Name,
                    List = new ObservableCollection<string>(item.List)
                };
            }

            inputList = source.Select(selector).ToList();
        }

        private void BindList(object list)
        {
            dgvLists.DataSourceResize(list);
        }

        private void RefreshSizeListGrid()
        {
            object dataSource = dgvLists.DataSource;
            dgvLists.DataSource = null;
            dgvLists.DataSourceResize(dataSource);
        }

        private void DisplaySizeListItems(string listId)
        {
            lstListEntries.DataSource = inputList.Find(id => id.ID == listId).List;
        }

        private void FilterSizeLists()
        {
            bool included = chkIncluded.Checked;
            string filterId = txtFilterId.Text;
            string filterName = txtFilterName.Text;

            if (included == true || filterId != string.Empty || filterName != string.Empty)
            {
                inputListFilter =
                    (from sz in inputList
                     let c1 = included ? sz.Include == true : true
                     let c2 = filterId != string.Empty ? sz.ID.Contains(filterId) : true
                     let c3 = filterName != string.Empty ? sz.Name.ToLower().Contains(filterName.ToLower()) : true
                     where c1 && c2 && c3
                     select sz).ToList();

                BindList(inputListFilter);
            }
            else
            {
                BindList(inputList);
            }
        }

        private void SetOutputList()
        {
            OutputList =
                (from item in inputList where item.Include orderby item.ID select item.ID).ToList();

            // close the form
            Close();
        }

        private void DeselectAllItems()
        {
            if (GetIncludedListIdCount() > 0)
            {
                foreach (var item in inputList)
                {
                    item.Include = false;
                }
                RefreshSizeListGrid();
                FilterSizeLists();
                btnDeselectAll.Enabled = false;
            }
        }

        private int GetIncludedListIdCount()
        {
            return inputList.Where(sl => sl.Include).Count();
        }

#pragma warning disable IDE1006 // Naming Styles
        private void AltListSelector_Load(object sender, EventArgs e)
        {
            BindList(inputList);
            dgvLists.Columns[1].ReadOnly = true;
            dgvLists.Columns[2].ReadOnly = true;

            if (GetIncludedListIdCount() > 0)
                btnDeselectAll.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvSizeLists_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLists.SelectedRows.Count <= 0)
                return;

            DataGridViewRow row = dgvLists.SelectedRows[0];

            if (row == null)
                return;

            string id = (string)row.Cells[1].Value;
            DisplaySizeListItems(id);
        }

        private void dgvSizeLists_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                dgvLists.EndEdit();
                btnDeselectAll.Enabled = GetIncludedListIdCount() > 0;
            }
        }

        private void chkIncluded_CheckedChanged(object sender, EventArgs e)
        {
            FilterSizeLists();
        }

        private void txtFilterId_TextChanged(object sender, EventArgs e)
        {
            FilterSizeLists();
        }

        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            FilterSizeLists();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SetOutputList();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            DeselectAllItems();
        }
    }
#pragma warning restore IDE1006 // Naming Styles
}