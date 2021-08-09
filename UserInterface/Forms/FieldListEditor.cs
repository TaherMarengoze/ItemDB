using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace UserInterface.Forms
{
    using Interfaces;
    using Models;

    public partial class FieldListEditor : Form
    {
        private List<string> existingIds;
        private bool validListId = false;
        private bool validEntry = false;
        private bool editMode;
        
        /// <summary>
        /// Constructor in case of new list.
        /// </summary>
        /// <param name="idList">List of all IDs to check for duplicate IDs.</param>
        /// <param name="ls">ListStructure object to create new XElement entry.</param>
        public FieldListEditor(IEnumerable<string> idList)
        {
            InitializeComponent();
            existingIds = idList.ToList();

            editMode = false;
            ModeUISetup();
        }

        public FieldListEditor(IEnumerable<string> idList, IBasicList editList)
        {
            InitializeComponent();
            existingIds = idList.ToList();

            FieldList = editList;

            editMode = true;
            validEntry = true;
            ModeUISetup();
        }

        public IBasicList FieldList { get; private set; }

        private void ModeUISetup()
        {
            if (editMode)
            {
                Text = Text + $": Editing {FieldList.ID}";
                btnAdd.Text = "Accept Changes";
                txtListID.Text = FieldList.ID;
                txtListName.Text = FieldList.Name;
                lblInitialEntry.Visible = false;
                txtInitialEntry.Visible = false;
            }
            else
            {
                Text = Text + ": New";
                btnAdd.Enabled = false;
            }
        }
        
        private void PopulateExistingId()
        {
            lbxExistingCodes.DataSource = existingIds;
        }

        private void CheckIdValidity(string inputId)
        {
            if (existingIds.Contains(inputId) == true && (!editMode || inputId != FieldList.ID))
            {
                //if (!editMode || inputId != meta.ID)
                //{
                    validListId = false;
                    lblValidID.Text = "* duplicate ID";
                    txtListID.SelectAll();
                    txtListID.Focus();
                //}
                //else
                //{
                //    validListId = true;
                //    lblValidID.Text = "* same ID";
                //}
            }
            else if (inputId == string.Empty)
            {
                validListId = false;
                lblValidID.Text = string.Empty;
            }
            else
            {
                validListId = true;
                lblValidID.Text = string.Empty;
            }
            CheckCompleteInput();
        }

        private void CheckInitialListEntry()
        {
            validEntry = txtInitialEntry.Text != string.Empty ? true : false;
            CheckCompleteInput();
        }

        private void CheckCompleteInput()
        {
            if (validListId && validEntry)
                btnAdd.Enabled = true;
            else
                btnAdd.Enabled = false;

        }

        private void CreateNewList()
        {
            string listId = txtListID.Text;
            string listName = txtListName.Text;
            string entry1 = txtInitialEntry.Text;
            
            FieldList = new BasicListView
            {
                ID = listId,
                Name = listName,
                List = new ObservableCollection<string>() { entry1 }
            };
        }

        private void EditExistingList()
        {
            string listId = txtListID.Text;
            string listName = txtListName.Text;

            FieldList.ID = listId;
            FieldList.Name = listName;
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (editMode)
            {
                EditExistingList();
            }
            else
            {
                CreateNewList();
            }
            
        }

        private void txtListID_TextChanged(object sender, EventArgs e)
        {
            FilterExistingId(txtListID.Text);
            CheckIdValidity(txtListID.Text); 
            
        }

        private void FilterExistingId(string inputId)
        {
            if (inputId != string.Empty)
            {
                List<string> filteredList = existingIds.Where(id => id.Contains(inputId)).ToList();
                lbxExistingCodes.DataSource = filteredList;
            }
            else
            {
                lbxExistingCodes.DataSource = existingIds;
            }
        }

        private void txtInitialEntry_TextChanged(object sender, EventArgs e)
        {
            CheckInitialListEntry();
        }

        private void FieldListEditor_Load(object sender, EventArgs e)
        {
            PopulateExistingId();
            if (editMode)
            {
                FilterExistingId(txtListID.Text);
            }
        }

    }
}
