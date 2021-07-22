using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UserInterface.Interfaces;

namespace UserInterface.Models
{
    public class XDataDocuments : IDataSource
    {
        public XDataDocuments(FilePathReader filePathReader)
        {
            Items = LoadXDocument(filePathReader.Items);
            SizeGroups = LoadXDocument(filePathReader.SizeGroups);
            Specs = LoadXDocument(filePathReader.Specs);
            Sizes = LoadXDocument(filePathReader.Sizes);
            Brands = LoadXDocument(filePathReader.Brands);
            Ends = LoadXDocument(filePathReader.Ends);
            CustomSpecs = LoadXDocument(filePathReader.CustomSpecs);
            CustomSizes = LoadXDocument(filePathReader.CustomSizes);
        }

        public XDocument Items { get; private set; }
        public XDocument SizeGroups { get; private set; }
        public XDocument Specs { get; private set; }
        public XDocument Sizes { get; private set; }
        public XDocument Brands { get; private set; }
        public XDocument Ends { get; private set; }
        public XDocument CustomSpecs { get; private set; }
        public XDocument CustomSizes { get; private set; }

        public static void Save(XDocument document, string savePath, bool test = false)
        {
            try
            {
                string path = savePath;
                if (test)
                {
                    path = FilePathReader.TestSave(savePath);
                }

                document.Save(path);

                if (File.Exists(path))
                {
                    MessageBox.Show("The file was saved successfully",
                        "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while saving the file\n\nError Details:\n" +
                    $"{ex.InnerException}\n{ex.Message}",
                    "File Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private static XDocument LoadXDocument(string filePath, string errorMessage = "")
        {
            if (File.Exists(filePath))
            {
                return XDocument.Load(filePath);
            }
            else
            {
                if (errorMessage == "")
                {
                    string fileName = Path.GetFileName(filePath);
                    MessageBox.Show(caption: "File not found",
                        text: $"Unable to load the XML file:\n• {fileName}\n\nFull Path:\n{filePath}",
                        buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(errorMessage, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }
    }
}
