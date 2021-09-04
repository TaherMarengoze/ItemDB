
using Interfaces.Operations;

namespace XmlDataSorce
{
    public class XmlContext : ISourceContext
    {
        public void Load()
        {
            Common.BrowseXmlFile(LoadXmlFile);
        }

        public void Save()
        {
            DataDocuments.Save(AppFactory.xDataDocs.Items, AppFactory.fpp.Items);
            DataDocuments.Save(AppFactory.xDataDocs.Specs, AppFactory.fpp.Specs);
            DataDocuments.Save(AppFactory.xDataDocs.SizeGroups, AppFactory.fpp.SizeGroups);
            DataDocuments.Save(AppFactory.xDataDocs.Sizes, AppFactory.fpp.Sizes);
            DataDocuments.Save(AppFactory.xDataDocs.Brands, AppFactory.fpp.Brands);
            DataDocuments.Save(AppFactory.xDataDocs.Ends, AppFactory.fpp.Ends);
            DataDocuments.Save(AppFactory.xDataDocs.CustomSpecs, AppFactory.fpp.CustomSpecs);
            DataDocuments.Save(AppFactory.xDataDocs.CustomSizes, AppFactory.fpp.CustomSizes);
        }

        public void Save(object options)
        {
            ContextEntity entity = (ContextEntity)options;

            if ((entity & ContextEntity.Items) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.Items, AppFactory.fpp.Items);

            if ((entity & ContextEntity.Specs) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.Specs, AppFactory.fpp.Specs);

            if ((entity & ContextEntity.SizeGroups) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.SizeGroups, AppFactory.fpp.SizeGroups);

            if ((entity & ContextEntity.Sizes) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.Sizes, AppFactory.fpp.Sizes);

            if ((entity & ContextEntity.Brands) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.Brands, AppFactory.fpp.Brands);

            if ((entity & ContextEntity.Ends) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.Ends, AppFactory.fpp.Ends);

            if ((entity & ContextEntity.CustomSpecs) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.CustomSpecs, AppFactory.fpp.CustomSpecs);

            if ((entity & ContextEntity.CustomSizes) != 0)
                DataDocuments.Save(AppFactory.xDataDocs.CustomSizes, AppFactory.fpp.CustomSizes);
        }

        private void LoadXmlFile(string filePath)
        {
            AppFactory.fpp = new FilePathProcessor(filePath);
            AppFactory.xDataDocs = new DataDocuments(AppFactory.fpp);

            // Instantiate the source reader and modifier
            AppFactory.reader = new XReader(AppFactory.xDataDocs);

            AppFactory.itemModifier = new ItemRepoX();

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
