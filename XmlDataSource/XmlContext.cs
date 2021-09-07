
using AppCore;
using CoreLibrary.Enums;
using Interfaces.Operations;
using XmlDataSource.Repository;

namespace XmlDataSource
{
    public class XmlContext : ISourceContext
    {
        private FilePathProcessor fpp;
        private DataDocuments dataDocs;

        public XmlContext()
        {

        }

        public void Load()
        {
            CoreLibrary.Common.BrowseXmlFile(LoadXmlContext);
        }

        public void Save()
        {
            DocumentProcessor.Save(dataDocs.Items, fpp.Items);
            DocumentProcessor.Save(dataDocs.Specs, fpp.Specs);
            DocumentProcessor.Save(dataDocs.SizeGroups, fpp.SizeGroups);
            DocumentProcessor.Save(dataDocs.Sizes, fpp.Sizes);
            DocumentProcessor.Save(dataDocs.Brands, fpp.Brands);
            DocumentProcessor.Save(dataDocs.Ends, fpp.Ends);
            DocumentProcessor.Save(dataDocs.CustomSpecs, fpp.CustomSpecs);
            DocumentProcessor.Save(dataDocs.CustomSizes, fpp.CustomSizes);
        }

        public void Save(object options)
        {
            ContextEntity entity = (ContextEntity)options;

            if ((entity & ContextEntity.Items) != 0)
                DocumentProcessor.Save(dataDocs.Items, fpp.Items);

            if ((entity & ContextEntity.Specs) != 0)
                DocumentProcessor.Save(dataDocs.Specs, fpp.Specs);

            if ((entity & ContextEntity.SizeGroups) != 0)
                DocumentProcessor.Save(dataDocs.SizeGroups, fpp.SizeGroups);

            if ((entity & ContextEntity.Sizes) != 0)
                DocumentProcessor.Save(dataDocs.Sizes, fpp.Sizes);

            if ((entity & ContextEntity.Brands) != 0)
                DocumentProcessor.Save(dataDocs.Brands, fpp.Brands);

            if ((entity & ContextEntity.Ends) != 0)
                DocumentProcessor.Save(dataDocs.Ends, fpp.Ends);

            if ((entity & ContextEntity.CustomSpecs) != 0)
                DocumentProcessor.Save(dataDocs.CustomSpecs, fpp.CustomSpecs);

            if ((entity & ContextEntity.CustomSizes) != 0)
                DocumentProcessor.Save(dataDocs.CustomSizes, fpp.CustomSizes);
        }

        private void LoadXmlContext(string filePath)
        {
            fpp = new FilePathProcessor(filePath);
            dataDocs = new DataDocuments(fpp);

            Globals.disableEditors = false;

            // Instantiate the source reader
            Globals.reader = new XmlReader(dataDocs);

            // Instantiate the repositories
            Globals.itemsRepo = new Item();
            Globals.specsRepo = new Specs();
            Globals.sizeGroupRepo = new SizeGroup();
            Globals.sizesRepo = new SizeList();
            //Globals.sizeManipulator = new FieldXmlManipulator(Globals.xDataDocs.Sizes.Document, FieldType.SIZE);
            Globals.brandsRepo = new BrandList();
            //Globals.brandManipulator = new FieldXmlManipulator(Globals.xDataDocs.Brands, FieldType.BRAND);
            Globals.endsRepo = new EndList();
            //Globals.endsManipulator = new FieldXmlManipulator(Globals.xDataDocs.Ends, FieldType.ENDS);
        }

        public void TestLoadXmlContext(string filePath)
        {
            LoadXmlContext(filePath);
        }
    }
}