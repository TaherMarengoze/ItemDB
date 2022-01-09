using Client.Controls;
using CoreLibrary.Enums;
using Interfaces.Models;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UserInterface.Shared;

namespace UserInterface.Forms
{
    public partial class FieldListEditor_ : Form
    {
        #region Logic Code
        public IFieldList OutputList { get; private set; }

        public void SetInputID(string value)
        {
            inputID = value;

            bool isDuplicate = existingLists.Contains(value);
            bool notBlank = string.IsNullOrWhiteSpace(value);
            bool isValidChar = true;

            if (isDuplicate)
            {
                SetStatusID(InputStatus.Duplicate);
            }
            else
            {
                if (notBlank)
                {
                    SetStatusID(InputStatus.Blank);
                }
                else
                {
                    if (isValidChar)
                        SetStatusID(InputStatus.Valid);
                    else
                        SetStatusID(InputStatus.Invalid);
                }
            }
            SetSimilarLists();
        }

        public void SetInputName(string value)
        {
            inputName = value;

            bool notBlank = string.IsNullOrWhiteSpace(value);
            bool isValidChar = true;

            SetStatusName(notBlank ? InputStatus.Blank :
                isValidChar ? InputStatus.Valid : InputStatus.Invalid);
        }

        private void SetInputFirstEntry(string value)
        {
            inputFirstEntry = value;

            bool notBlank = string.IsNullOrWhiteSpace(value);
            bool isValidChar = true;

            SetStatusEntry0(notBlank ? InputStatus.Blank :
                isValidChar ? InputStatus.Valid : InputStatus.Invalid);
        }
        
        private void SetStatusID(InputStatus value)
        {
            statusID = value;

            ChangedStatusID(value);

            // check all inputs status
            CheckReadyStatus();
        }
        
        private void SetStatusName(InputStatus value)
        {
            statusName = value;

            ChangedStatusName(value);

            // check all inputs status
            CheckReadyStatus();
        }
        
        private void SetStatusEntry0(InputStatus value)
        {
            statusEntry0 = value;

            ChangedStatusEntry0(value);

            // check all inputs status
            CheckReadyStatus();
        }

        private void SetSimilarLists()
        {
            List<string> similarLists =
                string.IsNullOrEmpty(inputID) ? null
                : existingLists.Where(id => id.Contains(inputID)).ToList();

            ChangedSimilarLists(similarLists);
        }

        private void CheckReadyStatus()
        {
            bool isValid = IsValidInputs();
            bool isChanged = IsDraftChanged();

            isReady = isValid && isChanged;

            ChangedReadyStatus(isReady);
        }

        private void CommitChanges()
        {
            if (OutputList == null)
                CreateNewList();
            else
                EditExistingList();
        }

        private void CreateNewList()
        {
            OutputList = new SizeList
            {
                ID = inputID,
                Name = inputID,
                List = new ObservableCollection<string> { inputFirstEntry }
            };
        }

        private void EditExistingList()
        {
            OutputList.ID = inputID;
            OutputList.Name = inputName;
        }

        private bool IsValidInputs()
        {
            InputStatus[] inputStatus = {
                statusID,
                statusName,
                statusEntry0
            };

            return inputStatus.All(status => status == InputStatus.Valid);
        }

        private bool IsDraftChanged()
        {
            if (OutputList == null)
                return true;

            bool[] draftChange = {
                inputID != null ? inputID != OutputList?.ID : false,
                inputName != null ? inputName != OutputList?.Name : false,
            };

            return draftChange.Any(change => change);
        }

        private readonly List<string> existingLists;
        private string inputID;
        private string inputName;
        private string inputFirstEntry;
        private InputStatus statusID;
        private InputStatus statusName;
        private InputStatus statusEntry0;
        private bool isReady;
        #endregion

        private enum Mode { New, Edit }

        private Mode mode;
        
        public FieldListEditor_(IEnumerable<string> currentLists)
        {
            InitializeComponent();
            existingLists = currentLists.ToList();

            SetEditorMode(Mode.New);
        }

        public FieldListEditor_(IEnumerable<string> currentLists, IFieldList editList)
        {
            InitializeComponent();
            existingLists = currentLists.ToList();
            OutputList = editList;
            SetEditorMode(Mode.Edit);
        }

        private void SetEditorMode(Mode value)
        {
            mode = value;
            switch (value)
            {
                case Mode.New:
                    // change title
                    Text = Text + ": New";
                    break;
                case Mode.Edit:
                    // change title
                    Text = Text + $": Editing {OutputList.ID}";
                    btnAdd.Text = "Accept Changes";
                    txtListID.Text = OutputList.ID;
                    txtListName.Text = OutputList.Name;
                    lblInitialEntry.Visible = false;
                    txtInitialEntry.Visible = false;
                    break;
            }
        }

        private void BindExistingList()
        {
            lbxExistingCodes.DataSource = existingLists;
        }

        private void FillFields()
        {
            txtListID.Text = OutputList.ID;
            txtListName.Text = OutputList.Name;
        }

        private void ChangedStatusID(InputStatus status)
        {
            lblValidID.ValidityInfo(status);
            switch (status)
            {
                case InputStatus.Duplicate:
                    if (txtListID.Text.Length >= txtListID.MaxLength)
                        txtListID.FocusSelectAll();

                    break;
                case InputStatus.Blank:
                    txtListID.FocusSelectAll();

                    break;
                case InputStatus.Invalid:
                    txtListID.FocusSelectAll();

                    break;
            }
        }

        private void ChangedStatusName(InputStatus status) => Console.WriteLine(status.ToString());

        private void ChangedStatusEntry0(InputStatus status) => Console.WriteLine(status.ToString());

        private void ChangedSimilarLists(List<string> similarLists)
        {
            if (similarLists == null)
                BindExistingList();
            else
                lbxExistingCodes.DataSource = similarLists;
        }

        private void ChangedReadyStatus(bool status)
        {
            btnAdd.Enabled = status;
        }

        private void FieldListEditor_Load(object sender, EventArgs e)
        {
            BindExistingList();

            if (mode == Mode.Edit)
                FillFields();
        }

        private void txtListID_TextChanged(object sender, EventArgs e)
        {
            SetInputID(((TextBox)sender).Text);
        }

        private void txtListName_TextChanged(object sender, EventArgs e)
        {
            SetInputName(((TextBox)sender).Text);
        }

        private void txtInitialEntry_TextChanged(object sender, EventArgs e)
        {
            SetInputFirstEntry(((TextBox)sender).Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CommitChanges();
        }
    }
}