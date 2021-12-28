
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XmlDataSource
{
    public static class DocumentProcessor
    {
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

        public static XDocument Load(string filePath)
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

        public static XDocument Load(string filePath, string errorMessage)
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