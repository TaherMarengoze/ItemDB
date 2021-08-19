namespace UserInterface
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
            XDataDocuments.Save(Program.xDataDocs.Items, Program.fpp.Items);
            XDataDocuments.Save(Program.xDataDocs.Specs, Program.fpp.Specs);
            XDataDocuments.Save(Program.xDataDocs.SizeGroups, Program.fpp.SizeGroups);
            XDataDocuments.Save(Program.xDataDocs.Sizes, Program.fpp.Sizes);
            XDataDocuments.Save(Program.xDataDocs.Brands, Program.fpp.Brands);
            XDataDocuments.Save(Program.xDataDocs.Ends, Program.fpp.Ends);
            XDataDocuments.Save(Program.xDataDocs.CustomSpecs, Program.fpp.CustomSpecs);
            XDataDocuments.Save(Program.xDataDocs.CustomSizes, Program.fpp.CustomSizes);

            //throw new NotImplementedException();
        }

        public void Save(object options)
        {
            ContextEntity entity = (ContextEntity)options;

            if ((entity & ContextEntity.Items) != 0)
                XDataDocuments.Save(Program.xDataDocs.Items, Program.fpp.Items);

            if ((entity & ContextEntity.Specs) != 0)
                XDataDocuments.Save(Program.xDataDocs.Specs, Program.fpp.Specs);

            if ((entity & ContextEntity.SizeGroups) != 0)
                XDataDocuments.Save(Program.xDataDocs.SizeGroups, Program.fpp.SizeGroups);

            if ((entity & ContextEntity.Sizes) != 0)
                XDataDocuments.Save(Program.xDataDocs.Sizes, Program.fpp.Sizes);

            if ((entity & ContextEntity.Brands) != 0)
                XDataDocuments.Save(Program.xDataDocs.Brands, Program.fpp.Brands);

            if ((entity & ContextEntity.Ends) != 0)
                XDataDocuments.Save(Program.xDataDocs.Ends, Program.fpp.Ends);

            if ((entity & ContextEntity.CustomSpecs) != 0)
                XDataDocuments.Save(Program.xDataDocs.CustomSpecs, Program.fpp.CustomSpecs);

            if ((entity & ContextEntity.CustomSizes) != 0)
                XDataDocuments.Save(Program.xDataDocs.CustomSizes, Program.fpp.CustomSizes);
        }

        private void LoadXmlFile(string filePath)
        {
            Program.fpp = new FilePathProcessor(filePath);
            Program.xDataDocs = new XDataDocuments(Program.fpp);

            // Instantiate the source reader and modifier
            Program.reader = new XReader(Program.xDataDocs);
            Program.itemModifier = new ModifyXml();

            Program.specsRepo = new SpecsRepoX(Program.xDataDocs.Specs);

            Program.sizeGroupRepo = new SizeGroupsXmlRepository(Program.xDataDocs.SizeGroups);

            Program.sizesRepo = new FieldXmlRepository(Program.xDataDocs.Sizes, FieldType.SIZE);
            Program.sizeManipulator = new FieldXmlManipulator(Program.xDataDocs.Sizes.Document, FieldType.SIZE);

            Program.brandsRepo = new FieldXmlRepository(Program.xDataDocs.Brands, FieldType.BRAND);
            Program.brandManipulator = new FieldXmlManipulator(Program.xDataDocs.Brands, FieldType.BRAND);

            Program.endsRepo = new FieldXmlRepository(Program.xDataDocs.Ends, FieldType.ENDS);
            Program.endsManipulator = new FieldXmlManipulator(Program.xDataDocs.Ends, FieldType.ENDS);
        }

        public void TestLoadXmlFile(string filePath)
        {
            LoadXmlFile(filePath);
        }
    }
}
