
using System.IO;


namespace XmlDataSorce
{
    public class FilePathProcessor
    {
        /// <summary>
        /// Set the path of all the data documents.
        /// </summary>
        /// <param name="browsedPath">The full path of the browsed file.</param>
        public FilePathProcessor(string browsedPath)
        {
            //Get the parent directory of the browsed file
            string parentDir = Path.GetDirectoryName(browsedPath);

            Items = Path.Combine(parentDir, "Items.xml");
            SizeGroups = Path.Combine(parentDir, "SizeGroups.xml");
            Specs = Path.Combine(parentDir, "Specs.xml");
            Sizes = Path.Combine(parentDir, "Sizes.xml");
            Brands = Path.Combine(parentDir, "Brands.xml");
            Ends = Path.Combine(parentDir, "Ends.xml");
            CustomSpecs = Path.Combine(parentDir, "CustomSpecs.xml");
            CustomSizes = Path.Combine(parentDir, "CustomSize.xml");
            ImageRepos = Path.Combine(parentDir, @"images\");
        }


        public string Items { get; private set; }

        public string SizeGroups { get; private set; }

        public string Specs { get; private set; }

        public string Sizes { get; private set; }

        public string Brands { get; private set; }

        public string Ends { get; private set; }

        public string ImageRepos { get; private set; }

        public string CustomSpecs { get; private set; }

        public string CustomSizes { get; private set; }


        public static string TestSave(string filePath)
        {
            //Get file name from path
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            return filePath.Replace(fileName, $"{fileName}_Save");
        }
    }
}