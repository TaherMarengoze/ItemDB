
using AppCore;
using CoreLibrary.Enums;
using Interfaces.Operations;
using XmlDataSource.Repository;

namespace XmlDataSource
{
    public class XmlContext : ISourceContext
    {
        private FilePathProcessor fpp;

        internal DataDocuments DataDocs { get; private set; }

        public XmlContext()
        {

        }

        public void Load()
        {
            CoreLibrary.Common.BrowseXmlFile(LoadXmlContext);
        }

        public void Save()
        {
            DocumentProcessor.Save(DataDocs.Items, fpp.Items);
            DocumentProcessor.Save(DataDocs.Specs, fpp.Specs);
            DocumentProcessor.Save(DataDocs.SizeGroups, fpp.SizeGroups);
            DocumentProcessor.Save(DataDocs.Sizes, fpp.Sizes);
            DocumentProcessor.Save(DataDocs.Brands, fpp.Brands);
            DocumentProcessor.Save(DataDocs.Ends, fpp.Ends);
            DocumentProcessor.Save(DataDocs.CustomSpecs, fpp.CustomSpecs);
            DocumentProcessor.Save(DataDocs.CustomSizes, fpp.CustomSizes);
        }

        public void Save(object options)
        {
            ContextEntity entity = (ContextEntity)options;

            if ((entity & ContextEntity.Items) != 0)
                DocumentProcessor.Save(DataDocs.Items, fpp.Items);

            if ((entity & ContextEntity.Specs) != 0)
                DocumentProcessor.Save(DataDocs.Specs, fpp.Specs);

            if ((entity & ContextEntity.SizeGroups) != 0)
                DocumentProcessor.Save(DataDocs.SizeGroups, fpp.SizeGroups);

            if ((entity & ContextEntity.Sizes) != 0)
                DocumentProcessor.Save(DataDocs.Sizes, fpp.Sizes);

            if ((entity & ContextEntity.Brands) != 0)
                DocumentProcessor.Save(DataDocs.Brands, fpp.Brands);

            if ((entity & ContextEntity.Ends) != 0)
                DocumentProcessor.Save(DataDocs.Ends, fpp.Ends);

            if ((entity & ContextEntity.CustomSpecs) != 0)
                DocumentProcessor.Save(DataDocs.CustomSpecs, fpp.CustomSpecs);

            if ((entity & ContextEntity.CustomSizes) != 0)
                DocumentProcessor.Save(DataDocs.CustomSizes, fpp.CustomSizes);
        }

        private void LoadXmlContext(string filePath)
        {
            fpp = new FilePathProcessor(filePath);
            DataDocs = new DataDocuments(fpp);

            Globals.disableEditors = false;

            // Instantiate the source reader
            Globals.reader = new XmlReader(DataDocs);

            // Instantiate the repositories
            Globals.categoryRepo = new Category();
            Globals.itemsRepo = new Item();
            Globals.specsRepo = new Specs();
            Globals.sizeGroupRepo = new SizeGroup();
            Globals.sizesRepo = new SizeList();
            Globals.brandsRepo = new BrandList();
            Globals.endsRepo = new EndList();
        }

        public void TestLoadXmlContext(string filePath)
        {
            LoadXmlContext(filePath);
        }
    }
}