
using System.Drawing;
using System.IO;

namespace CoreLibrary.Models
{
    public class ItemImage
    {
        private string _customName = "";

        public ItemImage(string filePath)
        {
            ImageFullName = filePath;
            FileName = Path.GetFileNameWithoutExtension(filePath);
            ImagePreview = Image.FromFile(filePath);
            DraftDisplayName = $"(default): { FileName }";
        }

        public ItemImage(string filePath, string defaultName)
        {
            ImageFullName = filePath;
            FileName = Path.GetFileNameWithoutExtension(filePath);
            if (File.Exists(filePath))
            {
                ImagePreview = Image.FromFile(filePath);
            }
            DraftDisplayName = $"(default): { FileName }";

            if (FileName != defaultName)
            {
                CustomName = FileName;
            }
        }

        public string ImageFullName { get; private set; }
        public string FileName { get; private set; }
        public Image ImagePreview { get; private set; }
        public string CustomName
        {
            get => _customName;
            set
            {
                _customName = value;
                DraftDisplayName = $"(custom): { value }";
            }
        }
        public string DraftDisplayName { get; private set; }

        public void Modify(ItemImage newObject)
        {
            ImageFullName = newObject.ImageFullName;
            FileName = newObject.FileName;
            ImagePreview = Image.FromFile(newObject.ImageFullName);
            DraftDisplayName = newObject.DraftDisplayName;
            _customName = newObject._customName;
        }
    }
}
