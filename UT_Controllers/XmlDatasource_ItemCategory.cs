using Interfaces.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modeling.DataModels;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;
using XmlDataSource.Repository;

namespace UT_Controllers
{
    [TestClass]
    public class XmlDatasource_ItemCategory
    {
        private Category repository;
        private bool eventHandeled = false;

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();
            repository = new Category();
            repository.OnChange += OnChangeEventResponse;
        }

        [TestMethod]
        public void ShouldCreate()
        {
            var testCategory = new ItemCategory("CAT06", "New Category");
            repository.Create(testCategory);

            XElement xItemCategory =
                repository.UnitTestGetCategoryXElement("CAT06");

            Assert.IsNotNull(xItemCategory);

            Console.WriteLine(xItemCategory);

            if (eventHandeled)
            {
                Console.WriteLine("'{0}' event was fired by the '{1}' method."
                    , typeof(Category).GetEvents()[0].Name
                    , MethodBase.GetCurrentMethod().Name.Replace("Should", "")
                    );
            }
        }

        [TestMethod]
        [DataRow("PIP",true,"PIP","Pipes", DisplayName = "Existing Category")]
        [DataRow("XYZ", false, "", "", DisplayName = "Non-existing Category")]
        public void ShoudRead(string inputValue, bool expectObject, string expId, string expName)
        {
            IItemCategory category = repository.Read(inputValue);

            if (expectObject)
            {
                Assert.IsNotNull(category);
                Assert.AreEqual(expId, category.CatID);
                Assert.AreEqual(expName, category.CatName);
            }
            else
            {
                Assert.IsNull(category);
            }

            Console.WriteLine("Category ID = {0}, Name = {1}",
                category?.CatID, category?.CatName);

            XElement xSource =
                repository.UnitTestGetCategoryXElement(inputValue);

            XElement xCopy = xSource is null ? null :new XElement(xSource);
            xCopy?.RemoveNodes();

            Console.WriteLine(xCopy);

            if (eventHandeled)
            {
                Console.WriteLine("'{0}' event was fired by the '{1}' method."
                    , typeof(Category).GetEvents()[0].Name
                    , MethodBase.GetCurrentMethod().Name.Replace("Should", "")
                    );
            }
        }

        [TestMethod]
        public void ShouldUpdate()
        {
            repository.Update("PIP", new ItemCategory("PIPTB", "Pipes and Tubing"));

            XElement xItemCategory =
                repository.UnitTestGetCategoryXElement("PIPTB");

            Assert.AreEqual("PIPTB", xItemCategory.Attribute("catID").Value);
            Assert.AreEqual("Pipes and Tubing", xItemCategory.Attribute("name").Value);

            Console.WriteLine(xItemCategory);

            if (eventHandeled)
            {
                Console.WriteLine("'{0}' event was fired by the '{1}' method."
                    , typeof(Category).GetEvents()[0].Name
                    , MethodBase.GetCurrentMethod().Name.Replace("Should", "")
                    );
            }
        }

        [TestMethod]
        public void ShouldDelete()
        {
            XElement rootElement =
                repository.UnitTestGetCategoryXElement("PIP").Document.Root;

            repository.Delete("PIP");

            XElement xItemCategory =
                repository.UnitTestGetCategoryXElement("PIP");

            Assert.IsNull(xItemCategory);

            Console.WriteLine(rootElement);

            if (eventHandeled)
            {
                Console.WriteLine("'{0}' event was fired by the '{1}' method."
                    , typeof(Category).GetEvents()[0].Name
                    , MethodBase.GetCurrentMethod().Name.Replace("Should", "")
                    );
            }
        }

        private void OnChangeEventResponse(object sender, EventArgs e)
        {
            eventHandeled = true;
        }
    }
}