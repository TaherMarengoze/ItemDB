
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
            XDataDocuments.Save(AppFactory.xDataDocs.Items, AppFactory.fpp.Items);
            XDataDocuments.Save(AppFactory.xDataDocs.Specs, AppFactory.fpp.Specs);
            XDataDocuments.Save(AppFactory.xDataDocs.SizeGroups, AppFactory.fpp.SizeGroups);
            XDataDocuments.Save(AppFactory.xDataDocs.Sizes, AppFactory.fpp.Sizes);
            XDataDocuments.Save(AppFactory.xDataDocs.Brands, AppFactory.fpp.Brands);
            XDataDocuments.Save(AppFactory.xDataDocs.Ends, AppFactory.fpp.Ends);
            XDataDocuments.Save(AppFactory.xDataDocs.CustomSpecs, AppFactory.fpp.CustomSpecs);
            XDataDocuments.Save(AppFactory.xDataDocs.CustomSizes, AppFactory.fpp.CustomSizes);
        }

        public void Save(object options)
        {
            ContextEntity entity = (ContextEntity)options;

            if ((entity & ContextEntity.Items) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.Items, AppFactory.fpp.Items);

            if ((entity & ContextEntity.Specs) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.Specs, AppFactory.fpp.Specs);

            if ((entity & ContextEntity.SizeGroups) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.SizeGroups, AppFactory.fpp.SizeGroups);

            if ((entity & ContextEntity.Sizes) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.Sizes, AppFactory.fpp.Sizes);

            if ((entity & ContextEntity.Brands) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.Brands, AppFactory.fpp.Brands);

            if ((entity & ContextEntity.Ends) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.Ends, AppFactory.fpp.Ends);

            if ((entity & ContextEntity.CustomSpecs) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.CustomSpecs, AppFactory.fpp.CustomSpecs);

            if ((entity & ContextEntity.CustomSizes) != 0)
                XDataDocuments.Save(AppFactory.xDataDocs.CustomSizes, AppFactory.fpp.CustomSizes);
        }

        private void LoadXmlFile(string filePath)
        {
            AppFactory.fpp = new FilePathProcessor(filePath);
            AppFactory.xDataDocs = new XDataDocuments(AppFactory.fpp);

            // Instantiate the source reader and modifier
            AppFactory.reader = new XReader(AppFactory.xDataDocs);

            AppFactory.itemModifier = new ModifyXml();

            AppFactory.specsRepo = new SpecsRepoX(AppFactory.xDataDocs.Specs);

            AppFactory.sizeGroupRepo = new SizeGroupsXmlRepository(AppFactory.xDataDocs.SizeGroups);

            AppFactory.sizesRepo = new FieldXmlRepository(AppFactory.xDataDocs.Sizes, FieldType.SIZE);
            AppFactory.sizeManipulator = new FieldXmlManipulator(AppFactory.xDataDocs.Sizes.Document, FieldType.SIZE);

            AppFactory.brandsRepo = new FieldXmlRepository(AppFactory.xDataDocs.Brands, FieldType.BRAND);
            AppFactory.brandManipulator = new FieldXmlManipulator(AppFactory.xDataDocs.Brands, FieldType.BRAND);

            AppFactory.endsRepo = new FieldXmlRepository(AppFactory.xDataDocs.Ends, FieldType.ENDS);
            AppFactory.endsManipulator = new FieldXmlManipulator(AppFactory.xDataDocs.Ends, FieldType.ENDS);

        }

        public void TestLoadXmlFile(string filePath)
        {
            LoadXmlFile(filePath);
        }
    }
}
