
using AppCore;
using CoreLibrary.Enums;
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
            DataDocuments.Save(Globals.xDataDocs.Items, Globals.fpp.Items);
            DataDocuments.Save(Globals.xDataDocs.Specs, Globals.fpp.Specs);
            DataDocuments.Save(Globals.xDataDocs.SizeGroups, Globals.fpp.SizeGroups);
            DataDocuments.Save(Globals.xDataDocs.Sizes, Globals.fpp.Sizes);
            DataDocuments.Save(Globals.xDataDocs.Brands, Globals.fpp.Brands);
            DataDocuments.Save(Globals.xDataDocs.Ends, Globals.fpp.Ends);
            DataDocuments.Save(Globals.xDataDocs.CustomSpecs, Globals.fpp.CustomSpecs);
            DataDocuments.Save(Globals.xDataDocs.CustomSizes, Globals.fpp.CustomSizes);
        }

        public void Save(object options)
        {
            ContextEntity entity = (ContextEntity)options;

            if ((entity & ContextEntity.Items) != 0)
                DataDocuments.Save(Globals.xDataDocs.Items, Globals.fpp.Items);

            if ((entity & ContextEntity.Specs) != 0)
                DataDocuments.Save(Globals.xDataDocs.Specs, Globals.fpp.Specs);

            if ((entity & ContextEntity.SizeGroups) != 0)
                DataDocuments.Save(Globals.xDataDocs.SizeGroups, Globals.fpp.SizeGroups);

            if ((entity & ContextEntity.Sizes) != 0)
                DataDocuments.Save(Globals.xDataDocs.Sizes, Globals.fpp.Sizes);

            if ((entity & ContextEntity.Brands) != 0)
                DataDocuments.Save(Globals.xDataDocs.Brands, Globals.fpp.Brands);

            if ((entity & ContextEntity.Ends) != 0)
                DataDocuments.Save(Globals.xDataDocs.Ends, Globals.fpp.Ends);

            if ((entity & ContextEntity.CustomSpecs) != 0)
                DataDocuments.Save(Globals.xDataDocs.CustomSpecs, Globals.fpp.CustomSpecs);

            if ((entity & ContextEntity.CustomSizes) != 0)
                DataDocuments.Save(Globals.xDataDocs.CustomSizes, Globals.fpp.CustomSizes);
        }

        private void LoadXmlFile(string filePath)
        {
            Globals.fpp = new FilePathProcessor(filePath);
            Globals.xDataDocs = new DataDocuments(Globals.fpp);

            // Instantiate the source reader and modifier
            Globals.reader = new XReader(Globals.xDataDocs);

            Globals.itemModifier = new ItemRepoX();

            Globals.specsRepo = new SpecsRepoX(Globals.xDataDocs.Specs);

            Globals.sizeGroupRepo = new SizeGroupsXmlRepository(Globals.xDataDocs.SizeGroups);

            Globals.sizesRepo = new FieldXmlRepository(Globals.xDataDocs.Sizes, FieldType.SIZE);
            Globals.sizeManipulator = new FieldXmlManipulator(Globals.xDataDocs.Sizes.Document, FieldType.SIZE);

            Globals.brandsRepo = new FieldXmlRepository(Globals.xDataDocs.Brands, FieldType.BRAND);
            Globals.brandManipulator = new FieldXmlManipulator(Globals.xDataDocs.Brands, FieldType.BRAND);

            Globals.endsRepo = new FieldXmlRepository(Globals.xDataDocs.Ends, FieldType.ENDS);
            Globals.endsManipulator = new FieldXmlManipulator(Globals.xDataDocs.Ends, FieldType.ENDS);

        }

        public void TestLoadXmlFile(string filePath)
        {
            LoadXmlFile(filePath);
        }
    }
}
