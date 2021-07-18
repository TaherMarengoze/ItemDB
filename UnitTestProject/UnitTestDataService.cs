using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.Linq;
using UserInterface.Models;
using UserInterface.Operation;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestDataService
    {
        //private static string testPath = @"C:\Users\taher.marengoze\source\repos\ModifyXML\Items.xml";
        //private static string testPath = @"D:\Developer\source\repos\ModifyXML\Items.xml";
        //private static FileProcessor fp = new FileProcessor(testPath);
        //private XDataDocuments xDocs = new XDataDocuments(fp);

        /*ItemRawData testData = new ItemRawData()
        {
            ItemID = "XYZ",
            CatID = "PIP",
            CatName = "Pipes",
            BaseName = "Test Item Base Name",
            DisplayName = "Test Item Display Name",
            CommonNames = new List<string>()
                {
                    "Sample Item",
                    "Specimen Item"
                },
            Description = "A simple test sample item for testing",
            ImagesNames = new List<string>()
                {
                    "SampleImage1.jpg",
                    "SampleImage2.jpg",
                    "SampleImage3.jpg"
                },
            UoM = "Ea",
            SpecsID = "SPEC",
            SpecsRequired = true,
            SizeGroupID = "SIZE",
            SizeRequired = true,
            BrandListID = "BRAND",
            BrandRequired = false,
            EndsListID = "CONN",
            EndsRequired = false
        };*/


        //private TestForm tf = new TestForm();

        //[TestMethod]
        public void Test_GetAllItems()
        {

            /*fp = new FileProcessor(testPath);
            xDocs = new XDataDocuments(fp);*/
            //List<Item> list = DataService.GetAllItems(xDocs.Items);

            //tf.dgvDisplayResult.DataSource = list;
            //tf.dgvDisplayResult.AutoResizeColumns();
            //tf.ShowDialog();

            //list.ForEach(item => Console.WriteLine(
            //    string.Format("{0,10}{1,50}{2,50}{3,10}{4,50}{5,10}{6,20}",
            //    item.ItemID, //0
            //    item.BaseName, //1
            //    item.DisplayName, //2
            //    item.CommonNames.Count, //3
            //    item.Description, //4
            //    item.ImagesFileName.Count, //5
            //    item.Details) //6
            //    ));
            //Console.WriteLine("End of Test");
        }

        //[TestMethod]
        public void Test_DeleteItem()
        {
            //DataService.LoadFile(xDocs.Items);

            //string deletedItemId = "BSP";

            //Item deletedItem = DataService.TestRepos.Items.Where(id => id.ItemID == deletedItemId).FirstOrDefault();

            //DataService.DeleteItem(deletedItemId);

            //CollectionAssert.DoesNotContain(DataService.TestRepos.Items, deletedItem);
        }

        //[TestMethod]
        public void Test_NewItem()
        {
            //XElement test = DataService.SerializeItem(DataService.ProcessItemRawData(testData));
            //XElement expected = XDocument.Load(@"C:\Users\Taher\Desktop\test.xml").Root;

            //string objA = expected.ToString();
            //string objB = test.ToString();

            //Assert.AreEqual(objA, objB);
        }

        //[TestMethod]
        public void Test_AddItemToXDocument()
        {
            //DataService.AddItemToXDocument(xDocs.Items, testData);
        }

        //[TestMethod]
        public void Test_ModifyItemXDocument()
        {
            //DataService.InitializeRepos(xDocs);
            //DataService.ModifyItemXDocument(xDocs.Items, "BSP", testData);
        }
    }
}