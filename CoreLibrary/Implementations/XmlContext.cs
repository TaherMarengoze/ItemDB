
namespace CoreLibrary
{
    using Enums;
    using Interfaces;
    using Models;

    public class XmlContext : ISourceContext
    {
        public void Load()
        {
            Common.BrowseXmlFile(LoadXmlFile);
        }

        public void Save()
        {
            XDataDocuments.Save(GlobalsX.xDataDocs.Items, GlobalsX.fpp.Items);
            XDataDocuments.Save(GlobalsX.xDataDocs.Specs, GlobalsX.fpp.Specs);
            XDataDocuments.Save(GlobalsX.xDataDocs.SizeGroups, GlobalsX.fpp.SizeGroups);
            XDataDocuments.Save(GlobalsX.xDataDocs.Sizes, GlobalsX.fpp.Sizes);
            XDataDocuments.Save(GlobalsX.xDataDocs.Brands, GlobalsX.fpp.Brands);
            XDataDocuments.Save(GlobalsX.xDataDocs.Ends, GlobalsX.fpp.Ends);
            XDataDocuments.Save(GlobalsX.xDataDocs.CustomSpecs, GlobalsX.fpp.CustomSpecs);
            XDataDocuments.Save(GlobalsX.xDataDocs.CustomSizes, GlobalsX.fpp.CustomSizes);
        }

        public void Save(object options)
        {
            ContextEntity entity = (ContextEntity)options;

            if ((entity & ContextEntity.Items) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.Items, GlobalsX.fpp.Items);

            if ((entity & ContextEntity.Specs) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.Specs, GlobalsX.fpp.Specs);

            if ((entity & ContextEntity.SizeGroups) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.SizeGroups, GlobalsX.fpp.SizeGroups);

            if ((entity & ContextEntity.Sizes) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.Sizes, GlobalsX.fpp.Sizes);

            if ((entity & ContextEntity.Brands) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.Brands, GlobalsX.fpp.Brands);

            if ((entity & ContextEntity.Ends) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.Ends, GlobalsX.fpp.Ends);

            if ((entity & ContextEntity.CustomSpecs) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.CustomSpecs, GlobalsX.fpp.CustomSpecs);

            if ((entity & ContextEntity.CustomSizes) != 0)
                XDataDocuments.Save(GlobalsX.xDataDocs.CustomSizes, GlobalsX.fpp.CustomSizes);
        }

        private void LoadXmlFile(string filePath)
        {
            GlobalsX.fpp = new FilePathProcessor(filePath);
            GlobalsX.xDataDocs = new XDataDocuments(GlobalsX.fpp);

            // Instantiate the source reader and modifier
            GlobalsX.reader = new XReader(GlobalsX.xDataDocs);

            GlobalsX.itemsRepo = new ItemRepoX();

            GlobalsX.specsRepo = new SpecsRepoX(GlobalsX.xDataDocs.Specs);

            GlobalsX.sizeGroupRepo = new SizeGroupsXmlRepository(GlobalsX.xDataDocs.SizeGroups);

            GlobalsX.sizesRepo = new FieldXmlRepository(GlobalsX.xDataDocs.Sizes, FieldType.SIZE);
            GlobalsX.sizeManipulator = new FieldXmlManipulator(GlobalsX.xDataDocs.Sizes.Document, FieldType.SIZE);

            GlobalsX.brandsRepo = new FieldXmlRepository(GlobalsX.xDataDocs.Brands, FieldType.BRAND);
            GlobalsX.brandManipulator = new FieldXmlManipulator(GlobalsX.xDataDocs.Brands, FieldType.BRAND);

            GlobalsX.endsRepo = new FieldXmlRepository(GlobalsX.xDataDocs.Ends, FieldType.ENDS);
            GlobalsX.endsManipulator = new FieldXmlManipulator(GlobalsX.xDataDocs.Ends, FieldType.ENDS);

        }

        public void TestLoadXmlFile(string filePath)
        {
            LoadXmlFile(filePath);
        }
    }
}
