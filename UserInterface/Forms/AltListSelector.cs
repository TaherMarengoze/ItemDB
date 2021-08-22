
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace UserInterface.Forms
{
    using CoreLibrary.Models;


    public partial class AltListSelector : Form
    {
        public List<string> OutputAltSizesIDs { get; private set; }

        private List<SelectionSizeList> selSizeLists;
        private List<SelectionSizeList> selSizeListsFiltered;

        /// <summary>
        /// Constructor for creating a new size ID list.
        /// </summary>
        /// <param name="lists">The size list passed from the caller parent.</param>
        public AltListSelector(List<BasicListView/*SizeList*/> lists)
        {
            InitializeComponent();
            selSizeLists = NewSelectionSizeList(lists);
            //OutputAltSizesIDs = new List<string>();
        }

        /// <summary>
        /// Constructor for modifying an existing size ID list.
        /// </summary>
        /// <param name="lists">The size list passed from the caller parent.</param>
        /// <param name="selectionList">The list of the selected size list IDs.</param>
        public AltListSelector(List<BasicListView/*SizeList*/> lists, List<string> selectionList)
        {
            InitializeComponent();
            selSizeLists = ModifiedSelectionSizeList(lists, selectionList);
        }

        /// <summary>
        /// Creates a new list of SelectionSizeList, with all IDs unselected to pick from.
        /// </summary>
        /// <param name="lists">The base SizeList to create this one upon.</param>
        private List<SelectionSizeList> NewSelectionSizeList(List<BasicListView/*SizeList*/> lists)
        {
            return
                (from sl in lists
                 select new SelectionSizeList()
                 {
                     //Include=true,
                     ID = sl.ID,
                     Name = sl.Name,
                     List = new ObservableCollection<string>( sl.List.Select(sz => sz))
                 }).ToList();
        }

        /// <summary>
        /// Creates a new list of SelectionSizeList, with specific IDs in a given list already selected.
        /// </summary>
        /// <param name="lists">The base SizeList to create this one upon.</param>
        /// <param name="selectionList">The list of the selected size list IDs.</param>
        /// <returns></returns>
        private List<SelectionSizeList> ModifiedSelectionSizeList(List<BasicListView/*SizeList*/> lists, List<string> selectionList)
        {
            return
                (from sl in lists
                 let included = selectionList.Contains(sl.ID)
                 select new SelectionSizeList()
                 {
                     Include = included,
                     ID = sl.ID,
                     Name = sl.Name,
                     List = new ObservableCollection<string>( sl.List.Select(sz => sz))
                 }).ToList();
        }

        private void SetSizeListGrid(List<SelectionSizeList> list)
        {
            dgvSizeLists.DataSource = list;
            AutoSizeGridCells();
        }
        
        private void RefreshSizeListGrid()
        {
            object dataSource = dgvSizeLists.DataSource;
            dgvSizeLists.DataSource = null;
            dgvSizeLists.DataSource = dataSource;
            AutoSizeGridCells();
        }

        private void AutoSizeGridCells()
        {
            dgvSizeLists.AutoResizeColumns();
            dgvSizeLists.AutoResizeRows();
        }

        /// <summary>
        /// Displays the size items of the selected size list.
        /// </summary>
        /// <param name="listId">The size list ID to display its size items.</param>
        private void DisplaySizeListItems(string listId)
        {
            lstSizeListItems.DataSource = selSizeLists.Find(id => id.ID == listId).List;
        }

        private void FilterSizeLists()
        {
            bool included = chkIncluded.Checked;
            string filterId = txtFilterId.Text;
            string filterName = txtFilterName.Text;

            if (included == true || filterId != string.Empty || filterName != string.Empty)
            {
                selSizeListsFiltered =
                    (from sz in selSizeLists
                     let c1 = included ? sz.Include == true : true
                     let c2 = filterId != string.Empty ? sz.ID.Contains(filterId) : true
                     let c3 = filterName != string.Empty ? sz.Name.ToLower().Contains(filterName.ToLower()) : true
                     where c1 && c2 && c3
                     select sz).ToList();

                SetSizeListGrid(selSizeListsFiltered);
            }
            else
            {
                SetSizeListGrid(selSizeLists);
            }
        }

        private void SetOutputList()
        {
            OutputAltSizesIDs =
                (from sl in selSizeLists where sl.Include orderby sl.ID select sl.ID).ToList();

            // Close the form
            Close();

            //TEST
            //TestOutputList();
        }

        private void DeselectAllItems()
        {
            if (GetIncludedListIdCount() > 0)
            {
                foreach (SelectionSizeList item in selSizeLists)
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
            return selSizeLists.Where(sl => sl.Include).Count();
        }

        private void TestOutputList()
        {
            txtTest.Text = string.Join(" • ", OutputAltSizesIDs);
        }

#pragma warning disable IDE1006 // Naming Styles
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        //TEST METHOD
        private void AltListSelector_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void AltListSelector_Load(object sender, EventArgs e)
        {
            SetSizeListGrid(selSizeLists);
            dgvSizeLists.Columns[1].ReadOnly = true;
            dgvSizeLists.Columns[2].ReadOnly = true;

            if (GetIncludedListIdCount() > 0)
                btnDeselectAll.Enabled = true;
            
        }

        private void dgvSizeLists_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSizeLists.SelectedRows.Count <= 0)
                return;

            DataGridViewRow row = dgvSizeLists.SelectedRows[0];

            if (row == null)
                return;

            string id = (string)row.Cells[1].Value;
            DisplaySizeListItems(id);
        }

        private void dgvSizeLists_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                dgvSizeLists.EndEdit();
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
#pragma warning restore IDE1006 // Naming Styles
    }
}