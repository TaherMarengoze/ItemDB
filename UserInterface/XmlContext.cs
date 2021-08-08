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

            if ((entity & Enums.ContextEntity.Items) != 0)
                XDataDocuments.Save(Program.xDataDocs.Items, Program.fpp.Items);

            if ((entity & Enums.ContextEntity.Specs) != 0)
                XDataDocuments.Save(Program.xDataDocs.Specs, Program.fpp.Specs);

            if ((entity & Enums.ContextEntity.SizeGroups) != 0)
                XDataDocuments.Save(Program.xDataDocs.SizeGroups, Program.fpp.SizeGroups);

            if ((entity & Enums.ContextEntity.Sizes) != 0)
                XDataDocuments.Save(Program.xDataDocs.Sizes, Program.fpp.Sizes);

            if ((entity & Enums.ContextEntity.Brands) != 0)
                XDataDocuments.Save(Program.xDataDocs.Brands, Program.fpp.Brands);

            if ((entity & Enums.ContextEntity.Ends) != 0)
                XDataDocuments.Save(Program.xDataDocs.Ends, Program.fpp.Ends);

            if ((entity & Enums.ContextEntity.CustomSpecs) != 0)
                XDataDocuments.Save(Program.xDataDocs.CustomSpecs, Program.fpp.CustomSpecs);

            if ((entity & Enums.ContextEntity.CustomSizes) != 0)
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

            Program.sizesRepo = new SizesRepoX(Program.xDataDocs.Sizes);
            Program.sizeManipulator = new SizesManipulator(Program.xDataDocs.Sizes);

            Program.brandsRepo = new BrandsRepoX(Program.xDataDocs.Brands);
            Program.brandManipulator = new BrandsManipulator(Program.xDataDocs.Brands);
        }

        public void TestLoadXmlFile(string filePath)
        {
            LoadXmlFile(filePath);
        }
    }
}
