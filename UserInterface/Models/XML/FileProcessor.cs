using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Factory;
using UserInterface.Types;

namespace UserInterface.Models
{
    public class FileProcessor
    {
        public FileProcessor(string browsedPath)
        {
            //Get the parent directory of this file
            string parentDir = Path.GetDirectoryName(browsedPath);

            //Items = browsedPath;
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

        public static string FieldFilePath(FieldType fieldType)
        {
            return (string)
                Delegators.FieldFunctionCallback(fieldType,
                    delegate { return Program.fp.Sizes; },
                    delegate { return Program.fp.Brands; },
                    delegate { return Program.fp.Ends; });
        }

        public static string TestSave(string filePath)
        {
            //Get file name from path
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            return filePath.Replace(fileName, $"{fileName}_Save");
        }
    }
}
