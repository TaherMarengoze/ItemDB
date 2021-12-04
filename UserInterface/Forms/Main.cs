
//using AppCore;
//using ClientService;
//using CoreLibrary;
using CoreLibrary.Enums;
//using CoreLibrary.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
//using UserService;


namespace UserInterface.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void ItemViewer_Click(object sender, EventArgs e)
        {
            LauchEditor(new ItemViewer());
        }

        private void SpecsEditor_Click(object sender, EventArgs e)
        {
            LauchEditor(new SpecsEditor());
        }

        private void SizeGroupsEditor_Click(object sender, EventArgs e)
        {
            LauchEditor(new SizeGroupEditor());
        }

        private void SizeEditor_Click(object sender, EventArgs e)
        {
            LauchEditor(new FieldEditor(FieldType.SIZE));
        }

        private void BrandEditor_Click(object sender, EventArgs e)
        {
            LauchEditor(new FieldEditor(FieldType.BRAND));
        }

        private void EndsEditor_Click(object sender, EventArgs e)
        {
            LauchEditor(new FieldEditor(FieldType.ENDS));
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LauchEditor(Form editor)
        {
            //bool check = GlobalsX.fpp != null && GlobalsX.xDataDocs != null;
            bool check = !AppCore.Globals.disableEditors;
            //bool check = true;

            if (check)
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
            TestActions();
        }

        private void TestActions()
        {
            tsmiAutoLoad.Checked = Program.TestAutoLoad;
            Runtime.Test.AutoLoad(((CoreLibrary.XmlContext)CoreLibrary.GlobalsX.context).TestLoadXmlFile);
            
            // Requires a reference to the Interfaces.dll remove after test
            XmlDataSource.XmlContext context = (XmlDataSource.XmlContext)AppCore.Globals.context;
            Runtime.Test.LoadCallback testLoadXmlContext = context.TestLoadXmlContext;

            Runtime.Test.AutoLoad(testLoadXmlContext);
            Runtime.Test.DoSomething(PostLoading);
            //Runtime.Test.DoSomething(delegate { LauchEditor(new SpecsEditor()); });
            btnSizeGroupsEditor.PerformClick();
        }

        private void tsmiLoadAll_Click(object sender, EventArgs e)
        {
            CoreLibrary.GlobalsX.context.Load();
            //Globals.context.Load();
            ClientService.ContextProvider.Load();
            PostLoading();
        }

        private void PostLoading()
        {
            UserService.Data.InitializeRepos();
            ClientService.CacheIO.InitLists();

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