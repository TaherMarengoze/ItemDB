using System.Linq;
using System.Xml.Linq;

namespace UserInterface
{
    using Interfaces;
    using Operation;

    public class SizesManipulator : IFieldManipulator
    {
        private readonly XDocument dataSource;
        private readonly ISchema schema;

        public SizesManipulator(XDocument source)
        {
            dataSource = source;
            schema = new Models.ListStructure("size");
        }

        public void AddEntry(string listId, string entry)
        {
            
        }

        public void DeleteEntry(string listId, string entry)
        {
            GetListEntry(listId, entry).Remove();
        }

        #region Private Members
        /// <summary>
        /// Gets a specific list <see cref="XElement"/> by its ID.
        /// </summary>
        /// <param name="listId">ID of the list.</param>
        /// <returns></returns>
        private XElement GetList(string listId)
        {
            return
                (from list in dataSource.Descendants(schema.ListParent)
                 where list.Attribute(schema.ListId).Value == listId
                 select list).FirstOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="XElement"/> of a specific entry in a list.
        /// </summary>
        /// <param name="listId">List ID containing the entries.</param>
        /// <param name="entryValue">The entry to get.</param>
        /// <returns></returns>
        private XElement GetListEntry(string listId, string entryValue)
        {
            XElement fieldList = GetList(listId);

            return
                (from entry in fieldList.Descendants(schema.ListChild)
                 where entry.Value == entryValue
                 select entry).FirstOrDefault();
        }
        #endregion
    }
}
