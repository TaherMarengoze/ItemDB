using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    using Interfaces;
    using Models;

    public class XmlSourceProcessor : ISourceProcessor
    {
        public void Load()
        {
            Common.BrowseXmlFile(LoadXmlFile);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private void LoadXmlFile(string filePath)
        {
            // Browse and Read the path of the Xml documents
            Program.fpr = new FilePathReader(filePath);

            // Load all the required XML documents.
            Program.xDataDocs = new XDataDocuments(Program.fpr);

            // Instantiate the source reader and modifier
            Program.reader = new XSource(Program.xDataDocs);
            Program.itemModifier = new ModifyXml();
            Program.specsRepo = new SpecsRepoX(Program.xDataDocs.Specs);
        }

        public void TestLoadXmlFile(string filePath)
        {
            LoadXmlFile(filePath);
        }
    }
}
