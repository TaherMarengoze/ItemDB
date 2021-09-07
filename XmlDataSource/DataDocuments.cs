
using System.Xml.Linq;

namespace XmlDataSource
{
    /// <summary>
    /// Represents the context XML documents.
    /// </summary>
    public class DataDocuments
    {
        /// <summary>
        /// Instantiates a new instance and loads all XML documents from a specific path.
        /// </summary>
        /// <param name="processor">The <see cref="FilePathProcessor"/> containing all files path.</param>
        public DataDocuments(FilePathProcessor processor)
        {
            Items =
                DocumentProcessor.Load(processor.Items);

            SizeGroups =
                DocumentProcessor.Load(processor.SizeGroups);

            Specs =
                DocumentProcessor.Load(processor.Specs);

            Sizes =
                DocumentProcessor.Load(processor.Sizes);

            Brands =
                DocumentProcessor.Load(processor.Brands);

            Ends =
                DocumentProcessor.Load(processor.Ends);

            CustomSpecs =
                DocumentProcessor.Load(processor.CustomSpecs);

            CustomSizes =
                DocumentProcessor.Load(processor.CustomSizes);

        }

        public XDocument Items { get; private set; }

        public XDocument SizeGroups { get; private set; }

        public XDocument Specs { get; private set; }

        public XDocument Sizes { get; private set; }

        public XDocument Brands { get; private set; }

        public XDocument Ends { get; private set; }

        public XDocument CustomSpecs { get; private set; }

        public XDocument CustomSizes { get; private set; }
    }
}