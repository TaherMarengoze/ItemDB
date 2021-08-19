using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UserInterface.Models
{
    using Interfaces;

    public class XDataDocuments
    {
        /// <summary>
        /// Loads all XML documents in a specific path.
        /// </summary>
        /// <param name="processor">The <see cref="FilePathProcessor"/> containing all files path.</param>
        public XDataDocuments(FilePathProcessor processor)
        {
            Items = LoadXDocument(processor.Items);
            SizeGroups = LoadXDocument(processor.SizeGroups);
            Specs = LoadXDocument(processor.Specs);
            Sizes = LoadXDocument(processor.Sizes);
            Brands = LoadXDocument(processor.Brands);
            Ends = LoadXDocument(processor.Ends);
            CustomSpecs = LoadXDocument(processor.CustomSpecs);
            CustomSizes = LoadXDocument(processor.CustomSizes);
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
                    path = FilePathProcessor.TestSave(savePath);
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

        private static XDocument LoadXDocument(string filePath)
        {
            if (File.Exists(filePath))
            {
                return XDocument.Load(filePath);
            }
            else
            {
                string fileName = Path.GetFileName(filePath);
                MessageBox.Show(caption: "File not found",
                    text: $"Unable to load the XML file:\n• {fileName}\n\nFull Path:\n{filePath}",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);

                return null;
            }
        }

        private static XDocument LoadXDocument(string filePath, string errorMessage)
        {
            if (File.Exists(filePath))
            {
                return XDocument.Load(filePath);
            }
            else
            {
                MessageBox.Show(errorMessage,
                    "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }
    }
}
