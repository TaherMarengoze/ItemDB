using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UserInterface.Interfaces;
using UserInterface.Models;

namespace UserInterface.Forms
{
    public partial class FieldListEditor : Form
    {
        private ISchema xn;
        private ListMetadata meta;
        private List<string> IdList;
        private bool validListId = false;
        private bool validEntry = false;
        private bool editMode;
        
        /// <summary>
        /// Constructor in case of new list.
        /// </summary>
        /// <param name="parentIdList">List of all IDs to check for duplicate IDs.</param>
        /// <param name="ls">ListStructure object to create new XElement entry.</param>
        public FieldListEditor(List<string> parentIdList, ISchema ls)
        {
            InitializeComponent();
            IdList = parentIdList;

            xn = ls; /*new ListStructure()
            {
                ListId = ls.ListId,
                ListName = ls.ListName,
                ListParent = ls.ListParent,
                ListChild = ls.ListChild,
                ChildGroup = ls.ChildGroup
            };*/

            editMode = false;
            ModeUISetup();
        }

        public FieldListEditor(List<string> parentIdList, ListMetadata listMetadata)
        {
            InitializeComponent();
            IdList = parentIdList;

            meta = new ListMetadata(listMetadata.ID, listMetadata.Name);

            editMode = true;
            validEntry = true;
            ModeUISetup();
        }

        public XElement ListItem { get; internal set; }

        public ListMetadata ListMetadata { get; internal set; }

        private void ModeUISetup()
        {
            if (editMode)
            {
                Text = Text + $": Editing {meta.ID}";
                btnAdd.Text = "Accept Changes";
                txtListID.Text = meta.ID;
                txtListName.Text = meta.Name;
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
            lbxExistingCodes.DataSource = IdList;
        }

        private void CheckIdValidity(string inputId)
        {
            if (IdList.Contains(inputId) == true && (!editMode || inputId != meta.ID))
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
            
            ListItem = new XElement(xn.ListParent);

            ListItem.Add(new XAttribute(xn.ListId, listId));

            if (listName != string.Empty)
                ListItem.Add(new XAttribute(xn.ListName, listName));

            if (xn.ChildGroup != null)
            {
                ListItem.Add(
                    new XElement(xn.ChildGroup,
                        new XElement(xn.ListChild) { Value = entry1 }));
            }
            else
            {
                ListItem.Add(
                    new XElement(xn.ListChild) { Value = entry1 });
            }
            
        }

        private void EditExistingList()
        {
            string listId = txtListID.Text;
            string listName = txtListName.Text;

            ListMetadata = new ListMetadata(listId, listName);
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
                List<string> filteredList = IdList.Where(id => id.Contains(inputId)).ToList();
                lbxExistingCodes.DataSource = filteredList;
            }
            else
            {
                lbxExistingCodes.DataSource = IdList;
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
