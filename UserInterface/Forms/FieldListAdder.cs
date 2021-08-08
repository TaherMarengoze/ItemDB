using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserInterface.Controllers;
using UserInterface.Interfaces;
using UserInterface.Models;
using UserInterface.Models.Validators;
using UserInterface.Operation;
using UserInterface.Enums;

namespace UserInterface.Forms
{
    public partial class FieldListAdder : Form
    {
        public IFieldList FieldListItem { get; private set; } = new FieldList();

        private FieldType fieldType;
        private object existingFieldIds;
        private FieldListValidator inputValidator = new FieldListValidator();
        private List<string> listEntries = new List<string>();
        private SimpleListController listController;

        public FieldListAdder(FieldType field)
        {
            InitializeComponent();

            fieldType = field;

            existingFieldIds = DataService.GetFieldBasicView(fieldType);
            SetListItems(existingFieldIds);

            listController = new SimpleListController(btnAddEntry, btnEdit, btnDeleteEntry,
                txtEntryValue, lbxFieldListItems, listEntries);

            AddEventListners();
        }

        private void AddEventListners()
        {
            // Controls
            txtListID.KeyPress += Common.AlphaNumericId_KeyPress;

            // Objects
            listController.OnEntryAdd += ListController_OnEntryAdd;
            listController.OnEntryDelete += ListController_OnEntryDelete;

            inputValidator.OnComplete += InputValidator_OnComplete;
            inputValidator.OnIncomplete += InputValidator_OnIncomplete;
        }

        private void FieldListAdder_Load(object sender, EventArgs e)
        {
            dgvExistingCodes.AutoResizeColumns();
            dgvExistingCodes.AutoResizeRows();
        }

        private void ListController_OnEntryAdd(object sender, EventArgs e)
        {
            inputValidator.ValidEntries = true;
        }

        private void ListController_OnEntryDelete(object sender, EventArgs e)
        {
            if (listEntries.Count <= 0)
            {
                inputValidator.ValidEntries = false;
            }
        }

        private void InputValidator_OnComplete()
        {
            btnAccept.Enabled = true;
        }

        private void InputValidator_OnIncomplete()
        {
            btnAccept.Enabled = false;
        }
        
        private void txtListID_TextChanged(object sender, EventArgs e)
        {
            string input = ((TextBox)sender).Text;

            // Check duplicate ID
            bool duplicateFound = ((IEnumerable<BasicView>)existingFieldIds).Select(entry => entry.ID).Contains(input);

            if (input != string.Empty)
            {
                if (duplicateFound)
                {
                    inputValidator.ValidID = false;
                    lblValidID.Text = "• Duplicte ID";
                    if (input.Length >= txtListID.MaxLength)
                    {
                        txtListID.SelectAll();
                        txtListID.Focus();
                    }
                }
                else
                {
                    inputValidator.ValidID = true;
                    lblValidID.Text = string.Empty;
                }
            }
            else
            {
                inputValidator.ValidID = false;
                lblValidID.Text = string.Empty;
            }

            // Filter Similar IDs
            FilterExistingIds(input);
        }

        private void txtListName_TextChanged(object sender, EventArgs e)
        {
            string input = ((TextBox)sender).Text;

            if (input != string.Empty)
            {
                inputValidator.ValidName = true;
            }
            else
            {
                inputValidator.ValidName = false;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            FieldListItem.ID = txtListID.Text;
            FieldListItem.Name = txtListName.Text.Trim();
            FieldListItem.List.AddRange(listEntries);

            //foreach (var item in listEntries)
            //{
            //    FieldListItem.List.Add(item);
            //}
        }

        #region Custom
        private void SetListItems(object dataSource)
        {
            lbxExistingCodes.DataSource = dataSource;
            dgvExistingCodes.DataSource = dataSource;
        }

        private void FilterExistingIds(string inputId)
        {
            if (inputId != string.Empty)
            {
                IEnumerable<IBasicView> bvs = ((IEnumerable<BasicView>)existingFieldIds)
                    .Where(entry => entry.ID.Contains(inputId));

                SetListItems(bvs.ToList());
            }
            else
            {
                SetListItems(existingFieldIds);
            }
        }
        #endregion
    }
}