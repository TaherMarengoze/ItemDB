using System;
using System.Drawing;
using System.Windows.Forms;
using UserInterface.Models;
using UserInterface.Operation;
using UserInterface.Types;

namespace UserInterface.Forms
{
    public partial class Main : Form
    {
        public Main() => InitializeComponent();

        private void ItemViewer_Click(object sender, EventArgs e)
            => LauchEditor(new ItemViewer());

        private void SpecsEditor_Click(object sender, EventArgs e)
            => LauchEditor(new SpecsEditor());

        private void SizeGroupsEditor_Click(object sender, EventArgs e)
            => LauchEditor(new SizeGroupEditor());

        private void SizeEditor_Click(object sender, EventArgs e)
            => LauchEditor(new FieldEditor(FieldType.SIZE));

        private void BrandEditor_Click(object sender, EventArgs e)
            => LauchEditor(new FieldEditor(FieldType.BRAND));

        private void EndsEditor_Click(object sender, EventArgs e)
            => LauchEditor(new FieldEditor(FieldType.ENDS));

        private void Exit_Click(object sender, EventArgs e) => Application.Exit();

        private void LauchEditor(Form editor)
        {
            if (Program.fp != null && Program.xDataDocs != null)
            {
                Hide();
                editor.ShowDialog(this);
                Show();
            }
            else
            {
                MessageBox.Show("No data files loaded");
            }
        }

        private void tsmiAutoLoad_CheckedChanged(object sender, EventArgs e)
        {
            tsmiTest.BackColor = tsmiAutoLoad.Checked ? Color.Pink : SystemColors.Control;
            Program.TestAutoLoad = tsmiAutoLoad.Checked;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            EnableDisableEditorsLaunchUI(false);

            tsmiAutoLoad.Checked = Program.TestAutoLoad;
            Runtime.Test.AutoLoad(LoadXmlFile);
            Runtime.Test.AutoJump(delegate { new ItemEditor(Program.xDataDocs, Program.fp.ImageRepos).ShowDialog(); });
        }

        private void tsmiLoadAll_Click(object sender, EventArgs e)
        {
            Common.BrowseXmlFile(LoadXmlFile);
        }

        private void LoadXmlFile(string filePath)
        {
            // Load all the required XML file paths.
            Program.fp = new FileProcessor(filePath);

            // Load all the required XML documents.
            Program.xDataDocs = new XDataDocuments(Program.fp);

            PostLoading();
        }

        private void PostLoading()
        {
            DataService.InitializeRepos(Program.xDataDocs);

            EnableDisableEditorsLaunchUI(true);
        }

        private void EnableDisableEditorsLaunchUI(bool enable)
        {
            btnItemViewer.Enabled = enable;
            btnSpecsEditor.Enabled = enable;
            btnSizeGroupsEditor.Enabled = enable;
            btnSizesEditor.Enabled = enable;
            btnBrandsEditor.Enabled = enable;
            btnEndsEditor.Enabled = enable;

            tsmiForms.Enabled = enable;
        }

        private void tsmiRestartApp_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }
    }
}