using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserInterface.Forms
{
    using Enums;
    using Models;
    using Operation;

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

        private void Exit_Click(object sender, EventArgs e)
            => Application.Exit();

        private void LauchEditor(Form editor)
        {
            if (AppFactory.fpp != null && AppFactory.xDataDocs != null)
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
            Runtime.Test.AutoLoad(((XmlContext)AppFactory.context).TestLoadXmlFile);
            Runtime.Test.DoSomething(PostLoading);
        }

        private void tsmiLoadAll_Click(object sender, EventArgs e)
        {
            // TEST
            AppFactory.context.Load();
            //Common.BrowseXmlFile(LoadXmlFile);
            PostLoading();
        }

        private void LoadXmlFile(string filePath)
        {
            // Load all the required XML file paths.
            AppFactory.fpp = new FilePathProcessor(filePath);

            // Load all the required XML documents.
            AppFactory.xDataDocs = new XDataDocuments(AppFactory.fpp);

            // Instantiate the source reader and modifier
            AppFactory.reader = new XReader(AppFactory.xDataDocs);
            AppFactory.itemModifier = new ModifyXml();
            AppFactory.specsRepo = new SpecsRepoX(AppFactory.xDataDocs.Specs);

            PostLoading();
        }
        
        private void PostLoading()
        {
            DataService.InitializeRepos();
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